using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize) 
        {
            PageIndex = pageIndex;
            TotalPage = (count + pageSize - 1)/ pageSize;
            AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, Pagination pagination)
        {
            int count = source.Count();
            var items = source.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
            return new PaginatedList<T>(items, count, pagination.PageNumber, pagination.PageSize);
        }
    }
}
