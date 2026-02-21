// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Leaflet.Pub.RichText
{
    /// <summary>
    /// Facet feature for a URL. The text URL may have been simplified or truncated, but the facet reference should be a complete URL
    /// </summary>
    public sealed record LinkFacetFeature : FacetFeature
    {
        /// <summary>
        /// Creates a new instance of <see cref="LinkFacetFeature"/>.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> the facet refers to.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="uri"/> is null.</exception>
        [JsonConstructor]
        public LinkFacetFeature(Uri uri)
        {
            ArgumentNullException.ThrowIfNull(uri);
            Uri = uri;
        }

        /// <summary>
        /// Creates a new instance of <see cref="LinkFacetFeature"/>.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> the facet refers to.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="uri"/> is null, empty or whitespace.</exception>
        public LinkFacetFeature(string uri): this(new Uri(uri))
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(uri);
        }

        /// <summary>
        /// Gets the URI the facet refers to.
        /// </summary>
        [JsonRequired]
        public Uri Uri { get; init; }
    }
}
