using Athena.Application.Categories.Queries.GetCategories;
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
}