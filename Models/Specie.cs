namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;


public class PokemonSpecies
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}


public class PokemonDescription
{
    [JsonPropertyName("flavor_text_entries")]
    public List<FlavourTextEntry> FlavourTextEntries { get; set; } = new();

    [JsonPropertyName("evolution_chain")]
    public EvolutionInfo? EvolutionChain { get; set; }
}


public class FlavourTextEntry
{
    [JsonPropertyName("flavor_text")]
    public string? FlavourText { get; set; }
    [JsonPropertyName("language")]
    public LanguageInfo? Language { get; set;}
    [JsonPropertyName("version")]
    public GameVersion? Version { get; set;}
}


public class GameVersion
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}