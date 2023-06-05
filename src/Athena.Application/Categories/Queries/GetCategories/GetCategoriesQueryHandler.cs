using Athena.Application.DTOs.Category;
using Athena.Core.Exceptions;
using Athena.DataAccess.Persistence;
using Athena.Shared.DTOs;
using Athena.Shared.Linq.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Athena.Application.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, CategoriesVm?>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoriesVm?> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = _context.Categories.WhereIf(!string.IsNullOrWhiteSpace(request.Title),
                x => x.Title!.Contains(request.Title!)).ProjectTo<ViewCategoryDto>(_mapper.ConfigurationProvider)
            .AsNoTracking();

        var totalItems = await categories.CountAsync(cancellationToken);
        var pagedCategories = await categories
            .Skip(request.Skip)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);

        var vm = new CategoriesVm
        {
            Items = pagedCategories,
            Meta = new PageMetaDto
            {
                TotalItems = totalItems,
                Limit = request.Limit,
                Page = request.Page
            }
        };

        var serializedCategories = JsonConvert.SerializeObject(vm);
        vm = JsonConvert.DeserializeObject<CategoriesVm>(serializedCategories);

        return vm;
    }
}