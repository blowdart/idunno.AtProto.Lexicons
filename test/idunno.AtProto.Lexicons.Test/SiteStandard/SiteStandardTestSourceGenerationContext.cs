// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using idunno.AtProto.Lexicons.Standard.Site;

namespace idunno.AtProto.Lexicons.Test.SiteStandard
{
    /// <exclude />
    [JsonSourceGenerationOptions(
        AllowOutOfOrderMetadataProperties = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        IgnoreReadOnlyProperties = false,
        GenerationMode = JsonSourceGenerationMode.Metadata,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        WriteIndented = false)]


    [JsonSerializable(typeof(TestPreferences))]
    [JsonSerializable(typeof(CustomPreferences))]
    [JsonSerializable(typeof(Publication<CustomPreferences>))]
    internal partial class SiteStandardTestSourceGenerationContext : JsonSerializerContext
    {
    }
}
