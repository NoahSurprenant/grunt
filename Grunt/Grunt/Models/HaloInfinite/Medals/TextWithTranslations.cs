using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Surprenant.Grunt.Models.HaloInfinite.Medals;
public class TextWithTranslations
{
    [JsonPropertyName("value")]
    public string Value { get; set; }
    [JsonPropertyName("translations")]
    public Translations Translations { get; set; }
}
