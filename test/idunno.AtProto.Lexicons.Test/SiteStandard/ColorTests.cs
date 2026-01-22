// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.
//

using System.Text.Json;
using idunno.AtProto.Lexicons.Standard.Site;

namespace idunno.AtProto.Lexicons.Test.SiteStandard
{
    public class ColorTests
    {
        private readonly JsonSerializerOptions _lexiconSerializationOptions = LexiconJsonSerializerOptions.Default;

        [Fact]
        public void ColorRgbValidatesByDefault()
        {
            // All valid values are between 0 and 255 inclusive
            _ = new ThemeColorRgb(0, 128, 255);

            Exception? ex;
            ArgumentOutOfRangeException? argumentOutOfRangeException;
            int actualValue;
            string actualParamName;

            actualParamName = "red";
            actualValue = -1;
            ex = Record.Exception(() => new ThemeColorRgb(actualValue, 128, 255));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualValue = 256;
            ex = Record.Exception(() => new ThemeColorRgb(actualValue, 128, 255));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualParamName = "green";
            actualValue = -1;
            ex = Record.Exception(() => new ThemeColorRgb(0, actualValue, 255));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualValue = 256;
            ex = Record.Exception(() => new ThemeColorRgb(0, actualValue, 255));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualParamName = "blue";
            actualValue = -1;
            ex = Record.Exception(() => new ThemeColorRgb(0, 128, actualValue));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualValue = 256;
            ex = Record.Exception(() => new ThemeColorRgb(0, 128, actualValue));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);
        }

        [Fact]
        public void ColorRgbAValidatesByDefault()
        {
            // All valid values are between 0 and 255 inclusive
            _ = new ThemeColorRgba(0, 128, 255, 0);

            Exception? ex;
            ArgumentOutOfRangeException? argumentOutOfRangeException;
            int actualValue;
            string actualParamName;

            actualParamName = "red";
            actualValue = -1;
            ex = Record.Exception(() => new ThemeColorRgba(actualValue, 128, 255, 0));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualValue = 256;
            ex = Record.Exception(() => new ThemeColorRgba(actualValue, 128, 255, 0));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualParamName = "green";
            actualValue = -1;
            ex = Record.Exception(() => new ThemeColorRgba(0, actualValue, 255, 0));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualValue = 256;
            ex = Record.Exception(() => new ThemeColorRgba(0, actualValue, 255, 0));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualParamName = "blue";
            actualValue = -1;
            ex = Record.Exception(() => new ThemeColorRgba(0, 128, actualValue, 0));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualValue = 256;
            ex = Record.Exception(() => new ThemeColorRgba(0, 128, actualValue, 0));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualParamName = "alpha";
            actualValue = -1;
            ex = Record.Exception(() => new ThemeColorRgba(0, 128, 255, actualValue));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);

            actualValue = 256;
            ex = Record.Exception(() => new ThemeColorRgba(0, 128, 255, actualValue));
            Assert.IsType<ArgumentOutOfRangeException>(ex);
            argumentOutOfRangeException = ex as ArgumentOutOfRangeException;
            Assert.NotNull(argumentOutOfRangeException);
            Assert.Equal(actualParamName, argumentOutOfRangeException.ParamName);
            Assert.Equal(actualValue, argumentOutOfRangeException.ActualValue);
        }

        [Fact]
        public void ThemeColorRgbDeserializesCorrectlyWithNoSerializationContext()
        {
            string json = """
            {
                "$type": "site.standard.theme.color#rgb",
                "r": 34,
                "g": 139,
                "b": 34
            }
            """;

            ThemeColor? color = JsonSerializer.Deserialize<ThemeColor>(json, JsonSerializerOptions.Web);

            Assert.NotNull(color);
            Assert.IsType<ThemeColorRgb>(color);

            ThemeColorRgb rgbColor = (ThemeColorRgb)color;
            Assert.Equal(34, rgbColor.Red);
            Assert.Equal(139, rgbColor.Green);
            Assert.Equal(34, rgbColor.Blue);
        }

        [Fact]
        public void ThemeColorRgbDeserializesCorrectlyWithSerializationContext()
        {
            string json = """
            {
                "$type": "site.standard.theme.color#rgb",
                "r": 34,
                "g": 139,
                "b": 34
            }
            """;

            ThemeColor? color = JsonSerializer.Deserialize<ThemeColor>(json, _lexiconSerializationOptions);

            Assert.NotNull(color);
            Assert.IsType<ThemeColorRgb>(color);

            ThemeColorRgb? rgbColor = color as ThemeColorRgb;
            Assert.NotNull(rgbColor);
            Assert.Equal(34, rgbColor.Red);
            Assert.Equal(139, rgbColor.Green);
            Assert.Equal(34, rgbColor.Blue);
        }

        [Fact]
        public void ThemeColorRgbWillNotDeserializesWithInvalidValuesAndDefaultWebOptions()
        {
            string json = """
            {
                "$type": "site.standard.theme.color#rgb",
                "r": -1,
                "g": 139,
                "b": 34
            }
            """;

            Assert.Throws<ArgumentOutOfRangeException>(() => JsonSerializer.Deserialize<ThemeColor>(json, JsonSerializerOptions.Web));
        }

        [Fact]
        public void ThemeColorRgbWillNotDeserializesWithInvalidValuesAndSerializationContext()
        {
            string json = """
            {
                "$type": "site.standard.theme.color#rgb",
                "r": -1,
                "g": 139,
                "b": 34
            }
            """;

            Assert.Throws<ArgumentOutOfRangeException>(() => JsonSerializer.Deserialize<ThemeColor>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void ThemeColorRgbWillNotDeserializeWithMissingRequiredValuesAndSerializationContext()
        {
            string json = """
            {
                "$type": "site.standard.theme.color#rgb",
                "g": 139,
                "b": 34
            }
            """;

            Assert.Throws<JsonException>(()=> JsonSerializer.Deserialize<ThemeColor>(json, JsonSerializerOptions.Web));
        }

        [Fact]
        public void RgbSerializesAsExpected()
        {
            ThemeColorRgb color = new(34, 139, 34);
            string json = JsonSerializer.Serialize<ThemeColor>(color, _lexiconSerializationOptions);
            string expectedJson = """
            {"$type":"site.standard.theme.color#rgb","r":34,"g":139,"b":34}
            """;
            Assert.Equal(expectedJson, json);
        }

        [Fact]
        public void ThemeColorRgbWillNotDeserializeToBaseTypeWithMissingRequiredValuesAndSerializationContext()
        {
            string json = """
            {
                "$type": "site.standard.theme.color#rgb",
                "g": 139,
                "b": 34
            }
            """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<ThemeColor>(json, _lexiconSerializationOptions));
        }

        [Fact]
        public void ThemeColorRgbWillNotDeserializeToBaseTypeWithMissingRequiredValues()
        {
            string json = """
            {
                "$type": "site.standard.theme.color#rgb",
                "g": 139,
                "b": 34
            }
            """;

            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<ThemeColor>(json, JsonSerializerOptions.Web));
        }
    }
}
