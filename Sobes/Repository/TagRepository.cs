using DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace Sobes.Repository
{
    public interface ITagRepository
    {
        Task<Tag?> GetTagByIdAsync(int id);
        Task<List<Tag>> GetAllTagsAsync();
        Task AddTagAsync(Tag Tag);
        Task UpdateTagAsync(Tag Tag);
        Task DeleteTagAsync(int id);
    }

    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;

        public TagRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _context.Tags.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task AddTagAsync(Tag Tag)
        {
            _context.Tags.Add(Tag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(Tag Tag)
        {
            _context.Tags.Update(Tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int id)
        {
            var Tag = await _context.Tags.FindAsync(id);
            if (Tag != null)
            {
                _context.Tags.Remove(Tag);
                await _context.SaveChangesAsync();
            }
        }
    }
}

