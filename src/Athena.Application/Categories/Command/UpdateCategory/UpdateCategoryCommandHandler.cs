using Athena.Application.Commons.Exceptions;
using Athena.Core.Entities;
using Athena.DataAccess.Extensions;
using Athena.DataAccess.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Athena.Application.Categories.Command.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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