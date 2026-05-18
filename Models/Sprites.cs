namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

public class PokemonSprites
{
    [JsonPropertyName("front_default")]
    public string? FrontDefault { get; set; }
    [JsonPropertyName("front_shiny")]
    public string? FrontShiny { get; set; }
}