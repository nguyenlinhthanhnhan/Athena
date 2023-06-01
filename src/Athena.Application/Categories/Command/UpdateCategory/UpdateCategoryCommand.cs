using Athena.Application.Commons.Mappings;
using Athena.Core.Entities;
using Athena.Shared.CommandModels.Category;
using MediatR;

namespace Athena.Application.Categories.Command.UpdateCategory;

public class UpdateCategoryCommand : IUpdateCategoryCommand, IRequest<Unit>, IMapTo<Category>
{
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? MetaTitle { get; set; }
    
    public string? Slug { get; set; }
    
    public string? Content { get; set; }
}