// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;

using idunno.AtProto.Lexicons.Lexicon.Community.Calendar;
using idunno.AtProto.Lexicons.Lexicon.Community.Location;

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
                new EventUri
                {
                    Name = "BeansCon2020",
                    Uri = new Uri("https://conference2026.example.com/")
                },
                new Fsq
                {
                    FsqPlaceId = "FsqPlace",
                    Name = "H. J. Heinz, Wigan",
                    Latitude = "53.551667",
                    Longitude = "-2.683056"
                }
            };

            var expectedUris = new EventUri[]
            {
                new() {
                    Name = "BeansCon2020",
                    Uri = new Uri("https://conference2026.example.com/")
                }
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
        public void EventWithEventUriLocationDeserializesCorrectly()
        {
            string expected = """
                {
                    "name": "Conference 2026",
                    "createdAt": "2026-05-15T09:00:00+00:00",
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

            Assert.NotNull(actual);
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
        }

    }
}
