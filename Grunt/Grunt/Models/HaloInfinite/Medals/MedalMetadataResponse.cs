using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Surprenant.Grunt.Models.HaloInfinite.Medals;
public class MedalMetadataResponse
{
    [JsonPropertyName("difficulties")]
    public List<string> Difficulties { get; set; }
    [JsonPropertyName("types")]
    public List<string> Types { get; set; }
    [JsonPropertyName("sprites")]
    public Sprites Sprites { get; set; }
    [JsonPropertyName("medals")]
    public List<Medal> Medals { get; set; }
}
