// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace idunno.AtProto.Lexicons
{
    /// <summary>
    /// Extension methods for manipulating the resolver chain on <see cref="JsonSerializerOptions"/>.
    /// </summary>
    public static class JsonSerializerOptionsExtensions
    {
        /// <summary>
        /// Appends the type resolver from <paramref name="other"/>into the type resolver chain for this instance.
        /// </summary>
        /// <param name="jsonSerializerOptions">The <see cref="JsonSerializerOptions"/> to append the resolver to.</param>
        /// <param name="other">The <see cref="JsonSerializerOptions"/> whose type information resolver should be appended.</param>
        /// <returns>The <see cref="JsonSerializerOptions"/>.</returns>
        public static JsonSerializerOptions AppendTypeResolver(this JsonSerializerOptions jsonSerializerOptions, JsonSerializerOptions other)
        {
            if (jsonSerializerOptions is null)
            {
                return other;
            }

            if (other is null || other.TypeInfoResolver is null || other.TypeInfoResolverChain is null)
            {
                return jsonSerializerOptions;
            }

            return AppendTypeResolver(jsonSerializerOptions, other.TypeInfoResolver);
        }

        /// <summary>
        /// Appends the <paramref name="jsonTypeInfoResolver" /> to the type resolver chain for this instance.
        /// </summary>
        /// <param name="jsonSerializerOptions">The <see cref="JsonSerializerOptions"/> to append the resolver to.</param>
        /// <param name="jsonTypeInfoResolver">The type information resolver to append.</param>
        /// <returns>The <see cref="JsonSerializerOptions"/>.</returns>
        public static JsonSerializerOptions AppendTypeResolver(this JsonSerializerOptions jsonSerializerOptions, IJsonTypeInfoResolver jsonTypeInfoResolver)
        {
            ArgumentNullException.ThrowIfNull(jsonSerializerOptions);
            ArgumentNullException.ThrowIfNull(jsonTypeInfoResolver);

            jsonSerializerOptions.TypeInfoResolverChain.Insert(jsonSerializerOptions.TypeInfoResolverChain.Count, jsonTypeInfoResolver);

            return jsonSerializerOptions;
        }

        /// <summary>
        /// Inserts the type resolver from <paramref name="other"/>into the type resolver chain for this instance.
        /// </summary>
        /// <param name="jsonSerializerOptions">The <see cref="JsonSerializerOptions"/> to append the resolver to.</param>
        /// <param name="other">The <see cref="JsonSerializerOptions"/> whose type information resolver should be inserted.</param>
        /// <returns>The <see cref="JsonSerializerOptions"/>.</returns>
        public static JsonSerializerOptions InsertTypeResolver(this JsonSerializerOptions jsonSerializerOptions, JsonSerializerOptions other)
        {
            if (jsonSerializerOptions is null)
            {
                return other;
            }

            if (other is null || other.TypeInfoResolver is null || other.TypeInfoResolverChain is null)
            {
                return jsonSerializerOptions;
            }

            return InsertTypeResolver(jsonSerializerOptions, other.TypeInfoResolver);
        }

        /// <summary>
        /// Generates a new <see cref="JsonSerializerOptions"/> based on <paramref name="jsonSerializerOptions"/> with the specified <paramref name="jsonTypeInfoResolver"/> at the beginning of the type resolver chain.
        /// </summary>
        /// <param name="jsonSerializerOptions">The <see cref="JsonSerializerOptions"/> to insert the resolver into.</param>
        /// <param name="jsonTypeInfoResolver">The type information resolver to append.</param>
        /// <returns>The <see cref="JsonSerializerOptions"/>.</returns>
        public static JsonSerializerOptions InsertTypeResolver(this JsonSerializerOptions jsonSerializerOptions, IJsonTypeInfoResolver jsonTypeInfoResolver)
        {
            ArgumentNullException.ThrowIfNull(jsonSerializerOptions);
            ArgumentNullException.ThrowIfNull(jsonTypeInfoResolver);

            jsonSerializerOptions.TypeInfoResolverChain.Insert(0, jsonTypeInfoResolver);

            return jsonSerializerOptions;
        }
    }
}
