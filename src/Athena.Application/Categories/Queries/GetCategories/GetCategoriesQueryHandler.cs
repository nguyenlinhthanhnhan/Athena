using Athena.Application.DTOs.Category;
using Athena.Core.Exceptions;
using Athena.DataAccess.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Athena.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoriesVm> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .ProjectTo<ViewCategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var serializedCategories = JsonConvert.SerializeObject(categories);
        return JsonConvert.DeserializeObject<CategoriesVm>(serializedCategories) ??
               throw new ResourceNotFoundException();
    }
}