using Athena.Application.Commons.Mappings;

namespace Athena.Application.DTOs.Category;

public class ViewCategoryDto : IMapFrom<Core.Entities.Category>
{
    public int Id { get; set; }
    
    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }
}