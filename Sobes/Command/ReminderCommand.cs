using DatabaseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sobes.Service;
using System;


namespace Sobes.Command
{
    public record GetAllRemindersQuery() : IRequest<List<Reminder>>;

    public class GetAllRemindersQueryHandler : IRequestHandler<GetAllRemindersQuery, List<Reminder>>
    {
        private readonly ReminderService _ReminderService;

        public GetAllRemindersQueryHandler(ReminderService ReminderService)
        {
            _ReminderService = ReminderService;
        }

        public async Task<List<Reminder>> Handle(GetAllRemindersQuery request, CancellationToken cancellationToken)
        {
            return await _ReminderService.GetAllRemindersAsync();
        }
    }
    public record CreateReminderCommand(string Title, string Text, List<int> TagIds, DateTime ReminderTime) : IRequest<int>;

    public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, int>
    {
        private readonly ReminderService _ReminderService;
        private readonly AppDbContext _context;

        public CreateReminderCommandHandler(ReminderService ReminderService, AppDbContext context)
        {
            _ReminderService = ReminderService;
            _context = context;
        }

        public async Task<int> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.Where(t => request.TagIds.Contains(Convert.ToInt32(t.Id))).ToListAsync();
            
            var Reminder = new Reminder { Title = request.Title, Text = request.Text, Tags = tags,
                CreatedDate = DateTime.UtcNow.Date, ModifiedDate = DateTime.UtcNow.Date, ReminderTime = request.ReminderTime };
            await _ReminderService.CreateReminderAsync(Reminder);
            return Reminder.Id;
        }
    }

    public record DeleteReminderCommand(int Id) : IRequest;

    public class DeleteReminderCommandHandler : IRequestHandler<DeleteReminderCommand>
    {
        private readonly ReminderService _ReminderService;

        public DeleteReminderCommandHandler(ReminderService ReminderService)
        {
            _ReminderService = ReminderService;
        }

        public async Task Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
        {
            await _ReminderService.DeleteReminderAsync(request.Id);
        }
    }

    public record UpdateReminderCommand(int Id, string Title,string Text, DateTime reminderTime) : IRequest
    {
        public int Id { get; set; }
    }


    public class UpdateReminderCommandHandler : IRequestHandler<UpdateReminderCommand>
    {
        private readonly ReminderService _ReminderService;
        private readonly AppDbContext _context;


        public UpdateReminderCommandHandler(ReminderService ReminderService, AppDbContext context)
        {
            _ReminderService = ReminderService;
            _context = context;
        }

        public async Task Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            var reminder = _context.Reminders.Find(request.Id);
            if (reminder == null)
            {
                throw new Exception("Напоминалка не найдена");
            }

            reminder.Title = request.Title;
            reminder.Text = request.Text;
            reminder.ModifiedDate = DateTime.UtcNow.Date;
            reminder.ReminderTime = request.reminderTime;

            await _ReminderService.UpdateReminderAsync(reminder);
        }

    }


    public record GetReminder(int Id) : IRequest<Reminder>;

    public class GetReminderCommandHander : IRequestHandler<GetReminder, Reminder>
    {
        private readonly ReminderService _ReminderService;

        public GetReminderCommandHander(ReminderService ReminderService)
        {
            _ReminderService = ReminderService;
        }

        public async Task<Reminder?> Handle(GetReminder request, CancellationToken cancellationToken)
        {

            return await _ReminderService.GetReminderAsync(request.Id);

        }
    }
}
