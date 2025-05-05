using GerenciadorTarefas.Domain.Enums;

namespace GerenciadorTarefas.Domain.Entities;

public class Tarefa
{
    public int Id { get; set; }
    public string Titulo{ get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao{ get; set; }
    public DateTime? DataConclusao{ get; set; }
    public ETarefaStatus Status { get; set; }
}


