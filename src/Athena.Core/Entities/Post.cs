using Athena.Core.Common;
using Athena.Core.Common.Interfaces;

namespace Athena.Core.Entities;

/// <summary>
/// Post entity
/// </summary>
public class Post : FullAuditedEntity<long>
{
    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }

    public string? ShortDescription { get; set; }

    public string? ImageUrl { get; set; }

    public string? ImageAlt { get; set; }

    public bool IsPublished { get; set; }

    public ICollection<PostMeta>? PostMetas { get; set; }
}