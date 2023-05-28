using Athena.Application.DTOs.Category;
using Athena.Shared.ViewModel.Category;

namespace Athena.Application.Categories.Queries.GetCategories;

public class CategoriesVm : ICategoriesVm<ViewCategoryDto>
{
    public IList<ViewCategoryDto> Lists { get; set; }
}