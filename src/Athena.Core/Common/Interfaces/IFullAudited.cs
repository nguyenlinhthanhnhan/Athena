namespace Athena.Core.Common.Interfaces;

/// <summary>
/// Full audited entity interface which contains all audit fields and soft delete.
/// </summary>
public interface IFullAudited : IAudited, IDeletionAudited
{
}