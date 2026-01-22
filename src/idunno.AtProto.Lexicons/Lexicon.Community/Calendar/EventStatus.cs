// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// The status of an <see cref="Event"/>.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<EventStatus>))]
    public enum EventStatus
    {
        /// <summary>
        /// The event is created, but not finalized.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#planned")]
        Planned,

        /// <summary>
        /// The event is created, and scheduled.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#scheduled")]
        Scheduled,

        /// <summary>
        /// The event has been rescheduled.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#rescheduled")]
        Rescheduled,

        /// <summary>
        /// The event has been cancelled.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#cancelled")]
        Cancelled,

        /// <summary>
        /// The event has been postponed.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#planned")]
        Postponed

    }
}
