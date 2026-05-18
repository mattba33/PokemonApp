namespace PokemonApp.Models;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.Maui.Controls.Shapes;

public class PokemonEvolutionChain
{
    [JsonPropertyName("evolution_chain")]
    public EvolutionInfo? EvolutionChain { get; set; }
}

public class EvolutionInfo
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }
}

public class EvolutionLine
{
    public List<List<string>> Paths { get; set; } = new();

    public override string ToString()
    {
        return string.Join("\n", Paths.Select(path => string.Join(" -> ", path)));
    }

    public static EvolutionLine BuildEvolutionLine(EvolutionChainLink? chain)
    {
        var result = new EvolutionLine();

        if (chain == null)
            return result;

        Traverse(chain, new List<string>(), result);

        return result;
    }

    private static void Traverse(EvolutionChainLink current, List<String> currentPath, EvolutionLine result)
    {
        currentPath.Add(current.Species?.Name ?? "Unknown");

        if (current.EvolvesTo == null || current.EvolvesTo.Count == 0)
        {
            result.Paths.Add(new List<string>(currentPath));
        }
        else
        {
            foreach (var next in current.EvolvesTo)
            {
                Traverse(next, new List<string>(currentPath), result);
            }
        }
    }
}

public class EvolutionChainResponse
{
    [JsonPropertyName("chain")]
    public EvolutionChainLink? Chain { get; set;}
}

public class EvolutionChainLink
{
    [JsonPropertyName("species")]
    public PokemonSpecies? Species { get; set;}
    [JsonPropertyName("evolves_to")]
    public List<EvolutionChainLink> EvolvesTo { get; set; } = new();
}
