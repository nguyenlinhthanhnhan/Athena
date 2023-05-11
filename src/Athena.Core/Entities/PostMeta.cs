using Athena.Core.Common;
using Athena.Core.Common.Interfaces;

namespace Athena.Core.Entities;

/// <summary>
/// PostMeta entity.
/// </summary>
public class PostMeta : AuditedEntity<Guid>
{
    public Post Post { get; set; } = default!;

    public string? Key { get; set; }

    public string? Value { get; set; }
}