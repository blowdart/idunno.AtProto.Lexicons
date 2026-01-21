# idunno.AtProto.Lexicons

## About

Well known lexicons for [AT Protocol](https://docs.bsky.app/docs/api/at-protocol-xrpc-api),
including support for source-generation-backed `System.Text.Json` serialization, trimming and AOT.

## Lexicons supported

* [site.standard](standard.site) : `Standard.Site.Publication`, `Standard.Site.Document`
* [xyz.statusphere](statusphere.xyz) : `Statusphere.Xyz.Status`

## Using the library

Add the nuget package to your project and using the `AtProto.Agent` from [idunno.AtProto](https://www.nuget.org/packages/idunno.AtProto)
then you can create records for the supported lexicons, like so:

```csharp
using idunno.AtProto.Lexicons.Statusphere.Xyz;

// Create an xyz.statusphere status record.

var status = new StatusphereStatus
{
  Value = "🫘"
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
```
Please see the [source repository](https://github.com/blowdart/idunno.AtProto.Lexicons) documentation for more detailed instructions.

## Version History

A full [version history](https://github.com/blowdart/idunno.AtProto.Lexicons/blob/main/CHANGELOG.md) can be found on the project's
[GitHub](https://github.com/blowdart/idunno.AtProto.Lexicons/) repository.

* [idunno.AtProto](https://www.nuget.org/packages/idunno.AtProto) for interacting with the [AtProto](https://atproto.com/) network.
* [idunno.AtProto.Types](https://www.nuget.org/packages/idunno.AtProto.Types) implementing base types for [AtProto](https://atproto.com/).
* [idunno.Bluesky](https://www.nuget.org/packages/idunno.Bluesky) for interacting with the [Bluesky social network](https://docs.bsky.app/).
* [idunno.AtProto.OAuthCallback](https://www.nuget.org/packages/idunno.AtProto.OAuthCallback) which provides a local callback server for OAuth authentication.
