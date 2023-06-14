namespace Athena.Shared.CQRS.QueryModels.Category;

public interface IGetCategoriesQuery
{
    string? Title { get; set; }
}