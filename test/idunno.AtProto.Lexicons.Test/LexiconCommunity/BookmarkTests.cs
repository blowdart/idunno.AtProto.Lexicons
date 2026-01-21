// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;

using idunno.AtProto.Lexicons.Lexicon.Community.Bookmarks;

namespace idunno.AtProto.Lexicons.Test.LexiconCommunity
{
    public class BookmarkTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void ConstructsWithMinimalNecessaryDataAndSerializesCorrectly()
        {
            string expectedUri = "https://example.com";
            string expectedType = "community.lexicon.bookmarks.bookmark";

            var bookmark = new Bookmark(new Uri(expectedUri));

            string json = JsonSerializer.Serialize(bookmark, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedType, jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedUri, jsonNode["subject"]!.GetValue<string>());
            Assert.NotNull(jsonNode["createdAt"]!.GetValue<string>());

            _ = jsonNode["createdAt"]!.GetValue<DateTimeOffset>();
        }

        [Fact]
        public void ConstructsWithMinimalNecessaryDataAndSerializesCorrectlyWithTypeInfo()
        {
            string expectedUri = "https://example.com";
            string expectedType = "community.lexicon.bookmarks.bookmark";

            var bookmark = new Bookmark(new Uri(expectedUri));

            string json = JsonSerializer.Serialize(bookmark, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedType, jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedUri, jsonNode["subject"]!.GetValue<string>());
            Assert.NotNull(jsonNode["createdAt"]!.GetValue<string>());

            _ = jsonNode["createdAt"]!.GetValue<DateTimeOffset>();
        }

        [Fact]
        public void ConstructsWithSubjectAndCreatedAtAndSerializesCorrectly()
        {
            string expectedUri = "https://example.com";
            string expectedType = "community.lexicon.bookmarks.bookmark";

            var bookmark = new Bookmark(new Uri(expectedUri));

            string json = JsonSerializer.Serialize(bookmark, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedType, jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedUri, jsonNode["subject"]!.GetValue<string>());
            Assert.NotNull(jsonNode["createdAt"]);
        }

        [Fact]
        public void ConstructsWithSubjectAndCreatedAtAndSerializesCorrectlyWithTypeInfo()
        {
            string expectedUri = "https://example.com";
            string expectedType = "community.lexicon.bookmarks.bookmark";
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;

            var bookmark = new Bookmark(new Uri(expectedUri), expectedCreatedAt);

            string json = JsonSerializer.Serialize(bookmark, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedType, jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedUri, jsonNode["subject"]!.GetValue<string>());
            Assert.NotNull(jsonNode["createdAt"]);
        }

        [Fact]
        public void ConstructsWithSubjectCreatedAtAndTagsAndSerializesCorrectly()
        {
            string expectedUri = "https://example.com";
            string expectedType = "community.lexicon.bookmarks.bookmark";
            string[] expectedTags = ["news", "funny videos"];
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;

            var bookmark = new Bookmark(new Uri(expectedUri), expectedCreatedAt, expectedTags);

            string json = JsonSerializer.Serialize(bookmark, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedType, jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedUri, jsonNode["subject"]!.GetValue<string>());
            Assert.Equal(expectedCreatedAt, jsonNode["createdAt"]!.GetValue<DateTimeOffset>());

            JsonArray actualTagArray = jsonNode["tags"]!.AsArray();
            Assert.Equal(expectedTags.Length, actualTagArray.Count);

            List<string> actualTags = [];
            foreach (JsonNode? tag in actualTagArray)
            {
                actualTags.Add(tag!.GetValue<string>());
            }

            Assert.Equivalent(expectedTags, actualTags);
        }

        [Fact]
        public void ConstructsWithSubjectCreatedAtAndTagsAndSerializesCorrectlyWithTypeInfo()
        {
            string expectedUri = "https://example.com";
            string expectedType = "community.lexicon.bookmarks.bookmark";
            string[] expectedTags = ["news", "funny videos"];
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;

            var bookmark = new Bookmark(new Uri(expectedUri), expectedCreatedAt, expectedTags);

            string json = JsonSerializer.Serialize(bookmark, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal(expectedType, jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedUri, jsonNode["subject"]!.GetValue<string>());
            Assert.Equal(expectedCreatedAt, jsonNode["createdAt"]!.GetValue<DateTimeOffset>());

            JsonArray actualTagArray = jsonNode["tags"]!.AsArray();
            Assert.Equal(expectedTags.Length, actualTagArray.Count);

            List<string> actualTags = [];
            foreach (JsonNode? tag in actualTagArray)
            {
                actualTags.Add(tag!.GetValue<string>());
            }

            Assert.Equivalent(expectedTags, actualTags);
        }

        [Fact]
        public void DeserializesCorrectlyWithMinimumData()
        {
            string jsonString = """
                {
                    "$type": "community.lexicon.bookmarks.bookmark",
                    "subject" : "https://example.com",
                    "createdAt": "2026-01-01T00:00:00.000Z"
                }
                """;

            Bookmark? actual = JsonSerializer.Deserialize<Bookmark>(jsonString, JsonSerializerOptions.Web);

            Assert.NotNull(actual);
            Assert.Equal(new Uri("https://example.com"), actual.Subject);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, new TimeSpan(0)), actual.CreatedAt);
        }

        [Fact]
        public void DeserializesCorrectlyWithMinimumDataAndTypeInfo()
        {
            string jsonString = """
                {
                    "$type": "community.lexicon.bookmarks.bookmark",
                    "subject" : "https://example.com",
                    "createdAt": "2026-01-01T00:00:00.000Z"
                }
                """;

            Bookmark? actual = JsonSerializer.Deserialize<Bookmark>(jsonString, _lexiconSerializationOptions);

            Assert.NotNull(actual);
            Assert.Equal(new Uri("https://example.com"), actual.Subject);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, new TimeSpan(0)), actual.CreatedAt);
        }

        [Fact]
        public void DeserializesFailsWhenSubjectIsMissing()
        {
            string jsonString = """
                {
                    "$type": "community.lexicon.bookmarks.bookmark",
                    "createdAt": "2026-01-01T00:00:00.000Z"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Bookmark>(jsonString, JsonSerializerOptions.Web));
        }

        [Fact]
        public void DeserializesFailsWhenCreatedAtIsMissing()
        {
            string jsonString = """
                {
                    "$type": "community.lexicon.bookmarks.bookmark",
                    "subject" : "https://example.com"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Bookmark>(jsonString, JsonSerializerOptions.Web));
        }
    }
}
