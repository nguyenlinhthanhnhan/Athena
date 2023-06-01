using Athena.DataAccess.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Athena.Application.Posts.Queries.GetPost;

public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostVm?>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public GetPostQueryHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostVm?> Handle(GetPostQuery request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .ProjectTo<PostVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);

        return post;
    }
}