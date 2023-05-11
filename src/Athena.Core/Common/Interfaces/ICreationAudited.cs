namespace Athena.Core.Common.Interfaces;

/// <summary>
/// This interface is implemented by entities that is wanted to store creation information (who and when created).
/// </summary>
public interface ICreationAudited : IHasCreationTime
{
    long? CreatorUserId { get; set; }
}