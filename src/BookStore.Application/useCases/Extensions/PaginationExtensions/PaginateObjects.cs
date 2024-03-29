using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Application.useCases.Extensions.PaginationExtensions
{
    public class PaginateObjects<T>
    {
        public List<T> paginatedObjects { get; set; }
        public PaginateObjects(List<T> objects, int page, int pageSize)
        {
            int startIndex = (page - 1) * pageSize;
            int count = Math.Min(pageSize, objects.Count - startIndex);
            if (count < 1)
            {
                paginatedObjects = new List<T>();
            }
            else
            {
                paginatedObjects = objects.GetRange(startIndex, count);
            }
        }
    }
}
