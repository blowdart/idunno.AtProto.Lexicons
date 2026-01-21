// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// The status of an event.
    /// </summary>
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = false, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(EventStatus), typeDiscriminator: "community.lexicon.calendar.event")]
    [JsonDerivedType(typeof(Planned), typeDiscriminator: "community.lexicon.calendar.event#planned")]
    [JsonDerivedType(typeof(Scheduled), typeDiscriminator: "community.lexicon.calendar.event#scheduled")]
    [JsonDerivedType(typeof(Rescheduled), typeDiscriminator: "community.lexicon.calendar.event#rescheduled")]
    [JsonDerivedType(typeof(Cancelled), typeDiscriminator: "community.lexicon.calendar.event#cancelled")]
    [JsonDerivedType(typeof(Postponed), typeDiscriminator: "community.lexicon.calendar.event#postponed")]
    public record EventStatus
    {
    }

    /// <summary>
    /// An event status indicating the event is created, but not finalized.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record Planned : EventStatus
    {
    }

    /// <summary>
    /// An event status indicating the event is created, and scheduled.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record Scheduled : EventStatus
    {
    }

    /// <summary>
    /// An event status indicating the event has been rescheduled.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record Rescheduled : EventStatus
    {
    }

    /// <summary>
    /// An event status indicating the event has been cancelled.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record Cancelled: EventStatus
    {
    }

    /// <summary>
    /// An event status indicating the event has been postponed.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record Postponed : EventStatus
    {
    }
}
