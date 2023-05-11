namespace Athena.Core.Common.Interfaces;

/// <summary>
/// Interface to store creation date for a entity.
/// </summary>
public interface IHasCreationTime
{
    DateTime CreationTime { get; set; }
}