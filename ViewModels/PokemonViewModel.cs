namespace PokemonApp.ViewModels;

using PokemonApp.Models;
using PokemonApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public class PokemonViewModel : INotifyPropertyChanged
{
    private readonly PokemonApiService _api;

    public PokemonViewModel(PokemonApiService api)
    {
        _api = api;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public string Name { get; set; } = "";
    public ObservableCollection<string> Types { get; set; } = new();
    public PokemonSprites? Sprites { get; set; }
    public string Description { get; set; } = "";
    public ObservableCollection<PokemonStat> Stats { get; set; } = new();  

    public ObservableCollection<string> Abilities { get; set; } = new();
    public ObservableCollection<string> Moves { get; set; } = new();

    public EvolutionLine? EvolutionLine { get; set; }

    public async Task LoadPokemonAsync(int id)
    {
        var pokemon = await _api.GetPokemonAsync(id);
        if (pokemon == null) return;

        Name = pokemon.Name ?? "";

        Types.Clear();
        foreach (var type in pokemon.Types)
        {
            Types.Add(type.Type.Name);
        }

        Sprites = pokemon.Sprites;

        var species = await _api.GetDescriptionAsync(pokemon.Species.Url);
        if (species != null)
        {
            Description = _api.GetLatestEnglishPokemonDescription(species) ?? "";
        }

        Stats.Clear();
        foreach (var stat in pokemon.Stats)
        {
            Stats.Add(stat);
        }

        Abilities.Clear();
        foreach (var ability in pokemon.Abilities)
        {
            var abilityinfo = await _api.GetAbilityDescriptionAsync(ability.Ability.Url);

            if (abilityinfo != null)
            {
                var text = _api.GetEnglishAbilityDescription(abilityinfo);
                var isHidden = ability.IsHidden ? " (Hidden)" : "";

                Abilities.Add($"{ability.Ability.Name}{isHidden} - {text}");
            }
        }

        Moves.Clear();
        foreach (var move in pokemon.Moves.Take(5))
        {
            var moveinfo = await _api.GetMoveInfoAsync(move.Move.Url);

            if (moveinfo != null)
            {
                var text = _api.GetEnglishMoveDescription(moveinfo);
                Moves.Add($"{move.Move.Name} - Power: {moveinfo.Power} Accuracy: {moveinfo.Accuracy} Type: {moveinfo.Type.Name} - {text}");
            }
        }

        if (species?.EvolutionChain?.Url != null)
        {
            EvolutionLine = await _api.GetEvolutionLineAsync(species.EvolutionChain.Url);
        }

        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Types));
        OnPropertyChanged(nameof(Sprites));
        OnPropertyChanged(nameof(Description));
        OnPropertyChanged(nameof(Stats));
        OnPropertyChanged(nameof(Abilities));
        OnPropertyChanged(nameof(Moves));
        OnPropertyChanged(nameof(EvolutionLine));
    }
}