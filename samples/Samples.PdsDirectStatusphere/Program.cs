// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Net;
using idunno.AtProto;
using idunno.AtProto.Lexicons;
using idunno.AtProto.Lexicons.Statusphere.Xyz;
using Microsoft.Extensions.Logging;
using Samples.Common;

namespace Samples.PdsDirectStatusphere
{
    internal static class Program
    {
        static async Task<int> Main(string[] args)
        {
            // Necessary to render emojis.
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var parser = Helpers.ConfigureCommandLine(
                args,
                "idunno.AtProtoLexicons Statusphere Direct Writing to PDS Demonstration",
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

            HttpClient httpClient;

            if (proxyUri is not null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.All,
                    // If a proxy is being used turn off certificate revocation checks.
                    //
                    // WARNING: this setting can introduce security vulnerabilities.
                    // The assumption in these samples is that any proxy is a debugging proxy,
                    // which tend to not support CRLs in the proxy HTTPS certificates they generate.
                    CheckCertificateRevocationList = false,
                    Proxy = new WebProxy(proxyUri)
                    {
                        BypassProxyOnLocal = false,
                        UseDefaultCredentials = false,
                    },
                    UseCookies = false
                };

                httpClient = new HttpClient(httpClientHandler);
            }
            else
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.All,
                    UseCookies = false,
                };

                httpClient = new HttpClient(httpClientHandler);
            }

            // Create a JsonSerializerOptions so source generated JSON.
            var serializerOptions = AtProtoJsonSerializerOptions.Options;
            serializerOptions.InsertTypeResolver(idunno.AtProto.Lexicons.SourceGenerationContext.Default);

            // Change the log level in the ConfigureConsoleLogging() to enable logging
            using (ILoggerFactory? loggerFactory = Helpers.ConfigureConsoleLogging(LogLevel.Debug))
            {
                // Get the DID for the user handle.
                Did? did = await idunno.AtProto.Resolution.ResolveHandle(userHandle, cancellationToken: cancellationToken);
                Debug.Assert(did is not null, $"DID for {userHandle} could not be resolved.");

                // Get the PDS for the user handle.
                Uri? pds = await idunno.AtProto.Resolution.ResolvePds(did, cancellationToken: cancellationToken);
                Debug.Assert(pds is not null, $"PDS for {userHandle} could not be resolved.");

                // Authenticate to the PDS via the CreateSession() api.
                var createSessionResult = await AtProtoServer.CreateSession(
                    service: pds,
                    identifier: userHandle,
                    password: password,
                    authFactorToken: authCode,
                    httpClient: httpClient,
                    loggerFactory: loggerFactory,
                    cancellationToken: cancellationToken);

                if (!createSessionResult.Succeeded)
                {
                    if (createSessionResult.AtErrorDetail is not null &&
                        string.Equals(createSessionResult.AtErrorDetail.Error!, "AuthFactorTokenRequired", StringComparison.OrdinalIgnoreCase))
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

                        if (createSessionResult.AtErrorDetail is not null)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;

                            Console.WriteLine($"Server returned {createSessionResult.AtErrorDetail.Error} / {createSessionResult.AtErrorDetail.Message}");
                            Console.ForegroundColor = oldColor;

                            return;
                        }
                    }
                }

                createSessionResult.EnsureSucceeded();

                Console.WriteLine("Login succeeded.");

                // Get the access credentials from the results of CreateSession() for use in subsequent requests.
                var accessCredentials = createSessionResult.Result.ToAccessCredentials(pds);

                // Create a new status instance
                var status = new StatusphereStatus
                {
                    Status = "😁"
                };

                var createResult = await AtProtoServer.CreateRecord(
                    record: status,
                    creator: did,
                    collection: StatusphereConstants.Collection,
                    rKey: TimestampIdentifier.Next(),
                    validate: false,
                    swapCommit: null,
                    service: pds,
                    accessCredentials: accessCredentials,
                    httpClient: httpClient,
                    jsonSerializerOptions: serializerOptions,
                    loggerFactory: loggerFactory,
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

                /// Clean up
                var deleteRecordResult = await AtProtoServer.DeleteRecord(
                    repo: did,
                    collection: StatusphereConstants.Collection,
                    rKey: createResult.Result.Uri.RecordKey!,
                    swapCommit: null,
                    swapRecord: null,
                    service: pds,
                    accessCredentials: accessCredentials,
                    httpClient: httpClient,
                    loggerFactory: loggerFactory,
                    cancellationToken: cancellationToken);
                deleteRecordResult.EnsureSucceeded();

                Console.WriteLine($"Deleted the record we created at at {createResult.Result.Uri}");
                Console.WriteLine($"You can check the record no longer exists at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}/{createResult.Result.Uri.RecordKey}");

                Debugger.Break();

            }
        }
    }
}
