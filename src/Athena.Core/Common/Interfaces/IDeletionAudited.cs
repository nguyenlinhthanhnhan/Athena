namespace Athena.Core.Common.Interfaces;

/// <summary>
/// This interface is implemented entities those are wanted to store deletion information (who and when deleted?).
/// </summary>
public interface IDeletionAudited : ISoftDelete
{
    DateTime? DeletionTime { get; set; }
    long? DeleterUserId { get; set; }
}