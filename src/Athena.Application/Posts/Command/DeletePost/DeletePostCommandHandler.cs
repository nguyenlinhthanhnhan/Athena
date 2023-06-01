using Athena.Application.Commons.Exceptions;
using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using MediatR;

namespace Athena.Application.Posts.Command.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
{
    private readonly AthenaDbContext _context;

    public DeletePostCommandHandler(AthenaDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Posts.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
        
        if (entity == null)
        {
            throw new NotFoundException(nameof(Post), request.Id);
        }
        
        _context.Posts.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}