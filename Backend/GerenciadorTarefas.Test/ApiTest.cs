using GerenciadorTarefas.Application.Models;
using GerenciadorTarefas.Domain.Entities;
using System.Numerics;
using System.Text;

namespace GerenciadorTarefas.Test
{
    public class ApiTest
    {
        HttpClient _client;
        ApiApplication _app;
        public ApiTest()
        {
            _app = new ApiApplication();
            _client = _app.CreateClient();
        }

        [Fact]
        public async Task TestEndpoint__api_Tarefa_GetCountByStatus()
        {
            var response = await _client.GetAsync("api/Tarefa/GetCountByStatus");

            string content = await response.Content.ReadAsStringAsync();
            var options = new System.Text.Json.JsonSerializerOptions()
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
            };
            (int total,int pendente,int emProgresso,int concluidas) CountByStatus =  System.Text.Json.JsonSerializer.Deserialize<(int, int, int, int)>(content, options);



            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestEndpointSuccessStatusCode__api_Tarefa_GetCountByStatus()
        {
            var response = await _client.GetAsync("api/Tarefa/GetCountByStatus");

            string content = await response.Content.ReadAsStringAsync();
            var options = new System.Text.Json.JsonSerializerOptions()
            {
                IncludeFields = true,
                PropertyNameCaseInsensitive = true,
            };
            (int total, int pendente, int emProgresso, int concluidas) CountByStatus = System.Text.Json.JsonSerializer.Deserialize<(int, int, int, int)>(content, options);



            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestEndpointSuccessStatusCode__api_Tarefa_Get()
        {
            var response = await _client.GetAsync("api/Tarefa?reference=0&pageSize=10&direction=0&currentPage=1");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestEndpointBadRequestStatusCode__api_Tarefa_Get()
        {
            var response = await _client.GetAsync("api/Tarefa?reference=0&pageSize=99&direction=0&currentPage=1");

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task TestEndpointSuccessStatusCode__api_Tarefa_GetById()
        {
            var response = await _client.GetAsync("api/Tarefa/1");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestEndpointNotFoundStatusCode__api_Tarefa_GetById()
        {
            var response = await _client.GetAsync("api/Tarefa/999");

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task TestEndpointSuccessStatusCode__api_Tarefa_Post()
        {
            IEnumerable<Tarefa> requestContent = [
                new Tarefa(){
                    Titulo="Tarefa teste",
                    Descricao = "Descrição da tarefa de teste",
                    Status = Domain.Enums.ETarefaStatus.EmProgresso
                },
                new Tarefa(){
                    Titulo="Tarefa teste2",
                    Descricao = "Descrição da tarefa de teste2",
                    DataConclusao = DateTime.UtcNow,
                    Status = Domain.Enums.ETarefaStatus.Concluida,
                    
                }
                ];
            StringContent content = new(System.Text.Json.JsonSerializer.Serialize(requestContent), Encoding.UTF8,"application/json");
            var response = await _client.PostAsync("api/Tarefa", content);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task TestEndpointBadRequestStatusCode__api_Tarefa_Post()
        {
            
            StringContent content = new("Lorem Ipsum");
            var response = await _client.PostAsync("api/Tarefa", content);

            Assert.True(!response.IsSuccessStatusCode);
        }
    }
}
