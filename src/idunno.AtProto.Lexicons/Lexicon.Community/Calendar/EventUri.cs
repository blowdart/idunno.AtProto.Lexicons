// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using idunno.AtProto.Lexicons.Lexicon.Community.Location;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// Represents an online event location.
    /// </summary>
    public sealed record EventUri : LocationBase
    {
        /// <summary>
        /// Creates a new instance of the <see cref="EventUri"/> class.
        /// </summary>
        public EventUri() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="EventUri"/> class.
        /// </summary>
        /// <param name="uri">The Uri for the event.</param>
        /// <param name="name">The name of the event.</param>
        [SetsRequiredMembers]
        public EventUri(Uri uri, string? name) : base(name)
        {
            Uri = uri;
        }

        /// <summary>
        /// Gets of sets the Uri for the event.
        /// </summary>
        public required Uri Uri { get; set; }
    }
}
