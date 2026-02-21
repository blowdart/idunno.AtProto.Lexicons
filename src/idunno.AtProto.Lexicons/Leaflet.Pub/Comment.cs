// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Leaflet.Pub
{
    /// <summary>
    /// Represents comments on a document.
    /// </summary>
    [JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(Comment), "pub.leaflet.comment")]
    public record Comment : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="Comment"/>.
        /// </summary>
        /// <param name="subject">The actor that created the comment.</param>
        /// <param name="createdAt">The date and time when the comment was created.</param>
        /// <param name="plainText">The content of the comment as plain text.</param>
        public Comment(AtUri subject, DateTimeOffset createdAt, string plainText)
        {
            ArgumentNullException.ThrowIfNull(subject);
            ArgumentException.ThrowIfNullOrEmpty(plainText);
            Subject = subject;
            CreatedAt = createdAt;
            PlainText = plainText;
        }

        /// <summary>
        /// Creates a new instance of <see cref="Comment"/>.
        /// </summary>
        /// <param name="subject">The actor that created the comment.</param>
        /// <param name="createdAt">The date and time when the comment was created.</param>
        /// <param name="plainText">The content of the comment as plain text.</param>
        /// <param name="replyRef">The reference to the parent comment if this comment is a reply.</param>
        /// <param name="facets">The collection of facets that provide additional metadata or categorization for plain text content in the comment.</param>
        /// <param name="onPage">The page the comment is on, if any.</param>
        public Comment(
            AtUri subject,
            DateTimeOffset createdAt,
            string plainText,
            ReplyRef? replyRef,
            ICollection<RichText.Facet>? facets,
            string? onPage)
        {
            ArgumentNullException.ThrowIfNull(subject);
            ArgumentException.ThrowIfNullOrEmpty(plainText);
            Subject = subject;
            CreatedAt = createdAt;
            PlainText = plainText;
            Reply = replyRef;
            Facets = facets;
            OnPage = onPage;
        }

        /// <summary>
        /// Gets the actor that created the comment.
        /// </summary>
        [JsonRequired]
        public AtUri Subject { get; init; }

        /// <summary>
        /// Gets the date and time when the comment was created.
        /// </summary>
        [JsonRequired]
        public DateTimeOffset CreatedAt { get; init; }
        
        /// <summary>
        /// Gets the reference to the parent comment if this comment is a reply.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ReplyRef? Reply { get; set; }

        /// <summary>
        /// The content of the comment as plain text. The content may include markup that is described in the <see cref="Facets"/> property.
        /// </summary>
        [JsonRequired]
        [JsonPropertyName("plaintext")]
        public string PlainText
        {
            get;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value);

                field = value;
            }
        }

        /// <summary>
        /// Gets the collection of facets that provide additional metadata or categorization for plain text content in the comment.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<RichText.Facet>? Facets { get; init; }

        /// <summary>
        /// Gets the page the comment is on, if any.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OnPage { get; set; }

        // TODO: Linear document quote
    }

    /// <summary>
    /// Represents a reference to a reply, containing the URI of the parent entity.
    /// </summary>
    public record ReplyRef
    {
        /// <summary>
        /// Creates a new instance of <see cref="ReplyRef"/> with the specified parent URI.
        /// </summary>
        /// <param name="parent"></param>
        public ReplyRef(AtUri parent)
        {
            ArgumentNullException.ThrowIfNull(parent);
            Parent = parent;
        }

        /// <summary>
        /// Gets the parent <see cref="AtUri"/> that the comment is replying to.
        /// </summary>
        [JsonRequired]
        public AtUri Parent { get; init; }
    }
}
