// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using idunno.AtProto.Lexicons.Standard.Site;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Test.SiteStandard
{
    [ExcludeFromCodeCoverage]
    public class PublicationTests
    {
        [Fact]
        public void PublicationConstructsCorrectlyWithOnlyRequiredParameters()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";

            var publication = new Publication<TestPreferences>(url, name);

            Assert.Equal(url, publication.Url);
            Assert.Equal(name, publication.Name);
            Assert.Null(publication.Icon);
            Assert.Null(publication.Description);
            Assert.Null(publication.BasicTheme);
            Assert.Null(publication.Preferences);
        }

        [Fact]
        public void PublicationConstructsCorrectly()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            Blob icon = new (new BlobReference("blobLink"), "image/png", 1024);
            string description = "This is a test publication.";
            BasicTheme basicTheme = new (new ThemeColorRgb(255, 255, 255), new ThemeColorRgb(0, 0, 0), new ThemeColorRgb(128, 128, 128), new ThemeColorRgb(192, 192, 192));
            TestPreferences preferences = new (true);

            var publication = new Publication<TestPreferences>(url, name, icon, description, basicTheme, preferences);

            Assert.Equal(url, publication.Url);
            Assert.Equal(name, publication.Name);
            Assert.Equal(icon, publication.Icon);
            Assert.Equal(description, publication.Description);
            Assert.Equal(basicTheme, publication.BasicTheme);

            Assert.NotNull(publication.Preferences);
            Assert.Equal(preferences, publication.Preferences);
            Assert.True(publication.Preferences.ShowInDiscover);
            Assert.Equal("test.idunno.atproto.lexicons.test.site.standard.preferences", publication.Preferences.Type);
        }

        [Fact]
        public void PublicationConstructorThrowsOnEmptyName()
        {
            var url = new Uri("https://standard.site");
            string name = string.Empty;
            Assert.Throws<ArgumentException>(() => new Publication<TestPreferences>(url, name));
        }

        [Fact]
        public void PublicationConstructorThrowsOnTooLongAName()
        {
            var url = new Uri("https://standard.site");
            string name = new('a', 129);
            Assert.Throws<ArgumentOutOfRangeException>(() => new Publication<TestPreferences>(url, name));
        }

        [Fact]
        public void PublicationConstructorThrowsOnInvalidIconMimeType()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            Blob invalidIcon = new (new BlobReference("blobLink"), "application/pdf", 1024);
            Assert.Throws<ArgumentException>(() => new Publication<TestPreferences>(url, name, invalidIcon));
        }

        [Fact]
        public void PublicationConstructorThrowsOnInvalidIconSize()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            Blob invalidIcon = new(new BlobReference("blobLink"), "image/png", 1000001);
            Assert.Throws<ArgumentOutOfRangeException>(() => new Publication<TestPreferences>(url, name, invalidIcon));
        }

        [Fact]
        public void PublicationConstructorThrowsOnTooLongADescription()
        {
            var url = new Uri("https://standard.site");
            string name = "Test Publication";
            string description = new('a', 301);
            Assert.Throws<ArgumentOutOfRangeException>(() => new Publication<TestPreferences>(url, name, description: description));
        }
    }

    [ExcludeFromCodeCoverage]
    internal record class TestPreferences(bool ShowInDiscover) : Preferences(ShowInDiscover)
    {
        public override string Type => "test.idunno.atproto.lexicons.test.site.standard.preferences";
    }
}
