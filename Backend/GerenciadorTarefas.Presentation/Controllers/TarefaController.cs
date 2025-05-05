using GerenciadorTarefas.Application.Models;
using GerenciadorTarefas.Application.Services.Interfaces;
using GerenciadorTarefas.Domain.Entities;
using GerenciadorTarefas.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GerenciadorTarefas.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        ITarefaCRUDService _tcs;
        ILogger<TarefaController> _logger;
        public TarefaController(ITarefaCRUDService tcs, ILogger<TarefaController> logger)
        {
            _tcs = tcs;
            _logger = logger;
        }

        // GET: api/<TarefaController>
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery(Name = "reference")] int reference,
            [FromQuery(Name = "pageSize")] int pageSize,
            [FromQuery(Name = "direction")] EPaginatedDirectionEnum direction,
            [FromQuery(Name = "currentPage")] int currentPage,
            [FromQuery(Name = "status")] ETarefaStatus? status,
            [FromQuery(Name = "titulo")] string? titulo
            )
        {
            if (reference is < 0 || pageSize is < 1 or > 20 || currentPage is < 0)
            {
                _logger.LogInformation("Erro ao processar QueryParams");
                return BadRequest(
                        new ProblemDetails()
                        {
                            Title = "Erro ao processar QueryParams",
                            Detail = "Query Parameter invalido",
                            Status = 400
                        }
                    );
            }

            var response = await _tcs.GetPaginatedTarefaAsync(reference, pageSize, direction, currentPage, status, titulo);

            return Ok(response);
        }

        // GET api/<TarefaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var tarefa = await _tcs.GetbyIdAsync(id);

            if (tarefa is null)
            {
                return NotFound($"Registro '{id}' não encontrado");
            }

            return Ok(tarefa);
        }

        // POST api/<TarefaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<Tarefa> tarefas)
        {

            ResponseCreateUpdateDeletedDto response = await _tcs.CreateAsync(tarefas);


            return Ok(response);
        }

        // PUT api/<TarefaController>
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] IEnumerable<Tarefa> tarefas)
        {
            ResponseCreateUpdateDeletedDto response = await _tcs.UpdateAsync(tarefas);

            return Ok(response);
        }

        // DELETE api/<TarefaController
        [HttpDelete()]
        public async Task<IActionResult> Delete([FromQuery(Name = "ids")] string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                _logger.LogInformation("Erro ao processar QueryParams, nenhum registro pra ser deletado");
                return BadRequest(
                        new ProblemDetails()
                        {
                            Title = "Erro ao processar QueryParams",
                            Detail = "Query Parameter invalido",
                            Status = 400
                        }
                    );
            }

            IEnumerable<int> idsToDelete = [];
            try
            {
                idsToDelete = ids.Split(',').Select(s => Convert.ToInt32(s));
            }
            catch
            {
                return BadRequest(
                        new ProblemDetails()
                        {
                            Title = "Erro ao processar QueryParams",
                            Detail = "Query Parameter invalido, incapaz de converter para lista de IDs",
                            Status = 400
                        }
                    );
            }

            ResponseCreateUpdateDeletedDto response =  await _tcs.DeleteAsync(idsToDelete);

            return Ok(response);
        }
        
        [HttpGet("GetCountByStatus")]
        public async Task<IActionResult> GetCountByStatuc()
        {
            (int all, int pendente, int emProgresso, int concluida) response = await _tcs.GetCountByStatucAsync();
            return Ok(response);
        }
    }
}
