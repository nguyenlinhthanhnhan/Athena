using Athena.Core.Common;
using Athena.Core.Common.Interfaces;

namespace Athena.Core.Entities;

/// <summary>
/// Tag entity
/// </summary>
public class Tag : AuditedEntity<int>
{
    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }
}