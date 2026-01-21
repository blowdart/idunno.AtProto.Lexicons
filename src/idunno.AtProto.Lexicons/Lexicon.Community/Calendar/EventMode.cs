// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// The attendance mode of the event.
    /// </summary>
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = false, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(InPerson), typeDiscriminator: "community.lexicon.calendar.event#inperson")]
    [JsonDerivedType(typeof(Virtual), typeDiscriminator: "community.lexicon.calendar.event#virtual")]
    [JsonDerivedType(typeof(Hybrid), typeDiscriminator: "community.lexicon.calendar.event#hybrid")]
    public record EventMode
    {
    }

    /// <summary>
    /// An in-person event that takes place offline.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record InPerson : EventMode
    {
    }

    /// <summary>
    /// A virtual event that takes place online.
    /// </summary>
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record Virtual : EventMode
    {
    }

    /// <summary>
    /// A hybrid event that takes place both online and offline.
    /// </summary>
    [SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "It's called Event in the lexicon, so matching.")]
    [SuppressMessage("Minor Code Smell", "S2094:Classes should not be empty", Justification = "Lexical type token with no actual properties.")]
    public sealed record Hybrid : EventMode
    {
    }

}
