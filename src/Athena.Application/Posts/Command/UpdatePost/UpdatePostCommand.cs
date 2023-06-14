using Athena.Application.Commons.Mappings;
using Athena.Core.Entities;
using Athena.Shared.CQRS.CommandModels.Post;
using MediatR;

namespace Athena.Application.Posts.Command.UpdatePost;

public class UpdatePostCommand : IUpdatePostCommand, IRequest<Unit>, IMapTo<Post>
{
    public long Id { get; set; }

    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }

    public string? ShortDescription { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageAlt { get; set; }

    public bool IsPublished { get; set; }
}