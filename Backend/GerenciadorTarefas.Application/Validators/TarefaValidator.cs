using FluentValidation;
using GerenciadorTarefas.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Application.Validators
{
    internal class TarefaValidator : AbstractValidator<Tarefa>
    {

        public TarefaValidator()
        {
            RuleFor(s => s.Titulo)
                .NotEmpty()
                    .WithMessage("Titulo é obrigatorio");

            RuleFor(t => t)
                .Must(t=> t.DataConclusao is null || t.DataConclusao.Value.Date >= t.DataCriacao.Date)
                    .WithMessage("Data de conclusão não pode ser inferior a data de criação");

            RuleFor(t => t.Status)
                .IsInEnum()
                    .WithMessage("É necessário informar um status valido para tarefa");

            RuleFor(t => t)
                .Must(t =>
                {
                    if(t.Status == Domain.Enums.ETarefaStatus.Concluida)
                    {
                        return t.DataConclusao is not null;
                    }
                    return t.DataConclusao is null;
                })
                .WithMessage("Data de conclusao é obrigatoria para tarefas concluidas");

        }
    }
}
