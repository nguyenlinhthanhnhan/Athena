using Microsoft.EntityFrameworkCore;

namespace Athena.Core.Entities;

/// <summary>
/// Many-to-many relationship between <see cref="Post"/> and <see cref="Category"/>.
/// </summary>
[PrimaryKey(nameof(PostId), nameof(CategoryId))]
public class PostCategory
{
    public long PostId { get; set; }
    public Post Post { get; set; } = default!;

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
}