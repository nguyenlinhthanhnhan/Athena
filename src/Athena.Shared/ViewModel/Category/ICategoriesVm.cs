namespace Athena.Shared.ViewModel.Category;

public interface ICategoriesVm<T> where T : IViewCategoryDto
{
    IList<T> Lists { get; set; }
}