using Athena.Shared.DTOs;
using MediatR;

namespace Athena.Application.Categories.Queries.GetCategories;

public class GetCategoriesQuery : PageOptionDto, IRequest<CategoriesVm>
{
    public string? Title { get; set; }
}