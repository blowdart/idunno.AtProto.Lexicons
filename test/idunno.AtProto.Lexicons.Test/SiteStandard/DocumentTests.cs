// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using idunno.AtProto.Lexicons.Standard.Site;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Test.SiteStandard
{
    [ExcludeFromCodeCoverage]
    public class DocumentTests
    {
        [Fact]
        public void DocumentConstructsCorrectlyWithOnlyRequiredParametersAndSiteIsHttps()
        {
            string site = "https://example.com";
            string title = "Test Document";
            var document = new Document(site, title);

            Assert.Equal(site, document.Site);
            Assert.Equal(title, document.Title);

            Assert.Null(document.Path);
            Assert.Null(document.Description);
            Assert.Null(document.CoverImage);
            Assert.Null(document.Content);
            Assert.Null(document.TextContent);
            Assert.Null(document.BSkyPostRef);
            Assert.Null(document.Tags);
            Assert.Null(document.TextContent);

            Assert.NotNull(document.PublishedAt);

            Assert.Null(document.UpdatedAt);
        }

        [Fact]
        public void DocumentConstructsCorrectlyWithOnlyRequiredParametersAndSiteIsAtUri()
        {
            string site = "at://did:plc:identifier/blue.idunno.collection";
            string title = "Test Document";
            var document = new Document(site, title);

            Assert.Equal(site, document.Site);
            Assert.Equal(title, document.Title);

            Assert.Null(document.Path);
            Assert.Null(document.Description);
            Assert.Null(document.CoverImage);
            Assert.Null(document.Content);
            Assert.Null(document.TextContent);
            Assert.Null(document.BSkyPostRef);
            Assert.Null(document.Tags);
            Assert.Null(document.TextContent);

            Assert.NotNull(document.PublishedAt);

            Assert.Null(document.UpdatedAt);
        }

        [Fact]
        public void DocumentConstructorFailsWhenSiteIsNotUriOrAtUri()
        {
            string site = "javascript://alert('popping calc')";
            string title = "Test Document";

            Assert.Throws<ArgumentException>(() => new Document(site, title));
        }

        [Fact]
        public void DocumentConstructorFailsWhenSiteHasTrailingSlash()
        {
            string site = "at://did:plc:identifier/blue.idunno.collection/";
            string title = "Test Document";

            Assert.Throws<ArgumentException>(() => new Document(site, title));
        }

        [Fact]
        public void DocumentConstructorFailsWhenSiteIsHttpUri()
        {
            string site = "http://example.com";
            string title = "Test Document";
            Assert.Throws<ArgumentException>(() => new Document(site, title));
        }

        [Fact]
        public void DocumentConstructorFailsWhenSiteIsInvalidAtUri()
        {
            string site = "at://did:plc:identifier/invalid";
            string title = "Test Document";
            Assert.Throws<ArgumentException>(() => new Document(site, title));
        }

        [Fact]
        public void DocumentConstructorFailsWhenTitleIsEmpty()
        {
            string site = "https://example.com";
            string title = string.Empty;
            Assert.Throws<ArgumentException>(() => new Document(site, title));
        }

        [Fact]
        public void DocumentConstructorFailsWhenTitleIsTooLong()
        {
            string site = "https://example.com";
            string title = new('a', 129);
            Assert.Throws<ArgumentOutOfRangeException>(() => new Document(site, title));
        }

        [Fact]
        public void DocumentConstructorFailesWhenDescriptionIsTooLong()
        {
            string site = "https://example.com";
            string title = "Test Document";
            string description = new('a', 301);
            Assert.Throws<ArgumentOutOfRangeException>(() => new Document(site, title, description: description));
        }

        [Fact]
        public void DocumentConstructorFailsWhenCoverImageIsTooLarge()
        {
            string site = "https://example.com";
            string title = "Test Document";
            var coverImage = new Blob(new BlobReference("blobLink"), "image/png", 1000000 + 1);

            Assert.Throws<ArgumentOutOfRangeException>(() => new Document(site, title, coverImage: coverImage));
        }

        [Fact]
        public void DocumentConstructorFailsWhenCoverImageIsNotAnImageMimeType()
        {
            string site = "https://example.com";
            string title = "Test Document";
            var coverImage = new Blob(new BlobReference("blobLink"), "application/pdf", 1000000);

            Assert.Throws<ArgumentException>(() => new Document(site, title, coverImage: coverImage));
        }

        [Fact]
        public void DocumentConstructorFailsWhenCoverImageHasEmptyMimeType()
        {
            string site = "https://example.com";
            string title = "Test Document";
            var coverImage = new Blob(new BlobReference("blobLink"), string.Empty, 1000000);

            Assert.Throws<ArgumentException>(() => new Document(site, title, coverImage: coverImage));
        }
    }
}
