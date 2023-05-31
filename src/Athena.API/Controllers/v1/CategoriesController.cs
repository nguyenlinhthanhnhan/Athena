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
    public async Task<ActionResult<CategoriesVm>> Get()
    {
        var vm = await Mediator.Send(new GetCategoriesQuery());
        return vm.Lists.Count == 0 ? NotFound() : Ok(vm);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryVm>> Get(int id)
    {
        var vm = await Mediator.Send(new GetCategoryQuery { Id = id });
        return vm == null ? NotFound() : Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateCategoryCommand command)
    {
        return Ok(ApiResult<int>.Success(await Mediator.Send(command)));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Unit>> Delete(int id)
    {
        return await Mediator.Send(new DeleteCategoryCommand { Id = id });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Unit>> Update(int id, UpdateCategoryCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }
}