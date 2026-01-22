// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Statusphere.Xyz
{
    /// <summary>
    /// Represents a status record in the statusphere.xyz lexicon.
    /// </summary>
    public sealed record StatusphereStatus : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of the <see cref="StatusphereStatus"/> class.
        /// </summary>
        /// <param name="status">The status to create. Must be a minimum of 1 character, less that 32 characters and less than 1 grapheme.</param>
        public StatusphereStatus([DisallowNull] string status) : this(status, DateTimeOffset.UtcNow)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="StatusphereStatus"/> class.
        /// </summary>
        /// <param name="status">The status to create. Must be a minimum of 1 character, less that 32 characters and less than 1 grapheme.</param>
        /// <param name="createdAt">The time the status was created.</param>
        [JsonConstructor]
        public StatusphereStatus(string status, DateTimeOffset createdAt)
        {
            ArgumentException.ThrowIfNullOrEmpty(status);
            ArgumentOutOfRangeException.ThrowIfLessThan(status.Length, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(status.Length, 32);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(status.GetGraphemeLength(), 1);

            Status = status;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Gets the lexicon type identifier for the record.
        /// </summary>
        /// <remarks><para>Needs to have an init to keep SerializationContext happy.</para></remarks>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public string Type { get; init; } = "xyz.statusphere.status";

        /// <summary>
        /// Gets the status message.
        /// </summary>
        [JsonRequired]
        public string Status
        {
            get;

            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value);
                ArgumentOutOfRangeException.ThrowIfLessThan(value.Length, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, 32);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.GetGraphemeLength(), 1);

                field = value;
            }
        }

        /// <summary>
        /// Gets the date and time the status was created.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset CreatedAt { get; init; }
    }
}
