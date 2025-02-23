using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Hospital.Controllers;

public class PaginatedList<T> : List<T> {
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public bool HasPreviousPage {
        get {
            return PageIndex > 1;
        }
    }
    public bool HasNextPage {
        get {
            return PageIndex < TotalPages;
        }
    }

    public PaginatedList(List<T> items, int count, int index, int size) {
        PageIndex = index;
        TotalPages = (int)Math.Ceiling(count / (double)size);
        AddRange(items);
    }

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int index, int size) {
        int count = await source.CountAsync();
        List<T> items = await source.Skip((index - 1) * size).Take(size).ToListAsync();
        return new PaginatedList<T>(items, count, index, size);
    }
}