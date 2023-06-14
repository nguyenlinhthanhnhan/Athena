using Athena.Application.Commons.Mappings;
using Athena.Core.Entities;
using Athena.Shared.CQRS.CommandModels.Category;
using MediatR;

namespace Athena.Application.Categories.Command.CreateCategory;

public class CreateCategoryCommand : ICreateCategoryCommand, IRequest<int>, IMapTo<Category>
{
    public string? Title { get; set; }

    public string? MetaTitle { get; set; }

    public string? Slug { get; set; }

    public string? Content { get; set; }
}