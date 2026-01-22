// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using idunno.AtProto.Lexicons.Lexicon.Community.Location;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// Encapsulates a calendar event.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "It's called Event in the lexicon, so matching.")]
    public record Event : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of<see cref="Event"/>.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="createdAt">Client-declared timestamp when the event was created.</param>
        /// <param name="description">A description for the event.</param>
        /// <param name="startsAt">Client-declared timestamp when the event starts.</param>
        /// <param name="endsAt">Client-declared timestamp when the event ends.</param>
        /// <param name="mode">The attendance mode of the event.</param>
        /// <param name="status">The status of the event.</param>
        /// <param name="locations">The locations where the event takes place.</param>
        /// <param name="uris">URIs associated with the event.</param>
        [JsonConstructor]
        public Event(
            string name,
            DateTimeOffset createdAt,
            string? description = null,
            DateTimeOffset? startsAt = null,
            DateTimeOffset? endsAt = null,
            EventMode? mode = null,
            EventStatus? status = null,
            IEnumerable<LocationBase>? locations = null,
            IEnumerable<EventUri>? uris = null)
        {
            Name = name;
            CreatedAt = createdAt;
            Description = description;
            StartsAt = startsAt;
            EndsAt = endsAt;
            Mode = mode;
            Status = status;
            Locations = locations;
            Uris = uris;
        }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        [JsonRequired]
        public string Name
        {
            get;
            set
            {
                ArgumentNullException.ThrowIfNull(value);
                field = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of the event.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets client declared date and time the event was created.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Gets or sets the client-declared timestamp when the event starts.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? StartsAt { get; set; }

        /// <summary>
        /// Gets or sets the client-declared timestamp when the event ends.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? EndsAt { get; set; }

        /// <summary>
        /// Gets or sets the attendance mode of the event
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EventMode? Mode { get; set; }

        /// <summary>
        /// Gets or sets the status of the event.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public EventStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the locations where the event takes place.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<LocationBase>? Locations { get; set; }

        /// <summary>
        /// Gets or sets URIs associated with the event.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<EventUri>? Uris { get; set; }
    }
}
