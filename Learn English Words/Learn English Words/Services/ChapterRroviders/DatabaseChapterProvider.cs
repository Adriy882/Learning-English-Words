using Learn_English_Words.DBContext;
using Learn_English_Words.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn_English_Words.Services.ChapterProviders
{
    public class DatabaseChapterProvider : IChapterProviders
    {
        private readonly MyWordsDbContextFactory _dbContextFactory;

        public DatabaseChapterProvider(MyWordsDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Chapter>> GetAll()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Chapters.ToListAsync();
            }
        }
        public async Task<string[]> GetAllName()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                // Query to get all chapter names
                var chapterNames = await context.Chapters
                                                 .Select(c => c.NameChapter) 
                                                 .ToArrayAsync(); 

                return chapterNames;
            }
        }
        public async Task<Chapter> GetByName(string nameChapter)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Chapters
                                    .Include(c => c.Words) 
                                    .FirstOrDefaultAsync(c => c.NameChapter == nameChapter);
            }
        }

        public async Task UpdateChapterName(string oldNameChapter, string newNameChapter)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var chapter = await context.Chapters.FirstOrDefaultAsync(c => c.NameChapter == oldNameChapter);
                if (chapter != null)
                {
                    var words = await context.Words.Where(w => w.NameChapter == oldNameChapter).ToListAsync();

                    var newChapter = new Chapter
                    {
                        NameChapter = newNameChapter
                    };
                    context.Chapters.Add(newChapter);

                    foreach (var word in words)
                    {
                        word.NameChapter = newNameChapter;
                    }

                    context.Chapters.Remove(chapter);

                    await context.SaveChangesAsync();
                }
            }
        }


        public async Task Delete(string nameChapter)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var chapter = await context.Chapters.FindAsync(nameChapter);
                if (chapter != null)
                {
                    context.Chapters.Remove(chapter);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task CreateChapter(string NameChapter)
        {
            try
            {
                using (var context = _dbContextFactory.CreateDbContext())
                {

                    context.Chapters.Add(NewChapter(NameChapter));
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex) { }
        }

        public Chapter NewChapter(string NameChapter)
        {
            Chapter chapter = new Chapter();
            chapter.NameChapter = NameChapter;
            chapter.Description = "";
            return chapter;
        }
    }
}
