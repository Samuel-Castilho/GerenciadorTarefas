namespace GerenciadorTarefas.Client.Models
{

    public class TarefaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public ETarefaStatus Status { get; set; }
    }

    public enum ETarefaStatus : sbyte
    {
        Pendente = 0,
        EmProgresso = 1,
        Concluida = 2,
    }
}
