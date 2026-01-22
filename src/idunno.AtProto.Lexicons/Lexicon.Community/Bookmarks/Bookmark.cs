// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Bookmarks
{
    // https://github.com/lexicon-community/lexicon/blob/main/community/lexicon/bookmarks/bookmark.json

    /// <summary>
    /// Record bookmarking a link to come back to later.
    /// </summary>
    public record Bookmark : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Bookmark"/> class.
        /// </summary>
        /// <param name="subject">The <see cref="Uri"/> to bookmark</param>
        /// <param name="tags">An optional set of tags for content the bookmark may be related to, for example 'news' or 'funny videos'</param>
        public Bookmark(Uri subject, IEnumerable<string>? tags = null) : this(subject, DateTimeOffset.UtcNow, tags)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Bookmark"/> class.
        /// </summary>
        /// <param name="subject">The <see cref="Uri"/> to bookmark</param>
        /// <param name="createdAt">The time the bookmark was created.</param>
        /// <param name="tags">An optional set of tags for content the bookmark may be related to, for example 'news' or 'funny videos'</param>
        [JsonConstructor]
        public Bookmark(Uri subject, DateTimeOffset createdAt, IEnumerable<string>? tags = null)
        {
            ArgumentNullException.ThrowIfNull(subject);

            Subject = subject;
            CreatedAt = createdAt;
            Tags = tags;
        }

        /// <summary>
        /// Gets the lexicon type identifier for the record.
        /// </summary>
        /// <remarks><para>Needs to have an init to keep SerializationContext happy.</para></remarks>
        [JsonInclude]
        [JsonPropertyName("$type")]
        public string Type { get; init; } = "community.lexicon.bookmarks.bookmark";

        /// <summary>
        /// Gets or sets the link for the bookmark.
        /// </summary>
        [JsonRequired]
        public Uri Subject
        {
            get;

            set
            {
                ArgumentNullException.ThrowIfNull(value);

                field = value;
            }
        }

        /// <summary>
        /// Gets an optional set of tags for content the bookmark may be related to, for example 'news' or 'funny videos'.
        /// </summary>
        public IEnumerable<string>? Tags { get;set; }

        /// <summary>
        /// Gets the date and time the bookmark was created.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset CreatedAt { get; init; }
    }
}
