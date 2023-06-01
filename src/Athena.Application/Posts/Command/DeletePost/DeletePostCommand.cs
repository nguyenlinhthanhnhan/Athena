using Athena.Shared.CommandModels.Post;
using MediatR;

namespace Athena.Application.Posts.Command.DeletePost;

public class DeletePostCommand : IDeletePostCommand, IRequest<Unit>
{
    public long Id { get; set; }
}