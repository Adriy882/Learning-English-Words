using Learn_English_Words.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn_English_Words.Services.ChapterProviders
{
    public interface IChapterProviders
    {
        Task<IEnumerable<Chapter>> GetAll();
        Task<string[]> GetAllName();
        Task<Chapter> GetByName(string nameChapter);
        Task UpdateChapterName(string oldNameChapter, string newNameChapter);
        Task Delete(string nameChapter);
        Task CreateChapter(string NameChapter);
        Task UpdateDescription(string NameChapter, string NewDescription);
    }
}
