// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

using idunno.AtProto.Lexicons.Lexicon.Community.Location;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Calendar
{
    /// <summary>
    /// Represents an online event location.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This expands on the location lexicon in an event specific way. This is a good demonstration of how you could
    /// add a new derived type to a base type you don't control. If you look at the LocationBase source code, you
    /// will see that EventUri is not defined there as a polymorphic option. Instead, the LexiconJsonSerializerOptions
    /// class uses a Modifier on the DefaultJsonTypeInfoResolver to add EventUri as a derived type of LocationBase.
    ///
    /// This approach allows you to extend the existing lexicon types with your own types without modifying the original source code,
    /// for example, if you wanted to add a custom facet to the Bluesky post facets.
    /// </para>
    /// </remarks>
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
