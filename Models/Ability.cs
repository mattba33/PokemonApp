namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using PokemonApp.Models;

//Pokemon will have varying abilites from 1-3, the last is normally hidden
public class PokemonAbility
{
    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; set; }
    [JsonPropertyName("slot")]
    public int Slot { get; set; }
    [JsonPropertyName("ability")]
    public AbilityInfo? Ability { get; set; }
}

public class AbilityInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

public class AbilityDescription
{
    [JsonPropertyName("effect_entries")]
    public List<EffectEntry> EffectEntries { get; set; } = new();
}
