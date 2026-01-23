// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Interaction
{
    /// <summary>
    /// Encapsulates A 'like' interaction with another AT Protocol record.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "It is Like in the lexicon")]
    public record Like: AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="Like"/>
        /// </summary>
        /// <param name="subject">The subject of the Like.</param>
        public Like(StrongReference subject) : this(subject, DateTimeOffset.UtcNow)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="Like"/>
        /// </summary>
        /// <param name="subject">The subject of the Like.</param>
        /// <param name="createdAt">The <see cref="DateTimeOffset"/> the like was created.</param>
        [JsonConstructor]
        public Like(StrongReference subject, DateTimeOffset createdAt)
        {
            ArgumentNullException.ThrowIfNull(subject);

            Subject = subject;
            CreatedAt = createdAt;
        }

        /// <summary>
        /// Gets and sets the lexicon type identifier for the record.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public string Type { get; init; } = "community.lexicon.interaction.like";

        /// <summary>
        /// Gets the subject of the Rsvp. This property is required.
        /// </summary>
        [JsonRequired]
        public StrongReference Subject { get; init; }

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> when the like was created."/>
        /// </summary>
        [JsonRequired]
        public DateTimeOffset CreatedAt { get; init; }
    }
}
