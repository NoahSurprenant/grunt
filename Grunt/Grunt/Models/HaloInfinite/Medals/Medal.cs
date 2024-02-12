

using System.Text.Json.Serialization;

namespace Surprenant.Grunt.Models.HaloInfinite.Medals;
public class Medal
{
    [JsonPropertyName("name")]
    public TextWithTranslations Name { get; set; }
    [JsonPropertyName("description")]
    public TextWithTranslations Description { get; set; }
    [JsonPropertyName("spriteIndex")]
    public int SpriteIndex { get; set; }
    [JsonPropertyName("sortingWeight")]
    public int SortingWeight { get; set; }
    [JsonPropertyName("difficultyIndex")]
    public int DifficultyIndex { get; set; }
    [JsonPropertyName("typeIndex")]
    public int TypeIndex { get; set; }
    [JsonPropertyName("personalScore")]
    public int PersonalScore { get; set; }
    [JsonPropertyName("nameId")]
    public long NameID { get; set; }
}
