using DatabaseModels;
using Sobes.Repository;

namespace Sobes.Service
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Note?> GetNoteAsync(int id)
        {
            return await _noteRepository.GetNoteByIdAsync(id);
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _noteRepository.GetAllNotesAsync();
        }

        public async Task CreateNoteAsync(Note note)
        {
            await _noteRepository.AddNoteAsync(note);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            await _noteRepository.UpdateNoteAsync(note);
        }

        public async Task DeleteNoteAsync(int id)
        {
            await _noteRepository.DeleteNoteAsync(id);
        }
    }
}
