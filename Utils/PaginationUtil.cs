using Microsoft.EntityFrameworkCore;
using Sneaker_Shop_API.Dto;

namespace Sneaker_Shop_API.Utils;

public class PaginationUtil
{
    public static async Task<Page<T>> Paginate<T>(IQueryable<T> queryable, int page, int size)
    {
        var result = await queryable
            .Skip(size * page)
            .Take(size)
            .ToListAsync();
        return new Page<T>(result, page, size, queryable.Count());
    }
}