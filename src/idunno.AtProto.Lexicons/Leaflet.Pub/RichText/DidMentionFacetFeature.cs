// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Leaflet.Pub.RichText
{
    /// <summary>
    /// Facet feature for mentioning a did.
    /// </summary>
    public sealed record DidMentionFacetFeature : FacetFeature
    {
        /// <summary>
        /// Creates a new instance of <see cref="DidMentionFacetFeature"/>.
        /// </summary>
        /// <param name="did">The <see cref="Did"/> being mentioned</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="did"/> is null.</exception>
        public DidMentionFacetFeature(Did did)
        {
            ArgumentNullException.ThrowIfNull(did);
            Did = did;
        }

        /// <summary>
        /// The <see cref="Did"/> being mentioned.
        /// </summary>
        [JsonRequired]
        public Did Did { get; init; }
    }
}
