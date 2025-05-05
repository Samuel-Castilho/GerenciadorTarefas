namespace GerenciadorTarefas.Client.Models
{

    public class PaginatedTarefa
    {
        public List<TarefaViewModel> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public PaginatedTarefa()
        {
            
        }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

    }
}
