// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Leaflet.Pub.RichText
{
    /// <summary>
    /// Facet feature for mentioning an AT URI.
    /// </summary>
    public sealed record AtMentionFacetFeature : FacetFeature
    {
        /// <summary>
        /// Creates a new instance of <see cref="AtMentionFacetFeature"/>.
        /// </summary>
        /// <param name="uri">The <see cref="AtUri"/> being mentioned.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="uri"/> is null.</exception>"
        public AtMentionFacetFeature(AtUri uri)
        {
            ArgumentNullException.ThrowIfNull(uri);
            Uri = uri;
        }

        /// <summary>
        /// The <see cref="AtUri"/> being mentioned.
        /// </summary>
        [JsonRequired]
        public AtUri Uri { get; init; }
    }
}
