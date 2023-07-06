using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using HD.Station.QuanLyTinTuc.Abstractions.Exceptions;
using HD.Station.QuanLyTinTuc.Abstractions.Helpers;

public static class DbSetExtensions
{
    public static async Task<TSource> FindAsync<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await source.Where(predicate).SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException();
    }

    public static async Task<PagedResponse<TSource>> PaginateAsync<TSource>(
        this IQueryable<TSource> source,
        PagedQuery query
    )
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip(query.Offset)
            .Take(query.Limit)
            .ToListAsync();

        return new PagedResponse<TSource>(items, count);
    }
}
