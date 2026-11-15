using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CadastroEventos.Models;

public sealed class Evento : INotifyPropertyChanged
{
	private static readonly CultureInfo CulturaBrasileira = CultureInfo.GetCultureInfo("pt-BR");
	private string _nomeEvento = string.Empty;
	private DateTime _dataInicio = DateTime.Today;
	private DateTime _dataTermino = DateTime.Today;
	private int _numeroParticipantes;
	private string _numeroParticipantesTexto = string.Empty;
	private string _localEvento = string.Empty;
	private decimal _custoPorParticipante;
	private string _custoPorParticipanteTexto = string.Empty;

	public event PropertyChangedEventHandler? PropertyChanged;

	public string NomeEvento
	{
		get => _nomeEvento;
		set => SetProperty(ref _nomeEvento, value);
	}

	public DateTime DataInicio
	{
		get => _dataInicio;
		set
		{
			if (SetProperty(ref _dataInicio, value))
			{
				OnPropertyChanged(nameof(DataInicioFormatada));
				OnPropertyChanged(nameof(Duracao));
				OnPropertyChanged(nameof(DuracaoEmDias));
			}
		}
	}

	public DateTime DataTermino
	{
		get => _dataTermino;
		set
		{
			if (SetProperty(ref _dataTermino, value))
			{
				OnPropertyChanged(nameof(DataTerminoFormatada));
				OnPropertyChanged(nameof(Duracao));
				OnPropertyChanged(nameof(DuracaoEmDias));
			}
		}
	}

	public int NumeroParticipantes
	{
		get => _numeroParticipantes;
		set
		{
			if (SetProperty(ref _numeroParticipantes, value))
			{
				NumeroParticipantesTexto = value.ToString(CulturaBrasileira);
			}
		}
	}

	public string NumeroParticipantesTexto
	{
		get => _numeroParticipantesTexto;
		set
		{
			if (SetProperty(ref _numeroParticipantesTexto, value))
			{
				if (int.TryParse(value, NumberStyles.Integer, CulturaBrasileira, out var numero))
				{
					SetProperty(ref _numeroParticipantes, numero, nameof(NumeroParticipantes));
				}
			}
		}
	}

	public string LocalEvento
	{
		get => _localEvento;
		set => SetProperty(ref _localEvento, value);
	}

	public decimal CustoPorParticipante
	{
		get => _custoPorParticipante;
		set
		{
			if (SetProperty(ref _custoPorParticipante, value))
			{
				CustoPorParticipanteTexto = value.ToString("0.##", CulturaBrasileira);
				OnPropertyChanged(nameof(CustoPorParticipanteFormatado));
				OnPropertyChanged(nameof(CustoTotal));
				OnPropertyChanged(nameof(CustoTotalFormatado));
			}
		}
	}

	public string CustoPorParticipanteTexto
	{
		get => _custoPorParticipanteTexto;
		set
		{
			if (SetProperty(ref _custoPorParticipanteTexto, value)
				&& decimal.TryParse(value, NumberStyles.Number, CulturaBrasileira, out var custo))
			{
				if (SetProperty(ref _custoPorParticipante, custo, nameof(CustoPorParticipante)))
				{
					OnPropertyChanged(nameof(CustoPorParticipanteFormatado));
					OnPropertyChanged(nameof(CustoTotal));
					OnPropertyChanged(nameof(CustoTotalFormatado));
				}
			}
		}
	}

	public TimeSpan Duracao => DataTermino.Date - DataInicio.Date;

	public int DuracaoEmDias => Duracao.Days;

	public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;

	public string DataInicioFormatada => DataInicio.ToString("dd/MM/yyyy", CulturaBrasileira);

	public string DataTerminoFormatada => DataTermino.ToString("dd/MM/yyyy", CulturaBrasileira);

	public string CustoPorParticipanteFormatado => CustoPorParticipante.ToString("C", CulturaBrasileira);

	public string CustoTotalFormatado => CustoTotal.ToString("C", CulturaBrasileira);

	private bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(backingField, value))
		{
			return false;
		}

		backingField = value;
		OnPropertyChanged(propertyName);
		return true;
	}

	private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}