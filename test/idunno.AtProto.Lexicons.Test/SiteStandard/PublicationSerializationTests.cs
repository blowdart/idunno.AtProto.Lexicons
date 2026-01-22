// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization;
using idunno.AtProto.Lexicons.Lexicon.Community.Location;
using idunno.AtProto.Lexicons.Standard.Site;

namespace idunno.AtProto.Lexicons.Test.SiteStandard
{
    public class PublicationSerializationTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void FullyPopulatedPublicationDeserializesCorrectly()
        {
            string json = """
                {
                    "icon": {
                        "$type": "blob",
                        "ref": {
                            "$link": "bafkreighxlygswtkinvkvcm67igorlys6ldigjftn2zzvy35d6fbnr6rbi"
                        },
                        "mimeType": "image/png",
                        "size": 123456
                    },
                    "name": "Name",
                    "url": "https://site.standard",
                    "description": "A description of the site.",
                    "basicTheme": {
                        "background": {"$type" : "site.standard.theme.color#rgb", "r": 255, "g": 255, "b": 255 },
                        "foreground": {"$type" : "site.standard.theme.color#rgb", "r": 0, "g": 0, "b": 0 },
                        "accent": {"$type" : "site.standard.theme.color#rgb", "r": 0, "g": 120, "b": 215 },
                        "accentForeground": {"$type" : "site.standard.theme.color#rgb", "r": 1, "g": 2, "b": 3 }
                    },
                    "preferences": {
                        "showComments": true,
                        "showInDiscover": true
                    }
                }
                """;

            Publication<CustomPreferences>? publication = JsonSerializer.Deserialize<Publication<CustomPreferences>>(json, JsonSerializerOptions.Web);

            Assert.NotNull(publication);
            Assert.Equal("Name", publication.Name);
            Assert.Equal(new Uri("https://site.standard"), publication.Url);
            Assert.Equal("A description of the site.", publication.Description);
            Assert.NotNull(publication.Icon);
            Assert.Equal("image/png", publication.Icon.MimeType);
            Assert.Equal(123456, publication.Icon.Size);
            Assert.NotNull(publication.Icon.Reference);
            Assert.Equal("bafkreighxlygswtkinvkvcm67igorlys6ldigjftn2zzvy35d6fbnr6rbi", publication.Icon.Reference.Link);
            Assert.NotNull(publication.Preferences);
            Assert.True(publication.Preferences.ShowInDiscover);
            Assert.True(publication.Preferences.ShowComments);

            Assert.NotNull(publication.BasicTheme);
            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Background);

            ThemeColorRgb background = (ThemeColorRgb)publication.BasicTheme.Background;
            Assert.Equal(255, background.Red);
            Assert.Equal(255, background.Green);
            Assert.Equal(255, background.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Foreground);
            ThemeColorRgb foreground = (ThemeColorRgb)publication.BasicTheme.Foreground;
            Assert.Equal(0, foreground.Red);
            Assert.Equal(0, foreground.Green);
            Assert.Equal(0, foreground.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Accent);
            ThemeColorRgb accent = (ThemeColorRgb)publication.BasicTheme.Accent;
            Assert.Equal(0, accent.Red);
            Assert.Equal(120, accent.Green);
            Assert.Equal(215, accent.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.AccentForeground);
            ThemeColorRgb accentForeground = (ThemeColorRgb)publication.BasicTheme.AccentForeground;
            Assert.Equal(1, accentForeground.Red);
            Assert.Equal(2, accentForeground.Green);
            Assert.Equal(3, accentForeground.Blue);
        }

        [Fact]
        public void MinimallyPopulatedPublicationDeserializesCorrectly()
        {
            string json = """
                {
                    "name": "Name",
                    "url": "https://site.standard"
                }
                """;

            Publication<CustomPreferences>? publication = JsonSerializer.Deserialize<Publication<CustomPreferences>>(json, JsonSerializerOptions.Web);

            Assert.NotNull(publication);
            Assert.Equal("Name", publication.Name);
            Assert.Equal(new Uri("https://site.standard"), publication.Url);
            Assert.Null(publication.Description);
            Assert.Null(publication.Icon);
            Assert.Null(publication.Preferences);
            Assert.Null(publication.BasicTheme);
        }

        [Fact]
        public void MinimallyPopulatedNoneGenericPublicationDeserializesCorrectly()
        {
            string json = """
                {
                    "name": "Name",
                    "url": "https://site.standard"
                }
                """;

            Publication? publication = JsonSerializer.Deserialize<Publication>(json, JsonSerializerOptions.Web);

            Assert.NotNull(publication);
            Assert.Equal("Name", publication.Name);
            Assert.Equal(new Uri("https://site.standard"), publication.Url);
            Assert.Null(publication.Description);
            Assert.Null(publication.Icon);
            Assert.Null(publication.Preferences);
            Assert.Null(publication.BasicTheme);
        }

        [Fact]
        public void FullyPopulatedNonGenericPublicationDeserializesCorrectly()
        {
            string json = """
                {
                    "icon": {
                        "$type": "blob",
                        "ref": {
                            "$link": "bafkreighxlygswtkinvkvcm67igorlys6ldigjftn2zzvy35d6fbnr6rbi"
                        },
                        "mimeType": "image/png",
                        "size": 123456
                    },
                    "name": "Name",
                    "url": "https://site.standard",
                    "description": "A description of the site.",
                    "basicTheme": {
                        "background": {"$type" : "site.standard.theme.color#rgb", "r": 255, "g": 255, "b": 255 },
                        "foreground": {"$type" : "site.standard.theme.color#rgb", "r": 0, "g": 0, "b": 0 },
                        "accent": {"$type" : "site.standard.theme.color#rgb", "r": 0, "g": 120, "b": 215 },
                        "accentForeground": {"$type" : "site.standard.theme.color#rgb", "r": 1, "g": 2, "b": 3 }
                    },
                    "preferences": {
                        "showComments": true,
                        "showInDiscover": true
                    }
                }
                """;

            Publication? publication = JsonSerializer.Deserialize<Publication>(json, JsonSerializerOptions.Web);

            Assert.NotNull(publication);
            Assert.Equal("Name", publication.Name);
            Assert.Equal(new Uri("https://site.standard"), publication.Url);
            Assert.Equal("A description of the site.", publication.Description);
            Assert.NotNull(publication.Icon);
            Assert.Equal("image/png", publication.Icon.MimeType);
            Assert.Equal(123456, publication.Icon.Size);
            Assert.NotNull(publication.Icon.Reference);
            Assert.Equal("bafkreighxlygswtkinvkvcm67igorlys6ldigjftn2zzvy35d6fbnr6rbi", publication.Icon.Reference.Link);
            Assert.NotNull(publication.Preferences);
            Assert.True(publication.Preferences.ShowInDiscover);

            Assert.NotNull(publication.BasicTheme);
            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Background);

            ThemeColorRgb background = (ThemeColorRgb)publication.BasicTheme.Background;
            Assert.Equal(255, background.Red);
            Assert.Equal(255, background.Green);
            Assert.Equal(255, background.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Foreground);
            ThemeColorRgb foreground = (ThemeColorRgb)publication.BasicTheme.Foreground;
            Assert.Equal(0, foreground.Red);
            Assert.Equal(0, foreground.Green);
            Assert.Equal(0, foreground.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Accent);
            ThemeColorRgb accent = (ThemeColorRgb)publication.BasicTheme.Accent;
            Assert.Equal(0, accent.Red);
            Assert.Equal(120, accent.Green);
            Assert.Equal(215, accent.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.AccentForeground);
            ThemeColorRgb accentForeground = (ThemeColorRgb)publication.BasicTheme.AccentForeground;
            Assert.Equal(1, accentForeground.Red);
            Assert.Equal(2, accentForeground.Green);
            Assert.Equal(3, accentForeground.Blue);
        }

        [Fact]
        public void MinimallyPopulatedNoneGenericPublicationDeserializesCorrectlyWithSerializationContext()
        {
            string json = """
                {
                    "name": "Name",
                    "url": "https://site.standard"
                }
                """;

            Publication? publication = JsonSerializer.Deserialize<Publication>(json, _lexiconSerializationOptions);

            Assert.NotNull(publication);
            Assert.Equal("Name", publication.Name);
            Assert.Equal(new Uri("https://site.standard"), publication.Url);
            Assert.Null(publication.Description);
            Assert.Null(publication.Icon);
            Assert.Null(publication.Preferences);
            Assert.Null(publication.BasicTheme);
        }

        [Fact]
        public void DeserializationFailsWithoutName()
        {
            string json = """
                {
                    "url": "https://site.standard"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Publication>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void DeserializationFailsWithoutUrl()
        {
            string json = """
                {
                    "name": "Name"
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Publication>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void DeserializationFailsWithPreferencesButNoShowInDiscover()
        {
            string json = """
                {
                    "name": "Name",
                    "url": "https://site.standard",
                    "preferences": {
                    }
                }
                """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Publication>(json, _lexiconSerializationOptions));
        }


        [Fact]
        public void FullyPopulatedNonGenericPublicationDeserializesCorrectlyWithSerializationContext()
        {
            string json = """
                {
                    "icon": {
                        "$type": "blob",
                        "ref": {
                            "$link": "bafkreighxlygswtkinvkvcm67igorlys6ldigjftn2zzvy35d6fbnr6rbi"
                        },
                        "mimeType": "image/png",
                        "size": 123456
                    },
                    "name": "Name",
                    "url": "https://site.standard",
                    "description": "A description of the site.",
                    "basicTheme": {
                        "background": {"$type" : "site.standard.theme.color#rgb", "r": 255, "g": 255, "b": 255 },
                        "foreground": {"$type" : "site.standard.theme.color#rgb", "r": 0, "g": 0, "b": 0 },
                        "accent": {"$type" : "site.standard.theme.color#rgb", "r": 0, "g": 120, "b": 215 },
                        "accentForeground": {"$type" : "site.standard.theme.color#rgb", "r": 1, "g": 2, "b": 3 }
                    },
                    "preferences": {
                        "showComments": true,
                        "showInDiscover": true
                    }
                }
                """;

            Publication? publication = JsonSerializer.Deserialize<Publication>(json, _lexiconSerializationOptions);

            Assert.NotNull(publication);
            Assert.Equal("Name", publication.Name);
            Assert.Equal(new Uri("https://site.standard"), publication.Url);
            Assert.Equal("A description of the site.", publication.Description);
            Assert.NotNull(publication.Icon);
            Assert.Equal("image/png", publication.Icon.MimeType);
            Assert.Equal(123456, publication.Icon.Size);
            Assert.NotNull(publication.Icon.Reference);
            Assert.Equal("bafkreighxlygswtkinvkvcm67igorlys6ldigjftn2zzvy35d6fbnr6rbi", publication.Icon.Reference.Link);
            Assert.NotNull(publication.Preferences);
            Assert.True(publication.Preferences.ShowInDiscover);

            Assert.NotNull(publication.BasicTheme);
            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Background);

            ThemeColorRgb background = (ThemeColorRgb)publication.BasicTheme.Background;
            Assert.Equal(255, background.Red);
            Assert.Equal(255, background.Green);
            Assert.Equal(255, background.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Foreground);
            ThemeColorRgb foreground = (ThemeColorRgb)publication.BasicTheme.Foreground;
            Assert.Equal(0, foreground.Red);
            Assert.Equal(0, foreground.Green);
            Assert.Equal(0, foreground.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.Accent);
            ThemeColorRgb accent = (ThemeColorRgb)publication.BasicTheme.Accent;
            Assert.Equal(0, accent.Red);
            Assert.Equal(120, accent.Green);
            Assert.Equal(215, accent.Blue);

            Assert.IsType<ThemeColorRgb>(publication.BasicTheme.AccentForeground);
            ThemeColorRgb accentForeground = (ThemeColorRgb)publication.BasicTheme.AccentForeground;
            Assert.Equal(1, accentForeground.Red);
            Assert.Equal(2, accentForeground.Green);
            Assert.Equal(3, accentForeground.Blue);
        }

        [Fact]
        public void MinimalPublicationSerializesCorrectly()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            var publication = new Publication(url, name);

            string expected = """
                {"$type":"site.standard.publication","url":"https://standard.site","name":"Test Publication"}
                """;

            string actual = JsonSerializer.Serialize(publication, JsonSerializerOptions.Web);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MinimalPublicationWithCustomPreferencesSerializesCorrectly()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            var publication = new Publication<CustomPreferences>(url, name);

            string expected = """
                {"$type":"site.standard.publication","url":"https://standard.site","name":"Test Publication"}
                """;

            string actual = JsonSerializer.Serialize(publication, JsonSerializerOptions.Web);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MinimalPublicationWithSerializationOptionsSerializesCorrectly()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            var publication = new Publication(url, name);

            string expected = """
                {"$type":"site.standard.publication","url":"https://standard.site","name":"Test Publication"}
                """;
           
            string actual = JsonSerializer.Serialize(publication, _lexiconSerializationOptions);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MinimalPublicationWithCustomPreferencesAndSerializationOptionsSerializesCorrectly()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            var publication = new Publication<CustomPreferences>(url, name);

            string expected = """
                {"$type":"site.standard.publication","url":"https://standard.site","name":"Test Publication"}
                """;

            var jsonSerializerOptions = new JsonSerializerOptions(_lexiconSerializationOptions);
            jsonSerializerOptions.InsertJsonInfoTypeResolver(SiteStandardTestSourceGenerationContext.Default);

            string actual = JsonSerializer.Serialize(publication, jsonSerializerOptions);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MinimalPublicationAndCustomPreferencesAndSerializationOptionsSerializesCorrectly()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            var publication = new Publication<CustomPreferences>(url, name, preferences: new CustomPreferences(true, true));

            string expected = """
                {"$type":"site.standard.publication","url":"https://standard.site","name":"Test Publication","preferences":{"showComments":true,"showInDiscover":true}}
                """;

            var jsonSerializerOptions = new JsonSerializerOptions(_lexiconSerializationOptions);
            jsonSerializerOptions.InsertJsonInfoTypeResolver(SiteStandardTestSourceGenerationContext.Default);

            string actual = JsonSerializer.Serialize(publication, jsonSerializerOptions);

            Assert.Equal(expected, actual);
        }
    }

    internal record CustomPreferences : Lexicons.Standard.Site.Preferences
    {
        [JsonConstructor]
        public CustomPreferences(bool showInDiscover, bool showComments) : base(showInDiscover)
        {
            ShowComments = showComments;
        }

        public bool ShowComments { get; init; }
    }
}
