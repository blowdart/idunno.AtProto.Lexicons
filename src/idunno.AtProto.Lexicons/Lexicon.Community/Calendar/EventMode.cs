// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// The attendance mode of the event.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter<EventMode>))]
    public enum EventMode
    {
        /// <summary>
        /// An in-person event that takes place offline.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#inperson")]
        InPerson,

        /// <summary>
        /// A virtual event that takes place online.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#virtual")]
        Virtual,

        /// <summary>
        /// A hybrid event that takes place both online and offline.
        /// </summary>
        [JsonStringEnumMemberName("community.lexicon.calendar.event#hybrid")]
        Hybrid
    }
}
