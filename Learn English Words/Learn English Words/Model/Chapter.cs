using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_English_Words.Models
{
    class Chapter
    {
        public string NameChapter { get; set; }
        public string? Description { get; set; }
        public ICollection<Word> Words { get; set; }
    }

}
