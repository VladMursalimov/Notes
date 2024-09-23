using DatabaseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sobes.Command;


namespace Sobes.Controller
{

    [Route("api/v1/function/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command)
        {
            var TagId = await _mediator.Send(command);
            return Ok(TagId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var Tags = await _mediator.Send(new GetAllTagsQuery());
            return Ok(Tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNode(int id)
        {

            var Tag = await _mediator.Send(new GetTag(id));
            return Ok(Tag);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var deleteTagCommand = new DeleteTagCommand(id);

            await _mediator.Send(deleteTagCommand);

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
