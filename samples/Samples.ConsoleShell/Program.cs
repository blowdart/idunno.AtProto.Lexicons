// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics;

using Microsoft.Extensions.Logging;

using Samples.Common;

using idunno.AtProto;
using idunno.AtProto.Repo;
using idunno.AtProto.Lexicons;
using idunno.AtProto.Lexicons.Statusphere.Xyz;

namespace Samples.ConsoleShell
{
    public sealed class Program
    {
        static async Task<int> Main(string[] args)
        {
            // Necessary to render emojis.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var parser = Helpers.ConfigureCommandLine(
                args,
                "idunno.AtProtoLexicons Console Demonstration Template",
                PerformOperations);

            return await parser.InvokeAsync();
        }

        static async Task PerformOperations(string? userHandle, string? password, string? authCode, Uri? proxyUri, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(userHandle);
            ArgumentException.ThrowIfNullOrEmpty(password);

            // Uncomment the next line to route all requests through Fiddler Everywhere
            proxyUri = new Uri("http://localhost:8866");

            // Uncomment the next line to route all requests  through Fiddler Classic
            // proxyUri = new Uri("http://localhost:8888");

            // If a proxy is being used turn off certificate revocation checks.
            //
            // WARNING: this setting can introduce security vulnerabilities.
            // The assumption in these samples is that any proxy is a debugging proxy,
            // which tend to not support CRLs in the proxy HTTPS certificates they generate.
            bool checkCertificateRevocationList = true;
            if (proxyUri is not null)
            {
                checkCertificateRevocationList = false;
            }

            // Change the log level in the ConfigureConsoleLogging() to enable logging
            using (ILoggerFactory? loggerFactory = Helpers.ConfigureConsoleLogging(LogLevel.Debug))

            // Create a new AtProto agent so we can authenticate and potentially write records
            using (var agent = new AtProtoAgent(
                service: new Uri("https://bsky.social"),
                options: new AtProtoAgentOptions()
                {
                    LoggerFactory = loggerFactory,

                    HttpClientOptions = new HttpClientOptions()
                    {
                        CheckCertificateRevocationList = checkCertificateRevocationList,
                        ProxyUri = proxyUri
                    },
                }))
            {
                // Delete if your test code does not require authentication
                // START-AUTHENTICATION
                var loginResult = await agent.Login(userHandle, password, authCode, cancellationToken: cancellationToken);
                if (!loginResult.Succeeded)
                {
                    if (loginResult.AtErrorDetail is not null &&
                        string.Equals(loginResult.AtErrorDetail.Error!, "AuthFactorTokenRequired", StringComparison.OrdinalIgnoreCase))
                    {
                        ConsoleColor oldColor = Console.ForegroundColor;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Login requires an authentication code.");
                        Console.WriteLine("Check your email and use --authCode to specify the authentication code.");
                        Console.ForegroundColor = oldColor;

                        return;
                    }
                    else
                    {
                        ConsoleColor oldColor = Console.ForegroundColor;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Login failed.");
                        Console.ForegroundColor = oldColor;

                        if (loginResult.AtErrorDetail is not null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine($"Server returned {loginResult.AtErrorDetail.Error} / {loginResult.AtErrorDetail.Message}");
                            Console.ForegroundColor = oldColor;

                            return;
                        }
                    }
                }
                // END-AUTHENTICATION

                // Your code goes here

                // Create a JsonSerializerOptions so we can use the AOT/Trimmable repo methods.
                var serializerOptions = AtProtoJsonSerializerOptions.Options;
                serializerOptions.InsertTypeResolver(SourceGenerationContext.Default);

                //// Create a new status instance
                //var status = new idunno.AtProto.Lexicons.Statusphere.Xyz.Status
                //{
                //    Value = "🫘" // Beans 
                //};

                //Console.WriteLine("Creating");

                ///// Create a record status via the agent and the bsky app view, with the jsonSerializerOptions we created previously.
                //AtProtoHttpResult<CreateRecordResult> createResult = await agent.CreateRecord(
                //    record: status,
                //    collection: idunno.AtProto.Lexicons.Statusphere.Xyz.Statusphere.Collection,
                //    rKey: TimestampIdentifier.Next(),
                //    validate: false,
                //    jsonSerializerOptions: serializerOptions,
                //    cancellationToken: cancellationToken);

                //Debugger.Break();

                //if (createResult.Succeeded)
                //{
                //Console.WriteLine($"Created record {createResult.Result.Uri}.");

                    const int maximumRecordsToList = 25;

                    var listResult = await agent.ListRecords<StatusphereStatus>(
                        collection: StatusphereConstants.Collection,
                        limit: maximumRecordsToList,
                        jsonSerializerOptions: serializerOptions,
                        cancellationToken: cancellationToken);

                    if (listResult.Succeeded)
                    {
                        if (listResult.Result.Cursor is null || listResult.Result.Count < maximumRecordsToList)
                        {
                            Console.WriteLine($"There are {listResult.Result.Count} record(s) in {StatusphereConstants.Collection}");
                        }
                        else
                        {
                            Console.WriteLine($"There may be more than {listResult.Result.Count} records in {StatusphereConstants.Collection}");
                            Console.WriteLine($"Listing the first {listResult.Result.Count} records.");
                        }

                        foreach (var record in listResult.Result)
                        {
                            Console.WriteLine($"{record.Value.Status} @ {record.Value.CreatedAt:G} rKey: [{record.Uri.RecordKey}] contentId: [{record.Cid}]");
                        }

                        Debugger.Break();
                    //}

                    var getRecordResult = await agent.GetRecord<StatusphereStatus>(
                        "at://did:plc:ec72yg6n2sydzjvtovvdlxrk/xyz.statusphere.status/3mcmqsqrlfj25",
                        cancellationToken: cancellationToken);

                    var statusToUpdate = getRecordResult.Result!;

                    statusToUpdate.Value.Status = "😈";

                    //var correctUpdateResult = await agent.PutRecord(
                    //    statusToUpdate.Value,
                    //    collection: StatusphereConstants.Collection,
                    //    "3mcmqsqrlfj25",
                    //    false,
                    //    null,
                    //    getRecordResult.Result!.Cid,
                    //    cancellationToken: cancellationToken);

                    //Debugger.Break();

                    var updateResult = await agent.PutRecord(statusToUpdate, validate:false, cancellationToken: cancellationToken);

                    updateResult.EnsureSucceeded();

                    //// Clean up
                    //await agent.DeleteRecord(createResult.Result.StrongReference, cancellationToken: cancellationToken);
                    Debugger.Break();
                }
            }
        }
    }
}
