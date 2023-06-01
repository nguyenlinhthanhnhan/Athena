using Athena.Application.Commons.Mappings;
using Athena.Core.Entities;
using MediatR;

namespace Athena.Application.Posts.Command.CreatePost;

public class CreatePostCommand : IRequest<long>, IMapTo<Post>
{
    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }

    public string? ShortDescription { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageAlt { get; set; }

    public bool IsPublished { get; set; } = false;
    
    public string? CategoryIds { get; set; }
}