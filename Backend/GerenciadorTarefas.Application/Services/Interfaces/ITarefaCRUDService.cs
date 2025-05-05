using GerenciadorTarefas.Application.Models;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Application.Services.Interfaces
{
    public interface ITarefaCRUDService
    {

        public Task<PaginatedResponse<Tarefa>> GetPaginatedTarefaAsync(int reference, int pageSize, EPaginatedDirectionEnum direction, int currentPage, ETarefaStatus? status = null, string titulo = null);
        public Task<Tarefa?> GetbyIdAsync(int id);
        public Task<ResponseCreateUpdateDeletedDto> CreateAsync(IEnumerable<Tarefa> tarefas);
        public Task<ResponseCreateUpdateDeletedDto> UpdateAsync(IEnumerable<Tarefa> tarefas);
        public Task<ResponseCreateUpdateDeletedDto> DeleteAsync(IEnumerable<int> tarefas);


        public Task<(int all,int pendente,int emProgresso,int concluida)> GetCountByStatucAsync();



    }
}
