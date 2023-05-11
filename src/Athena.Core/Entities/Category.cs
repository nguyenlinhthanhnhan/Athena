using Athena.Core.Common;
using Athena.Core.Common.Interfaces;

namespace Athena.Core.Entities;

/// <summary>
/// Category entity.
/// </summary>
public class Category : AuditedEntity<int>
{
    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }
}