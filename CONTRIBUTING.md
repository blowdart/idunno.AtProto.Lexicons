# Contributing

🎉 Firstly a huge thank you for taking the time to contribute.🎉

The following is a set of guidelines for contributing to these libraries.
These are just guidelines, not rules, so feel free to use your own judgement, and to propose changes to these guidelines in a pull request.

## Building locally

You will need, at a minimum, a [.NET SDK 10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0) installed.

If you want to use Visual Studio you will need [Visual Studio 2026](https://visualstudio.microsoft.com/downloads/) with the
ASP.NET and web development workload installed. Visual Studio 2026 community edition will work just fine.

## Issues

Issues will be marked stale after 14 days of inactivity, and closed 14 days after they have been marked stale.

_If an issue has been closed and you still feel it's relevant, @ a maintainer in a comment to the closed issue._

## Pull requests

Pull Requests are the way concrete changes are made to the code, documentation, dependencies, and tools.

When creating a pull request please create an issue first, unless it's a simple change like a spelling correction.

* Setup your build environment
  * [Fork](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/about-forks) the repo.
  * Build your fork.
  * [Branch](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-branches) your fork.
* Making your code changes
  * Create an issue for the changes you want to make.
  * Build your code at the command line with `dotnet build`, this ensures all the code and documentation analyzers run.
  * [Test](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test) with `dotnet test` to ensure all tests pass.
  * [Commit](https://docs.github.com/en/pull-requests/committing-changes-to-your-project/creating-and-editing-commits/about-commits)
    your changes to your branch, with a meaningful commit message.
  * [Rebase](https://docs.github.com/en/get-started/using-git/about-git-rebase)
  * [Test](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test) with `dotnet test` to ensure all tests pass.
  * [Push](https://docs.github.com/en/get-started/using-git/pushing-commits-to-a-remote-repository) your commits to your fork.
  * Open a [pull request](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests).
  * Act on any comments in the pull request or associated issue.
  * Finally, hopefully, your pull request is merged into the main branch.

## Implementing a lexicon best practice

* Create a new folder in `src/idunno.AtProto.Lexicons` named after the lexicon you are implementing. Lexicons are typically named
in reverse domain notation, e.g. `com.example.myapp`, but .NET namespaces are in forward reverse domain notation with casing,
e.g. `MyApp.Example`, so name your folder with idiomatic .NET naming.
* Create a readme.md file in the folder explaining the app the lexicon is for, and link to the lexicon definitions
* Implement each lexicon record as a C# record in a file named after the record.
* **Ensure** you inherit from `AtProtoRecord` for records that are atproto records.
* Do **not** use primary constructors in your records
* **Ensure** you have a constructor that takes all record properties as parameters and is decorated with `[JsonConstructor]` and `[SetsRequiredMembers]`.
* **Ensure** you have a `[JsonConstructor]` marked constructor validates its parameters.
* Add any necessary attributes to your record properties , such as `[JsonInclude]` or [`JsonPropertyName`].
* **Ensure** you mark required properties as `[JsonRequired]`
* **Ensure** you use `DateTimeOffset` for date/time properties.
* If the lexicon requires a json $type property and the class is not used in polymorphic scenarios implement one like this:
    ```csharp
    [JsonInclude]
    [JsonPropertyName("$type")]
    public string Type { get; init; } = "com.example.myapp.typeName";
    ```
* Decorate any optional properties `[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]`.
* **Ensure** you have validation for each property that is settable in the `set` for the property.
* Avoid naming any class `Value` as this leads to ugly syntax when setting properties in `AtRepositoryRecord<>` usage.
* Add any ancillary records/classes your records require.
* **Ensure** your records and ancillary records or classes to `SourceGenerationContext.cs` as `JsonSerializable` attributes.
* **Ensure** that for each record that represents a repository record you also add `[JsonSerializable(typeof(AtProtoRepositoryRecord<MyRecord))]` to `SourceGenerationContext.cs` as a `JsonSerializable` attribute.
* Add a static class named after the app containing constants for AtProto, for example the Collection name
  ```csharp
  public static class MyAppConstants
  {
      public static Nsid Collection => new("com.example.myapp");
  }
  ```
* **Ensure** you have written XML documentation for all public types and members.
#### Tests
* Write tests
* Add a new test class in the `tests/idunno.AtProto.Lexicons.Tests` project named after your lexicon, e.g. `MyAppTests.cs`.
* For each record in your lexicon add tests for:
  * Serialization with and without the default `JsonSerializerOptions.Web` serializer options
  * Deserialization with and without `LexiconJsonSerializerOptions.Default` serializer options
  * Property validation
  * Deserialization of invalid data, e.g. missing required properties, ensuring appropriate exceptions are thrown.

## Dependency Upgrades

Dependencies in the libraries should only be altered by maintainers.
For security reasons, we will not accept PRs that alter our `Directory.Build.props` or `nuget.config` files.
If you feel dependencies need upgrading, or new dependencies added please file an issue.

## Actions Upgrades
GitHub Actions workflows should only be altered by maintainers.
For security reasons, we will not accept PRs that alter action workflows.
If you feel an action workflow need changing please file an issue.
