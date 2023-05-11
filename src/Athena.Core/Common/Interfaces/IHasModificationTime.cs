namespace Athena.Core.Common.Interfaces;

/// <summary>
/// Interface to store modification date for a entity.
/// </summary>
public interface IHasModificationTime
{
    DateTime? LastModificationTime { get; set; }
}