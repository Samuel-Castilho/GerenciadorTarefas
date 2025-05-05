using GerenciadorTarefas.Client.Models;
using System.Text;
using System.Text.Json;
using static GerenciadorTarefas.Client.Pages.Home;

namespace GerenciadorTarefas.Client.Services
{
    public class TarefaHttpClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions jsonOptions;


        public TarefaHttpClient(HttpClient client)
        {
            _client = client;
            jsonOptions = new JsonSerializerOptions()
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
            };
        }


        public async Task<PaginatedTarefa> GetAsync(int reference,int pageSize,EDirection direction,ETarefaStatus? status,string? titulo,int currentPage)
        {
            HttpResponseMessage? response  = await _client.GetAsync($"Tarefa?reference={reference}&pageSize={pageSize}&direction={(int)direction}&currentPage={currentPage}{(status != null ? $"&status={status}" : "")}{(titulo != null ? $"&titulo={titulo}" : "")}");

            string content = await response.Content.ReadAsStringAsync();
                
            PaginatedTarefa pagTaf = System.Text.Json.JsonSerializer.Deserialize<PaginatedTarefa>(content, jsonOptions);

            return pagTaf;

        }

        public async Task<TarefaViewModel> GetByIdAsync(int id)
        {
            HttpResponseMessage? response = await _client.GetAsync($"Tarefa/{id}");

            string content = await response.Content.ReadAsStringAsync();

            TarefaViewModel tarefa = System.Text.Json.JsonSerializer.Deserialize<TarefaViewModel>(content, jsonOptions);

            return tarefa;

        }

        public async Task<ResponseCreateUpdateDeleteViewModel> CreateAsync(IEnumerable<TarefaViewModel> tarefas)
        {
            StringContent requestContent = new(System.Text.Json.JsonSerializer.Serialize(tarefas), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Tarefa", requestContent);

            string responseContent = await response.Content.ReadAsStringAsync();

            ResponseCreateUpdateDeleteViewModel created = System.Text.Json.JsonSerializer.Deserialize<ResponseCreateUpdateDeleteViewModel>(responseContent, jsonOptions);

            return created;

        }

        public async Task<ResponseCreateUpdateDeleteViewModel> UpdateAsync(IEnumerable<TarefaViewModel> tarefas)
        {
            StringContent requestContent = new(System.Text.Json.JsonSerializer.Serialize(tarefas), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("Tarefa", requestContent);

            string responseContent = await response.Content.ReadAsStringAsync();

            ResponseCreateUpdateDeleteViewModel updated = System.Text.Json.JsonSerializer.Deserialize<ResponseCreateUpdateDeleteViewModel>(responseContent, jsonOptions);

            return updated;

        }

        public async Task<ResponseCreateUpdateDeleteViewModel> DeleteAsync(IEnumerable<int> tarefasIds)
        {
            var response = await _client.DeleteAsync($"Tarefa?ids={string.Join(',',tarefasIds)}");

            string responseContent = await response.Content.ReadAsStringAsync();

            ResponseCreateUpdateDeleteViewModel updated = System.Text.Json.JsonSerializer.Deserialize<ResponseCreateUpdateDeleteViewModel>(responseContent, jsonOptions);

            return updated;

        }

        public async Task<(int all, int pendente, int emProgresso, int concluida)> GetCountByStatus()
        {
            HttpResponseMessage? response = await _client.GetAsync($"Tarefa/GetCountByStatus");

            string content = await response.Content.ReadAsStringAsync();

            (int all, int pendente, int emProgresso, int concluida) tarefa = System.Text.Json.JsonSerializer.Deserialize<(int all, int pendente, int emProgresso, int concluida)>(content, jsonOptions);

            return tarefa;

        }

    }
}
