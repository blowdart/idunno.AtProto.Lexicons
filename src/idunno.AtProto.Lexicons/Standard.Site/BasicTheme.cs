// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

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
        [SetsRequiredMembers]
        public BasicTheme(ThemeColor background, ThemeColor foreground, ThemeColor accent, ThemeColor accentForeground)
        {
            Background = background;
            Foreground = foreground;
            Accent = accent;
            AccentForeground = accentForeground;
        }

        /// <summary>
        /// Color used for content background.
        /// </summary>
        public required ThemeColor Background { get; set; }

        /// <summary>
        /// Color used for content foreground.
        /// </summary>
        public required ThemeColor Foreground { get; set; }

        /// <summary>
        /// Color used for links and button background.
        /// </summary>
        public required ThemeColor Accent { get; set; }

        /// <summary>
        /// Color used for button text.
        /// </summary>
        public required ThemeColor AccentForeground { get; set ; }
    }
}
