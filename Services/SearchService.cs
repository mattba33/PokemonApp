namespace PokemonApp.Services;

using PokemonApp.Services;
using PokemonApp.Models;


public class SearchService
{
    private readonly PokemonApiService _api;

    public SearchService(PokemonApiService api)
    {
        _api = api;
    }

    public Task<Pokemon?> SearchPokemonAsync(string pokemonName)
    {
        return _api.GetPokemonAsync(pokemonName.ToLower());
    }
}