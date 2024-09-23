using DatabaseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sobes.Command;


namespace Sobes.Controller
{

    [Route("api/v1/function/reminder")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReminderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReminder([FromBody] CreateReminderCommand command)
        {
            var ReminderId = await _mediator.Send(command);
            return Ok(ReminderId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var Reminders = await _mediator.Send(new GetAllRemindersQuery());
            return Ok(Reminders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNode(int id)
        {

            var Reminder = await _mediator.Send(new GetReminder(id));
            return Ok(Reminder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder(int id)
        {
            var deleteReminderCommand = new DeleteReminderCommand(id);

            await _mediator.Send(deleteReminderCommand);

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReminder(int id, [FromBody] UpdateReminderCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
