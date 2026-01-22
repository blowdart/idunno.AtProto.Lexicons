// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;

using idunno.AtProto.Lexicons.Lexicon.Community.Calendar;
using idunno.AtProto.Lexicons.Lexicon.Community.Location;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Test.LexiconCommunity
{
    public class EventTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void EventCanBeConstructedWithOnlyRequiredProperties()
        {
            _ = new Event("Sample Event", DateTimeOffset.UtcNow);
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

        [Fact]
        public void PopulatedEventSerializesCorrectly()
        {
            string expectedEventName = "Conference 2026";
            var expectedCreatedAt = new DateTimeOffset(2026, 1, 1, 9, 0, 0, 0, new TimeSpan(0));
            string expectedDescription = "An example conference.";
            var expectedStartsAt = new DateTimeOffset(2026, 6, 1, 9, 0, 0, 0, new TimeSpan(0));
            var expectedEndsAt = new DateTimeOffset(2026, 6, 1, 17, 0, 0, 0, new TimeSpan(0));

            var expectedLocations = new LocationBase[]
            {
                new EventUri(name: "BeansCon2020", uri: new Uri("https://conference2026.example.com/")),
                new Fsq(fsqPlaceId: "FsqPlace",
                    name : "H. J. Heinz, Wigan",
                    latitude : "53.551667",
                    longitude : "-2.683056")
            };

            var expectedUris = new EventUri[]
            {
                new (name: "BeansCon2020", uri: new Uri("https://conference2026.example.com/"))
            };

            var expected = new Event(
                expectedEventName,
                expectedCreatedAt,
                description: expectedDescription,
                startsAt: expectedStartsAt,
                endsAt: expectedEndsAt,
                mode: EventMode.Hybrid,
                status: EventStatus.Scheduled,
                locations: expectedLocations,
                uris: expectedUris);
            string json = JsonSerializer.Serialize(expected, _lexiconSerializationOptions);

            JsonNode? actual = JsonNode.Parse(json);

            Assert.NotNull(actual);
            Assert.Equal(expectedEventName, actual["name"]!.GetValue<string>());
            Assert.Equal(expectedCreatedAt, actual["createdAt"]!.GetValue<DateTimeOffset>());
            Assert.Equal(expectedDescription, actual["description"]!.GetValue<string>());
            Assert.Equal(expectedStartsAt, actual["startsAt"]!.GetValue<DateTimeOffset>());
            Assert.Equal(expectedEndsAt, actual["endsAt"]!.GetValue<DateTimeOffset>());
            Assert.Equal("community.lexicon.calendar.event#hybrid", actual["mode"]!.GetValue<string>());
            Assert.Equal("community.lexicon.calendar.event#scheduled", actual["status"]!.GetValue<string>());

            JsonArray actualLocationsArray = actual["locations"]!.AsArray();
            Assert.Equal(expectedLocations.Length, actualLocationsArray.Count);
            List<LocationBase> actualLocations = [];
            foreach (JsonNode? tag in actualLocationsArray)
            {
                actualLocations.Add(JsonSerializer.Deserialize<LocationBase>(tag!, _lexiconSerializationOptions)!);
            }
            Assert.Equivalent(expectedLocations, actualLocations);

            JsonArray actualUrisArray = actual["uris"]!.AsArray();
            Assert.Equal(expectedUris.Length, actualUrisArray.Count);
            List<EventUri> actualUris = [];
            foreach (JsonNode? tag in actualUrisArray)
            {
                actualUris.Add(JsonSerializer.Deserialize<EventUri>(tag!, _lexiconSerializationOptions)!);
            }
            Assert.Equivalent(expectedUris, actualUris);
        }

        [Fact]
        public void FullyPopulatedJsonDeserializesCorrectly()
        {
            string expected = """
                {
                    "name": "Conference 2026",
                    "createdAt": "2026-05-15T09:00:00+00:00",
                    "description": "An example conference.",
                    "startsAt": "2026-06-01T09:00:00+00:00",
                    "endsAt": "2026-06-01T17:00:00+00:00",
                    "mode": "community.lexicon.calendar.event#hybrid",
                    "status": "community.lexicon.calendar.event#scheduled",
                    "locations": [
                        {
                            "$type": "community.lexicon.calendar.event#uri",
                            "name": "BeansCon2020",
                            "uri": "https://conference2026.example.com/"
                        },
                        {
                            "$type":"community.lexicon.location.fsq",
                            "fsq_place_id":"FsqPlace",
                            "name":"H. J. Heinz, Wigan",
                            "latitude":"53.551667",
                            "longitude":"-2.683056"
                        }
                    ],
                    "uris" : [
                        {
                            "$type": "community.lexicon.calendar.event#uri",
                            "name": "BeansCon2020",
                            "uri": "https://conference2026.example.com/"
                        }
                    ]
                }
                """;

            Event? actual = JsonSerializer.Deserialize<Event>(expected, _lexiconSerializationOptions);

            Assert.Equal("Conference 2026", actual!.Name);
            Assert.Equal(new DateTimeOffset(2026, 05, 15, 9, 0, 0, new TimeSpan(0)), actual!.CreatedAt);
            Assert.Equal("An example conference.", actual.Description);
            Assert.Equal(new DateTimeOffset(2026, 06, 01, 9, 0, 0, new TimeSpan(0)), actual!.StartsAt);
            Assert.Equal(new DateTimeOffset(2026, 06, 01, 17, 0, 0, new TimeSpan(0)), actual!.EndsAt);
            Assert.Equal(EventMode.Hybrid, actual.Mode);
            Assert.Equal(EventStatus.Scheduled, actual.Status);

            Assert.Collection(actual.Locations!,
                location =>
                {
                    EventUri uriLocation = Assert.IsType<EventUri>(location);

                    Assert.Equal("BeansCon2020", uriLocation.Name);
                    Assert.Equal("https://conference2026.example.com/", uriLocation.Uri.ToString());
                },
                location =>
                {
                    Fsq fsqLocation = Assert.IsType<Fsq>(location);
                    Assert.Equal("FsqPlace", fsqLocation.FsqPlaceId);
                    Assert.Equal("H. J. Heinz, Wigan", fsqLocation.Name);
                    Assert.Equal("53.551667", fsqLocation.Latitude);
                    Assert.Equal("-2.683056", fsqLocation.Longitude);
                });

            EventUri actualEventUri = Assert.Single(actual.Uris!);
            Assert.Equal("BeansCon2020", actualEventUri.Name);
            Assert.Equal("https://conference2026.example.com/", actualEventUri.Uri.ToString());
        }

        [Fact]
        public void DeserializationFailsWhenCreatedAtIsMissing()
        {
            string json = """
                {
                    "name": "Conference 2026",
                    "description": "An example conference."
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Event>(json));
        }

        [Fact]
        public void DeserializationFailsWhenNameIsMissing()
        {
            string json = """
                {
                    "createdAt": "2026-05-15T09:00:00+00:00",
                    "description": "An example conference."
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Event>(json));
        }

        [Fact]
        public void RsvpCanBeCreatedWithOnlyRequiredProperties()
        {
            _ = new Rsvp(
                new StrongReference(
                    "at://user.bsky.social/com.example.events/record",
                    "bafybeigdyrzt5sfp7udm7hu76uh7y26nf3efuylqabf3oclgtqy55fbzdi"),
                RsvpStatus.Interested);
        }
    }
}
