namespace Athena.Shared.DTOs;

public class PageMetaDto
{
    public long TotalItems { get; set; }
    
    public long Page { get; set; }
    
    public long Limit { get; set; }
    
    public long TotalPage => (long) Math.Ceiling((double) TotalItems / Limit);
    
    public bool HasNextPage => Page < TotalPage;
    
    public bool HasPreviousPage => Page > 1;
}