// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;
using idunno.AtProto.Lexicons.Lexicon.Community.Calendar;

namespace idunno.AtProto.Lexicons.Test.LexiconCommunity
{
    public class EventTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void EventCanBeConstructedWithOnlyRequiredProperties()
        {
            _ = new Event("Sample Event", DateTimeOffset.UtcNow);

            _ = new Event
            {
                Name = "Sample Event",
                CreatedAt = DateTimeOffset.UtcNow
            };
        }

        [Fact]
        public void MinimalEventSerializesCorrectly()
        {
            string expectedEventName = "Sample Event";
            var expectedCreatedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, 0, new TimeSpan(0));

            var expected = new Event(
                expectedEventName,
                expectedCreatedAt);

            string json = JsonSerializer.Serialize(expected, JsonSerializerOptions.Web);

            JsonNode? actual = JsonNode.Parse(json);
            Assert.NotNull(actual);

            Assert.Equal(expectedEventName, actual["name"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", actual["createdAt"]!.GetValue<string>());
        }
    }
}
