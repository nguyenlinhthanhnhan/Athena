namespace Athena.Core.Common.Interfaces;

/// <summary>
/// This interface is implemented by entities that is wanted to store creation and modification information (who and when created and changed lastly).
/// </summary>
public interface IAudited : ICreationAudited, IModificationAudited
{
}