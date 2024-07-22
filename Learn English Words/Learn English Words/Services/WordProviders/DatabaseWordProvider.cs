using Learn_English_Words.DBContext;
using Learn_English_Words.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn_English_Words.Services.WordProviders
{
    public class DatabaseWordProvider : IWordProviders
    {
        private readonly MyWordsDbContextFactory _dbContextFactory;

        public DatabaseWordProvider(MyWordsDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Word>> GetAll()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Words.ToListAsync();
            }
        }

        public async Task<IEnumerable<Word>> GetByChapter(string nameChapter)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Words
                            .Where(w => w.NameChapter == nameChapter)
                            .ToListAsync();
            }
        }

        public async Task CreateWord(string nameChapter)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var word = NewWord(nameChapter);
                context.Words.Add(word);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateWord(Word word,int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var existingWord = await context.Words.FirstOrDefaultAsync(w => w.Id == id);
                if (existingWord != null)
                {
                    existingWord.EnglishWord = word.EnglishWord;
                    existingWord.TranslateWord = word.TranslateWord;
                    existingWord.Chapter = word.Chapter;
                    existingWord.Description = word.Description;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var existingWord = await context.Words.FirstOrDefaultAsync(w => w.Id == id);
                if (existingWord != null)
                {
                    context.Words.Remove(existingWord);
                    await context.SaveChangesAsync();
                }
            }
        }

        public Word NewWord(string NameChapter)
        {
             Word word = new Word
            {
                Description = "",
                EnglishWord = "",
                TranslateWord = "",
                NameChapter = NameChapter
            };
            return word;
        }      
    }
}
