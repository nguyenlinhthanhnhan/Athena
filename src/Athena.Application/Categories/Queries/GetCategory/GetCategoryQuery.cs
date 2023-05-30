using MediatR;

namespace Athena.Application.Categories.Queries.GetCategory;

public class GetCategoryQuery : IRequest<CategoryVm>
{
    public int Id { get; set; }
}