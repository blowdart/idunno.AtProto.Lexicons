// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Leaflet.Pub.RichText
{
    /// <summary>
    /// The detailed features of a <see cref="Facet"/>.
    /// </summary>
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
    [JsonDerivedType(typeof(LinkFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#link")]
    [JsonDerivedType(typeof(DidMentionFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#didMention")]
    [JsonDerivedType(typeof(AtMentionFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#atMention")]
    [JsonDerivedType(typeof(CodeFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#code")]
    [JsonDerivedType(typeof(HighlightFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#highlight")]
    [JsonDerivedType(typeof(UnderlineFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#underline")]
    [JsonDerivedType(typeof(StrikethroughFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#strikethrough")]
    [JsonDerivedType(typeof(IdFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#id")]
    [JsonDerivedType(typeof(BoldFacetFeature), typeDiscriminator: "leaflet.pub.richtext.facet#bold")]
    public record FacetFeature
    {
        /// <summary>
        /// A list of keys and element data that do not map to any strongly typed properties.
        /// </summary>
        [JsonExtensionData]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Needs to be writable for json deserialization")]
        public IDictionary<string, JsonElement>? ExtensionData { get; set; } = new Dictionary<string, JsonElement>();
    }
}
