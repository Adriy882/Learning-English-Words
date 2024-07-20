using Learn_English_Words.Services.ChapterProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Learn_English_Words.ViewModel
{
    class ChapterVM : Utilities.ViewModelBase
    {
        private readonly IChapterProviders _providers;
        public ChapterVM()
        {
 
        }
        public ChapterVM(IChapterProviders providers)
        {
            _providers = providers;
        }

    }
}
