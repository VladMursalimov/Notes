using DatabaseModels;
using Sobes.Repository;

namespace Sobes.Service
{
    public class TagService
    {
        private readonly ITagRepository _TagRepository;

        public TagService(ITagRepository TagRepository)
        {
            _TagRepository = TagRepository;
        }

        public async Task<Tag?> GetTagAsync(int id)
        {
            return await _TagRepository.GetTagByIdAsync(id);
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _TagRepository.GetAllTagsAsync();
        }

        public async Task CreateTagAsync(Tag Tag)
        {
            await _TagRepository.AddTagAsync(Tag);
        }

        public async Task UpdateTagAsync(Tag Tag)
        {
            await _TagRepository.UpdateTagAsync(Tag);
        }

        public async Task DeleteTagAsync(int id)
        {
            await _TagRepository.DeleteTagAsync(id);
        }
    }
}
