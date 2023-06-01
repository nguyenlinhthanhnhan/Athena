namespace Athena.Shared.ViewModel.Post;

public interface IPostsVm<T> where T: IViewPostDto
{
    IList<T> Lists { get; set; }
}