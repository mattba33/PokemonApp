namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

//Pokemon will either have a single type or dual typing
public class PokemonTypeSlot
{
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    [JsonPropertyName("type")]
    public TypeInfo? Type { get; set; }
}

public class TypeInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}