using GerenciadorTarefas.Domain.Constants;
using GerenciadorTarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Infrastructure.Persistence.TypeConfiguration
{
    internal class TarefaTypeConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Titulo).HasMaxLength(TarefaConstants.TituloMaxLengh).IsRequired();
            builder.Property(p => p.DataCriacao).IsRequired();

           

        }
    }
}
