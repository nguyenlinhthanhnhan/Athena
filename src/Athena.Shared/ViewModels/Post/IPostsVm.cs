namespace Athena.Shared.ViewModels.Post;

public interface IPostsVm<T> where T: IViewPostDto
{
    IList<T> Lists { get; set; }
}