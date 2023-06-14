namespace Athena.Shared.CQRS.CommandModels.Category;

public interface IUpdateCategoryCommand
{
    int Id { get; set; }

    string? Title { get; set; }

    string? MetaTitle { get; set; }

    string? Slug { get; set; }

    string? Content { get; set; }
}