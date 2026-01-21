// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

using idunno.AtProto.Repo;

namespace idunno.AtProto.Lexicons.Lexicon.Community.Location
{
    /// <summary>
    /// Encapsulates the minimum necessary properties for location. Can be used as the base type for more specific location types via JSON polymorphism.
    /// </summary>
    [JsonPolymorphic(IgnoreUnrecognizedTypeDiscriminators = false, UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization)]
    [JsonDerivedType(typeof(Address), typeDiscriminator: "community.lexicon.location.address")]
    [JsonDerivedType(typeof(Fsq), typeDiscriminator: "community.lexicon.location.fsq")]
    [JsonDerivedType(typeof(Geo), typeDiscriminator: "community.lexicon.location.geo")]
    [JsonDerivedType(typeof(HThree), typeDiscriminator: "community.lexicon.location.hthree")]
    public abstract record LocationBase : AtProtoRecord
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Location"/> class.
        /// </summary>
        protected LocationBase()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="name">The name of the location.</param>
        protected LocationBase(string? name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string? Name { get; set; }
    }
}
