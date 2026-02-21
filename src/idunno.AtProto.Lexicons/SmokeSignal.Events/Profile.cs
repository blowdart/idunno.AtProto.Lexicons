// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

using idunno.AtProto.Repo;
using idunno.Bluesky.RichText;

namespace idunno.AtProto.Lexicons.SmokeSignal.Events
{
    /// <summary>
    /// A user profile for SmokeSignal
    /// </summary>
    [SuppressMessage("Naming", "CA1724", Justification = "The System.Web Profile class is part of ASP.NET and has not been carried over to .NET")]
    [JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType)]
    [JsonDerivedType(typeof(Profile), "events.smokesignal.profile")]
    public record Profile : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of <see cref="Profile"/>.
        /// </summary>
        /// <param name="displayName">The display name of the identity.</param>
        /// <param name="profileHost">The format used for profile links. Known values are defined in <see cref="ProfileFormat"/>.</param>
        /// <param name="description">A free text description of the identity.</param>
        /// <param name="facets">A collection of annotations of text (mentions, URLs, hashtags, etc) in the description.</param>
        /// <param name="avatar">A small image to be displayed next to events. AKA, 'profile picture'</param>
        /// <param name="banner">A larger horizontal image to display behind profile view.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="displayName"/>, <paramref name="profileHost"/>, <paramref name="description"/>, <paramref name="avatar"/> or <paramref name="banner"/> is too big,
        /// or when <paramref name="avatar"/> or <paramref name="banner"/> is not a png or jpg.
        /// </exception>
        [JsonConstructor]
        public Profile(
            string? displayName = null,
            string? profileHost = null,
            string? description = null,
            IEnumerable<Facet>? facets = null,
            Blob? avatar = null,
            Blob? banner = null)
        {
            if (displayName is not null)
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(displayName.Length, 200);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(displayName.GetGraphemeLength(), 200);
            }

            if (profileHost is not null)
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(profileHost.Length, 50);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(profileHost.GetGraphemeLength(), 50);
            }

            if (description is not null)
            {
                ArgumentOutOfRangeException.ThrowIfGreaterThan(description.Length, 2000);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(description.GetGraphemeLength(), 2000);
            }

            if (avatar is not null)
            {
                if (avatar.MimeType != "image/png" && avatar.MimeType != "image/jpg")
                {
                    throw new ArgumentOutOfRangeException(nameof(avatar), "MimeType must be either image/png or image/jpg");
                }

                ArgumentOutOfRangeException.ThrowIfGreaterThan(avatar.Size, 3000000);
            }

            if (banner is not null)
            {
                if (banner.MimeType != "image/png" && banner.MimeType != "image/jpg")
                {
                    throw new ArgumentOutOfRangeException(nameof(banner), "MimeType must be either image/png or image/jpg");
                }

                ArgumentOutOfRangeException.ThrowIfGreaterThan(banner.Size, 3000000);
            }

            DisplayName = displayName;
            ProfileHost = profileHost;
            Description = description;
            Facets = facets;
            Avatar = avatar;
            Banner = banner;
        }

        /// <summary>
        /// Gets or sets the display name of the identity.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DisplayName
        {
            get;

            set
            {
                if (value is not null)
                {
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, 200);
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.GetGraphemeLength(), 200);
                }

                field = value;
            }
        }

        /// <summary>
        /// The format used for profile links. Known values are defined in <see cref="ProfileFormat"/>.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ProfileHost
        {
            get;

            set
            {
                if (value is not null)
                {
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, 50);
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.GetGraphemeLength(), 50);
                }

                field = value;
            }
        }

        /// <summary>
        /// A free text description of the identity.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description
        {
            get;

            set
            {
                if (value is not null)
                {
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, 2000);
                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.GetGraphemeLength(), 2000);
                }

                field = value;
            }
        }

        /// <summary>
        /// Annotations of text (mentions, URLs, hashtags, etc) in the description.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<Facet>? Facets { get; set; }

        /// <summary>
        /// Small image to be displayed next to events. AKA, 'profile picture'.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Blob? Avatar
        {
            get;

            set
            {
                if (value is not null)
                {
                    if (value.MimeType != "image/png" && value.MimeType != "image/jpg")
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), "MimeType must be either image/png or image/jpg");
                    }

                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Size, 3000000);
                }

                field = value;
            }
        }

        /// <summary>
        /// Larger horizontal image to display behind profile view.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Blob? Banner
        {
            get;

            set
            {
                if (value is not null)
                {
                    if (value.MimeType != "image/png" && value.MimeType != "image/jpg")
                    {
                        throw new ArgumentOutOfRangeException(nameof(value), "MimeType must be either image/png or image.jpg");
                    }

                    ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Size, 3000000);
                }

                field = value;
            }
        }
    }

    /// <summary>
    /// Well know values for profile formats
    /// </summary>
    public static class ProfileFormat
    {
        /// <summary>
        /// Profile links are in the Bluesky app format
        /// </summary>
        public const string BSkyApp = "bsky.app";

        /// <summary>
        /// Profile links are in the Blacksky community format.
        /// </summary>
        public const string BlackskyCommunity = "blacksky.community";

        /// <summary>
        /// Profile links are an ATUri format.
        /// </summary>
        [SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "This is how it is named in the lexicon.")]
        public const string AtUri = "aturi";
    }
}
