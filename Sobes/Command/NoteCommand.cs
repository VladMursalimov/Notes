using DatabaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sobes.Service;
using System;


namespace Sobes.Command
{
    public record GetAllNotesQuery() : IRequest<List<Note>>;

    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, List<Note>>
    {
        private readonly NoteService _noteService;

        public GetAllNotesQueryHandler(NoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task<List<Note>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            return await _noteService.GetAllNotesAsync();
        }
    }
    public record CreateNoteCommand(string Title, string Text, List<int> TagIds) : IRequest<int>;

    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, int>
    {
        private readonly NoteService _noteService;
        private readonly AppDbContext _context;

        public CreateNoteCommandHandler(NoteService noteService, AppDbContext context)
        {
            _noteService = noteService;
            _context = context;
        }

        public async Task<int> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.Where(t => request.TagIds.Contains(Convert.ToInt32(t.Id))).ToListAsync();
            var note = new Note { Title = request.Title, Text = request.Text, Tags = tags, CreatedDate = DateTime.UtcNow.Date, ModifiedDate = DateTime.UtcNow.Date };
            await _noteService.CreateNoteAsync(note);
            return note.Id;
        }
    }

    public record DeleteNoteCommand(int Id) : IRequest;

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
    {
        private readonly NoteService _noteService;

        public DeleteNoteCommandHandler(NoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            await _noteService.DeleteNoteAsync(request.Id);
        }
    }

    public record UpdateNoteCommand(int Id, string Title, string Text, List<int> TagIds) : IRequest
    {
        public int Id { get; set; }
    }


    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly NoteService _noteService;
        private readonly AppDbContext _context;


        public UpdateNoteCommandHandler(NoteService noteService, AppDbContext context)
        {
            _noteService = noteService;
            _context = context;
        }

        public async Task Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = _context.Notes.Find(request.Id);
            if (note == null)
            {
                throw new Exception("Заметка не найдена");
            }
            note.Title = request.Title;
            note.Text = request.Text;
            note.ModifiedDate = DateTime.UtcNow.Date;

            // Обновляем список тегов
            note.Tags = await _context.Tags
                .Where(t => request.TagIds.Contains(t.Id))
                .ToListAsync();
            await _noteService.UpdateNoteAsync(note);
        }

    }


    public record GetNote(int Id) : IRequest<Note>;

    public class GetNoteCommandHander : IRequestHandler<GetNote, Note>
    {
        private readonly NoteService _noteService;

        public GetNoteCommandHander(NoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task<Note?> Handle(GetNote request, CancellationToken cancellationToken)
        {

            return await _noteService.GetNoteAsync(request.Id);

        }
    }
}
