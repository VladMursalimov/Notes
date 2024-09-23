using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Sobes.Repository
{
    public interface INoteRepository
    {
        Task<Note?> GetNoteByIdAsync(int id);
        Task<List<Note>> GetAllNotesAsync();
        Task AddNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(int id);
    }

    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.Include(n => n.Tags).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.Include(n => n.Tags).ToListAsync();
        }

        public async Task AddNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}

