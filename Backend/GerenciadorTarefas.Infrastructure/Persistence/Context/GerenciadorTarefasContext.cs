using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Infrastructure.Persistence.TypeConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Infrastructure.Persistence.Context
{
    public class GerenciadorTarefasContext : DbContext
    {

        public GerenciadorTarefasContext(DbContextOptions<GerenciadorTarefasContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TarefaTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            optionsBuilder.UseSeeding((context, _) =>
            {
                var tarefa = context.Set<Tarefa>().FirstOrDefault();
                if (tarefa == null)
                {
                    context.Set<Tarefa>().AddRange([
                            new Tarefa() { Titulo = "Corrigir bug no módulo de autenticação", Descricao = "Investigar e corrigir erro que impede login de usuários.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Implementar cache para requisições de API", Descricao = "Reduzir carga no banco de dados e otimizar tempo de resposta.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Criar documentação para o serviço de notificações", Descricao = "Escrever guias detalhados sobre eventos e gatilhos de notificação.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Refatorar código do módulo de pagamentos", Descricao = "Melhorar estrutura do código e otimizar chamadas externas.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Configurar monitoramento de erros via Sentry", Descricao = "Integrar Sentry para capturar e reportar erros automaticamente.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Criar testes unitários para o módulo de pedidos", Descricao = "Desenvolver e validar regras de negócio do serviço de pedidos.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Realizar atualização de dependências no projeto", Descricao = "Verificar e atualizar pacotes utilizados no sistema.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Otimizar consultas ao banco de dados", Descricao = "Revisar queries para reduzir tempo de resposta.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Configurar CI/CD no GitHub Actions", Descricao = "Criar automação para testes e deploy contínuo.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Desenvolver novo endpoint para relatórios", Descricao = "Criar endpoint na API para geração de relatórios customizados.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Criar interface de administração no sistema", Descricao = "Desenvolver painel para gestão de usuários e permissões.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Implementar sistema de notificações via WebSocket", Descricao = "Criar notificações em tempo real no front-end.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Melhorar logging do sistema",Descricao = "Configurar logs mais detalhados para troubleshooting.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Estudar padrões de design", Descricao = "Revisar os principais padrões de design, como Singleton, Factory e Observer.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Praticar algoritmos e estruturas de dados", Descricao = "Resolver exercícios de ordenação, busca e manipulação de estruturas como listas e árvores.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Assistir curso sobre Clean Code", Descricao = "Aprender boas práticas de código limpo e manutenção de software.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Ler sobre arquitetura hexagonal", Descricao = "Estudar abordagem para organização de código e separação de responsabilidades.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Implementar projeto pessoal com DDD", Descricao = "Criar um pequeno sistema aplicando conceitos de Domain-Driven Design.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Ler livro sobre desenvolvimento de software", Descricao = "Escolher um livro técnico e estudar conceitos avançados.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Explorar um novo framework frontend", Descricao = "Aprender e testar um framework moderno como React ou Vue.js.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Criar projeto utilizando APIs REST", Descricao = "Desenvolver um serviço utilizando boas práticas de APIs RESTful.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Praticar testes automatizados", Descricao = "Escrever testes unitários e de integração para um pequeno projeto.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Aprender sobre mensageria com RabbitMQ", Descricao = "Entender como filas de mensagens melhoram a escalabilidade de sistemas.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Criar API GraphQL", Descricao = "Explorar GraphQL e desenvolver uma API para consultas eficientes.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Refatorar código aplicando SOLID", Descricao = "Melhorar um projeto aplicando princípios SOLID.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Explorar computação serverless", Descricao = "Aprender sobre AWS Lambda e Azure Functions.", Status = Domain.Enums.ETarefaStatus.Concluida, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = DateTime.UtcNow + TimeSpan.FromDays(Random.Shared.NextInt64() % 80) },
                            new Tarefa() { Titulo = "Entender funcionamento do Kubernetes", Descricao = "Explorar orquestração de containers e automação de deploy.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Realizar curso de arquitetura de software", Descricao = "Estudar arquiteturas monolíticas, microservices e event-driven.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Fazer compras no mercado", Descricao = "Comprar itens essenciais como frutas, verduras e produtos de higiene.", Status = Domain.Enums.ETarefaStatus.Pendente, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                            new Tarefa() { Titulo = "Organizar espaço de trabalho", Descricao = "Arrumar a mesa, revisar documentos e deixar o ambiente mais produtivo.", Status = Domain.Enums.ETarefaStatus.EmProgresso, DataCriacao = DateTime.UtcNow - TimeSpan.FromDays(Random.Shared.NextInt64() % 80), DataConclusao = null },
                ]);
                    context.SaveChanges();
                }
            });
            base.OnConfiguring(optionsBuilder);
        }


    }
}
