// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using idunno.AtProto.Lexicons.NearHorizon.App.Actor;

namespace idunno.AtProto.Lexicons.Test.NearHorizon.App
{
    public class PronounTests
    {
        [Fact]
        public void PronounsDeserializesCorrect()
        {
            string json = """
                {
                    "sets": [
                        {
                            "forms": [
                                "it",
                                "she",
                                "we"
                            ]
                        }
                    ],
                    "$type": "app.nearhorizon.actor.pronouns",
                    "createdAt": "2026-02-17T01:07:18.835Z",
                    "displayMode": "all"
                }
                """;

            Pronouns? pronouns = JsonSerializer.Deserialize<Lexicons.NearHorizon.App.Actor.Pronouns>(json, LexiconJsonSerializerOptions.Default);

            Assert.NotNull(pronouns);
        }
    }
}
