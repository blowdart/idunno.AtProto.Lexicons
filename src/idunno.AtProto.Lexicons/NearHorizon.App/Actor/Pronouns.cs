// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.NearHorizon.App.Actor
{
    /// <summary>
    /// A user's pronoun preferences, stored as an ordered array of pronoun sets.
    /// </summary>
    [JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(Pronouns), "app.nearhorizon.actor.pronouns")]
    public record Pronouns : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="Pronouns"/> with the specified properties.
        /// </summary>
        /// <param name="sets">Ordered array of pronoun sets. First entry is primary. Maximum 10.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="sets"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="sets"/> is empty or has more than 10 entries.</exception>
        public Pronouns(ICollection<PronounSet> sets) : this(sets, displayMode: null, createdAt: DateTimeOffset.UtcNow, updatedAt: null)
        {
            ArgumentNullException.ThrowIfNull(sets);

            ArgumentOutOfRangeException.ThrowIfLessThan(sets.Count, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(sets.Count, 10);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pronouns"/> with the specified properties.
        /// </summary>
        /// <param name="sets">Ordered array of pronoun sets. First entry is primary. Maximum 10.</param>
        /// <param name="displayMode">An option preferred display mode for their pronouns. Known values are defined in <see cref="DisplayModeKnownValues"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="sets"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="sets"/> is empty or has more than 10 entries.</exception>
        public Pronouns(ICollection<PronounSet> sets, string? displayMode) : this(sets, displayMode, createdAt: DateTimeOffset.UtcNow, updatedAt: null)
        {
            ArgumentNullException.ThrowIfNull(sets);

            ArgumentOutOfRangeException.ThrowIfLessThan(sets.Count, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(sets.Count, 10);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pronouns"/> with the specified properties.
        /// </summary>
        /// <param name="sets">Ordered array of pronoun sets. First entry is primary. Maximum 10.</param>
        /// <param name="displayMode">An option preferred display mode for their pronouns. Known values are defined in <see cref="DisplayModeKnownValues"/>.</param>
        /// <param name="createdAt">The <see cref="DateTime"/> the record was created.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="sets"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="sets"/> is empty or has more than 10 entries.</exception>
        public Pronouns(ICollection<PronounSet> sets, string? displayMode, DateTimeOffset createdAt) : this(sets, displayMode, createdAt, updatedAt: null)
        {
            ArgumentNullException.ThrowIfNull(sets);

            ArgumentOutOfRangeException.ThrowIfLessThan(sets.Count, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(sets.Count, 10);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pronouns"/> with the specified properties.
        /// </summary>
        /// <param name="sets">Ordered array of pronoun sets. First entry is primary. Maximum 10.</param>
        /// <param name="displayMode">An option preferred display mode for their pronouns. Known values are defined in <see cref="DisplayModeKnownValues"/>.</param>
        /// <param name="createdAt">The <see cref="DateTime"/> the record was created.</param>
        /// <param name="updatedAt">An optional <see cref="DateTime"/> the record was last updated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="sets"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="sets"/> is empty or has more than 10 entries.</exception>
        [JsonConstructor]
        public Pronouns(ICollection<PronounSet> sets, string? displayMode, DateTimeOffset createdAt, DateTimeOffset? updatedAt)
        {
            ArgumentNullException.ThrowIfNull(sets);

            ArgumentOutOfRangeException.ThrowIfLessThan(sets.Count, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(sets.Count, 10);

            Sets = sets;
            DisplayMode = displayMode;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pronouns"/> with the specified properties.
        /// </summary>
        /// <param name="set">Ordered array of pronoun sets. First entry is primary. Maximum 10.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="set"/> is null.</exception>
        public Pronouns(PronounSet set) : this([set], displayMode: null, createdAt: DateTimeOffset.UtcNow, updatedAt: null)
        {
            ArgumentNullException.ThrowIfNull(set);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pronouns"/> with the specified properties.
        /// </summary>
        /// <param name="set">Ordered array of pronoun sets. First entry is primary. Maximum 10.</param>
        /// <param name="displayMode">An option preferred display mode for their pronouns. Known values are defined in <see cref="DisplayModeKnownValues"/>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="set"/> is null.</exception>
        public Pronouns(PronounSet set, string? displayMode) : this(
            [set], displayMode: displayMode, createdAt: DateTimeOffset.UtcNow, updatedAt: null)
        {
            ArgumentNullException.ThrowIfNull(set);
        }

        /// <summary>
        /// Creates a new instance of <see cref="Pronouns"/> with the specified properties.
        /// </summary>
        /// <param name="set">Ordered array of pronoun sets. First entry is primary. Maximum 10.</param>
        /// <param name="displayMode">An option preferred display mode for their pronouns. Known values are defined in <see cref="DisplayModeKnownValues"/>.</param>
        /// <param name="createdAt">The <see cref="DateTime"/> the record was created.</param>
        /// <param name="updatedAt">An optional <see cref="DateTime"/> the record was last updated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="set"/> is null.</exception>
        public Pronouns(PronounSet set, string? displayMode, DateTimeOffset createdAt, DateTimeOffset? updatedAt) : this(
            [set], displayMode: displayMode, createdAt: createdAt, updatedAt: updatedAt)
        {
            ArgumentNullException.ThrowIfNull(set);
        }

        /// <summary>
        /// Ordered array of pronoun sets. First entry is primary. Maximum 10.
        /// </summary>
        [JsonRequired]
        public ICollection<PronounSet> Sets { get; init; }

        /// <summary>
        /// Gets or sets the author's preferred display mode for their pronouns. If absent or unrecognized, clients MUST treat as 'all'.
        /// </summary>
        /// <remarks>
        /// <para>Known values are defined in <see cref="DisplayModeKnownValues"/>.</para>
        /// </remarks>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DisplayMode { get; set; }

        /// <summary>
        /// Gets the date and time the record was created.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// Gets the date and time the record was last updated.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Known values for the <see cref="Pronouns.DisplayMode"/> property.
    /// </summary>
    public static class DisplayModeKnownValues
    {
        /// <summary>
        /// Indicates all pronoun sets should be displayed.
        /// </summary>
        public const string All = "all";

        /// <summary>
        /// Indicates only the first pronoun set should be displayed.
        /// </summary>
        public const string FirstOnly = "firstOnly";
    }
}
