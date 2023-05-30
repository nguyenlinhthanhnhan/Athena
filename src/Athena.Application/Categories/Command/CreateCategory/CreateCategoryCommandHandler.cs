using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using AutoMapper;
using MediatR;

namespace Athena.Application.Categories.Command.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly AthenaDbContext _context;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(AthenaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = _mapper.Map<Category>(request);

        _context.Categories.Add(newCategory);
        await _context.SaveChangesAsync(cancellationToken);

        return newCategory.Id;
    }
}