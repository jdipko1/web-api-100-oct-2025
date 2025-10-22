namespace Shows.Api.Models;

public class CollectionResponseModel<T>
{
    public IList<T> Data { get; set; } = new List<T>();
}

public record ShowSummaryItem
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StreamingService { get; set; } = string.Empty;
}