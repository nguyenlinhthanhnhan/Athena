namespace Athena.Core.Common.Interfaces;

/// <summary>
/// Modification audited entity interface which contains last modifier user id and last modification time.
/// </summary>
public interface IModificationAudited : IHasModificationTime
{
    long? LastModifierUserId { get; set; }
}