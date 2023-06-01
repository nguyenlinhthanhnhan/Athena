using Athena.Shared.CommandModels.Category;
using MediatR;

namespace Athena.Application.Categories.Command.DeleteCategory;

public class DeleteCategoryCommand : IDeleteCategoryCommand, IRequest<Unit>
{
    public int Id { get; set; }
}