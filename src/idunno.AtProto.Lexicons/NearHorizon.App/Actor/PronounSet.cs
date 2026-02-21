// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace idunno.AtProto.Lexicons.NearHorizon.App.Actor
{
    /// <summary>
    /// A single pronoun set, stored as an open-ended array of forms exactly as the user entered them
    /// </summary>
    public record PronounSet
    {
        /// <summary>
        /// Creates a new instance of <see cref="PronounSet"/>.
        /// </summary>
        /// <param name="forms">Pronoun forms in the order the user provided them.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="forms"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="forms"/> has less than 1 or more than 12 items, or
        ///     when any form has less than 1 character, more than 64 characters, or more than 24 graphemes.
        /// </exception>
        [JsonConstructor]
        public PronounSet(ICollection<string> forms)
        {
            ArgumentNullException.ThrowIfNull(forms);
            ArgumentOutOfRangeException.ThrowIfLessThan(forms.Count, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(forms.Count, 12);

            foreach (string form in forms)
            {
                ArgumentException.ThrowIfNullOrEmpty(form);
                ArgumentOutOfRangeException.ThrowIfLessThan(form.Length, 1);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(form.Length, 64);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(form.GetGraphemeLength(), 24);
            }

            Forms = forms;
        }

        /// <summary>
        /// Pronoun forms in the order the user provided them.
        /// Conventionally forms[0] is subject and forms[1] is object, but clients MUST NOT assume a fixed number of forms.
        /// Display by joining with '/'.
        /// </summary>
        [JsonRequired]
        public ICollection<string> Forms { get; init; }
    }
}
