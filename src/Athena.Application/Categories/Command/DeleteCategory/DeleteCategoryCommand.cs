using MediatR;

namespace Athena.Application.Categories.Command.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Unit>
{
    public int Id { get; set; }
}