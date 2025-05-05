# Gerenciador de tarefas

Gerenciador de tarefas criado com dotnet .NET Core
## Funcionalidades
    - Listar, Criar, Atualizar e deletar tarefas

## Tecnologias utilizadas

- [AspNet Core] - API
- [EntityFramework] - ORM
- [SQL Server] - Banco de dados
- [FluentValidation] - Validação
- [XUnit] - Testes


## Build e execucao

Para buildar e executar o projeto é necessário possuir .NET SDK e a ferramento CLI do Entity Framework

--estando no diretório da solução
```sh
(instalar ferramenta CLI do Entity Framework)
dotnet tool install --global dotnet-ef

(Apenas caso queira gerar um novo migration)
dotnet ef migrations add MigrationInicial -p .\Backend\GerenciadorTarefas.Infrastructure\GerenciadorTarefas.Infrastructure.csproj -s .\Backend\GerenciadorTarefas.Presentation\GerenciadorTarefas.Presentation.csproj

(Atualiza/Cria o banco)
dotnet ef database update -p .\Backend\GerenciadorTarefas.Infrastructure\GerenciadorTarefas.Infrastructure.csproj -s .\Backend\GerenciadorTarefas.Presentation\GerenciadorTarefas.Presentation.csproj
```


## Para executar os testes
--estando no diretório da solução, banco com seed padrao
```sh
dotnet test .\Backend\GerenciadorTarefas.Test\GerenciadorTarefas.Test.csproj
```


## Executar aplicação
--estando no diretório da solução
```sh
dotnet run --project .\Backend\GerenciadorTarefas.Presentation\GerenciadorTarefas.Presentation.csproj
```
```sh
dotnet run --project .\Frontend\GerenciadorTarefas.Client\GerenciadorTarefas.Client.csproj
```