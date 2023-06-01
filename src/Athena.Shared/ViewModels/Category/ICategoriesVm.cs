namespace Athena.Shared.ViewModels.Category;

public interface ICategoriesVm<T> where T : IViewCategoryDto
{
    IList<T> Lists { get; set; }
}