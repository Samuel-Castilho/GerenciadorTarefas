using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorTarefas.Application.Models
{

    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Items { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public PaginatedResponse(IEnumerable<T> items, int count, int pageNumber, int pageSize, bool hasPrevious, bool hasNext)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
            HasPreviousPage = hasPrevious;
            HasNextPage = hasNext;
        }

        public bool HasPreviousPage { get; }

        public bool HasNextPage { get; }

    }
}
