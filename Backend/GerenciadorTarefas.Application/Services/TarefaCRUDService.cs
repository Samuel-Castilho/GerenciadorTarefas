using GerenciadorTarefas.Application.Models;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Infrastructure.Persistence.Context;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorTarefas.Domain.Enums;
using GerenciadorTarefas.Application.Services.Interfaces;
using GerenciadorTarefas.Application.Utils;
using GerenciadorTarefas.Application.Validators;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Azure;

namespace GerenciadorTarefas.Application.Services
{
    public class TarefaCRUDService : ITarefaCRUDService
    {
        IMemoryCache _mc;
        GerenciadorTarefasContext _db;
        ITarefaRepository _tr;

        public TarefaCRUDService(IMemoryCache mc, GerenciadorTarefasContext db, ITarefaRepository tr)
        {
            _mc = mc;
            _db = db;
            _tr = tr;
        }

        public async Task<PaginatedResponse<Tarefa>> GetPaginatedTarefaAsync(int reference, int pageSize, EPaginatedDirectionEnum direction,int currentPage, ETarefaStatus? status = null, string titulo = null)
        {

            var paginatedTuple = await _tr.GetPaginatedByKeySetAsync(reference, pageSize, (int)direction, status, titulo);

            int total = await _mc.GetOrCreateAsync(
                CacheUtils.CacheKeyCountByStatusTitulo(status,titulo),
                async ce =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    return await _tr.GetCountByStatusTituloAsync(status, titulo);
                }
            );

            int newPage = direction == EPaginatedDirectionEnum.Forward ? currentPage + 1 : currentPage - 1;

            PaginatedResponse<Tarefa> response = new PaginatedResponse<Tarefa>(paginatedTuple.tarefas, total, newPage, pageSize, paginatedTuple.hasPrevious, paginatedTuple.hasNext);

            return response;
        }

        public async Task<Tarefa?> GetbyIdAsync(int id)
        {
            Tarefa? tarefa = await _tr.GetByIdAsync(id);

            return tarefa;
        }


        public async Task<ResponseCreateUpdateDeletedDto> CreateAsync(IEnumerable<Tarefa> tarefas)
        {
            
            TarefaValidator validator = new();
            ValidationResult vr;
            ResponseCreateUpdateDeletedDto response = new ResponseCreateUpdateDeletedDto();
            List<Tarefa> tarefasToCreate = new();


            foreach (Tarefa tf in tarefas)
            {
                vr = validator.Validate(tf);
                if(vr.IsValid == false)
                {
                    response.Fails.Add(new TarefaResponseDto(tf, string.Join('\n', vr.Errors.Select(s => s.ErrorMessage))));
                }
                else
                {
                    tf.DataCriacao = DateTime.UtcNow;
                    tarefasToCreate.Add(tf);
                }
                
            }

            IEnumerable<Tarefa> created = [];
            await this.ExecuteInTransaction(async () =>
            {
                created = await _tr.CreateAsync(tarefasToCreate);
            });

            foreach (var tf in created)
            {
                response.Successes.Add(new TarefaResponseDto(tf, $"Tarefa criada com Id {tf.Id}"));
            }
            _mc.RemoveCacheStartsWith(CacheUtils.CacheKeyCountByStatusTituloPrefix);
            _mc.RemoveCacheStartsWith(CacheUtils.CacheKeyCountByStatusTarefaPrefix);

            response.CountByStatus = await this.GetCountByStatucAsync();
            return response;
        }


        public async Task<ResponseCreateUpdateDeletedDto> UpdateAsync(IEnumerable<Tarefa> tarefas)
        {
            TarefaValidator validator = new();
            ValidationResult vr;
            ResponseCreateUpdateDeletedDto response = new ResponseCreateUpdateDeletedDto();
            List<Tarefa> tarefasToUpdate = new();


            foreach (Tarefa tf in tarefas)
            {
                vr = validator.Validate(tf);
                if (vr.IsValid == false)
                {
                    response.Fails.Add(new TarefaResponseDto(tf, string.Join('\n', vr.Errors.Select(s => s.ErrorMessage))));
                }
                else
                {
                    tarefasToUpdate.Add(tf);
                }

            }

            IEnumerable<Tarefa> updated = [];
            await this.ExecuteInTransaction(async () =>
            {
                updated = await _tr.UpdateAsync(tarefasToUpdate); 
            });
            

            foreach (var tf in updated)
            {
                response.Successes.Add(new TarefaResponseDto(tf, $"Tarefa atualizada com sucesso"));
            }

            _mc.RemoveCacheStartsWith(CacheUtils.CacheKeyCountByStatusTituloPrefix);
            _mc.RemoveCacheStartsWith(CacheUtils.CacheKeyCountByStatusTarefaPrefix);

            response.CountByStatus = await this.GetCountByStatucAsync();
            return response;
        }

        public async Task<ResponseCreateUpdateDeletedDto> DeleteAsync(IEnumerable<int> tarefas)
        {
            ResponseCreateUpdateDeletedDto response = new ResponseCreateUpdateDeletedDto();

            int deleted = 0;
            await this.ExecuteInTransaction(async () =>
            {
                deleted = await _tr.DeleteAsync(tarefas);
            });

            _mc.RemoveCacheStartsWith(CacheUtils.CacheKeyCountByStatusTituloPrefix);
            _mc.RemoveCacheStartsWith(CacheUtils.CacheKeyCountByStatusTarefaPrefix);
            response.CountByStatus = await this.GetCountByStatucAsync();
            return response;

        }

        public async Task<(int all, int pendente, int emProgresso, int concluida)> GetCountByStatucAsync()
        {
            int total = await _mc.GetOrCreateAsync(
                CacheUtils.CacheKeyCountByStatusTarefa(null),
                async ce =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    return await _tr.GetCountByStatusTituloAsync(null, null);
                }
            );
            int pendente = await _mc.GetOrCreateAsync(
                CacheUtils.CacheKeyCountByStatusTarefa(ETarefaStatus.Pendente),
                async ce =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    return await _tr.GetCountByStatusTituloAsync(ETarefaStatus.Pendente);
                }
            );
            int emProgresso = await _mc.GetOrCreateAsync(
                CacheUtils.CacheKeyCountByStatusTarefa(ETarefaStatus.EmProgresso),
                async ce =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    return await _tr.GetCountByStatusTituloAsync(ETarefaStatus.EmProgresso, null);
                }
            );
            int concluida = await _mc.GetOrCreateAsync(
                CacheUtils.CacheKeyCountByStatusTarefa(ETarefaStatus.Concluida),
                async ce =>
                {
                    ce.SlidingExpiration = TimeSpan.FromMinutes(5);
                    return await _tr.GetCountByStatusTituloAsync(ETarefaStatus.Concluida, null);
                }
            );

            return (total, pendente, emProgresso, concluida);
        }






        private async Task ExecuteInTransaction(Func<Task> asyncAction)
        {
            await _db.Database.BeginTransactionAsync();
            await asyncAction.Invoke();
            await _db.Database.CommitTransactionAsync();
        }
    }
}
