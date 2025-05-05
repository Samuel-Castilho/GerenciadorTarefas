using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Domain.Enums
{
    public enum ETarefaStatus : sbyte
    {
        Pendente = 0,
        EmProgresso = 1,
        Concluida = 2,
    }
}
