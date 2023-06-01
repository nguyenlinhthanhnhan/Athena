using Athena.Application.Commons.Exceptions;
using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Athena.Application.Posts.Command.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, long>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public CreatePostCommandHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<long> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<Post>(request);

        _context.Posts.Add(post);

        await _context.SaveChangesAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(request.CategoryIds)) return post.Id;
        
        var categoryIds = request.CategoryIds.Split(',').Select(int.Parse).ToArray();

        var categories = await _context.Categories.Where(c => categoryIds.Contains(c.Id))
            .ToListAsync(cancellationToken);

        if(categories.Count != categoryIds.Length)
            throw new BadRequestException("One or more category ids are invalid");
        
        var postCategories = categoryIds.Select(categoryId => new PostCategory
        {
            PostId = post.Id,
            CategoryId = categoryId
        }).ToList();
        
        await _context.PostCategories.AddRangeAsync(postCategories, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}