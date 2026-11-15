# CadastroEventos

Aplicativo .NET MAUI da Agenda 15 de Desenvolvimento de Sistemas II para cadastrar eventos e consultar um resumo com duração e custos calculados.

## Funcionalidades

- Cadastro de nome, datas, participantes, local e custo por participante.
- Validação dos campos obrigatórios, dos valores numéricos e do período do evento.
- Entrada monetária com vírgula decimal e apresentação em reais no padrão brasileiro.
- Resumo com datas, duração em dias e custo total.
- Navegação de volta ao cadastro preservando os dados preenchidos.

## Executar no Windows

Requisitos: .NET 10 SDK, workload .NET MAUI e Windows 10 versão 1809 (build 17763) ou posterior.

No PowerShell, na pasta do projeto:

```powershell
dotnet workload install maui
dotnet build -f net10.0-windows10.0.19041.0
dotnet run -f net10.0-windows10.0.19041.0
```

O aplicativo é configurado como Windows não empacotado para execução local. Para compilar outro destino, use o target framework correspondente disponível no ambiente, por exemplo `net10.0-android` em um ambiente Android configurado.