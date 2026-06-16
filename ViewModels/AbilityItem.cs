namespace PokemonApp.ViewModels;

public class AbilityItem
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsHidden { get; set; } = false;

    public string DisplayName => IsHidden ? $"{Name} (Hidden)" : Name;
}
