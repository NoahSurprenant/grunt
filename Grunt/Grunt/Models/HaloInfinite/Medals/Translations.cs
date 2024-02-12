using System.Text.Json.Serialization;

namespace Surprenant.Grunt.Models.HaloInfinite.Medals;
public class Translations
{
    [JsonPropertyName("pt-BR")]
    public string PortugueseBrazilian { get; set; }

    [JsonPropertyName("zh-CN")]
    public string CommunistChinese { get; set; }

    [JsonPropertyName("zh-TW")]
    public string TaiwanChinese { get; set; }

    [JsonPropertyName("de-DE")]
    public string German { get; set; }

    [JsonPropertyName("fr-FR")]
    public string French { get; set; }

    [JsonPropertyName("it-IT")]
    public string Italian { get; set; }

    [JsonPropertyName("ja-JP")]
    public string Japan { get; set; }

    [JsonPropertyName("ko-KR")]
    public string Korean { get; set; }

    [JsonPropertyName("es-MX")]
    public string SpanishMexico { get; set; }

    [JsonPropertyName("nl-NL")]
    public string Dutch { get; set; }

    [JsonPropertyName("pl-PL")]
    public string Polish { get; set; }

    [JsonPropertyName("ru-RU")]
    public string Russian { get; set; }

    [JsonPropertyName("es-ES")]
    public string SpanishSpain { get; set; }
}