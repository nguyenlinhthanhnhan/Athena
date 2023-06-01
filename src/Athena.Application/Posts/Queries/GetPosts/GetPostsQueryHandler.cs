using Athena.Application.Categories.Queries.GetCategories;
using Athena.Application.DTOs.Post;
using Athena.DataAccess.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Athena.Application.Posts.Queries.GetPosts;

public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, PostsVm?>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public GetPostsQueryHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostsVm?> Handle(GetPostsQuery request, CancellationToken cancellationToken)
    {
        var vm = new PostsVm
        {
            Lists = await _context.Posts
                .ProjectTo<ViewPostDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };

        var serializedCategories = JsonConvert.SerializeObject(vm);
        vm = JsonConvert.DeserializeObject<PostsVm>(serializedCategories);

        return vm;
    }
}