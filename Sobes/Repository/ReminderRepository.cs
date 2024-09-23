using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Sobes.Repository
{
    public interface IReminderRepository
    {
        Task<Reminder?> GetReminderByIdAsync(int id);
        Task<List<Reminder>> GetAllRemindersAsync();
        Task AddReminderAsync(Reminder Reminder);
        Task UpdateReminderAsync(Reminder Reminder);
        Task DeleteReminderAsync(int id);
    }

    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;

        public ReminderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reminder?> GetReminderByIdAsync(int id)
        {
            return await _context.Reminders.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Reminder>> GetAllRemindersAsync()
        {
            return await _context.Reminders.ToListAsync();
        }

        public async Task AddReminderAsync(Reminder Reminder)
        {
            _context.Reminders.Add(Reminder);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReminderAsync(Reminder Reminder)
        {
            _context.Reminders.Update(Reminder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReminderAsync(int id)
        {
            var Reminder = await _context.Reminders.FindAsync(id);
            if (Reminder != null)
            {
                _context.Reminders.Remove(Reminder);
                await _context.SaveChangesAsync();
            }
        }
    }
}

