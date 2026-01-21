// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;
using idunno.AtProto.Lexicons.Standard.Site;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Test.SiteStandard
{
    public class DocumentSerializationTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void MinimalDocumentWithHttpsSiteSerializesCorrectly()
        {
            string expectedSite = "https://example.org";
            string expectedTitle = "Example Title";
            var expectedPublishedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0));

            var document = new Document
            {
                Site = expectedSite,
                Title = expectedTitle,
                PublishedAt = expectedPublishedAt,
            };

            string json = JsonSerializer.Serialize(document, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedSite, jsonNode["site"]!.GetValue<string>());
            Assert.Equal(expectedTitle, jsonNode["title"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", jsonNode["publishedAt"]!.GetValue<string>());
        }

        [Fact]
        public void MinimalDocumentWithATUriSerializesCorrectly()
        {
            string expectedSite = "at://did:plc:hfgp6pj3akhqxntgqwramlbg/site.standard.document";
            string expectedTitle = "Example Title";
            var expectedPublishedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0));

            var document = new Document
            {
                Site = expectedSite,
                Title = expectedTitle,
                PublishedAt = expectedPublishedAt,
            };

            string json = JsonSerializer.Serialize(document, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedSite, jsonNode["site"]!.GetValue<string>());
            Assert.Equal(expectedTitle, jsonNode["title"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", jsonNode["publishedAt"]!.GetValue<string>());
        }

        [Fact]
        public void MinimalDocumentWithHttpsUriSerializesCorrectly()
        {
            string expectedSite = "https://example.org";
            string expectedTitle = "Example Title";
            var expectedPublishedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0));

            var document = new Document(new Uri(expectedSite), expectedTitle, expectedPublishedAt);

            string json = JsonSerializer.Serialize(document, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedSite, jsonNode["site"]!.GetValue<string>());
            Assert.Equal(expectedTitle, jsonNode["title"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", jsonNode["publishedAt"]!.GetValue<string>());
        }

        [Fact]
        public void MinimalDocumentWithATUriParameterSerializesCorrectly()
        {
            string expectedSite = "at://did:plc:hfgp6pj3akhqxntgqwramlbg/site.standard.document";
            string expectedTitle = "Example Title";
            var expectedPublishedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0));

            var document = new Document(new AtUri(expectedSite), expectedTitle, expectedPublishedAt);

            string json = JsonSerializer.Serialize(document, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedSite, jsonNode["site"]!.GetValue<string>());
            Assert.Equal(expectedTitle, jsonNode["title"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", jsonNode["publishedAt"]!.GetValue<string>());
        }

        [Fact]
        public void DocumentSerializesCorrectly()
        {
            string expectedSite = "https://example.org";
            string expectedTitle = "Example Title";
            var expectedPublishedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0));
            string expectedPath = "/path";
            string expectedDescription = "This is an example description.";
            Blob expectedCoverImage = new (new BlobReference("expectedLink"), "image/png", 10240);
            string expectedTextContent = "Text content";
            StrongReference expectedBskyPostRef = new (
                new AtUri("at://did:plc:hfgp6pj3akhqxntgqwramlbg/app.bsky.feed.post/3l66cdbste424"),
                new Cid("bafyreibtb55kfqsny3qk4c2puoqom4cu5zkpnp5xdwu5dud264b4efocyq"));
            IEnumerable<string> expectedTags = ["tag1", "tag2"];
            var expectedUpdatedAt = new DateTimeOffset(2026, 1, 1, 1, 0, 0, 0, new TimeSpan(0));

            string expectedContentJson = """
                {
                    "$type" : "org.example.document",
                    "markdown" : "# Hello World"
                }
                """;
            JsonNode? expectedContentNode = JsonNode.Parse(expectedContentJson);

            var document = new Document(
                site: expectedSite,
                title: expectedTitle,
                publishedAt: expectedPublishedAt,
                path: expectedPath,
                description: expectedDescription,
                coverImage: expectedCoverImage,
                content: expectedContentNode,
                textContent: expectedTextContent,
                bskyPostRef: expectedBskyPostRef,
                tags: expectedTags,
                updatedAt: expectedUpdatedAt);

            string json = JsonSerializer.Serialize(document, _lexiconSerializationOptions);

            JsonNode? actual = JsonNode.Parse(json);
            Assert.NotNull(actual);

            Assert.Equal(expectedSite, actual["site"]!.GetValue<string>());
            Assert.Equal(expectedTitle, actual["title"]!.GetValue<string>());
            Assert.Equal(expectedPublishedAt, actual["publishedAt"]!.GetValue<DateTimeOffset>());
            Assert.Equal(expectedPath, actual["path"]!.GetValue<string>());
            Assert.Equal(expectedDescription, actual["description"]!.GetValue<string>());
            Assert.Equal(expectedCoverImage.Type, actual["coverImage"]!["$type"]!.GetValue<string>());
            Assert.Equal(expectedCoverImage.Reference.Link, actual["coverImage"]!["ref"]!["$link"]!.GetValue<string>());
            Assert.Equal(expectedCoverImage.MimeType, actual["coverImage"]!["mimeType"]!.GetValue<string>());
            Assert.Equal(expectedCoverImage.Size, actual["coverImage"]!["size"]!.GetValue<int>());
            Assert.Equal("org.example.document", actual["content"]!["$type"]!.GetValue<string>());
            Assert.Equal("# Hello World", actual["content"]!["markdown"]!.GetValue<string>());
            Assert.Equal(expectedTextContent, actual["textContent"]!.GetValue<string>());
            Assert.Equal(expectedBskyPostRef.Uri.ToString(), actual["bskyPostRef"]!["uri"]!.GetValue<string>());
            Assert.Equal(expectedBskyPostRef.Cid.ToString(), actual["bskyPostRef"]!["cid"]!.GetValue<string>());
            Assert.Equal(expectedUpdatedAt, actual["updatedAt"]!.GetValue<DateTimeOffset>());

            JsonArray actualTagArray = actual["tags"]!.AsArray();
            Assert.Equal(expectedTags.Count(), actualTagArray.Count);
            List<string> actualTags = [];
            foreach (JsonNode? tag in actualTagArray)
            {
                actualTags.Add(tag!.GetValue<string>());
            }

            Assert.Equivalent(expectedTags, actualTags);
        }

        [Fact]
        public void DocumentDeserializesCorrectlyWithAllPropertiesAndWebOptions()
        {
            string expected = """
                {
                    "$type": "site.standard.document",
                    "site": "https://example.org",
                    "path": "/path",
                    "title": "Example Title",
                    "description": "This is an example description.",
                    "coverImage": {
                        "$type": "blob",
                        "ref": {
                            "$link": "expectedLink"
                        },
                        "mimeType": "image/png",
                        "size": 10240
                    },
                    "content": {
                        "$type": "org.example.document",
                        "markdown": "# Hello World"
                    },
                    "textContent": "Text content",
                    "bSkyPostRef": {
                        "uri": "at://did:plc:hfgp6pj3akhqxntgqwramlbg/app.bsky.feed.post/3l66cdbste424",
                        "cid": "bafyreibtb55kfqsny3qk4c2puoqom4cu5zkpnp5xdwu5dud264b4efocyq"
                    },
                    "tags": [
                        "tag1",
                        "tag2"
                    ],
                    "publishedAt": "2026-01-01T00:00:00+00:00",
                    "updatedAt": "2026-01-01T01:00:00+00:00"
                }
                """;

            string[] expectedTags = ["tag1", "tag2"];


            Document? actual = JsonSerializer.Deserialize<Document>(expected, JsonSerializerOptions.Web);

            Assert.NotNull(actual);
            Assert.Equal("https://example.org", actual.Site);
            Assert.Equal("/path", actual.Path);
            Assert.Equal("Example Title", actual.Title);
            Assert.Equal("This is an example description.", actual.Description);
            Assert.Equal(new Blob(new BlobReference(link: "expectedLink"), "image/png", 10240), actual.CoverImage);
            Assert.Equal("Text content", actual.TextContent);
            Assert.Equal(new StrongReference("at://did:plc:hfgp6pj3akhqxntgqwramlbg/app.bsky.feed.post/3l66cdbste424", "bafyreibtb55kfqsny3qk4c2puoqom4cu5zkpnp5xdwu5dud264b4efocyq"), actual.BSkyPostRef);
            Assert.Equivalent(expectedTags, actual.Tags);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0)), actual.PublishedAt);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 1, 0, 0, 0, new TimeSpan(0)), actual.UpdatedAt);

            Assert.IsType<JsonNode>(actual.Content, exactMatch: false);
            Assert.Equal("org.example.document", actual.Content["$type"]!.GetValue<string>());
            Assert.Equal("# Hello World", actual.Content["markdown"]!.GetValue<string>());
        }

        [Fact]
        public void DocumentDeserializesCorrectlyWithAllPropertiesAndLexiconOptions()
        {
            string expected = """
                {
                    "$type": "site.standard.document",
                    "site": "https://example.org",
                    "path": "/path",
                    "title": "Example Title",
                    "description": "This is an example description.",
                    "coverImage": {
                        "$type": "blob",
                        "ref": {
                            "$link": "expectedLink"
                        },
                        "mimeType": "image/png",
                        "size": 10240
                    },
                    "content": {
                        "$type": "org.example.document",
                        "markdown": "# Hello World"
                    },
                    "textContent": "Text content",
                    "bSkyPostRef": {
                        "uri": "at://did:plc:hfgp6pj3akhqxntgqwramlbg/app.bsky.feed.post/3l66cdbste424",
                        "cid": "bafyreibtb55kfqsny3qk4c2puoqom4cu5zkpnp5xdwu5dud264b4efocyq"
                    },
                    "tags": [
                        "tag1",
                        "tag2"
                    ],
                    "publishedAt": "2026-01-01T00:00:00+00:00",
                    "updatedAt": "2026-01-01T01:00:00+00:00"
                }
                """;

            string[] expectedTags = ["tag1", "tag2"];


            Document? actual = JsonSerializer.Deserialize<Document>(expected, _lexiconSerializationOptions);

            Assert.NotNull(actual);
            Assert.Equal("https://example.org", actual.Site);
            Assert.Equal("/path", actual.Path);
            Assert.Equal("Example Title", actual.Title);
            Assert.Equal("This is an example description.", actual.Description);
            Assert.Equal(new Blob(new BlobReference(link: "expectedLink"), "image/png", 10240), actual.CoverImage);
            Assert.Equal("Text content", actual.TextContent);
            Assert.Equal(new StrongReference("at://did:plc:hfgp6pj3akhqxntgqwramlbg/app.bsky.feed.post/3l66cdbste424", "bafyreibtb55kfqsny3qk4c2puoqom4cu5zkpnp5xdwu5dud264b4efocyq"), actual.BSkyPostRef);
            Assert.Equivalent(expectedTags, actual.Tags);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0)), actual.PublishedAt);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 1, 0, 0, 0, new TimeSpan(0)), actual.UpdatedAt);

            Assert.IsType<JsonNode>(actual.Content, exactMatch: false);
            Assert.Equal("org.example.document", actual.Content["$type"]!.GetValue<string>());
            Assert.Equal("# Hello World", actual.Content["markdown"]!.GetValue<string>());
        }

        [Fact]
        public void DocumentDeserializesWithJustRequiredProperties()
        {
            string expected = """
                {
                    "$type": "site.standard.document",
                    "site": "https://example.org",
                    "title": "Example Title",
                    "publishedAt": "2026-01-01T00:00:00+00:00"
                }
                """;

            Document? actual = JsonSerializer.Deserialize<Document>(expected, JsonSerializerOptions.Web);

            Assert.NotNull(actual);
            Assert.Equal("https://example.org", actual.Site);
            Assert.Equal("Example Title", actual.Title);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0)), actual.PublishedAt);

            Assert.Null(actual.Path);
            Assert.Null(actual.Description);
            Assert.Null(actual.CoverImage);
            Assert.Null(actual.Content);
            Assert.Null(actual.TextContent);
            Assert.Null(actual.BSkyPostRef);
            Assert.Null(actual.Tags);
            Assert.Null(actual.UpdatedAt);
        }

        [Fact]
        public void DeserializationFailsWithoutSite()
        {
            string json = """
                {
                    "$type": "site.standard.document",
                    "title": "Example Title",
                    "publishedAt": "2026-01-01T00:00:00+00:00"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Document>(json, JsonSerializerOptions.Web));
        }

        [Fact]
        public void DeserializationFailsWithoutTitle()
        {
            string json = """
                {
                    "$type": "site.standard.document",
                    "site": "https://example.org",
                    "publishedAt": "2026-01-01T00:00:00+00:00"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Document>(json, JsonSerializerOptions.Web));
        }

        [Fact]
        public void DeserializationFailsWithoutPublishedAt()
        {
            string json = """
                {
                    "$type": "site.standard.document",
                    "site": "https://example.org",
                    "title": "Example Title"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Document>(json, JsonSerializerOptions.Web));
        }
    }
}
