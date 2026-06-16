namespace PokemonApp.ViewModels;

public class MoveViewModel
{
    public string Name { get; set; } = "";
    public int? Power{ get; set; }
    public int? Accuracy { get; set; }
    public string Type { get; set; } = "";
    public string Description { get; set; } = "";

    public string DisplayPower => Power?.ToString() ?? "N/A";
    public string DisplayAccuracy => Accuracy?.ToString() ?? "N/A";
}