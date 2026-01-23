// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using idunno.AtProto.Lexicons.Lexicon.Community.Payments;

namespace idunno.AtProto.Lexicons.Test.LexiconCommunity
{
    public class WebMonetizationTests
    {
        [Fact]
        public void WebMonetizationCanBeConstructed()
        {
            var address = new Uri("https://example.com/wallet");
            string description = "My wallet for web monetization";

            var webMonetization = new Lexicons.Lexicon.Community.Payments.WebMonetization(address, description);
            Assert.Equal(address, webMonetization.Address);
            Assert.Equal(description, webMonetization.Description);
            Assert.Equal("community.lexicon.payments.webMonetization", webMonetization.Type);
        }

        [Fact]
        public void WebMonetizationThrowsOnNullAddress()
        {
            Assert.Throws<ArgumentNullException>(() => new Lexicons.Lexicon.Community.Payments.WebMonetization(null!));
        }

        [Fact]
        public void WebMonetizationAllowsNullDescription()
        {
            var address = new Uri("https://example.com/wallet");
            var webMonetization = new Lexicons.Lexicon.Community.Payments.WebMonetization(address, null);
            Assert.Equal(address, webMonetization.Address);
            Assert.Null(webMonetization.Description);
        }

        [Fact]
        public void WebMonetizationAllowsEmptyDescription()
        {
            var address = new Uri("https://example.com/wallet");
            var webMonetization = new Lexicons.Lexicon.Community.Payments.WebMonetization(address, string.Empty);
            Assert.Equal(address, webMonetization.Address);
            Assert.Equal(string.Empty, webMonetization.Description);
        }

        [Fact]
        public void WebMonetizationSerializesCorrectly()
        {
            var address = new Uri("https://example.com/wallet");
            string description = "My wallet for web monetization";
            var webMonetization = new Lexicons.Lexicon.Community.Payments.WebMonetization(address, description);

            string expected = $"{{\"$type\":\"community.lexicon.payments.webMonetization\",\"address\":\"{address}\",\"description\":\"{description}\"}}";

            string actual = JsonSerializer.Serialize(webMonetization, LexiconJsonSerializerOptions.Default);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WebMonetizationSerializesCorrectlyWithoutDescription()
        {
            var address = new Uri("https://example.com/wallet");
            var webMonetization = new Lexicons.Lexicon.Community.Payments.WebMonetization(address);
            string expected = $"{{\"$type\":\"community.lexicon.payments.webMonetization\",\"address\":\"{address}\"}}";
            string actual = JsonSerializer.Serialize(webMonetization, LexiconJsonSerializerOptions.Default);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WebMonetizationDeserializesCorrectly()
        {
            var address = new Uri("https://example.com/wallet");
            string description = "My wallet for web monetization";
            string json = $"{{\"$type\":\"community.lexicon.payments.webMonetization\",\"address\":\"{address}\",\"description\":\"{description}\"}}";
            WebMonetization? webMonetization = JsonSerializer.Deserialize<Lexicons.Lexicon.Community.Payments.WebMonetization>(json, LexiconJsonSerializerOptions.Default);
            Assert.NotNull(webMonetization);
            Assert.Equal(address, webMonetization!.Address);
            Assert.Equal(description, webMonetization.Description);
        }

        [Fact]
        public void WebMonetizationDeserializationFailsWithoutAddress()
        {
            string description = "My wallet for web monetization";
            string json = $"{{\"$type\":\"community.lexicon.payments.webMonetization\",\"description\":\"{description}\"}}";

            Assert.Throws<System.Text.Json.JsonException>(() =>
            {
                JsonSerializer.Deserialize<Lexicons.Lexicon.Community.Payments.WebMonetization>(json, LexiconJsonSerializerOptions.Default);
            });
        }
    }
}
