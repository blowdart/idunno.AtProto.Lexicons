// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Serialization;
using idunno.AtProto.Lexicons.SmokeSignal.Events;

namespace idunno.AtProto.Lexicons.Test.SmokeSignal.Events
{
    public class LfgTests
    {
        [Fact]
        public void LfgCanBeConstructedWithValidParameters()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);

            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;

            var actual = new Lfg(
                location: expectedLocation,
                tags: expectedTags,
                startsAt: expectedStartsAt,
                endsAt: expectedEndsAt,
                createdAt: expectedCreatedAt,
                active: expectedActive);

            Assert.NotNull(actual);
            Assert.Equal(expectedLocation, actual.Location);
            Assert.Equal(expectedTags, actual.Tags);
            Assert.Equal(expectedStartsAt, actual.StartsAt);
            Assert.Equal(expectedEndsAt, actual.EndsAt);
            Assert.Equal(expectedCreatedAt, actual.CreatedAt);
            Assert.Equal(expectedActive, actual.Active);
        }

        [Fact]
        public void LfgCanBeConstructedWithoutCreatedAt()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);

            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            bool expectedActive = true;

            var actual = new Lfg(
                location: expectedLocation,
                tags: expectedTags,
                startsAt: expectedStartsAt,
                endsAt: expectedEndsAt,
                active: expectedActive);

            Assert.NotNull(actual);
            Assert.Equal(expectedLocation, actual.Location);
            Assert.Equal(expectedTags, actual.Tags);
            Assert.Equal(expectedStartsAt, actual.StartsAt);
            Assert.Equal(expectedEndsAt, actual.EndsAt);
            Assert.InRange(actual.CreatedAt, DateTimeOffset.UtcNow.AddMinutes(-1), DateTimeOffset.UtcNow.AddMinutes(1));
            Assert.Equal(expectedActive, actual.Active);
        }

        [Fact]
        public void LfgConstructorThrowsOnNullLocation()
        {
            var tags = new List<string> { "hiking" };
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new Lfg(
                    location: null!,
                    tags: tags,
                    startsAt: startsAt,
                    endsAt: endsAt,
                    active: true);
            });
        }

        [Fact]
        public void LfgConstructorThrowsOnNullTags()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            Assert.Throws<ArgumentNullException>(() =>
            {
                _ = new Lfg(
                    location: location,
                    tags: null!,
                    startsAt: startsAt,
                    endsAt: endsAt,
                    active: true);
            });
        }

        [Fact]
        public void LfgConstructorThrowsOnInvalidTagCount()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            // Test with zero tags
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new Lfg(
                    location: location,
                    tags: [],
                    startsAt: startsAt,
                    endsAt: endsAt,
                    active: true);
            });

            // Test with more than 10 tags
            var tooManyTags = Enumerable.Range(1, 11).Select(i => $"tag{i}").ToList();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new Lfg(
                    location: location,
                    tags: tooManyTags,
                    startsAt: startsAt,
                    endsAt: endsAt,
                    active: true);
            });
        }

        [Fact]
        public void LfgConstructorThrowsOnTooLongTag()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            string longTag = new('a', 65); // 65 characters
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new Lfg(
                    location: location,
                    tags: [longTag],
                    startsAt: startsAt,
                    endsAt: endsAt,
                    active: true);
            });
        }

        [Fact]
        public void LfgConstructorThrowsOnTooLongGraphemeTag()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            string longGraphemeTag = "a̐éö̲"; // Each character is a grapheme cluster, total 65 graphemes
            longGraphemeTag = string.Concat(Enumerable.Repeat(longGraphemeTag, 13)); // 13 * 5 = 65 graphemes
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = new Lfg(
                    location: location,
                    tags: [longGraphemeTag],
                    startsAt: startsAt,
                    endsAt: endsAt,
                    active: true);
            });
        }

        [Fact]
        public void LfgLocationSetterThrowsOnNull()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var tags = new List<string> { "hiking" };
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            var lfg = new Lfg(
                location: location,
                tags: tags,
                startsAt: startsAt,
                endsAt: endsAt,
                active: true);
            Assert.Throws<ArgumentNullException>(() =>
            {
                lfg.Location = null!;
            });
        }

        [Fact]
        public void LfgTagsSetterThrowsOnNull()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var tags = new List<string> { "hiking" };
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            var lfg = new Lfg(
                location: location,
                tags: tags,
                startsAt: startsAt,
                endsAt: endsAt,
                active: true);
            Assert.Throws<ArgumentNullException>(() =>
            {
                lfg.Tags = null!;
            });
        }

        [Fact]
        public void LfgTagsSetterThrowsOnEmptyList()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var tags = new List<string> { "hiking" };
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            var lfg = new Lfg(
                location: location,
                tags: tags,
                startsAt: startsAt,
                endsAt: endsAt,
                active: true);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                lfg.Tags = [];
            });
        }

        [Fact]
        public void LfgTagsSetterThrowsOnTooManyTags()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var tags = new List<string> { "hiking" };
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            var lfg = new Lfg(
                location: location,
                tags: tags,
                startsAt: startsAt,
                endsAt: endsAt,
                active: true);
            var tooManyTags = Enumerable.Range(1, 11).Select(i => $"tag{i}").ToList();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                lfg.Tags = tooManyTags;
            });
        }

        [Fact]
        public void LfgTagsSetterThrowsOnTooLongTag()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var tags = new List<string> { "hiking" };
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            var lfg = new Lfg(
                location: location,
                tags: tags,
                startsAt: startsAt,
                endsAt: endsAt,
                active: true);
            string longTag = new('a', 65); // 65 characters
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                lfg.Tags = [longTag];
            });
        }

        [Fact]
        public void LfgSetterThrowsOnTooLongGraphemeTag()
        {
            var location = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var tags = new List<string> { "hiking" };
            DateTimeOffset startsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset endsAt = DateTimeOffset.UtcNow.AddHours(5);
            var lfg = new Lfg(
                location: location,
                tags: tags,
                startsAt: startsAt,
                endsAt: endsAt,
                active: true);
            string longGraphemeTag = "a̐éö̲"; // Each character is a grapheme cluster, total 65 graphemes
            longGraphemeTag = string.Concat(Enumerable.Repeat(longGraphemeTag, 13)); // 13 * 5 = 65 graphemes
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                lfg.Tags = [longGraphemeTag];
            });
        }

        [Fact]
        public void LfgSerializesCorrectly()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;


            var lfg = new Lfg(
                location: expectedLocation,
                tags: expectedTags,
                startsAt: expectedStartsAt,
                endsAt: expectedEndsAt,
                createdAt: expectedCreatedAt,
                active: expectedActive);

            string json = JsonSerializer.Serialize(lfg, LexiconJsonSerializerOptions.Default);

            JsonNode? actual = JsonNode.Parse(json);
            Assert.NotNull(actual);

            Assert.Equal("events.smokesignal.lfg", actual["$type"]!.GetValue<string>());

            Assert.Equal("community.lexicon.location.geo", actual["location"]!["$type"]!.GetValue<string>());
            Assert.Equal("37.7749", actual["location"]!["latitude"]!.GetValue<string>());
            Assert.Equal("-122.4194", actual["location"]!["longitude"]!.GetValue<string>());

            Assert.Equal(2, actual["tags"]!.AsArray().Count);
            Assert.Equal("hiking", actual["tags"]!.AsArray()[0]!.GetValue<string>());
            Assert.Equal("gaming", actual["tags"]!.AsArray()[1]!.GetValue<string>());

            Assert.Equal(expectedStartsAt, actual["startsAt"]!.GetValue<DateTimeOffset>());
            Assert.Equal(expectedEndsAt, actual["endsAt"]!.GetValue<DateTimeOffset>());
            Assert.Equal(expectedCreatedAt, actual["createdAt"]!.GetValue<DateTimeOffset>());
        }

        [Fact]
        public void LfgDeserializesCorrectly()
        {
            var expectedLocation = new Lexicons.Lexicon.Community.Location.Geo(
                name: "Somewhere",
                latitude: 37.7749,
                longitude: -122.4194);
            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            var lfg = new Lfg(
                location: expectedLocation,
                tags: expectedTags,
                startsAt: expectedStartsAt,
                endsAt: expectedEndsAt,
                createdAt: expectedCreatedAt,
                active: expectedActive);
            string json = JsonSerializer.Serialize(lfg, LexiconJsonSerializerOptions.Default);
            Lfg? actual = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            Assert.NotNull(actual);
            Assert.Equal(expectedLocation.Altitude, actual.Location.Altitude);
            Assert.Equal(expectedLocation.Latitude, actual.Location.Latitude);
            Assert.Equal(expectedLocation.Longitude, actual.Location.Longitude);
            Assert.Equal(expectedLocation.Name, actual.Location.Name);
            Assert.Equal(expectedTags, actual.Tags);
            Assert.Equal(expectedStartsAt, actual.StartsAt);
            Assert.Equal(expectedEndsAt, actual.EndsAt);
            Assert.Equal(expectedCreatedAt, actual.CreatedAt);
            Assert.Equal(expectedActive, actual.Active);
        }

        [Fact]
        public void LfgDoesNotDeserializeWithMissingLocation()
        {
            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""tags"": [""hiking"", ""gaming""],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";

            Assert.Throws<JsonException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotDeserializeWithMissingTags()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location#geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<JsonException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotDeserializeWithEmptyTags()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotDeserializeWithTooManyTags()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string tooManyTags = string.Join("\", \"", Enumerable.Range(1, 11).Select(i => $"tag{i}"));
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [""{tooManyTags}""],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotDeserializeWithTooLongTag()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string longTag = new('a', 65); // 65 characters
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [""{longTag}""],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotDeserializeWithTooLongGraphemeTag()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string longGraphemeTag = "a̐éö̲"; // Each character is a grapheme cluster, total 65 graphemes
            longGraphemeTag = string.Concat(Enumerable.Repeat(longGraphemeTag, 13)); // 13 * 5 = 65 graphemes
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [""{longGraphemeTag}""],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotDeserializeWithMissingStartsAt()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow.AddHours(5);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [""hiking"", ""gaming""],
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<JsonException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotDeserializeWithMissingEndsAt()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [""hiking"", ""gaming""],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<JsonException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotSerializeWithMissingCreatedAt()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow;
            bool expectedActive = true;
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [""hiking"", ""gaming""],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""active"": {expectedActive.ToString().ToLower()}
            }}";
            Assert.Throws<JsonException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }

        [Fact]
        public void LfgDoesNotSerializeWithMissingActive()
        {
            var expectedLocation = new idunno.AtProto.Lexicons.Lexicon.Community.Location.Geo(
                latitude: 37.7749,
                longitude: -122.4194);
            var expectedTags = new List<string> { "hiking", "gaming" };
            DateTimeOffset expectedStartsAt = DateTimeOffset.UtcNow.AddHours(1);
            DateTimeOffset expectedEndsAt = DateTimeOffset.UtcNow;
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            string json = $@"
            {{
                ""$type"": ""events.smokesignal.lfg"",
                ""location"": {{
                    ""$type"": ""community.lexicon.location.geo"",
                    ""latitude"": ""37.7749"",
                    ""longitude"": ""-122.4194""
                }},
                ""tags"": [""hiking"", ""gaming""],
                ""startsAt"": ""{expectedStartsAt:o}"",
                ""endsAt"": ""{expectedEndsAt:o}"",
                ""createdAt"": ""{expectedCreatedAt:o}""
            }}";
            Assert.Throws<JsonException>(() =>
            {
                _ = JsonSerializer.Deserialize<Lfg>(json, LexiconJsonSerializerOptions.Default);
            });
        }
    }
}
