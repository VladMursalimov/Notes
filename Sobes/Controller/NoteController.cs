using DatabaseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sobes.Command;


namespace Sobes.Controller
{

    [Route("api/v1/function/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteCommand command)
        {
            var noteId = await _mediator.Send(command);
            return Ok(noteId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var notes = await _mediator.Send(new GetAllNotesQuery());
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNode(int id)
        {

            var note = await _mediator.Send(new GetNote(id));
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var deleteNoteCommand = new DeleteNoteCommand(id);

            await _mediator.Send(deleteNoteCommand);

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] UpdateNoteCommand command)
        {
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
