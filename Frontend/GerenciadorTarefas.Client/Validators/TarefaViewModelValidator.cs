using FluentValidation;
using GerenciadorTarefas.Client.Models;

namespace GerenciadorTarefas.Client.Validators
{
    public class TarefaViewModelValidator : AbstractValidator<TarefaViewModel>
    {

        public TarefaViewModelValidator()
        {
            RuleFor(s => s.Titulo)
                .NotEmpty()
                    .WithMessage("Titulo é obrigatorio");

            RuleFor(t => t)
                .Must(t => t.DataConclusao is null || t.DataConclusao.Value.Date >= t.DataCriacao.Date)
                    .WithMessage("Data de conclusão não pode ser inferior a data de criação");

            RuleFor(t => t.Status)
                .IsInEnum()
                    .WithMessage("É necessário informar um status valido para tarefa");

            RuleFor(t => t)
                .Must(t =>
                {
                    if (t.Status == ETarefaStatus.Concluida)
                    {
                        return t.DataConclusao is not null;
                    }
                    return t.DataConclusao is null;
                })
                .WithMessage("Data de conclusao é obrigatoria para tarefas concluidas");
        }
    }
}
