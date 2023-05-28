namespace Athena.Shared.DTOs;

public class PageDto<T> where T : class
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    
    public PageMetaDto Meta { get; set; } = new();
}