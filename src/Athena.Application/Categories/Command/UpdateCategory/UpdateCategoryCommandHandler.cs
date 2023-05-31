using Athena.Application.Commons.Exceptions;
using Athena.Core.Entities;
using Athena.DataAccess.Extensions;
using Athena.DataAccess.Persistence;
using MediatR;

namespace Athena.Application.Categories.Command.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly AthenaDbContext _context;

    public UpdateCategoryCommandHandler(AthenaDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _context.Categories.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Category), request.Id);
        }
        
        ObjectExtensions.UpdateModifiedFields(ref entity, request);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}