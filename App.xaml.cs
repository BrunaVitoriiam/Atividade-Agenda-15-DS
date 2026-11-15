namespace CadastroEventos;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var cadastroPage = new Views.CadastroEventoPage();
		var navigationPage = new NavigationPage(cadastroPage)
		{
			BarBackgroundColor = Color.FromArgb("#14243A"),
			BarTextColor = Color.FromArgb("#C6A15B")
		};

		return new Window(navigationPage);
	}
}