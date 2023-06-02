using Athena.Application.Categories.Command.CreateCategory;
using Athena.Application.Categories.Command.DeleteCategory;
using Athena.Application.Categories.Command.UpdateCategory;
using Athena.Application.Categories.Queries.GetCategories;
using Athena.Application.Categories.Queries.GetCategory;
using Athena.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Athena.API.Controllers.v1;

[ApiVersion("1.0")]
public class CategoriesController : ApiController
{
    [HttpGet]
    public async Task<ActionResult<CategoriesVm>> GetCategories()
    {
        var vm = await Mediator.Send(new GetCategoriesQuery());
        return vm.Lists.Count == 0 ? NotFound() : Ok(vm);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryVm>> GetCategory(int id)
    {
        var vm = await Mediator.Send(new GetCategoryQuery { Id = id });
        return vm == null ? NotFound() : Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory(CreateCategoryCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        await Mediator.Send(new DeleteCategoryCommand { Id = id });
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Unit>> UpdateCategory(int id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }
}