# idunno.AtProto.Lexicons Samples

This folder contains various samples to demonstrate various features of the `idunno.AtProto.Lexicons` library.

Most console samples uses the same command line arguments:

* `--handle` : The handle to use when authenticating.
* `--password` : The password to use when authenticating (this parameter is ignored by OAuth samples).
* `--authcode` : The authorization code to use when authenticating.
* `--proxy`: The URI of the proxy server you wish to use.

If you don't supply a handle or a password samples will check the `_BlueskyHandle` and `_BlueskyPassword` environment variables.

If you use an Bluesky app password you don't need to worry about authorization codes.
