using Athena.DataAccess.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Athena.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryVm?>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryVm?> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .ProjectTo<CategoryVm>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        return category;
    }
}