// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// An RSVP for an <see cref="AtProto.Lexicons.Lexicon.Community.Calendar.Event"/>
    /// </summary>
    public record Rsvp : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="Rsvp"/>
        /// </summary>
        /// <param name="subject">The subject of the RSVP.</param>
        /// <param name="status">The status of the RSVP.</param>
        public Rsvp(StrongReference subject, RsvpStatus status)
        {
            Subject = subject;
            Status = status;
        }

        /// <summary>
        /// Gets the subject of the Rsvp. This property is required.
        /// </summary>
        [JsonRequired]
        public StrongReference Subject
            {
            get;

            init
            {
                ArgumentNullException.ThrowIfNull(value);

                field = value;
            }
        }

        /// <summary>
        /// The status of the RSVP. This property is required.
        /// </summary>
        [JsonRequired]
        public RsvpStatus Status { get; set; }
    }
}
