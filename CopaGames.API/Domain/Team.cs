
using System.Text.Json.Serialization;

namespace CopaGames.API.Domain;

public abstract class Team
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("titulo")]
    public string Title { get; set; }
}

