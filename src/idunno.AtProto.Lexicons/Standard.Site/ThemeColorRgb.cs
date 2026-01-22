// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Standard.Site
{
    /// <summary>
    /// An RGB color value.
    /// </summary>
    public record ThemeColorRgb : ThemeColor
    {
        /// <summary>
        /// Create a new <see cref="ThemeColorRgb"/> instance.
        /// </summary>
        /// <param name="red">The red value for the color.</param>
        /// <param name="green">The green value for the color.</param>
        /// <param name="blue">The blue value for the color.</param>
        public ThemeColorRgb(int red, int green, int blue)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(red);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(red, 255);
            ArgumentOutOfRangeException.ThrowIfNegative(green);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(green, 255);
            ArgumentOutOfRangeException.ThrowIfNegative(blue);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(blue, 255);

            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>
        /// The red value of the color.
        /// </summary>
        [JsonPropertyName("r")]
        [JsonRequired]
        public int Red
        {
            get;
            set
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 255);

                field = value;
            }
        }

        /// <summary>
        /// The green value of the color.
        /// </summary>
        [JsonPropertyName("g")]
        [JsonRequired]
        public int Green
        {
            get;
            init
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 255);

                field = value;
            }
        }


        /// <summary>
        /// The blue value of the color.
        /// </summary>
        [JsonPropertyName("b")]
        [JsonRequired]
        public int Blue
        {
            get;
            init
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 255);

                field = value;
            }
        }

    }
}
