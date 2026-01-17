// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;
using idunno.AtProto.Lexicons.Statusphere.Xyz;

namespace idunno.AtProto.Lexicons.Test.StatusSphere
{
    [ExcludeFromCodeCoverage]
    public class SerializationTests
    {
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

            JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web);
            JsonSerializerOptions serializationOptions = jsonSerializerOptions;
            {
                serializationOptions.TypeInfoResolver = SourceGenerationContext.Default;
            }

            string json = JsonSerializer.Serialize(status, serializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal("xyz.statusphere.status", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal("😐", jsonNode["status"]!.GetValue<string>());
            Assert.Equal("2026-01-01T00:00:00+00:00", jsonNode["createdAt"]!.GetValue<string>());
        }

        [Fact]
        public void StatusDeserializesCorrectly()
        {
            string json = "{\"CreatedAt\":\"2026-01-01T00:00:00+00:00\",\"$type\":\"xyz.statusphere.status\",\"status\":\"\\uD83D\\uDE10\"}";

            StatusphereStatus? status = JsonSerializer.Deserialize<StatusphereStatus>(json);

            Assert.NotNull(status);
            Assert.Equal("😐", status.Status);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero), status.CreatedAt);
        }

        [Fact]
        public void StatusDeserializesWithTypeInfo()
        {
            string json = "{\"CreatedAt\":\"2026-01-01T00:00:00+00:00\",\"$type\":\"xyz.statusphere.status\",\"status\":\"\\uD83D\\uDE10\"}";

            JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web);
            JsonSerializerOptions serializationOptions = jsonSerializerOptions;
            {
                serializationOptions.TypeInfoResolver = SourceGenerationContext.Default;
            }

            StatusphereStatus? status = JsonSerializer.Deserialize<StatusphereStatus>(json, serializationOptions);

            Assert.NotNull(status);
            Assert.Equal("😐", status.Status);
            Assert.Equal(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero), status.CreatedAt);
        }
    }
}
