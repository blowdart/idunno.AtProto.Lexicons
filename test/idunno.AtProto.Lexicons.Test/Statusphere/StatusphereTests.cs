// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;
using idunno.AtProto.Lexicons.Statusphere.Xyz;

namespace idunno.AtProto.Lexicons.Test.Statusphere
{
    public class StatusphereTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void ConstructorsEnforceRequiredFields()
        {
            _ = new StatusphereStatus("😐");
        }

        [Fact]
        public void StatusSerializesCorrectly()
        {
            var dateTime = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var status = new StatusphereStatus("😐", createdAt: dateTime);

            string json = JsonSerializer.Serialize(status, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal("xyz.statusphere.status", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal("😐", jsonNode["status"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", jsonNode["createdAt"]!.GetValue<string>());
       }

        [Fact]
        public void StatusSerializesCorrectlyWithTypeInfo()
        {
            var dateTime = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var status = new StatusphereStatus("😐", createdAt: dateTime);

            string json = JsonSerializer.Serialize(status, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal("xyz.statusphere.status", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal("😐", jsonNode["status"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", jsonNode["createdAt"]!.GetValue<string>());
        }

        [Fact]
        public void StatusDeserializesCorrectly()
        {
            string json = """
                {
                    "createdAt":"2026-01-01T00:00:00+00:00",
                    "status":"\uD83D\uDE10"
                }
                """;

            StatusphereStatus? status = JsonSerializer.Deserialize<StatusphereStatus>(json, _lexiconSerializationOptions);

            Assert.NotNull(status);
            Assert.Equal("😐", status.Status);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero), status.CreatedAt);
        }

        [Fact]
        public void StatusDeserializesWithTypeInfo()
        {
            string json = """
                {
                    "$type":"xyz.statusphere.status",
                    "createdAt":"2026-01-01T00:00:00+00:00",
                    "status":"\uD83D\uDE10"
                }
                """;

            StatusphereStatus? status = JsonSerializer.Deserialize<StatusphereStatus>(json, _lexiconSerializationOptions);

            Assert.NotNull(status);
            Assert.Equal("😐", status.Status);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero), status.CreatedAt);
        }

        [Fact]
        public void DeserializationFailsWithMissingStatus()
        {
            string json = """
                {
                    "$type":"xyz.statusphere.status",
                    "CreatedAt":"2026-01-01T00:00:00+00:00"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<StatusphereStatus>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void DeserializationFailsWithMissingCreatedAt()
        {
            string json = """
                {
                    "$type":"xyz.statusphere.status",
                     "status":"😐"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<StatusphereStatus>(json, _lexiconSerializationOptions));
        }
    }
}
