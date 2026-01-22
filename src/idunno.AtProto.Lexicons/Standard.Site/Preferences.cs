// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Standard.Site
{
    /// <summary>
    /// Platform-specific preferences for the publication, including discovery and visibility settings.
    /// </summary>
    public record Preferences
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Preferences"/> record.
        /// </summary>
        /// <param name="showInDiscover">Flag indicating whether the publication should appear in the discover feed.</param>
        [JsonConstructor]
        public Preferences(bool showInDiscover)
        {
            ShowInDiscover = showInDiscover;
        }

        /// <summary>
        /// Gets the optional lexicon type identifier for the preferences record.
        /// </summary>
        [JsonInclude]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("$type")]
        public virtual string? Type { get; } = null;

        /// <summary>
        /// Flag indicating whether the publication should appear in the discover feed.
        /// </summary>
        [JsonRequired]
        public bool ShowInDiscover { get; set; }
    }
}
