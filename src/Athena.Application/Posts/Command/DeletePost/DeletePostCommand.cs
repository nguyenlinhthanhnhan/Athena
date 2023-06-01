using MediatR;

namespace Athena.Application.Posts.Command.DeletePost;

public class DeletePostCommand : IRequest<Unit>
{
    public long Id { get; set; }
}