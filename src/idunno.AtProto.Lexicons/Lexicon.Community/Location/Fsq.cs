// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Location
{
    /// <summary>
    /// A physical location contained in the Foursquare Open Source Places dataset.
    /// </summary>
    public sealed record Fsq : LocationBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="Fsq" />
        /// </summary>
        /// <param name="fsqPlaceId">The unique identifier of a Foursquare POI.</param>
        /// <param name="name">The name of the location.</param>
        /// <param name="latitude">The latitude of the location, given in decimal degrees north.</param>
        /// <param name="longitude">The latitude of the location, given in decimal degrees east.</param>
        [JsonConstructor]
        public Fsq(string fsqPlaceId, string? name = null, string? latitude = null, string? longitude = null)
            : base(name)
        {
            FsqPlaceId = fsqPlaceId;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Fsq" />
        /// </summary>
        /// <param name="fsqPlaceId">The unique identifier of a Foursquare POI.</param>
        /// <param name="name">The name of the location.</param>
        /// <param name="latitude">The latitude of the location, given in decimal degrees north.</param>
        /// <param name="longitude">The latitude of the location, given in decimal degrees east.</param>
        public Fsq(string fsqPlaceId, float latitude, float longitude, string? name = null)
            : base(name)
        {
            FsqPlaceId = fsqPlaceId;
            Latitude = latitude.ToString(CultureInfo.InvariantCulture);
            Longitude = longitude.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// The unique identifier of a Foursquare POI.
        /// </summary>
        [JsonPropertyName("fsq_place_id")]
        [JsonRequired]
        public string FsqPlaceId
        {
            get;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value);
                field = value;
            }
        }

        /// <summary>
        /// The latitude of the location, given in decimal degrees north. Avoid using minutes and seconds (i.e. DMS) format.
        /// </summary>
        /// <remarks><para>Stored as strings, due to the exclusion of floating-point numbers from the ATProtocol data model</para></remarks>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Latitude { get; set; }

        /// <summary>
        /// The longitude of the location, given in decimal degrees east. Avoid using minutes and seconds (i.e. DMS) format.
        /// </summary>
        /// <remarks><para>Stored as strings, due to the exclusion of floating-point numbers from the ATProtocol data model</para></remarks>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Longitude { get; set; }
    }
}
