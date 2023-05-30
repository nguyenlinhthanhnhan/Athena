using Athena.Application.Commons.Mappings;
using Athena.Core.Entities;
using Athena.Shared.ViewModel.Category;

namespace Athena.Application.Categories.Queries.GetCategory;

public class CategoryVm : IViewCategoryDto, IMapFrom<Category>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }
}