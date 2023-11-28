namespace SpendWise.Shared.Abstraction.Queries;

public abstract class PagedQuery : IPagedQuery
{
    public int Page { get; set; } = 1;
    public int Results { get; set; } = 10;
    public string OrderBy { get; set; }
    public string SortOrder { get; set; }
}
    
public abstract class PagedQuery<T> : PagedQuery, IPagedQuery<Paged<T>>
{
}