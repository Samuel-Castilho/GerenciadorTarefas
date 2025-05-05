using GerenciadorTarefas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Application.Models
{
    public class ResponseCreateUpdateDeletedDto
    {
        public List<TarefaResponseDto> Successes { get; set; } = new();
        public List<TarefaResponseDto> Fails { get; set; } = new();
        public (int all, int pendente, int emProgresso, int concluida) CountByStatus { get; set; }

        bool Success => Fails.Count == 0;    
    }


    public class TarefaResponseDto
    {
        Tarefa Item { get; set; }
        string Message { get; set; }

        public TarefaResponseDto(Tarefa item, string message)
        {
            Item = item;
            Message = message;
        }

    }
}
