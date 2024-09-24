using DatabaseModels;
using FluentValidation;
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

    public class GetAllNotesQueryValidator : AbstractValidator<GetAllNotesQuery>
    {
        // No validation rules here
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
            var val = new CreateNoteCommandValidator();
            var res = await val.ValidateAsync(request);
            if (!res.IsValid)
            {
                throw new ArgumentException(res.ToString());
            }
            var tags = await _context.Tags.Where(t => request.TagIds.Contains(Convert.ToInt32(t.Id))).ToListAsync();
            var note = new Note { Title = request.Title, Text = request.Text, Tags = tags, CreatedDate = DateTime.UtcNow.Date, ModifiedDate = DateTime.UtcNow.Date };
            await _noteService.CreateNoteAsync(note);
            return note.Id;
        }
    }

    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Заголовок не должен быть пустым.")
                .MaximumLength(255).WithMessage("Длина заголовка не должна превышать 255 символов.");

            RuleFor(x => x.Text)
                .MaximumLength(255).WithMessage("Длина текста не должна превышать 255 символов.");

            RuleFor(x => x.TagIds)
                .NotEmpty().WithMessage("Должен быть хотя бы один тэг.")
                .Must(tagIds => tagIds.All(tagId => tagId > 0)).WithMessage("Все тэги должны иметь допустимые значения.");
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
            var val = new DeleteNoteCommandValidator();
            var res = await val.ValidateAsync(request);
            if (!res.IsValid)
            {
                throw new ArgumentException(res.ToString());
            }
            await _noteService.DeleteNoteAsync(request.Id);
        }
    }
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID заметки должен быть положительным числом.");
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
            var val = new UpdateNoteCommandValidator();
            var res = await val.ValidateAsync(request);
            if (!res.IsValid)
            {
                throw new ArgumentException(res.ToString());
            }
            var note = _context.Notes.Find(request.Id);
            if (note == null)
            {
                throw new Exception("Заметка не найдена");
            }

            note.Title = request.Title;
            note.Text = request.Text;
            note.ModifiedDate = DateTime.UtcNow.Date;

            note.Tags = await _context.Tags
                .Where(t => request.TagIds.Contains(t.Id))
                .ToListAsync();
            await _noteService.UpdateNoteAsync(note);
        }

    }

    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID заметки должен быть положительным числом.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Заголовок не должен быть пустым.")
                .MaximumLength(255).WithMessage("Длина заголовка не должна превышать 255 символов.");

            RuleFor(x => x.Text)
                .MaximumLength(255).WithMessage("Длина текста не должна превышать 255 символов.");

            RuleFor(x => x.TagIds)
                .NotEmpty().WithMessage("Должен быть хотя бы один тэг.")
                .Must(tagIds => tagIds.All(tagId => tagId > 0)).WithMessage("Все тэги должны иметь допустимые значения.");
        }
    }


    public record GetNoteCommand(int Id) : IRequest<Note>;

    public class GetNoteCommandHander : IRequestHandler<GetNoteCommand, Note>
    {
        private readonly NoteService _noteService;

        public GetNoteCommandHander(NoteService noteService)
        {
            _noteService = noteService;
        }

        public async Task<Note?> Handle(GetNoteCommand request, CancellationToken cancellationToken)
        {
            var val = new GetNoteValidator();
            var res = await val.ValidateAsync(request);
            if (!res.IsValid)
            {
                throw new ArgumentException(res.ToString());
            }
            return await _noteService.GetNoteAsync(request.Id);

        }
    }

    public class GetNoteValidator : AbstractValidator<GetNoteCommand>
    {
        public GetNoteValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID заметки должен быть положительным числом.");
        }
    }

}
