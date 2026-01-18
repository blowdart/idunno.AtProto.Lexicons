// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Net;
using idunno.AtProto;
using idunno.AtProto.Lexicons;
using idunno.AtProto.Lexicons.Statusphere.Xyz;
using idunno.AtProto.Repo;
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

                var createRecordResult = await AtProtoServer.CreateRecord(
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

                createRecordResult.EnsureSucceeded();

                Console.WriteLine($"Created record {createRecordResult.Result.Uri}, rKey [{createRecordResult.Result.Uri.RecordKey}].");

                // You can check the creation using https://atp.tools
                // Enter the user handle you are testing as, then in the collections list you should see an xyz.statusphere.status collection.
                // Select it and you will see the record just created (and potentially more).
                // It will be the first record in the list, you can manually match the rKey and select it to see the record contents.
                Console.WriteLine($"You can validate the {StatusphereConstants.Collection} exists at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}");
                Console.WriteLine($"You can validate the new record exists and see its contents at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}/{createRecordResult.Result.Uri.RecordKey}");

                Debugger.Break();

                // Demonstrate listing custom records in a collection.

                const int maximumRecordsToList = 25;

                var listRecordsResult = await AtProtoServer.ListRecords<StatusphereStatus>(
                    repo: did,
                    collection: StatusphereConstants.Collection,
                    limit: maximumRecordsToList,
                    cursor: null,
                    reverse : false,
                    accessCredentials: null,
                    service: pds,
                    httpClient: httpClient,
                    jsonSerializerOptions: serializerOptions,
                    loggerFactory: loggerFactory,
                    cancellationToken: cancellationToken);

                listRecordsResult.EnsureSucceeded();

                if (listRecordsResult.Result.Cursor is null || listRecordsResult.Result.Count < maximumRecordsToList)
                {
                    Console.WriteLine($"There are {listRecordsResult.Result.Count} record(s) in {StatusphereConstants.Collection}");
                }
                else
                {
                    Console.WriteLine($"There may be more than {listRecordsResult.Result.Count} records in {StatusphereConstants.Collection}");
                    Console.WriteLine($"Listing the first {listRecordsResult.Result.Count} records.");
                }

                foreach (var record in listRecordsResult.Result)
                {
                    Console.WriteLine($"{record.Value.Status} @ {record.Value.CreatedAt:G} rKey: [{record.Uri.RecordKey}] contentId: [{record.Cid}]");
                }

                Debugger.Break();

                // Now get the record we created earlier, using its AtUri.
                var getRecordResult = await AtProtoServer.GetRecord<StatusphereStatus>(
                    repo: did,
                    collection: StatusphereConstants.Collection,
                    rKey: createRecordResult.Result.Uri.RecordKey!,
                    cid: null, // get the latest version,
                    service: pds,
                    accessCredentials: null,
                    httpClient: httpClient,
                    jsonSerializerOptions: serializerOptions,
                    loggerFactory: loggerFactory,
                    cancellationToken: cancellationToken);

                getRecordResult.EnsureSucceeded();

                Console.WriteLine($"Loaded record at {getRecordResult.Result.Uri} rKey: [{getRecordResult.Result.Uri.RecordKey}] => {getRecordResult.Result.Value.Status}");

                Debugger.Break();

                // Update the record we just retrieved.
                StatusphereStatus statusToUpdate = getRecordResult.Result.Value;
                statusToUpdate.Status = "😎";

                var putRecordResult = await AtProtoServer.PutRecord(
                    record: statusToUpdate,
                    collection: StatusphereConstants.Collection,
                    creator: did,
                    rKey: getRecordResult.Result.Uri.RecordKey!,
                    validate: false,
                    swapCommit: null,
                    swapRecord: getRecordResult.Result.Cid,
                    service: pds,
                    accessCredentials: accessCredentials,
                    httpClient: httpClient,
                    jsonSerializerOptions: serializerOptions,
                    loggerFactory: loggerFactory,
                    cancellationToken: cancellationToken);

                putRecordResult.EnsureSucceeded();

                Console.WriteLine($"Updated the status at {putRecordResult.Result.Uri}");

                // You can use https://atproto.tools to validate the status property of record has, in fact, changed
                Console.WriteLine($"You can validate record status changed at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}/{putRecordResult.Result.Uri.RecordKey}");

                // Get the updated record to confirm the update took place.
                getRecordResult = await AtProtoServer.GetRecord<StatusphereStatus>(
                    repo: did,
                    collection: StatusphereConstants.Collection,
                    rKey: getRecordResult.Result.Uri.RecordKey!,
                    cid: null, // get the latest version,
                    service: pds,
                    accessCredentials: null,
                    httpClient: httpClient,
                    jsonSerializerOptions: serializerOptions,
                    loggerFactory: loggerFactory,
                    cancellationToken: cancellationToken);

                getRecordResult.EnsureSucceeded();

                Console.WriteLine($"Loaded record at {getRecordResult.Result.Uri} rKey: [{getRecordResult.Result.Uri.RecordKey}] => {getRecordResult.Result.Value.Status}");

                Debugger.Break();

                /// Clean up
                var deleteRecordResult = await AtProtoServer.DeleteRecord(
                    repo: did,
                    collection: StatusphereConstants.Collection,
                    rKey: createRecordResult.Result.Uri.RecordKey!,
                    swapCommit: null,
                    swapRecord: null,
                    service: pds,
                    accessCredentials: accessCredentials,
                    httpClient: httpClient,
                    loggerFactory: loggerFactory,
                    cancellationToken: cancellationToken);
                deleteRecordResult.EnsureSucceeded();

                Console.WriteLine($"Deleted the record we created at at {createRecordResult.Result.Uri}");
                Console.WriteLine($"You can check the record no longer exists at https://atp.tools/at:/{userHandle}/{StatusphereConstants.Collection}/{createRecordResult.Result.Uri.RecordKey}");

                Debugger.Break();

            }
        }
    }
}
