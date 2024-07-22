using Learn_English_Words.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_English_Words.Services.WordProviders
{
    public interface IWordProviders
    {
        Task<IEnumerable<Word>> GetAll();
        Task<IEnumerable<Word>> GetByChapter(string nameChapter);
        Task CreateWord(string NameChapter);
        Task UpdateWord(Word UpdateWord,int id);
        Task Delete(int id);
    }
}
