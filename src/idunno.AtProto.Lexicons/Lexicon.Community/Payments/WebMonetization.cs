// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Payments
{
    /// <summary>
    /// <see href="https://webmonetization.org/">Web Monetization</see> integration
    /// </summary>
    public record WebMonetization : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="WebMonetization"/>.
        /// </summary>
        /// <param name="address">The wallet address</param>
        /// <param name="description">A short, human-readable description of how this wallet is related to this account.</param>
        [JsonConstructor]
        public WebMonetization(Uri address, string? description = null)
        {
            ArgumentNullException.ThrowIfNull(address);
            Address = address;
            Description = description;
        }

        /// <summary>
        /// Gets and sets the lexicon type identifier for the record.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public string Type { get; init; } = "community.lexicon.payments.webMonetization";


        /// <summary>
        /// Gets or sets the wallet address.
        /// </summary>
        [JsonRequired]
        public Uri Address { get; set; }

        /// <summary>
        /// Gets or sets a short, human-readable description of how this wallet is related to this account.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }
    }
}
