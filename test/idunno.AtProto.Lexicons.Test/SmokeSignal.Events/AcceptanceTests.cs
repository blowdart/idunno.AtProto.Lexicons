// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;

using idunno.AtProto.Lexicons.SmokeSignal.Events;

namespace idunno.AtProto.Lexicons.Test.SmokeSignal.Events
{
    public class AcceptanceTests
    {

        [Fact]
        public void AcceptanceCanBeConstructedWithValidParameters()
        {
            // Arrange
            var expectedCid = new Cid("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq");
            var actual = new Acceptance(expectedCid);

            Assert.Equal(expectedCid, actual.Cid);
        }

        [Fact]
        public void AcceptanceConstructorThrowsArgumentNullExceptionWhenCidIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Acceptance(null!));
        }

        [Fact]
        public void AcceptanceSerializesCorrectly()
        {
            var expectedCid = new Cid("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq");
            var expectedAcceptance = new Acceptance(expectedCid);

            string json = JsonSerializer.Serialize(expectedAcceptance, LexiconJsonSerializerOptions.Default);

            JsonNode? actual = JsonNode.Parse(json);

            Assert.NotNull(actual);

            Assert.Equal("events.smokesignal.calendar.acceptance", actual["$type"]!.GetValue<string>());
            Assert.Equal(expectedCid.ToString(), actual["cid"]!.GetValue<string>());
        }

        [Fact]
        public void AcceptanceDeserializesCorrectly()
        {
            string json = """
                {
                    "$type": "events.smokesignal.calendar.acceptance",
                    "cid": "bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"
                }
                """;

            var expectedCid = new Cid("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq");

            Acceptance? actualAcceptance = JsonSerializer.Deserialize<Acceptance>(json, LexiconJsonSerializerOptions.Default);
            Assert.NotNull(actualAcceptance);
            Assert.Equal(expectedCid, actualAcceptance!.Cid);
        }

        [Fact]
        public void AcceptanceDeserializationThrowsWhenCidIsMissing()
        {
            string json = """
                {
                    "$type": "events.smokesignal.calendar.acceptance"
                }
                """;
            Assert.Throws<JsonException>(() =>
            {
                JsonSerializer.Deserialize<Acceptance>(json, LexiconJsonSerializerOptions.Default);
            });
        }
    }
}
