namespace GerenciadorTarefas.Client.Models
{

    public class ResponseCreateUpdateDeleteViewModel
    {
        public List<TarefaResponseDto> Successes { get; set; } = new();
        public List<TarefaResponseDto> Fails { get; set; } = new();
        public (int all, int pendente, int emProgresso, int concluida) CountByStatus { get; set; }

        bool Success => Fails.Count == 0;
    }


    public class TarefaResponseDto
    {
        TarefaViewModel Item { get; set; }
        string Message { get; set; }

        public TarefaResponseDto()
        {
            
        }

    }
}
