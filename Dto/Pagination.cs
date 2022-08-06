namespace Sneaker_Shop_API.Dto;

public class Page<T>
{
    public Page(IEnumerable<T> content, int page, int size, int totalCount, int currentCount)
    {
        Content = content;
        CurrentPage = page;
        ItemsPerPage = size;
        TotalItems = totalCount;
        ItemsCount = currentCount;
        TotalPages = (int)Math.Ceiling(totalCount / (decimal)size);
    }

    public IEnumerable<T> Content { get; set; }
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int ItemsCount { get; set; }
    public int TotalPages { get; set; }
}

public record PaginationParams(int Page = 0, int Size = 10);