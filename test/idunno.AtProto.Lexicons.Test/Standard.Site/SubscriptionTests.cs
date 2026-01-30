// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using idunno.AtProto.Lexicons.Standard.Site.Graph;

namespace idunno.AtProto.Lexicons.Test.Standard.Site
{
    public class SubscriptionTests
    {
        [Fact]
        public void SubscriptionCanBeConstructedWithValidParameters()
        {
            var expectedPublicationUri = new AtUri("at://did:plc:abc123/site.standard.publication/xyz789");

            var actual = new idunno.AtProto.Lexicons.Standard.Site.Graph.Subscription(expectedPublicationUri);

            Assert.NotNull(actual);
            Assert.Equal("site.standard.graph.subscription", actual.Type);
            Assert.Equal(expectedPublicationUri, actual.Publication);
        }

        [Fact]
        public void SubscriptionConstructorThrowsOnNullPublication()
        {
            Assert.Throws<ArgumentNullException>(() => new idunno.AtProto.Lexicons.Standard.Site.Graph.Subscription(null!));
        }

        [Fact]
        public void SubscriptionSerializesToExpectedJson()
        {
            var publicationUri = new AtUri("at://did:plc:abc123/site.standard.publication/xyz789");
            var subscription = new idunno.AtProto.Lexicons.Standard.Site.Graph.Subscription(publicationUri);
            string json = JsonSerializer.Serialize(subscription, LexiconJsonSerializerOptions.Default);
            string expectedJson = "{\"$type\":\"site.standard.graph.subscription\",\"publication\":\"at://did:plc:abc123/site.standard.publication/xyz789\"}";
            Assert.Equal(expectedJson, json);
        }

        [Fact]
        public void SubscriptionDeserializesFromExpectedJson()
        {
            string json = "{\"$type\":\"site.standard.graph.subscription\",\"publication\":\"at://did:plc:abc123/site.standard.publication/xyz789\"}";
            Subscription? subscription = JsonSerializer.Deserialize<Subscription>(json, LexiconJsonSerializerOptions.Default);
            Assert.NotNull(subscription);
            Assert.Equal("site.standard.graph.subscription", subscription!.Type);
            Assert.Equal(new AtUri("at://did:plc:abc123/site.standard.publication/xyz789"), subscription.Publication);
        }

        [Fact]
        public void SubscriptionDeserializationFailsOnMissingPublication()
        {
            string json = "{\"$type\":\"site.standard.graph.subscription\"}";
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Subscription>(json, LexiconJsonSerializerOptions.Default));
        }
    }
}
