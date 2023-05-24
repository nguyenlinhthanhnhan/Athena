using Athena.Application.DTOs.Category;

namespace Athena.Application.Categories.Queries.GetCategories;

public class CategoriesVm
{
    public IList<ViewCategoryDto> Lists { get; set; }
}