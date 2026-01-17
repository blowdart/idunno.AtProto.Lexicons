// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Standard.Site
{
    /// <summary>
    /// Platform-specific preferences for the publication, including discovery and visibility settings.
    /// </summary>
    /// <param name="ShowInDiscover">Flag indicating whether the publication should appear in discovery feeds.</param>
    public record Preferences(bool ShowInDiscover)
    {
        /// <summary>
        /// Gets the optional lexicon type identifier for the preferences record.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public virtual string? Type { get; } = null;
    }
}
