namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

// Base Stat(Total), hp, attack, defense, special attack, special defense, speed
public class PokemonStat
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }
    [JsonPropertyName("stat")]
    public StatInfo? Stat { get; set; }
    public double Progress => BaseStat / 255.0; // May change based on future pokemon
}

public class StatInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}