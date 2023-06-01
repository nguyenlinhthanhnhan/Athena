using Athena.Application.Commons.Exceptions;
using Athena.Core.Entities;
using Athena.DataAccess.Extensions;
using Athena.DataAccess.Persistence;
using AutoMapper;
using MediatR;

namespace Athena.Application.Posts.Command.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
{
    private readonly AthenaDbContext _context;

    public UpdatePostCommandHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }

        ObjectExtensions.UpdateModifiedFields(ref entity, request);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}