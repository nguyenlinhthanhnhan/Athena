using Athena.Application.Posts.Command.CreatePost;
using Athena.Application.Posts.Command.DeletePost;
using Athena.Application.Posts.Command.UpdatePost;
using Athena.Application.Posts.Queries.GetPost;
using Athena.Application.Posts.Queries.GetPosts;
using Athena.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Athena.API.Controllers.v1;

[ApiVersion("1.0")]
public class PostsController : ApiController
{
    [HttpGet]
    public async Task<ActionResult<PostsVm>> GetPosts()
    {
        var vm = await Mediator.Send(new GetPostsQuery());
        return vm.Lists.Count == 0 ? NotFound() : Ok(vm);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostVm>> GetPost(int id)
    {
        var vm = await Mediator.Send(new GetPostQuery { Id = id });
        return vm == null ? NotFound() : Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult> CreatePost(CreatePostCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        await Mediator.Send(new DeletePostCommand { Id = id });
        return NoContent();
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<Unit>> UpdatePost(long id, UpdatePostCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        return await Mediator.Send(command);
    }
}