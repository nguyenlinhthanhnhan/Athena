namespace Athena.Shared.CQRS.CommandModels.Post;

public interface IDeletePostCommand
{
    long Id { get; set; }
}