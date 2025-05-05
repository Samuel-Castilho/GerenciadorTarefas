using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enums;
using MR.EntityFrameworkCore.KeysetPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Infrastructure.Repositories.Interfaces
{
    public interface ITarefaRepository
    {
        public Task<IEnumerable<Tarefa>> CreateAsync(IEnumerable<Tarefa> tarefas);
        public Task<IEnumerable<Tarefa>> UpdateAsync(IEnumerable<Tarefa> tarefas);
        public Task<Tarefa> GetByIdAsync(int id);
        public Task<(IEnumerable<Tarefa> tarefas, bool hasPrevious, bool hasNext)> GetPaginatedByKeySetAsync(int referencia, int quantidade, int direcao, ETarefaStatus? status = null, string titulo = null);
        public Task<int> GetCountByStatusTituloAsync(ETarefaStatus? status = null, string titulo = null);
        public Task<int> DeleteAsync(IEnumerable<int> ids);

    }
}
