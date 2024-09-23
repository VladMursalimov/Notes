using DatabaseModels;
using Sobes.Repository;

namespace Sobes.Service
{
    public class ReminderService
    {
        private readonly IReminderRepository _ReminderRepository;

        public ReminderService(IReminderRepository ReminderRepository)
        {
            _ReminderRepository = ReminderRepository;
        }

        public async Task<Reminder?> GetReminderAsync(int id)
        {
            return await _ReminderRepository.GetReminderByIdAsync(id);
        }

        public async Task<List<Reminder>> GetAllRemindersAsync()
        {
            return await _ReminderRepository.GetAllRemindersAsync();
        }

        public async Task CreateReminderAsync(Reminder Reminder)
        {
            await _ReminderRepository.AddReminderAsync(Reminder);
        }

        public async Task UpdateReminderAsync(Reminder Reminder)
        {
            await _ReminderRepository.UpdateReminderAsync(Reminder);
        }

        public async Task DeleteReminderAsync(int id)
        {
            await _ReminderRepository.DeleteReminderAsync(id);
        }
    }
}
