// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Location
{
    /// <summary>
    /// A physical location in the form of a street address.
    /// </summary>
    public sealed record Address : LocationBase
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Address"/> class.
        /// </summary>
        /// <param name="country">The ISO 3166 country code. Preferably the 2-letter code.</param>
        /// <param name="name">The name of the location.</param>
        /// <param name="postalCode">The postal code for the address.</param>
        /// <param name="region">The administrative region of the country. For example, a state in the USA.</param>
        /// <param name="locality">The locality of the region. For example, a city in the USA.</param>
        /// <param name="street">The street address.</param>
        [SetsRequiredMembers]
        public Address(
            string country,
            string? name = null,
            string? postalCode = null,
            string? region = null,
            string? locality = null,
            string? street = null)
            : base(name)
        {
            Country = country;
            PostalCode = postalCode;
            Region = region;
            Locality = locality;
            Street = street;
        }

        /// <summary>
        /// Gets or sets the ISO 3166 country code. Preferably the 2-letter code.
        /// </summary>
        [JsonRequired]
        public string Country
        {
            get;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value);
                ArgumentOutOfRangeException.ThrowIfLessThan(value.Length, 2);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, 10);
                field = value;
            }
        }

        /// <summary>
        /// Gets or sets the postal code for the address.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PostalCode { get; set; }

        /// <summary>
        /// The administrative region of the country. For example, a state in the USA.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Region { get; set; }

        /// <summary>
        /// The locality of the region. For example, a city in the USA.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Locality { get; set; }

        /// <summary>
        /// The street address.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Street { get; set; }
    }

}
