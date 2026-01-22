// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;

using idunno.AtProto.Lexicons.Lexicon.Community.Location;

namespace idunno.AtProto.Lexicons.Test.LexiconCommunity
{
    public class LocationTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void AddressCanBeConstructedWithOnlyRequiredProperties()
        {
            _ = new Address("UK");
        }

        [Fact]
        public void AddressConstructorFailsOnTooShortACountry()
        {
            string expectedCountry = "U";

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Address(expectedCountry));
        }

        [Fact]
        public void AddressConstructorFailsOnTooLongACountry()
        {
            string expectedCountry = "UUUUUUUUUUU";

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Address(expectedCountry));
        }

        [Fact]
        public void AddressConstructorSetsAllTheProperties()
        {
            string expectedCountry = "UK";
            string expectedName = "My Location";
            string expectedPostalCode = "AB12 3CD";
            string expectedRegion = "SomeRegion";
            string expectedLocality = "SomeLocality";
            string expectedStreet = "123 Some Street";

            var address = new Address(expectedCountry, expectedName, expectedPostalCode, expectedRegion, expectedLocality, expectedStreet);
            Assert.Equal(expectedCountry, address.Country);
            Assert.Equal(expectedName, address.Name);
            Assert.Equal(expectedPostalCode, address.PostalCode);
            Assert.Equal(expectedRegion, address.Region);
            Assert.Equal(expectedLocality, address.Locality);
            Assert.Equal(expectedStreet, address.Street);
        }

        [Fact]
        public void AddressSerializesCorrectlyAsLocationBase()
        {
            string expectedCountry = "UK";
            string expectedName = "My Location";
            string expectedPostalCode = "AB12 3CD";
            string expectedRegion = "SomeRegion";
            string expectedLocality = "SomeLocality";
            string expectedStreet = "123 Some Street";

            var address = new Address(expectedCountry, expectedName, expectedPostalCode, expectedRegion, expectedLocality, expectedStreet);

            string json = JsonSerializer.Serialize<LocationBase>(address, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);

            Assert.NotNull(jsonNode);
            Assert.Equal("community.lexicon.location.address", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedCountry, jsonNode["country"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedPostalCode, jsonNode["postalCode"]!.GetValue<string>());
            Assert.Equal(expectedRegion, jsonNode["region"]!.GetValue<string>());
            Assert.Equal(expectedLocality, jsonNode["locality"]!.GetValue<string>());
            Assert.Equal(expectedStreet, jsonNode["street"]!.GetValue<string>());
        }

        [Fact]
        public void AddressSerializesCorrectlyAsLocationBaseWithLexiconSerializerOptions()
        {
            string expectedCountry = "UK";
            string expectedName = "My Location";
            string expectedPostalCode = "AB12 3CD";
            string expectedRegion = "SomeRegion";
            string expectedLocality = "SomeLocality";
            string expectedStreet = "123 Some Street";

            var address = new Address(expectedCountry, expectedName, expectedPostalCode, expectedRegion, expectedLocality, expectedStreet);

            string json = JsonSerializer.Serialize<LocationBase>(address, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);

            Assert.NotNull(jsonNode);
            Assert.Equal("community.lexicon.location.address", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedCountry, jsonNode["country"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedPostalCode, jsonNode["postalCode"]!.GetValue<string>());
            Assert.Equal(expectedRegion, jsonNode["region"]!.GetValue<string>());
            Assert.Equal(expectedLocality, jsonNode["locality"]!.GetValue<string>());
            Assert.Equal(expectedStreet, jsonNode["street"]!.GetValue<string>());
        }

        [Fact]
        public void AddressSerializesCorrectlyAsAddress()
        {
            string expectedCountry = "UK";
            string expectedName = "My Location";
            string expectedPostalCode = "AB12 3CD";
            string expectedRegion = "SomeRegion";
            string expectedLocality = "SomeLocality";
            string expectedStreet = "123 Some Street";

            var address = new Address(expectedCountry, expectedName, expectedPostalCode, expectedRegion, expectedLocality, expectedStreet);

            string json = JsonSerializer.Serialize(address, JsonSerializerOptions.Web);

            JsonNode? jsonNode = JsonNode.Parse(json);

            Assert.NotNull(jsonNode);
            // No type as it's not serialized as the base record.
            Assert.Equal(expectedCountry, jsonNode["country"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedPostalCode, jsonNode["postalCode"]!.GetValue<string>());
            Assert.Equal(expectedRegion, jsonNode["region"]!.GetValue<string>());
            Assert.Equal(expectedLocality, jsonNode["locality"]!.GetValue<string>());
            Assert.Equal(expectedStreet, jsonNode["street"]!.GetValue<string>());
        }

        [Fact]
        public void AddressSerializesCorrectlyAsAddressWithLexiconSerializerOptions()
        {
            string expectedCountry = "UK";
            string expectedName = "My Location";
            string expectedPostalCode = "AB12 3CD";
            string expectedRegion = "SomeRegion";
            string expectedLocality = "SomeLocality";
            string expectedStreet = "123 Some Street";

            var address = new Address(expectedCountry, expectedName, expectedPostalCode, expectedRegion, expectedLocality, expectedStreet);

            string json = JsonSerializer.Serialize(address, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);

            Assert.NotNull(jsonNode);
            // No type as it's not serialized as the base record.
            Assert.Equal(expectedCountry, jsonNode["country"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedPostalCode, jsonNode["postalCode"]!.GetValue<string>());
            Assert.Equal(expectedRegion, jsonNode["region"]!.GetValue<string>());
            Assert.Equal(expectedLocality, jsonNode["locality"]!.GetValue<string>());
            Assert.Equal(expectedStreet, jsonNode["street"]!.GetValue<string>());
        }

        [Fact]
        public void AddressDeserializesCorrectly()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.address",
                    "country":"UK",
                    "name":"My Location",
                    "postalCode":"AB12 3CD",
                    "region":"SomeRegion",
                    "locality":"SomeLocality",
                    "street":"123 Some Street"
                }
                """;

            LocationBase? location = JsonSerializer.Deserialize<LocationBase>(json, _lexiconSerializationOptions);
            Assert.NotNull(location);
            Assert.IsType<Address>(location);

            var address = (Address)location!;
            Assert.Equal("UK", address.Country);
            Assert.Equal("My Location", address.Name);
            Assert.Equal("AB12 3CD", address.PostalCode);
            Assert.Equal("SomeRegion", address.Region);
            Assert.Equal("SomeLocality", address.Locality);
            Assert.Equal("123 Some Street", address.Street);
        }

        [Fact]
        public void AddressDeserializationFailsWithMissingCountry()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.address",
                    "name":"My Location",
                    "postalCode":"AB12 3CD",
                    "region":"SomeRegion",
                    "locality":"SomeLocality",
                    "street":"123 Some Street"
                }
                """;
            Assert.Throws<JsonException>(() => _ = JsonSerializer.Deserialize<LocationBase>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void AddressDeserializationFailsWithTooShortCountry()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.address",
                    "county":"U",
                    "name":"My Location",
                    "postalCode":"AB12 3CD",
                    "region":"SomeRegion",
                    "locality":"SomeLocality",
                    "street":"123 Some Street"
                }
                """;
            Assert.Throws<JsonException>(() => _ = JsonSerializer.Deserialize<LocationBase>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void AddressDeserializationFailsWithTooLongCountry()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.address",
                    "county":"UUUUUUUUUUU",
                    "name":"My Location",
                    "postalCode":"AB12 3CD",
                    "region":"SomeRegion",
                    "locality":"SomeLocality",
                    "street":"123 Some Street"
                }
                """;
            Assert.Throws<JsonException>(() => _ = JsonSerializer.Deserialize<LocationBase>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void FsqCanBeConstructedWithOnlyRequiredProperties()
        {
            _ = new Fsq("FsqPlace");
        }

        [Fact]
        public void FsqConstructorSetsAllTheProperties()
        {
            string expectedFsqPlaceId = "FsqPlace";
            string expectedName = "H. J. Heinz, Wigan";
            string expectedLatitude = "53.551667";
            string expectedLongitude = "-2.683056";

            var fsq = new Fsq(expectedFsqPlaceId, expectedName, expectedLatitude, expectedLongitude);
            Assert.Equal(expectedFsqPlaceId, fsq.FsqPlaceId);
            Assert.Equal(expectedName, fsq.Name);
            Assert.Equal(expectedLatitude, fsq.Latitude);
            Assert.Equal(expectedLongitude, fsq.Longitude);
        }

        [Fact]
        public void FsqSerializesCorrectlyAsLocationBase()
        {
            string expectedFsqPlaceId = "FsqPlace";
            string expectedName = "H. J. Heinz, Wigan";
            string expectedLatitude = "53.551667";
            string expectedLongitude = "-2.683056";

            var fsq = new Fsq(expectedFsqPlaceId, expectedName, expectedLatitude, expectedLongitude);
            string json = JsonSerializer.Serialize<LocationBase>(fsq, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);
            Assert.Equal("community.lexicon.location.fsq", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedFsqPlaceId, jsonNode["fsq_place_id"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedLatitude, jsonNode["latitude"]!.GetValue<string>());
            Assert.Equal(expectedLongitude, jsonNode["longitude"]!.GetValue<string>());
        }

        [Fact]
        public void FsqSerializesCorrectlyAsFsq()
        {
            string expectedFsqPlaceId = "FsqPlace";
            string expectedName = "H. J. Heinz, Wigan";
            string expectedLatitude = "53.551667";
            string expectedLongitude = "-2.683056";

            var fsq = new Fsq(expectedFsqPlaceId, expectedName, expectedLatitude, expectedLongitude);
            string json = JsonSerializer.Serialize(fsq, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);
            // No type as it's not serialized as the base record.
            Assert.Equal(expectedFsqPlaceId, jsonNode["fsq_place_id"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedLatitude, jsonNode["latitude"]!.GetValue<string>());
            Assert.Equal(expectedLongitude, jsonNode["longitude"]!.GetValue<string>());
        }

        [Fact]
        public void FsqDeserializesCorrectly()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.fsq",
                    "fsq_place_id":"FsqPlace",
                    "name":"H. J. Heinz, Wigan",
                    "latitude":"53.551667",
                    "longitude":"-2.683056"
                }
                """;
            LocationBase? location = JsonSerializer.Deserialize<LocationBase>(json, _lexiconSerializationOptions);
            Assert.NotNull(location);
            Assert.IsType<Fsq>(location);
            var fsq = (Fsq)location!;
            Assert.Equal("FsqPlace", fsq.FsqPlaceId);
            Assert.Equal("H. J. Heinz, Wigan", fsq.Name);
            Assert.Equal("53.551667", fsq.Latitude);
            Assert.Equal("-2.683056", fsq.Longitude);
        }

        [Fact]
        public void FsqDeserializesCorrectlyAsFsq()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.fsq",
                    "fsq_place_id":"FsqPlace",
                    "name":"H. J. Heinz, Wigan",
                    "latitude":"53.551667",
                    "longitude":"-2.683056"
                }
                """;
            Fsq? fsq = JsonSerializer.Deserialize<Fsq>(json, _lexiconSerializationOptions);
            Assert.NotNull(fsq);
            Assert.Equal("FsqPlace", fsq.FsqPlaceId);
            Assert.Equal("H. J. Heinz, Wigan", fsq.Name);
            Assert.Equal("53.551667", fsq.Latitude);
            Assert.Equal("-2.683056", fsq.Longitude);
        }

        [Fact]
        public void FsqDeserializationFailsWithoutFsqPlaceId()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.fsq",
                    "name":"H. J. Heinz, Wigan",
                    "latitude":"53.551667",
                    "longitude":"-2.683056"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Fsq>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void GeoCanBeConstructedWithOnlyRequiredProperties()
        {
            _ = new Geo("53.551667", "-2.683056");
            _ = new Geo(53.551667f, -2.683056f);
        }

        [Fact]
        public void GeoConstructorSetsProperties()
        {
            string expectedLatitude = "53.551667";
            string expectedLongitude = "-2.683056";
            string expectedAltitude = "0";
            string expectedName = "H. J. Heinz, Wigan";

            var geo = new Geo(expectedLatitude, expectedLongitude, expectedAltitude, expectedName);

            Assert.Equal(expectedLatitude, geo.Latitude);
            Assert.Equal(expectedLongitude, geo.Longitude);
            Assert.Equal(expectedAltitude, geo.Altitude);
            Assert.Equal(expectedName, expectedName);
        }

        [Fact]
        public void GeoSerializesCorrectlyAsLocationBase()
        {
            string expectedLatitude = "53.551667";
            string expectedLongitude = "-2.683056";
            string expectedAltitude = "0";
            string expectedName = "H. J. Heinz, Wigan";

            var geo = new Geo(expectedLatitude, expectedLongitude, expectedAltitude, expectedName);
            string json = JsonSerializer.Serialize<LocationBase>(geo, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);
            Assert.Equal("community.lexicon.location.geo", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedLatitude, jsonNode["latitude"]!.GetValue<string>());
            Assert.Equal(expectedLongitude, jsonNode["longitude"]!.GetValue<string>());
            Assert.Equal(expectedAltitude, jsonNode["altitude"]!.GetValue<string>());
        }

        [Fact]
        public void GeoSerializesCorrectlyAsGeo()
        {
            string expectedLatitude = "53.551667";
            string expectedLongitude = "-2.683056";
            string expectedAltitude = "0";
            string expectedName = "H. J. Heinz, Wigan";

            var geo = new Geo(expectedLatitude, expectedLongitude, expectedAltitude, expectedName);
            string json = JsonSerializer.Serialize(geo, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);
            // No type as it's not serialized as the base record.
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
            Assert.Equal(expectedLatitude, jsonNode["latitude"]!.GetValue<string>());
            Assert.Equal(expectedLongitude, jsonNode["longitude"]!.GetValue<string>());
            Assert.Equal(expectedAltitude, jsonNode["altitude"]!.GetValue<string>());
        }

        [Fact]
        public void GeoDeserializesCorrectlyAsLocationBase()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.geo",
                    "latitude":"53.551667",
                    "longitude":"-2.683056",
                    "altitude":"0",
                    "name":"H. J. Heinz, Wigan"}
                """;

            LocationBase? location = JsonSerializer.Deserialize<LocationBase>(json, _lexiconSerializationOptions);
            Assert.NotNull(location);
            Assert.IsType<Geo>(location);
            var geo = (Geo)location!;
            Assert.Equal("H. J. Heinz, Wigan", geo.Name);
            Assert.Equal("53.551667", geo.Latitude);
            Assert.Equal("-2.683056", geo.Longitude);
            Assert.Equal("0", geo.Altitude);
        }

        [Fact]
        public void GeoDeserializesCorrectlyAsGeo()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.geo",
                    "latitude":"53.551667",
                    "longitude":"-2.683056",
                    "altitude":"0",
                    "name":"H. J. Heinz, Wigan"}
                """;

            Geo? geo = JsonSerializer.Deserialize<Geo>(json, _lexiconSerializationOptions);
            Assert.NotNull(geo);
            Assert.Equal("H. J. Heinz, Wigan", geo.Name);
            Assert.Equal("53.551667", geo.Latitude);
            Assert.Equal("-2.683056", geo.Longitude);
            Assert.Equal("0", geo.Altitude);
        }

        [Fact]
        public void GeoDeserializationFailsWithMissingLatitude()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.geo",
                    "longitude":"-2.683056",
                    "altitude":"0",
                    "name":"H. J. Heinz, Wigan"}
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Geo>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void GeoDeserializationFailsWithMissingLongitude()
        {
            string json = """
                {
                    "$type":"community.lexicon.location.geo",
                    "longitude":"-2.683056",
                    "altitude":"0",
                    "name":"H. J. Heinz, Wigan"}
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Geo>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void HThreeCanBeConstructedWithOnlyRequiredProperties()
        {
            _ = new HThree("///rapport.spoiler.truffles");
        }

        [Fact]
        public void HThreeSerializesCorrectlyAsLocationBase()
        {
            string expectedValue = "///rapport.spoiler.truffles";
            string expectedName = "Heinz Factory, Wigan";
            var hThree = new HThree(expectedValue, expectedName);
            string json = JsonSerializer.Serialize<LocationBase>(hThree, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            Assert.Equal("community.lexicon.location.hthree", jsonNode["$type"]!.GetValue<string>());
            Assert.Equal(expectedValue, jsonNode["value"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
        }

        [Fact]
        public void HThreeSerializesCorrectlyAsHThree()
        {
            string expectedValue = "///rapport.spoiler.truffles";
            string expectedName = "Heinz Factory, Wigan";
            var hThree = new HThree(expectedValue, expectedName);
            string json = JsonSerializer.Serialize(hThree, _lexiconSerializationOptions);

            JsonNode? jsonNode = JsonNode.Parse(json);
            Assert.NotNull(jsonNode);

            // No type as it's not serialized as the base record.
            Assert.Equal(expectedValue, jsonNode["value"]!.GetValue<string>());
            Assert.Equal(expectedName, jsonNode["name"]!.GetValue<string>());
        }

        [Fact]
        public void HThreeDeserializesCorrectlyAsLocationBase()
        {
            string json = """
                {
                  "$type" : "community.lexicon.location.hthree",
                  "value" : "///rapport.spoiler.truffles",
                  "name" : "Heinz Factory, Wigan"
                }
                """;

            LocationBase? location = JsonSerializer.Deserialize<LocationBase>(json, _lexiconSerializationOptions);
            Assert.NotNull(location);
            Assert.IsType<HThree>(location);
            var hThree = (HThree)location!;
            Assert.Equal("Heinz Factory, Wigan", hThree.Name);
            Assert.Equal("///rapport.spoiler.truffles", hThree.Value);
        }

        [Fact]
        public void HThreeDeserializesCorrectlyAsHThree()
        {
            string json = """
                {
                  "$type" : "community.lexicon.location.hthree",
                  "value" : "///rapport.spoiler.truffles",
                  "name" : "Heinz Factory, Wigan"
                }
                """;

            HThree? hThree = JsonSerializer.Deserialize<HThree>(json, _lexiconSerializationOptions);
            Assert.NotNull(hThree);
            Assert.Equal("Heinz Factory, Wigan", hThree.Name);
            Assert.Equal("///rapport.spoiler.truffles", hThree.Value);
        }

        [Fact]
        public void HThreeDeserializationFailsWithMissingValue()
        {
            string json = """
                {
                  "$type" : "community.lexicon.location.hthree",
                  "name" : "Heinz Factory, Wigan"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<HThree>(json, _lexiconSerializationOptions));
        }
    }
}
