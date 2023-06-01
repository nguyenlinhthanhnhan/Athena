using Athena.Application.Commons.Mappings;
using Athena.Shared.ViewModels.Post;

namespace Athena.Application.Posts.Queries.GetPost;

public class PostVm : IViewPostDto, IMapFrom<Core.Entities.Post>
{
    public long Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? MetaTitle { get; set; }
    
    public string? Slug { get; set; }
    
    public string? Content { get; set; }
    
    public string? ShortDescription { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public string? ImageAlt { get; set; }
}