namespace PokemonApp.Services;

using PokemonApp.Models;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

public class PokemonApiService
{
	private readonly HttpClient _httpClient;

	public PokemonApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

	public async Task<Pokemon?> GetPokemonAsync(string pokemonName)
    {
        var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}");

        if (!response.IsSuccessStatusCode)
            return null;
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Pokemon>(content);
    }

    // public async Task<string> GetPokemonSprite(int id)
    // {
        
    // }

	public async Task<PokemonDescription?> GetDescriptionAsync(string speciesUrl)
    {
        var response = await _httpClient.GetAsync(speciesUrl);
        
        if (!response.IsSuccessStatusCode)   
            return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PokemonDescription>(content);
    }

	public string? GetLatestEnglishPokemonDescription(PokemonDescription description)
    {
        var englishLastEntries = description.FlavourTextEntries
        .Where(e => e.Language?.Name == "en")
        .LastOrDefault();

        return englishLastEntries?.FlavourText?
            .Replace("\n", " ")
            .Replace("\f", " ");
    }

    public async Task<AbilityDescription?> GetAbilityDescriptionAsync(string abilityUrl)
    {
        var response = await _httpClient.GetAsync(abilityUrl);

        if (!response.IsSuccessStatusCode)   
            return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AbilityDescription>(content);
    }

    public string? GetEnglishAbilityDescription(AbilityDescription description)
    {
        var englishEntries = description.EffectEntries
        .FirstOrDefault(e => e.Language?.Name == "en");

        return englishEntries?.Effect?
            .Replace("\n", " ")
            .Replace("\f", " ");
    }
    
    public async Task<MoveInfo?> GetMoveInfoAsync(string moveUrl)
    {
        var response = await _httpClient.GetAsync(moveUrl);

        if (!response.IsSuccessStatusCode)   
            return null;

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<MoveInfo>(content);
    }

    public string? GetEnglishMoveDescription(MoveInfo description)
    {
        var englishEntries = description.EffectEntries
        .FirstOrDefault(e => e.Language?.Name == "en");

        return englishEntries?.Effect?
            .Replace("\n", " ")
            .Replace("\f", " ");
    }

    public async Task<EvolutionLine?> GetEvolutionLineAsync(string evolutionChainLink)
    {
        var response = await _httpClient.GetAsync(evolutionChainLink);

        if (!response.IsSuccessStatusCode)
            return null;
        
        var content = await response.Content.ReadAsStringAsync();

        var evolutionData = JsonSerializer.Deserialize<EvolutionChainResponse>(content);
        return EvolutionLine.BuildEvolutionLine(evolutionData?.Chain);
    }
}