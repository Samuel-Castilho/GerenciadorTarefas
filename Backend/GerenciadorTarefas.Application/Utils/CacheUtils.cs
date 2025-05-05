using GerenciadorTarefas.Domain.Enums;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Application.Utils
{
    public static class CacheUtils
    {
        public const string CacheKeyCountByStatusTituloPrefix = "CountByStatusTitulo";
        public const string CacheKeyCountByStatusTarefaPrefix = "CountByStatusTarefa";
        public static string CacheKeyCountByStatusTitulo(ETarefaStatus? status,string? titulo) => $"{CacheKeyCountByStatusTituloPrefix}-{(status == null?string.Empty : status)},{titulo??string.Empty}";
        public static string CacheKeyCountByStatusTarefa(ETarefaStatus? status) => $"{CacheKeyCountByStatusTarefaPrefix}-{(status == null?string.Empty : status)}";



        public static void RemoveCacheStartsWith(this IMemoryCache imc,string prefix)
        {

            if(imc is MemoryCache mc)
            {
                foreach (var item in mc.Keys.Where(w => ((string)w).StartsWith(prefix)))
                {
                    mc.Remove(item);
                }
            }
        }


    }
}
