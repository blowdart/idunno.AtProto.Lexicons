// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Location
{
    /// <summary>
    /// A physical location in the form of a WGS84 coordinate.
    /// </summary>
    public record Geo : LocationBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="Geo"/>
        /// </summary>
        /// <param name="latitude">The latitude of the location, given in decimal degrees north.</param>
        /// <param name="longitude">The longitude of the location, given in decimal degrees east.</param>
        /// <param name="altitude">The altitude of the location, given in meters.</param>
        /// <param name="name">The name of the location.</param>
        [JsonConstructor]
        public Geo(string latitude, string longitude, string? altitude = null, string? name = null) : base(name)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Geo"/>
        /// </summary>
        /// <param name="latitude">The latitude of the location, given in decimal degrees north.</param>
        /// <param name="longitude">The longitude of the location, given in decimal degrees east.</param>
        /// <param name="altitude">The altitude of the location, given in meters.</param>
        /// <param name="name">The name of the location.</param>
        public Geo(float latitude, float longitude, float? altitude = null, string? name = null) : base(name)
        {
            Latitude = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            Longitude = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            if (altitude.HasValue)
            {
                Altitude = altitude.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// The latitude of the location, given in decimal degrees north. Avoid using minutes and seconds (i.e. DMS) format.
        /// </summary>
        /// <remarks><para>Stored as strings, due to the exclusion of floating-point numbers from the ATProtocol data model</para></remarks>
        [JsonRequired]
        public string Latitude
        {
            get;

            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                field = value;
            }
        }

        /// <summary>
        /// The longitude of the location, given in decimal degrees east. Avoid using minutes and seconds (i.e. DMS) format.
        /// </summary>
        /// <remarks><para>Stored as strings, due to the exclusion of floating-point numbers from the ATProtocol data model</para></remarks>
        [JsonRequired]
        public string Longitude
        {
            get;

            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                field = value;
            }
        }

        /// <summary>
        /// Gets or sets the altitude of the location, in meters.
        /// </summary>
        /// <remarks><para>Stored as strings, due to the exclusion of floating-point numbers from the ATProtocol data model</para></remarks>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Altitude { get; set; }
    }
}
