// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics;

using Microsoft.Extensions.Logging;

using Samples.Common;

using idunno.AtProto;
using idunno.AtProto.Lexicons;
using idunno.AtProto.Lexicons.Statusphere.Xyz;
using idunno.AtProto.Repo;

namespace Samples.AgentBasedStatusphere
{
    public sealed class Program
    {
        static async Task<int> Main(string[] args)
        {
            // Necessary to render emojis.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var parser = Helpers.ConfigureCommandLine(
                args,
                "idunno.AtProtoLexicons Statusphere With AtProtoAgent Demonstration",
                PerformOperations);

            return await parser.InvokeAsync();
        }

        static async Task PerformOperations(string? userHandle, string? password, string? authCode, Uri? proxyUri, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(userHandle);
            ArgumentException.ThrowIfNullOrEmpty(password);

            // Uncomment the next line to route all requests through Fiddler Everywhere
            // proxyUri = new Uri("http://localhost:8866");

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
            using (ILoggerFactory? loggerFactory = Helpers.ConfigureConsoleLogging(LogLevel.Error))

            // Create a new AtProto agent so we can authenticate and write records
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

                // Create a JsonSerializerOptions so source generated JSON.
                var serializerOptions = LexiconJsonSerializerOptions.Default;

                // Create a new status instance
                var status = new StatusphereStatus
                {
                    Status = "😁",
                    CreatedAt = DateTimeOffset.UtcNow

                };

                /// Create a record status via the agent and the bsky app view, with the jsonSerializerOptions we created previously.
                var createResult = await agent.CreateRecord(
                    record: status,
                    collection: StatusphereConstants.Collection,
                    rKey: TimestampIdentifier.Next(),
                    validate: false,
                    jsonSerializerOptions: serializerOptions,
                    cancellationToken: cancellationToken);

                createResult.EnsureSucceeded();

                Console.WriteLine($"Created record {createResult.Result.Uri}, rKey [{createResult.Result.Uri.RecordKey}].");

                // You can check the creation using https://atp.tools
                // Enter the user handle you are testing as, then in the collections list you should see an xyz.statusphere.status collection.
                // Select it and you will see the record just created (and potentially more).
                // It will be the first record in the list, you can manually match the rKey and select it to see the record contents.
                Console.WriteLine($"You can validate the {StatusphereConstants.Collection} exists at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}");
                Console.WriteLine($"You can validate the new record exists and see its contents at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}/{createResult.Result.Uri.RecordKey}");

                Debugger.Break();

                // Demonstrate listing custom records in a collection.

                const int maximumRecordsToList = 25;

                var listResult = await agent.ListRecords<StatusphereStatus>(
                    collection: StatusphereConstants.Collection,
                    limit: maximumRecordsToList,
                    jsonSerializerOptions: serializerOptions,
                    cancellationToken: cancellationToken);

                listResult.EnsureSucceeded();

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

                // Now get the record we created earlier, using its AtUri.
                var getRecordResult = await agent.GetRecord<StatusphereStatus>(
                    createResult.Result.Uri,
                    cancellationToken: cancellationToken);

                getRecordResult.EnsureSucceeded();

                Console.WriteLine($"Loaded record at {getRecordResult.Result.Uri} rKey: [{getRecordResult.Result.Uri.RecordKey}] => {getRecordResult.Result.Value.Status}");

                // Change the status, to demonstrate put.
                var statusToUpdate = getRecordResult.Result!;
                statusToUpdate.Value.Status = "😈";
                var updateResult = await agent.PutRecord(statusToUpdate, validate: false, cancellationToken: cancellationToken);
                updateResult.EnsureSucceeded();

                Console.WriteLine($"Updated the status at {createResult.Result.Uri}");

                // You can use https://atproto.tools to validate the status property of record has, in fact, changed
                Console.WriteLine($"You can validate record status changed at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}/{createResult.Result.Uri.RecordKey}");

                // Reload the edited record to show it has changed
                getRecordResult = await agent.GetRecord<StatusphereStatus>(
                    createResult.Result.Uri,
                    cancellationToken: cancellationToken);

                getRecordResult.EnsureSucceeded();

                Console.WriteLine($"Loaded record at {getRecordResult.Result.Uri} rKey: [{getRecordResult.Result.Uri.RecordKey}] => {getRecordResult.Result.Value.Status}");

                Debugger.Break();

                /// Clean up
                var deleteRecordResult = await agent.DeleteRecord(createResult.Result.StrongReference, cancellationToken: cancellationToken);
                deleteRecordResult.EnsureSucceeded();

                Console.WriteLine($"Deleted the record we created at at {createResult.Result.Uri}");
                Console.WriteLine($"You can check the record no longer exists at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}/{createResult.Result.Uri.RecordKey}");

                Debugger.Break();
            }
        }
    }
}

