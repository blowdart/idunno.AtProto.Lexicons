// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace idunno.AtProto.Lexicons
{
    /// <summary>
    /// Provides access to a default source generation JSON type info resolver for lexicon JSON types.
    /// </summary>
    public static class LexiconJsonSerializerOptions
    {
        [UnconditionalSuppressMessage("Trimming",
            "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code",
            Justification = "All types for the lexicon should be captured here.")]
        [UnconditionalSuppressMessage("AOT",
            "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.",
            Justification = "All types for the lexicon should be captured here.")]
        private static readonly IJsonTypeInfoResolver s_lexiconResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                typeInfo =>
                {
                    if (typeInfo.Type == typeof(Lexicon.Community.Location.LocationBase))
                    {
                        typeInfo.PolymorphismOptions!.DerivedTypes.Add(
                            new JsonDerivedType(typeof (Lexicon.Community.Calendar.EventUri), "community.lexicon.calendar.event#uri"));
                    }
                }
            }
        };

        /// <summary>
        /// Gets the custom set of options used for JSON serialization and deserialization operations of Lexicon classes.
        /// </summary>
        /// <remarks><para>Modifying the returned instance does not affect subsequent calls to this property.</para></remarks>
        public static JsonSerializerOptions Default { get => new(field); } = new(JsonSerializerDefaults.Web)
        {
            TypeInfoResolverChain = {
                s_lexiconResolver,
                AtProto.TypeResolver.JsonTypeInfoResolver
            }
        };
    }
}
