// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;

namespace idunno.AtProto.Lexicons
{
    /// <summary>
    /// Provides access to a default source generation JSON type info resolver for lexicon JSON types.
    /// </summary>
    public static class LexiconJsonSerializerOptions
    {
        /// <summary>
        /// Gets the custom set of options used for JSON serialization and deserialization operations of Lexicon classes.
        /// </summary>
        /// <remarks><para>Modifying the returned instance does not affect subsequent calls to this property.</para></remarks>
        public static JsonSerializerOptions Default { get => new(field); } = new(JsonSerializerDefaults.Web)
        {
            TypeInfoResolverChain = {
                SourceGenerationContext.Default,
                AtProto.TypeResolver.JsonTypeInfoResolver
            }
        };
    }
}
