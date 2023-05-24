namespace Athena.Shared.DTO;

public class PageOptionDto
{
    public int Page { get; set; } = 1;
    
    public int Limit { get; set; } = 10;
    
    public int Skip => (Page - 1) * Limit;
}