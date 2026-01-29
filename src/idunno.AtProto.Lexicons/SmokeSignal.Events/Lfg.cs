// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using idunno.AtProto.Lexicons.Lexicon.Community.Location;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.SmokeSignal.Events
{
    /// <summary>
    /// A Looking For Group (LFG) record that broadcasts interest in finding activity partners within a geographic area.
    /// </summary>
    public record Lfg : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="Lfg"/>.
        /// </summary>
        /// <param name="location">The geographic location for activity partner matching.</param>
        /// <param name="tags">Interest tags for matching with events and other users. Must contain at least one tag. Tags must be &lt; 50 characters and &lt; 50 graphemes.</param>
        /// <param name="startsAt">When the LFG becomes active.</param>
        /// <param name="endsAt">When the LFG expires and is no longer visible.</param>
        /// <param name="active">Flag indicating whether the LFG is currently active and visible to others.</param>
        public Lfg(
            Geo location,
            IEnumerable<string> tags,
            DateTimeOffset startsAt,
            DateTimeOffset endsAt,
            bool active) : this(
                location: location,
                tags: tags,
                startsAt: startsAt,
                endsAt: endsAt,
                createdAt: DateTimeOffset.UtcNow,
                active : active)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="Lfg"/>.
        /// </summary>
        /// <param name="location">The geographic location for activity partner matching.</param>
        /// <param name="tags">Interest tags for matching with events and other users. Must contain at least one tag. Tags must be &lt; 50 characters and &lt; 50 graphemes.</param>
        /// <param name="startsAt">When the LFG becomes active.</param>
        /// <param name="endsAt">When the LFG expires and is no longer visible.</param>
        /// <param name="createdAt">When the record was created.</param>
        /// <param name="active">Flag indicating whether the LFG is currently active and visible to others.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="location"/> or <paramref name="tags"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="tags"/> contains less than 1 or more than 10 tags, or any tag is longer than 64 characters or 64 graphemes.</exception>
        [JsonConstructor]
        public Lfg(
            Geo location,
            IEnumerable<string> tags,
            DateTimeOffset startsAt,
            DateTimeOffset endsAt,
            DateTimeOffset createdAt,
            bool active)
        {
            ArgumentNullException.ThrowIfNull(location);
            ArgumentNullException.ThrowIfNull(tags);

            // Materialize tags to a list to avoid multiple enumerations
            IList<string> tagsList = tags as IList<string> ?? [.. tags];
            int tagsCount = tagsList.Count;

            ArgumentOutOfRangeException.ThrowIfLessThan(tagsCount, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(tagsCount, 10);

            foreach (string tag in tagsList)
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(tag.Length, 64);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(tag.GetGraphemeLength(), 64);
            }

            Location = location;
            Tags = tagsList;
            StartsAt = startsAt;
            EndsAt = endsAt;
            CreatedAt = createdAt;
            Active = active;
        }

        /// <summary>
        /// Gets the lexicon type identifier for the record.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public string Type { get; init; } = "events.smokesignal.lfg";

        /// <summary>
        /// Gets or sets the geographic location for activity partner matching.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        [JsonRequired]
        public Geo Location
        {
            get;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                field = value;
            }
        }

        /// <summary>
        /// Gets or sets interest tags for matching with events and other users.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> contains less than 1 or more than 10 tags, or any tag is longer than 64 characters or 64 graphemes.</exception>
        [JsonRequired]
        public IEnumerable<string> Tags {
            get;

            set
            {
                ArgumentNullException.ThrowIfNull(value);

                // Materialize tags to a list to avoid multiple enumerations
                IList<string> tagsList = value as IList<string> ?? [.. value];
                int tagsCount = tagsList.Count;

                ArgumentOutOfRangeException.ThrowIfLessThan(tagsCount, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(tagsCount, 10);

                foreach (string tag in tagsList)
                {
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(tag.Length, 64);
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(tag.GetGraphemeLength(), 64);
                }

                field = value;
            }
        }

        /// <summary>
        /// Gets or sets when the LFG becomes active.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset StartsAt { get; set; }

        /// <summary>
        /// Gets or sets when the LFG expires and is no longer visible.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset EndsAt { get; set; }

        /// <summary>
        /// Gets or sets the record creation timestamp.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Gets or sets a flag indicating whether the LFG is currently active and visible to others.
        /// </summary>
        [JsonRequired]
        public bool Active { get; set; }
    }
}
