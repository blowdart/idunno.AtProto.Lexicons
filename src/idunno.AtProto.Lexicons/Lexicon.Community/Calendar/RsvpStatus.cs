// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// The status of an <see cref="Rsvp"/>.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<RsvpStatus>))]
    public enum RsvpStatus
    {
        /// <summary>
        /// Interested in the event
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.rsvp#interested")]
        Interested,

        /// <summary>
        /// Going to the event
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.rsvp#going")]
        Going,

        /// <summary>
        /// Not going to the event
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.rsvp#notgoing")]
        NotGoing
    }
}
