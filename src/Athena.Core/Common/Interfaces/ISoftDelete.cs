namespace Athena.Core.Common.Interfaces;

/// <summary>
/// Interface to soft delete a entity from database instead of hard delete.
/// </summary>
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}