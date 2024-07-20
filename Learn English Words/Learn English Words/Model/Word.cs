using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_English_Words.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string EnglishWord { get; set; }
        public string TranslateWord { get; set; }
        public string? Description { get; set; }

        public string NameChapter { get; set; }

        public Chapter Chapter { get; set; }
    }

}
