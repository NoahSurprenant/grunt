using System.Text.Json.Serialization;

namespace Surprenant.Grunt.Models.HaloInfinite.Medals;

public class Sprites
{
    [JsonPropertyName("small")]
    public Sprite Small { get; set; }
    [JsonPropertyName("medium")]
    public Sprite Medium { get; set; }
    [JsonPropertyName("extra-large")]
    public Sprite ExtraLarge { get; set; }
}

public class Sprite
{
    [JsonPropertyName("path")]
    public string Path { get; set; }
    [JsonPropertyName("columns")]
    public int Columns { get; set; }
    [JsonPropertyName("size")]
    public int Size { get; set; }
}
