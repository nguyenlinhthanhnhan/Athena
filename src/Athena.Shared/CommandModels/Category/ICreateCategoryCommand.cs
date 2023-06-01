namespace Athena.Shared.CommandModels.Category;

public interface ICreateCategoryCommand
{
    string? Title { get; set; }

    string? MetaTitle { get; set; }

    string? Slug { get; set; }

    string? Content { get; set; }
}