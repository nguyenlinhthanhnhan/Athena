using Athena.Application.DTOs.Post;
using Athena.Shared.ViewModel.Category;
using Athena.Shared.ViewModel.Post;

namespace Athena.Application.Posts.Queries.GetPosts;

public class PostsVm : IPostsVm<ViewPostDto>
{
    public IList<ViewPostDto> Lists { get; set; }
}