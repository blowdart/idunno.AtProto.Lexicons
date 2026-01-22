// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// The status of an event.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<EventStatus>))]
    public enum EventStatus
    {
        /// <summary>
        /// An event status indicating the event is created, but not finalized.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#planned")]
        Planned,

        /// <summary>
        /// An event status indicating the event is created, and scheduled.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#scheduled")]
        Scheduled,

        /// <summary>
        /// An event status indicating the event has been rescheduled.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#rescheduled")]
        Rescheduled,

        /// <summary>
        /// An event status indicating the event has been cancelled.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#cancelled")]
        Cancelled,

        /// <summary>
        /// An event status indicating the event has been postponed.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#planned")]
        Postponed

    }
}
