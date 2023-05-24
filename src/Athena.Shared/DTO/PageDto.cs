namespace Athena.Shared.DTO;

public class PageDto<T> where T : class
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    
    public PageMetaDto Meta { get; set; } = new();
}