using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Learn_English_Words.ViewModel
{
    public class DictionaryVM : INotifyPropertyChanged
    {
        private ObservableCollection<Word> words;
        public ObservableCollection<Word> Words
        {
            get { return words; }
            set
            {
                words = value;
                OnPropertyChanged(nameof(Words));
            }
        }

        public DictionaryVM()
        {

            LoadWords();
        }

        private void LoadWords()
        {
            Words = new ObservableCollection<Word>
            {
                new Word { EnglishWord = "Hello", TranslateWord = "Привіт", Description = "Greeting" },
                new Word { EnglishWord = "World", TranslateWord = "Світ", Description = "Planet Earth" },
                new Word { EnglishWord = "Cat", TranslateWord = "Кіт", Description = "Small domesticated mammal" },
                new Word { EnglishWord = "Dog", TranslateWord = "Собака", Description = "Domesticated mammal" },
                new Word { EnglishWord = "Apple", TranslateWord = "Яблуко", Description = "Round fruit" },
                new Word { EnglishWord = "Banana", TranslateWord = "Банан", Description = "Long curved fruit" },
                new Word { EnglishWord = "Car", TranslateWord = "Автомобіль", Description = "Road vehicle" },
                new Word { EnglishWord = "Bus", TranslateWord = "Автобус", Description = "Large vehicle" },
                new Word { EnglishWord = "Book", TranslateWord = "Книга", Description = "Written pages" },
                new Word { EnglishWord = "Pen", TranslateWord = "Ручка", Description = "Writing instrument" },
                new Word { EnglishWord = "Sun", TranslateWord = "Сонце", Description = "Earth's star" },
                new Word { EnglishWord = "Moon", TranslateWord = "Місяць", Description = "Natural satellite" },
                new Word { EnglishWord = "Star", TranslateWord = "Зірка", Description = "Luminous point" },
                new Word { EnglishWord = "Tree", TranslateWord = "Дерево", Description = "Woody plant" },
                new Word { EnglishWord = "Flower", TranslateWord = "Квітка", Description = "Seed-bearing part" },
                new Word { EnglishWord = "House", TranslateWord = "Будинок", Description = "Human habitation" },
                new Word { EnglishWord = "River", TranslateWord = "Річка", Description = "Natural stream" },
                new Word { EnglishWord = "Mountain", TranslateWord = "Гора", Description = "Natural elevation" },
                new Word { EnglishWord = "Sky", TranslateWord = "Небо", Description = "Atmosphere region" },
                new Word { EnglishWord = "Ocean", TranslateWord = "Океан", Description = "Large sea" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Word
    {
        public string EnglishWord { get; set; }
        public string TranslateWord { get; set; }
        public string Description { get; set; }
    }
}
