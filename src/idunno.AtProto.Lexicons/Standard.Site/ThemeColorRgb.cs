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
        /// Creates a new instance of the <see cref="ThemeColorRgb"/> record.
        /// </summary>
        public ThemeColorRgb() :base()
        {
        }

        /// <summary>
        /// Create a new <see cref="ThemeColorRgb"/> instance.
        /// </summary>
        /// <param name="red">The red value for the color.</param>
        /// <param name="green">The green value for the color.</param>
        /// <param name="blue">The blue value for the color.</param>
        [SetsRequiredMembers]
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
        [SuppressMessage("Minor Code Smell", "S3236:Caller information arguments should not be provided explicitly", Justification = "Matches the parameter name to the property name for a clearer exception")]
        public required int Red
        {
            get;
            init
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Red));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 255, nameof(Red));

                field = value;
            }
        }

        /// <summary>
        /// The green value of the color.
        /// </summary>
        [JsonPropertyName("g")]
        [SuppressMessage("Minor Code Smell", "S3236:Caller information arguments should not be provided explicitly", Justification = "Matches the parameter name to the property name for a clearer exception")]
        public required int Green
        {
            get;
            init
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Green));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 255, nameof(Green));

                field = value;
            }
        }


        /// <summary>
        /// The blue value of the color.
        /// </summary>
        [JsonPropertyName("b")]
        [SuppressMessage("Minor Code Smell", "S3236:Caller information arguments should not be provided explicitly", Justification = "Matches the parameter name to the property name for a clearer exception")]
        public required int Blue
        {
            get;
            init
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value, nameof(Blue));
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 255, nameof(Blue));

                field = value;
            }
        }

    }
}
