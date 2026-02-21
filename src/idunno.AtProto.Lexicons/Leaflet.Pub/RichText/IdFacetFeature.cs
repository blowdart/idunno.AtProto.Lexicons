// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

namespace idunno.AtProto.Lexicons.Leaflet.Pub.RichText
{
    /// <summary>
    /// Facet feature for an identifier. Used for linking to a segment.
    /// </summary>
    public record IdFacetFeature
    {
        /// <summary>
        /// Creates a new instance of <see cref="IdFacetFeature"/>.
        /// </summary>
        /// <param name="id">The identifier to link to.</param>
        public IdFacetFeature(string? id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string? Id { get; set; }
    }
}
