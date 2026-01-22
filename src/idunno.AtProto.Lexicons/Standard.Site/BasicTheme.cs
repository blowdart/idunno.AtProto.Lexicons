// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Standard.Site
{
    /// <summary>
    /// Simplified publication theme for tools and apps to utilize when displaying content.
    /// </summary>
    public record BasicTheme
    {
        /// <summary>
        /// Constructs a new instance of <see cref="BasicTheme"/>.
        /// </summary>
        /// <param name="background">Color used for content background.</param>
        /// <param name="foreground">Color used for content foreground.</param>
        /// <param name="accent">Color used for links and button background.</param>
        /// <param name="accentForeground">Color used for button text.</param>
        [JsonConstructor]
        public BasicTheme(ThemeColor background, ThemeColor foreground, ThemeColor accent, ThemeColor accentForeground)
        {
            ArgumentNullException.ThrowIfNull(background);
            ArgumentNullException.ThrowIfNull(foreground);
            ArgumentNullException.ThrowIfNull(accent);
            ArgumentNullException.ThrowIfNull(accentForeground);

            Background = background;
            Foreground = foreground;
            Accent = accent;
            AccentForeground = accentForeground;
        }

        /// <summary>
        /// Color used for content background.
        /// </summary>
        [JsonRequired]
        public ThemeColor Background { get; set; }

        /// <summary>
        /// Color used for content foreground.
        /// </summary>
        [JsonRequired]
        public ThemeColor Foreground { get; set; }

        /// <summary>
        /// Color used for links and button background.
        /// </summary>
        [JsonRequired]
        public ThemeColor Accent { get; set; }

        /// <summary>
        /// Color used for button text.
        /// </summary>
        [JsonRequired]
        public ThemeColor AccentForeground { get; set ; }
    }
}
