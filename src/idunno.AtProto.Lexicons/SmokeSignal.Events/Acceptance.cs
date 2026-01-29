// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.SmokeSignal.Events
{
    /// <summary>
    /// A cryptographic proof record that contains RSVP acceptance data.
    /// </summary>
    public record Acceptance : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="Acceptance"/>.
        /// </summary>
        /// <param name="cid">The CID (Content Identifier) of the rsvp that this proof validates</param>
        [JsonConstructor]
        public Acceptance(Cid cid)
        {
            ArgumentNullException.ThrowIfNull(cid);
            Cid = cid;
        }

        /// <summary>
        /// Gets the lexicon type identifier for the record.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public string Type { get; init; } = "events.smokesignal.calendar.acceptance";

        /// <summary>
        /// Gets or sets the CID (Content Identifier) of the rsvp that this proof validates.
        /// </summary>
        [JsonRequired]
        public Cid Cid { get; init; }
    }
}
