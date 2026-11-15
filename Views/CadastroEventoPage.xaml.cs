using CadastroEventos.Models;

namespace CadastroEventos.Views;

public partial class CadastroEventoPage : ContentPage
{
	public CadastroEventoPage()
	{
		InitializeComponent();
		BindingContext = new Evento();
	}

	private async void OnCadastrarEventoClicked(object? sender, EventArgs e)
	{
		var evento = (Evento)BindingContext;
		var mensagem = Validar(evento);

		if (mensagem is not null)
		{
			await DisplayAlertAsync("Verifique os dados", mensagem, "Entendi");
			return;
		}

		await Navigation.PushAsync(new ResumoEventoPage(evento));
	}

	private static string? Validar(Evento evento)
	{
		if (string.IsNullOrWhiteSpace(evento.NomeEvento))
		{
			return "Informe o nome do evento.";
		}

		if (string.IsNullOrWhiteSpace(evento.LocalEvento))
		{
			return "Informe o local do evento.";
		}

		if (!int.TryParse(evento.NumeroParticipantesTexto, out var participantes) || participantes <= 0)
		{
			return "Informe um número de participantes maior que zero.";
		}

		if (!decimal.TryParse(evento.CustoPorParticipanteTexto, System.Globalization.NumberStyles.Number,
			System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), out var custo) || custo < 0)
		{
			return "Informe um custo por participante válido e não negativo, como 150,00.";
		}

		if (evento.DataTermino.Date < evento.DataInicio.Date)
		{
			return "A data de término deve ser igual ou posterior à data de início.";
		}

		return null;
	}
}