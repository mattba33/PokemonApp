namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

public class PokemonMoveSlot
{
    [JsonPropertyName("move")]
    public PokemonMove? Move { get; set; }
}

public class PokemonMove
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("url")]
    public string? Url { get; set;}
}

public class MoveInfo
{
    [JsonPropertyName("accuracy")]
    public int? Accuracy { get; set; }

    [JsonPropertyName("damage_class")]
    public DamageClassInfo? DamageClass { get; set; }

    [JsonPropertyName("effect_entries")]
    public List<EffectEntry> EffectEntries { get; set; } = new();

    [JsonPropertyName("effect_chance")]
    public int? EffectChance { get; set; }

    [JsonPropertyName("power")]
    public int? Power { get; set; }
    
    [JsonPropertyName("pp")]
    public int? PP { get; set; }
    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("target")]
    public TargetName? Target { get; set; }

    [JsonPropertyName("type")]
    public Movetype? Type { get; set;}
}

//Either single, adjacent or entire field
public class TargetName
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

// Same selection as Pokemon Types
public class Movetype
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}

// Either physical, special or status
public class DamageClassInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}