// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using idunno.AtProto.Lexicons.Lexicon.Community.Interaction;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Test.LexiconCommunity
{
    public class LikeTests
    {
        [Fact]
        public void LikeCanBeConstructed()
        {
            var expectedSubject = new StrongReference("at://user.bsky.social/com.example.events/record", "bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi");

            var actual = new Lexicon.Community.Interaction.Like(expectedSubject);

            Assert.Equal(expectedSubject, actual.Subject);
            Assert.InRange(actual.CreatedAt, DateTimeOffset.UtcNow.AddMinutes(-1), DateTimeOffset.UtcNow.AddMinutes(1));
        }

        [Fact]
        public void LikeCanBeConstructedWithCreatedAt()
        {
            var expectedSubject = new StrongReference("at://user.bsky.social/com.example.events/record", "bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi");
            var expectedCreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var actual = new Lexicon.Community.Interaction.Like(expectedSubject, expectedCreatedAt);
            Assert.Equal(expectedSubject, actual.Subject);
            Assert.Equal(expectedCreatedAt, actual.CreatedAt);
        }

        [Fact]
        public void LikeCannotBeConstructedWithNullSubject()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicon.Community.Interaction.Like(null!));
        }

        [Fact]
        public void LikeSerializesCorrectly()
        {
            var expectedSubject = new StrongReference("at://user.bsky.social/com.example.events/record", "bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi");
            var expectedCreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var like = new Lexicon.Community.Interaction.Like(expectedSubject, expectedCreatedAt);
            string json = JsonSerializer.Serialize(like, LexiconJsonSerializerOptions.Default);
            string expectedJson = """
                {"$type":"community.lexicon.interaction.like","subject":{"uri":"at://user.bsky.social/com.example.events/record","cid":"bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi"},"createdAt":"2026-01-01T00:00:00+00:00"}
                """;
            Assert.Equal(expectedJson, json);
        }

        [Fact]
        public void LikeDeserializesCorrectly()
        {
            string jsonString = """
                {
                    "$type":"community.lexicon.interaction.like",
                    "subject":{
                        "uri":"at://user.bsky.social/com.example.events/record",
                        "cid":"bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi"
                    },
                    "createdAt":"2026-01-01T00:00:00+00:00"
                }
                """;
            Like? actual = JsonSerializer.Deserialize<Lexicon.Community.Interaction.Like>(jsonString, LexiconJsonSerializerOptions.Default);
            Assert.NotNull(actual);
            Assert.Equal(new StrongReference("at://user.bsky.social/com.example.events/record", "bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi"), actual!.Subject);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero), actual.CreatedAt);
        }

        [Fact]
        public void DeserializationFailsWithMissingSubject()
        {
            string jsonString = """
                {
                    "$type":"community.lexicon.interaction.like",
                    "createdAt":"2026-01-01T00:00:00+00:00"
                }
                """;
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Lexicon.Community.Interaction.Like>(jsonString, LexiconJsonSerializerOptions.Default));
        }

        [Fact]
        public void DeserializationFailsWithNullSubject()
        {
            string jsonString = """
                {
                    "$type":"community.lexicon.interaction.like",
                    "subject": null,
                    "createdAt":"2026-01-01T00:00:00+00:00"
                }
                """;
            Assert.Throws<ArgumentNullException>(() => JsonSerializer.Deserialize<Lexicon.Community.Interaction.Like>(jsonString, LexiconJsonSerializerOptions.Default));
        }

        [Fact]
        public void DeserializationFailsWithMissingCreatedAt()
        {
            string jsonString = """
                {
                    "$type":"community.lexicon.interaction.like",
                    "subject":{
                        "uri":"at://user.bsky.social/com.example.events/record",
                        "cid":"bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi"
                    }
                }
                """;
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Lexicon.Community.Interaction.Like>(jsonString, LexiconJsonSerializerOptions.Default));
        }
    }
}
