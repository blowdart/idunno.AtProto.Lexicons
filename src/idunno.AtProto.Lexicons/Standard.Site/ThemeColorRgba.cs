// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Standard.Site
{
    /// <summary>
    /// An RGBA color value.
    /// </summary>
    public record ThemeColorRgba : ThemeColorRgb
    {
        /// <summary>
        /// Create a new <see cref="ThemeColorRgb"/> instance.
        /// </summary>
        /// <param name="red">The red value for the color.</param>
        /// <param name="green">The green value for the color.</param>
        /// <param name="blue">The blue value for the color.</param>
        /// <param name="alpha">The alpha value for the color.</param>
        public ThemeColorRgba(int red, int green, int blue, int alpha) : base(red, green, blue)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(alpha);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(alpha, 100);
            Alpha = alpha;
        }

        /// <summary>
        /// The alpha value of the color.
        /// </summary>
        [JsonPropertyName("a")]
        [JsonRequired]
        public int Alpha
        {
            get;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 100);
                field = value;
            }
        }
    }
}
