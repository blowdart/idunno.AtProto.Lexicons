// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Standard.Site.Graph
{
    /// <summary>
    /// Record declaring a subscription to a publication.
    /// </summary>
    public record Subscription : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Subscription"/> class.
        /// </summary>
        /// <param name="publication">An <see cref="AtUri"/> reference to the publication record being subscribed to (ex: at://did:plc:abc123/site.standard.publication/xyz789)</param>
        public Subscription(AtUri publication)
        {
            ArgumentNullException.ThrowIfNull(publication);
            Publication = publication;
        }

        /// <summary>
        /// Gets the lexicon type identifier for the record.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public string Type { get; init; } = "site.standard.graph.subscription";

        /// <summary>
        /// Gets the reference to the publication being subscribed to.
        /// </summary>
        [JsonRequired]
        public AtUri Publication { get; init; }
    }
}
