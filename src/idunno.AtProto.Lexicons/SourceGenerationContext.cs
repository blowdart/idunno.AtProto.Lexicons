// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using idunno.AtProto.Lexicons.Standard.Site;
using idunno.AtProto.Lexicons.Statusphere.Xyz;
using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons
{
    /// <exclude />
    [JsonSourceGenerationOptions(
        AllowOutOfOrderMetadataProperties = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        IgnoreReadOnlyProperties = false,
        GenerationMode = JsonSourceGenerationMode.Default,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        RespectRequiredConstructorParameters = true,
        UseStringEnumConverter = true,
        WriteIndented = false)]
    [JsonSerializable(typeof(ThemeColor))]
    [JsonSerializable(typeof(ThemeColorRgb))]
    [JsonSerializable(typeof(ThemeColorRgba))]
    [JsonSerializable(typeof(Document))]
    [JsonSerializable(typeof(AtProtoRepositoryRecord<Document>))]
    [JsonSerializable(typeof(Publication))]
    [JsonSerializable(typeof(AtProtoRepositoryRecord<Publication>))]
    [JsonSerializable(typeof(Preferences))]

    [JsonSerializable(typeof(StatusphereStatus))]
    [JsonSerializable(typeof(AtProtoRepositoryRecord<StatusphereStatus>))]
    public partial class SourceGenerationContext : JsonSerializerContext
    {
    }
}
