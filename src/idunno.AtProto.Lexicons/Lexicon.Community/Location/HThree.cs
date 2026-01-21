// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Location
{
    /// <summary>
    /// A physical location in the form of a H3 encoded location
    /// </summary>
    public sealed record HThree : LocationBase
    {
        /// <summary>
        /// Creates a new instance of <see cref="HThree"/>.
        /// </summary>
        public HThree() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="HThree"/>
        /// </summary>
        /// <param name="value">The H3 encoded location.</param>
        /// <param name="name">The name of the location.</param>
        [SetsRequiredMembers]
        public HThree(string value, string? name = null) : base(name)
        {
            Value = value;
        }

        /// <summary>
        /// Gets or sets the H3 encoded location.
        /// </summary>
        public required string Value
        {
            get;
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                field = value;
            }
        }
    }
}
