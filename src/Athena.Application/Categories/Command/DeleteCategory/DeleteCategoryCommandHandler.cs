using Athena.Application.Commons.Exceptions;
using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using MediatR;

namespace Athena.Application.Categories.Command.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly AthenaDbContext _context;

    public DeleteCategoryCommandHandler(AthenaDbContext context)
    {
        _context = context;
    }

    public async Task<Unit>  Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories.FindAsync(new object?[]{request.Id}, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }

        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}