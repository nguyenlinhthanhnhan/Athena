using Athena.Core.Common.Interfaces;

namespace Athena.Core.Common;

/// <summary>
/// This class is used to simplify implementing <see cref="IAudited"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AuditedEntity<T> : Entity<T>, IAudited where T : struct
{
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    
    public long? CreatorUserId { get; set; }
    
    public DateTime? LastModificationTime { get; set; }
    
    public long? LastModifierUserId { get; set; }
}