namespace Athena.Shared.DTOs.Category;

public interface IViewCategoryDto
{
    int Id { get; set; }
    
    string? Title { get; set; }

    string? MetaTitle { get; set; }

    string? Slug { get; set; }

    string? Content { get; set; }
}