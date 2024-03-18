using BookStore.Application.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Extensions
{
    public static class PaginationExtension
    {
        private static int maxPageSize = 100;
        private static string paginationKey = "X-Pagination";

        public static List<T> ToPagedList<T>(
            this IQueryable<T> source,
            int pageSize = 10,
            int pageIndex = 1)
        {
            if (pageSize <= 0 || pageIndex <= 0)
            {
                throw new Exception(
                    "Page size or index should be greater than 0");
            }

            if (pageSize > maxPageSize)
            {
                throw new ValidationException(
                    $"Page size should be less than {maxPageSize}");
            }


            var paginationMetadata = new PaginationMetaData(
                totalCount: source.Count(),
                currentPage: pageIndex,
                pageSize: pageSize);

            return source
                .Skip(pageIndex - 1)
                .Take(pageSize)
                .ToList();
        }
    }
}
