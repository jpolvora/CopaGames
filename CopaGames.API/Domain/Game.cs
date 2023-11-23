
using System.Text.Json.Serialization;

namespace CopaGames.API.Domain;

public class Game : Team
{
    [JsonPropertyName("nota")]
    public decimal Grade { get; set; }
    [JsonPropertyName("ano")]
    public int Year { get; set; }
    [JsonPropertyName("urlImagem")]
    public string ImageUrl { get; set; }
}

