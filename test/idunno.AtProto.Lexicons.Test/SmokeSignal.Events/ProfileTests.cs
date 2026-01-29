// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Serialization;
using idunno.AtProto.Lexicons.SmokeSignal.Events;
using idunno.AtProto.Repo;
using idunno.Bluesky.RichText;

namespace idunno.AtProto.Lexicons.Test.SmokeSignal.Events
{
    public class ProfileTests
    {
        [Fact]
        public void ProfileCanBeConstructedWithValidParameters()
        {
            string expectedDisplayName = "Test User";
            string expectedProfileHost = ProfileFormat.AtUri;
            string expectedDescription = "Test description";
            Facet[] expectedFacets = [
                new(new ByteSlice(0, 10),
                    [new LinkFacetFeature(new Uri("https://example.org"))]
                    )];
            Blob expectedAvatar = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/png", 1024);
            Blob expectedBanner = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/jpg", 10240);

            var profile = new Profile(
                displayName: expectedDisplayName,
                profileHost: expectedProfileHost,
                description: expectedDescription,
                facets: expectedFacets,
                avatar: expectedAvatar,
                banner: expectedBanner);

            Assert.NotNull(profile);
            Assert.Equal(expectedDisplayName, profile.DisplayName);
            Assert.Equal(expectedProfileHost, profile.ProfileHost);
            Assert.Equal(expectedDescription, profile.Description);
            Assert.Equal(expectedFacets, profile.Facets);
            Assert.Equal(expectedAvatar, profile.Avatar);
            Assert.Equal(expectedBanner, profile.Banner);
        }

        [Fact]
        public void ProfileCannotBeConstructedInvalidAvatarMimeType()
        {
            Blob invalidAvatar = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/gif", 512);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(avatar: invalidAvatar);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedInvalidAvatarSize()
        {
            Blob invalidAvatar = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/png", 3000001);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(avatar: invalidAvatar);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedInvalidBannerMimeType()
        {
            Blob invalidBanner = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/gif", 512);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(banner: invalidBanner);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedInvalidBannerSize()
        {
            Blob invalidBanner = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/png", 3000001);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(banner: invalidBanner);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedWithTooLongDisplayName()
        {
            string longDisplayName = new('a', 201);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(displayName: longDisplayName);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedWithTooLongInGraphemesDisplayName()
        {
            // 🤦🏼‍♂️ is a single grapheme but seven code units

            StringBuilder longDisplayNameBuilder = new();
            for (int i = 0; i < 30; i++)
            {
                longDisplayNameBuilder.Append("🤦🏼‍♂️");
            }
            string longDisplayName = longDisplayNameBuilder.ToString();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(displayName: longDisplayName);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedWithTooLongProfileHost()
        {
            string longProfileHost = new('a', 51);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(profileHost: longProfileHost);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedWithTooLongProfileHostInGraphemes()
        {
            StringBuilder longProfileHostBuilder = new();
            for (int i = 0; i < 20; i++)
            {
                longProfileHostBuilder.Append("🤦🏼‍♂️");
            }
            string longProfileHost = longProfileHostBuilder.ToString();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(profileHost: longProfileHost);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedWithTooLongDescription()
        {
            string longDescription = new('a', 2001);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(description: longDescription);
            });
        }

        [Fact]
        public void ProfileCannotBeConstructedWithTooLongDescriptionInGraphemes()
        {
            StringBuilder longDescriptionBuilder = new();
            for (int i = 0; i < 290; i++)
            {
                longDescriptionBuilder.Append("🤦🏼‍♂️");
            }
            string longDescription = longDescriptionBuilder.ToString();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var profile = new Profile(description: longDescription);
            });
        }


        [Fact]
        public void EmptyProfileIsSerializedCorrectly()
        {
            var profile = new Profile();

            string expected = """{"$type":"events.smokesignal.profile"}""";

            string actual = JsonSerializer.Serialize(profile, LexiconJsonSerializerOptions.Default);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProfileIsSerializedCorrectly()
        {
            string expectedDisplayName = "Test User";
            string expectedProfileHost = ProfileFormat.AtUri;
            string expectedDescription = "Test description";
            Facet[] expectedFacets = [
                new(new ByteSlice(0, 10),
                    [new LinkFacetFeature(new Uri("https://example.org"))]
                    )];
            Blob expectedAvatar = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/png", 1024);
            Blob expectedBanner = new(new BlobReference("bafkreidflmwopkeahue7pzozzdiwujrqghv62arubktnr57fmp75cp2nyq"), "image/jpg", 10240);

            var profile = new Profile(
                displayName: expectedDisplayName,
                profileHost: expectedProfileHost,
                description: expectedDescription,
                facets: expectedFacets,
                avatar: expectedAvatar,
                banner: expectedBanner);

            string json = JsonSerializer.Serialize(profile, LexiconJsonSerializerOptions.Default);

            JsonNode? actual = JsonNode.Parse(json);
            Assert.NotNull(actual);

            Assert.Equal("events.smokesignal.profile", actual["$type"]!.GetValue<string>());

            Assert.Equal(expectedDisplayName, actual["displayName"]!.GetValue<string>());
            Assert.Equal(expectedProfileHost, actual["profileHost"]!.GetValue<string>());
            Assert.Equal(expectedDescription, actual["description"]!.GetValue<string>());

            JsonArray? actualFacetsArray = actual["facets"]!.AsArray();
            Assert.Equal(actualFacetsArray.Count, expectedFacets.Length);
            List<Facet> actualFacets = [];
            foreach (JsonNode? facet in actualFacetsArray)
            {
                Facet actualFacet = JsonSerializer.Deserialize<Facet>(facet!.ToJsonString(), LexiconJsonSerializerOptions.Default)!;
                actualFacets.Add(actualFacet);
            }
            Assert.Equivalent(expectedFacets, actualFacets);

            Assert.Equal(expectedAvatar, JsonSerializer.Deserialize<Blob>(actual["avatar"]!.ToJsonString(), LexiconJsonSerializerOptions.Default));
            Assert.Equal(expectedBanner, JsonSerializer.Deserialize<Blob>(actual["banner"]!.ToJsonString(), LexiconJsonSerializerOptions.Default));
        }

        [Fact]
        public void ProfileIsDeserializedCorrectly()
        {
            string json = """
            {
                "$type": "events.smokesignal.profile",
                "displayName": "Test User",
                "profileHost": "at://testuser",
                "description": "Test description",
                "facets": [
                    {
                        "index": { "byteStart": 0, "byteEnd": 10 },
                        "features": [
                            {
                                "$type": "app.bsky.richtext.facet#link",
                                "uri": "https://example.org"
                            }
                        ]
                    }
                ],
                "avatar": {
                    "ref": { "$type": "blobRef", "$link": "bafkreia3ww67kqsgkxy6bfgu4dxxyp52b3e2ghqbpoj7qt4iuupfx6c45a" },
                    "mimeType": "image/png",
                    "size": 1024
                },
                "banner": {
                    "ref": { "$type": "blobRef", "$link": "bafkreia3ww67kqsgkxy6bfgu4dxxyp52b3e2ghqbpoj7qt4iuupfx6c45a" },
                    "mimeType": "image/jpg",
                    "size": 10240
                }
            }
            """;
            Profile? profile = JsonSerializer.Deserialize<Profile>(json, LexiconJsonSerializerOptions.Default);
            Assert.NotNull(profile);
            Assert.Equal("Test User", profile!.DisplayName);
            Assert.Equal("at://testuser", profile.ProfileHost);
            Assert.Equal("Test description", profile.Description);

            Assert.Single(profile.Facets!);
            Assert.Equal(new ByteSlice(0, 10), profile.Facets!.First().Index);
            Assert.IsType<LinkFacetFeature>(profile.Facets!.First().Features.First());

            var actualLinkFeature = (LinkFacetFeature)profile.Facets!.First().Features.First();
            Assert.Equal(new Uri("https://example.org"), actualLinkFeature.Uri);

            Assert.Equal(new Blob(new BlobReference("bafkreia3ww67kqsgkxy6bfgu4dxxyp52b3e2ghqbpoj7qt4iuupfx6c45a"), "image/png", 1024), profile.Avatar);
            Assert.Equal(new Blob(new BlobReference("bafkreia3ww67kqsgkxy6bfgu4dxxyp52b3e2ghqbpoj7qt4iuupfx6c45a"), "image/jpg", 10240), profile.Banner);
        }
    }
}
