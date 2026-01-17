// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Standard.Site
{
    /// <summary>
    /// Color information for themes.
    /// </summary>
    /// <remarks>
    ///<para>Only <see cref="ThemeColorRgb"/> is supported at this moment. This class is used for future expansion.</para>
    /// </remarks>
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = true, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(ThemeColorRgb), typeDiscriminator: "site.standard.theme.color#rgb")]
    [JsonDerivedType(typeof(ThemeColorRgba), typeDiscriminator: "site.standard.theme.color#rgba")]
    public record ThemeColor()
    {
        /// <summary>
        /// A list of keys and element data that do not map to any strongly typed properties.
        /// </summary>
        [NotNull]
        [ExcludeFromCodeCoverage]
        [JsonExtensionData]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Needs to be settable for json deserialization")]
        public IDictionary<string, JsonElement>? ExtensionData { get; set; } = new Dictionary<string, JsonElement>();
    }
}
