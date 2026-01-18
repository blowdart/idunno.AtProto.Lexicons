# <img src="src/idunno.AtProto.Lexicons/icon.png" height="48" style="display: block; float: left"/> idunno.AtProto.Lexicons

A collection of lexicon implementations for [AT Protocol](https://docs.bsky.app/docs/api/at-protocol-xrpc-api) services for use with [idunno.AtProto](https://www.nuget.org/packages/idunno.AtProto).

[if you want me to wear 37 pieces of flair, like your pretty boy over there, Brian, why don't you just make the minimum 37 pieces of flair?]: #

[![GitHub License](https://img.shields.io/github/license/blowdart/idunno.AtProto.Lexicons)](https://github.com/blowdart/idunno.AtProto.Lexicons/blob/main/LICENSE)
[![Last Commit](https://img.shields.io/github/last-commit/blowdart/idunno.AtProto.Lexicons)](https://github.com/blowdart/idunno.AtProto.Lexicons/commits/main/)
[![GitHub Tag](https://img.shields.io/github/v/tag/blowdart/idunno.AtProto.Lexicons)](https://github.com/blowdart/idunno.AtProto.Lexicons/tags)
[![NuGet Version](https://img.shields.io/nuget/vpre/idunno.AtProto.Lexicons)](https://www.nuget.org/packages/idunno.AtProto.Lexicons/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/idunno.AtProto.Lexicons)](https://www.nuget.org/packages/idunno.AtProto.Lexicons/)

[![OpenSSF Scorecard](https://api.scorecard.dev/projects/github.com/blowdart/idunno.Bluesky/badge)](https://scorecard.dev/viewer/?uri=github.com/blowdart/idunno.Bluesky)
[![OpenSSF Best Practices](https://www.bestpractices.dev/projects/11797/badge)](https://www.bestpractices.dev/projects/11797)

## Getting Started

Add the `idunno.AtProto.Lexicons` package to your project and using the
`idunno.AtProto` agent you can create records for the supported lexicons, like so:

```c#
using idunno.AtProto.Lexicons.Statusphere.Xyz;

AtProtoAgent agent = new ();

var loginResult = await agent.Login(username, password);
if (loginResult.Succeeded)
{
  var status = new StatosphereStatus
  {
    Status = "🫘"
  };

  AtProtoHttpResult<CreateRecordResult> createResult =
    await agent.CreateRecord(
      record: status,
      collection: StatusphereConstants.Collection,
      rKey: TimestampIdentifier.Next(),
      validate: false);

  if (createResult.Succeeded)
  {
    // Successfully posted a status.
  }
}
```

To list the custom lexicon records in a collection you would write something like this:
```c#
var listResult = await agent.ListRecords<StatusphereStatus>(
  collection: StatusphereConstants.Collection,
  limit: maximumRecordsToList);
```

To get a single record you can do something like this:
```c#
var getRecordResult = await agent.GetRecord<StatusphereStatus>(
    "at://did:plc:ec72yg6n2sydzjvtovvdlxrk/xyz.statusphere.status/3mcmqsqrlfj25");
```

And, if that record belongs to the authenticated user, you could update the record `GetRecord()` returned with:
```c#
var statusToUpdate = getRecordResult.Result!;
statusToUpdate.Value.Status = "💩";

var updateResult = await agent.PutRecord(statusToUpdate);
```

When creating records it is typical to use `TimestampIdentifier.Next()` for the `rKey` parameter. This produces a unique
record key based on the current timestamp. Some applications may use "self" as a record key to identify a record of which a single
instance is being created (such as a user profile), or completely custom record keys. Please see the application
documentation for details.

Please see the [documentation](https://bluesky.idunno.dev/) for much more documentation on the `idunno.AtProto` agent.

### JSON Source Generation support

The library contains a source-generated `JsonSerializerContext`. To use it, create a `JsonSerializerOptions` from the
`idunno.AtProto` library and insert the library type resolver like so:

```c#
var serializerOptions = AtProtoJsonSerializerOptions.Options;
serializerOptions.InsertTypeResolver(SourceGenerationContext.Default);
```

then pass the options to the `CreateRecord`, `ListRecords`, `PutRecord `or `ApplyWrites` methods

```c#
AtProtoHttpResult<CreateRecordResult> createResult =
  await agent.CreateRecord(
    record: status,
    collection: StatusphereConstants.Collection,
    rKey: TimestampIdentifier.Next(),
    validate: false,
    jsonSerializerOptions: serializerOptions);
```

## Lexicons supported

* [site.standard](standard.site) : `Standard.Site.Publication`, `Standard.Site.Document`
* [xyz.statusphere](statusphere.xyz) : `Statusphere.Xyz.Status`

## Adding new lexicons

If you want to add a new lexicon please see the [contributing guide](CONTRIBUTING.md).

## Current Build Status

[![Build Status](https://github.com/blowdart/idunno.AtProto.Lexicons/actions/workflows/ci-build.yml/badge.svg?branch=main)](https://github.com/blowdart/idunno.AtProto.Lexicons/actions/workflows/ci-build.yml)
[![CodeQL Scan](https://github.com/blowdart/idunno.AtProto.Lexicons/actions/workflows/codeql-analysis.yml/badge.svg?branch=main)](https://github.com/blowdart/idunno.AtProto.Lexicons/actions/workflows/codeql-analysis.yml)
[![Dependency Review](https://github.com/blowdart/idunno.Bluesky/idunno.AtProto.Lexicons/workflows/dependency-review.yml/badge.svg)](https://github.com/blowdart/idunno.AtProto.Lexicons/actions/workflows/dependency-review.yml)


## License

`idunno.AtProto.Lexicons` is available under the MIT license, see the [LICENSE](LICENSE) file for more information.

## Tipping / Sponsoring

If you find this library useful please consider donating to
* a local food bank,
* a local animal rescue or shelter, or
* a national Multiple Sclerosis charity in your country
  * US: [National Multiple Sclerosis Society](https://www.nationalmssociety.org/)
  * UK: [MS Society UK](https://www.mssociety.org.uk/)
  * Canada: [MS Canada](https://mscanada.ca/)

If you want to give me the warm fuzzies, you can tag me on Bluesky at [@blowdart.me](https://bsky.app/profile/blowdart.me) to let me know.

## Release History

The [releases page](https://github.com/blowdart/idunno.AtProto.Lexicons/releases) provides details of each release and what was added, changed or removed.
The [changelog](CHANGELOG.md) also contains this information, as well as information on upcoming releases.

## Release Verification

The project uses an Authenticode certificate to sign assemblies and to author sign the nupkg packages.
nuget validates the signatures during its publication process.

To validate these signatures use

```
dotnet nuget verify [<package-path(s)>]
```

The subject name of the signing certificate should be

```
Subject Name: CN=Barry Dorrans, O=Barry Dorrans, L=Bothell, S=Washington, C=US
```

In addition, GitHub artifacts are attested during build,
and are also signed with [minisign](https://github.com/jedisct1/minisign) with the following public key.

```
RWTsT4BHHChe/Rj/GBAuZHg3RaZFnfBDqaZ7KzLvr44a7mO6fLCxSAFc
```

To validate a file using an artifact signature from a [release](https://github.com/blowdart/idunno.Bluesky/releases)
download the `.nupkg` from nuget and the appropriate `.minisig` from the release page, then use the following command,
replacing `<package-path>` with the file name you wish to verify.

```
minisign -Vm <package-path> -P RWTsT4BHHChe/Rj/GBAuZHg3RaZFnfBDqaZ7KzLvr44a7mO6fLCxSAFc
```

## Pre-releases

[![Prerelease Version](https://img.shields.io/myget/blowdart/vpre/idunnoidunno.AtProto.Lexicons?label=idunno.AtProto.Lexicons)](https://www.myget.org/gallery/blowdart)

If you want to test pre-releases you can find them in the [myget feed](https://www.myget.org/gallery/blowdart).

You can add this as a Package Source in [Visual Studio](https://learn.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio#package-sources)
or through the [command line](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-nuget-add-source), or by using the sample `nuget.config` file shown below:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <clear />
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
    <add key="blowdart.myget.org" value="https://www.myget.org/F/blowdart/api/v3/index.json" />
  </packageSources>

  <packageSourceMapping>
    <packageSource key="blowdart.myget.org">
      <package pattern="idunno.AtProto" />
      <package pattern="idunno.AtProto.*" />
      <package pattern="idunno.Bluesky" />
    </packageSource>
    <packageSource key="nuget.org">
      <package pattern="*" />
    </packageSource>
  </packageSourceMapping>
</configuration>
```

The package source URI is https://www.myget.org/F/blowdart/api/v3/index.json

Please note that nightly builds are signed with [Trusted Signing](https://azure.microsoft.com/en-us/products/trusted-signing),
the signing certificate chain will not match the signing chain of a release build. The subject name remains the same.

## Dependencies

The .NET 8.0 version of `idunno.AtProto` takes a dependency on `System.Text.Json` v9 to support deserializing derived types
where the `$type` property is not the first property in the JSON object. 

### External analyzers used during builds
* [DotNetAnalyzers.DocumentationAnalyzers](https://github.com/DotNetAnalyzers/DocumentationAnalyzers) - used to validate XML docs on public types.
* [SonarAnalyzer.CSharp](https://www.sonarsource.com/products/sonarlint/features/visual-studio/) - used for common code smell detection.

### External build &amp; testing tools

* [docfx](https://dotnet.github.io/docfx/) - used to generate the documentation site.
* [DotNet.ReproducibleBuilds](https://github.com/dotnet/reproducible-builds) - used to easily set .NET reproducible build settings.
* [Coverlet.Collector](https://github.com/coverlet-coverage/coverlet) - used to produce code coverage files
* [JunitXml.TestLogger](https://github.com/spekt/junit.testlogger) - used in CI builds to produce test results in a format understood by the [test-summary](https://github.com/test-summary/action) GitHub action.
* [NerdBank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning) - used for version stamping assemblies and packages.
* [ReportGenerator](https://github.com/danielpalme/ReportGenerator) - used to produce code coverage reports.
* [sign](https://github.com/dotnet/sign) - used to code sign assemblies and nuget packages.
* [xunit](https://github.com/xunit/xunit) - used for unit tests.

## Other .NET Bluesky libraries and projects

* [FishyFlip](https://github.com/drasticactions/FishyFlip)
* [X.Bluesky](https://github.com/a-gubskiy/X.Bluesky)
* [atprotosharp](https://github.com/taranasus/atprotosharp)
* [atompds](https://github.com/PassiveModding/atompds) - an implementation of an AtProto Personal Data Server in C#
* [AppViewLite](https://github.com/alnkesq/AppViewLite) - an implementation of the Bluesky AppView in C# focused on low resource consumption
