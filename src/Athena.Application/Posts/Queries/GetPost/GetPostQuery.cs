using MediatR;

namespace Athena.Application.Posts.Queries.GetPost;

public class GetPostQuery : IRequest<PostVm?>
{
    public long Id { get; set; }
}