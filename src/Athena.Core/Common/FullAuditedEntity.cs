using Athena.Core.Common.Interfaces;

namespace Athena.Core.Common;

/// <summary>
/// This class is used to simplify implementing <see cref="IFullAudited"/>.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class FullAuditedEntity<T> : Entity<T>, IFullAudited where T : struct
{
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    
    public long? CreatorUserId { get; set; }
    
    public DateTime? LastModificationTime { get; set; }
    
    public long? LastModifierUserId { get; set; }

    public bool IsDeleted { get; set; } = false;
    
    public DateTime? DeletionTime { get; set; }
    
    public long? DeleterUserId { get; set; }
}