using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Athena.Core.Entities;

/// <summary>
/// Many-to-many relationship between <see cref="Post"/> and <see cref="Tag"/>.
/// </summary>
[PrimaryKey(nameof(PostId), nameof(TagId))]
public class PostTag
{
    public long PostId { get; set; }
    public Post Post { get; set; } = default!;
    
    public int TagId { get; set; }
    public Tag Tag { get; set; } = default!;
}