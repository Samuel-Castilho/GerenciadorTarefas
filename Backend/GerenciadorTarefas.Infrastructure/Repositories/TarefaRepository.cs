using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enums;
using GerenciadorTarefas.Infrastructure.Persistence.Context;
using GerenciadorTarefas.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MR.EntityFrameworkCore.KeysetPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Infrastructure.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        GerenciadorTarefasContext _db;
        public TarefaRepository(GerenciadorTarefasContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Tarefa>> CreateAsync(IEnumerable<Tarefa> tarefas)
        {
            _db.Tarefas.AddRange(tarefas);
            await _db.SaveChangesAsync();

            return tarefas;
        }

        public async Task<IEnumerable<Tarefa>> UpdateAsync(IEnumerable<Tarefa> tarefas)
        {
            foreach (var item in tarefas)
            {
                _db.Tarefas.Update(item);
                _db.Tarefas.Entry(item).Property(p=> p.DataCriacao).IsModified = false;
            }
            await _db.SaveChangesAsync();

            return tarefas;
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            Tarefa tarefa = await _db.Tarefas.FirstOrDefaultAsync(f => f.Id == id);

            return tarefa;
        }

        public async Task<(IEnumerable<Tarefa> tarefas, bool hasPrevious, bool hasNext)> GetPaginatedByKeySetAsync(int referencia, int quantidade, int direcao, ETarefaStatus? status = null, string titulo = null)
        {
            KeysetPaginationDirection direction = (KeysetPaginationDirection)direcao;

                

            IQueryable<Tarefa> qTarefa = _db.Tarefas;

            if (status is not null && Enum.IsDefined<ETarefaStatus>((ETarefaStatus)status))
            {
                qTarefa = qTarefa.Where(w => w.Status == status);
            }

            if (titulo is not null)
            {
                qTarefa = qTarefa.Where(w => w.Titulo.Contains(titulo));
            }

            var keysetContext = qTarefa.KeysetPaginate(
                        b => b.Ascending(t => t.Id),
                        direction,
                        new { Id = referencia }
                );

            var tarefas = await keysetContext.Query
                .Take(quantidade)
                .ToListAsync();


            keysetContext.EnsureCorrectOrder(tarefas);

            var hasPrevious = await keysetContext.HasPreviousAsync(tarefas);

            var hasNext = await keysetContext.HasNextAsync(tarefas);


            return (tarefas, hasPrevious, hasNext);
        }

        public async Task<int> GetCountByStatusTituloAsync(ETarefaStatus? status = null, string titulo = null)
        {

            IQueryable<Tarefa> qCount = _db.Tarefas.AsQueryable();

            if (status is not null && Enum.IsDefined<ETarefaStatus>((ETarefaStatus)status))
            {
                qCount = qCount.Where(w => w.Status == status);
            }

            if (titulo is not null)
            {
                qCount = qCount.Where(w => w.Titulo.Contains(titulo));
            }

            int count = await qCount.CountAsync();

            return count;
        }


        public async Task<int> DeleteAsync(IEnumerable<int> ids)
        {
            int deleted = await  _db.Tarefas.Where(w=> ids.Contains(w.Id)).ExecuteDeleteAsync();

            return deleted;
        }


    }
}
