namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

public class Pokemon
{
    [JsonPropertyName("id")]
    public int PokedexNumber { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; } 

    [JsonPropertyName("sprites")]
    public PokemonSprites? Sprites { get; set; }

    [JsonPropertyName("types")]
    public List<PokemonTypeSlot> Types { get; set; } = new();

    [JsonPropertyName("species")]
    public PokemonSpecies? Species {get; set; }

    [JsonPropertyName("abilities")]
    public List<PokemonAbility> Abilities { get; set; } = new();

    [JsonPropertyName("stats")]
    public List<PokemonStat> Stats { get; set; } = new();

    [JsonPropertyName("moves")]
    public List<PokemonMoveSlot> Moves { get; set; } = new();

    private static string JoinNames(IEnumerable<string?> values) =>
        string.Join(", ", values.Where(v => !string.IsNullOrWhiteSpace(v)));

    public override string ToString()
    {
        return $"{Name} (#{PokedexNumber})\n" +
               $"Types: {JoinNames(Types.Select(t => t.Type?.Name))}\n" +
               $"Abilities: {JoinNames(Abilities.Select(a => a.Ability?.Name))}\n" +
               $"Stats: {string.Join(", ", Stats.Select(s => $"{s.Stat?.Name ?? "Unknown"}: {s.BaseStat}"))}\n" +
               $"Moves: {JoinNames(Moves.Take(5).Select(m => m.Move?.Name))}\n";
    }
}

//Used in Ability.cs and Moves.cs
public class EffectEntry
{
    [JsonPropertyName("effect")]
    public string? Effect { get; set; }
    [JsonPropertyName("language")]
    public LanguageInfo? Language { get; set; }
}

//Used in EffectEntries (Which is Used in Ability.cs and Moves.cs)
public class LanguageInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

