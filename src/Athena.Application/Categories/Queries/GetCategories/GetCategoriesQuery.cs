using Athena.Shared.CQRS.QueryModels.Category;
using Athena.Shared.DTOs;
using MediatR;

namespace Athena.Application.Categories.Queries.GetCategories;

public class GetCategoriesQuery : PageOptionDto, IGetCategoriesQuery, IRequest<CategoriesVm>
{
    public string? Title { get; set; }
}