namespace PokemonApp;

using PokemonApp.Views;

public partial class AppShell : Shell
{
	private const string ThemeKey = "AppTheme";

	public AppShell()
	{
		InitializeComponent();

		var theme = Preferences.Get(ThemeKey, "Light");

		if (theme == "Dark")
        {
            Application.Current.Resources["GlobalPageBackground"] = Colors.Black;
			Application.Current.Resources["GlobalTextColor"] = Colors.White;
        }
		else
		{
			Application.Current.Resources["GlobalPageBackground"] = Colors.White;
			Application.Current.Resources["GlobalTextColor"] = Colors.Black;
		}
	}
	

}
