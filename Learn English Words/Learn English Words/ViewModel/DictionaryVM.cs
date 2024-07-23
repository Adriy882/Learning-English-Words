using Learn_English_Words.Models;
using Learn_English_Words.Services.ChapterProviders;
using Learn_English_Words.Services.WordProviders;
using Learn_English_Words.Utilities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Learn_English_Words.ViewModel
{
    public class DictionaryVM : ViewModelBase
    {
        private readonly IWordProviders _wordProviders;
        private readonly IChapterProviders _chapterProviders;
        private List<string> _nameChapters;

        // Binding properties
        public ObservableCollection<string> ChapterNames { get; } = new ObservableCollection<string>();

        private ObservableCollection<Word> words;
        public ObservableCollection<Word> Words
        {
            get => words;
            set
            {
                words = value;
                OnPropertyChanged(nameof(Words));
            }
        }

        private string _selectedChapterName;
        public string SelectedChapterName
        {
            get => _selectedChapterName;
            set
            {
                if (_selectedChapterName != value)
                {
                    _selectedChapterName = value;
                    OnPropertyChanged();
                    OnSelectedChapterNameChanged(); // Call method to update chapter information
                }
            }
        }

        private Word selectedWord;
        public Word SelectedWord
        {
            get => selectedWord;
            set
            {
                selectedWord = value;
                OnPropertyChanged(nameof(SelectedWord));
            }
        }

        private string _englishWord;
        public string EnglishWord
        {
            get => _englishWord;
            set
            {
                if (_englishWord != value)
                {
                    _englishWord = value;
                    OnPropertyChanged(nameof(EnglishWord));
                }
            }
        }

        private string _translateWord;
        public string TranslateWord
        {
            get => _translateWord;
            set
            {
                if (_translateWord != value)
                {
                    _translateWord = value;
                    OnPropertyChanged(nameof(TranslateWord));
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    OnPropertyChanged(nameof(InputText));
                    HandleTextChanged(_inputText);
                }
            }
        }

        // Commands
        public ICommand AddWordCommand { get; }
        public ICommand DeleteWordCommand { get; }

        public DictionaryVM(IWordProviders wordProviders, IChapterProviders chapterProviders)
        {
            _wordProviders = wordProviders;
            _chapterProviders = chapterProviders;

            // Initialize commands
            AddWordCommand = new RelayCommand(OnAddWordButtonClick);
            DeleteWordCommand = new RelayCommand(OnDeleteWordButtonClick);

            // Initialize ComboBox and load words
            SetComboBoxAsync().ConfigureAwait(false);
            LoadWords();
        }

        /// <summary>
        /// Handles text changes in the input field and filters words accordingly.
        /// </summary>
        private async void HandleTextChanged(string inputText)
        {
            if (string.IsNullOrEmpty(inputText))
            {
                LoadWords();
            }
            else
            {
                var modelWords = await _wordProviders.GetByChapter(SelectedChapterName);
                var filteredWords = modelWords.Where(w => w.EnglishWord.StartsWith(inputText, StringComparison.OrdinalIgnoreCase));
                Words = GetWordsCollection(filteredWords);
            }
        }

        /// <summary>
        /// Handles the click event of the Delete Word button.
        /// </summary>
        private async void OnDeleteWordButtonClick(object parameter)
        {
            if (selectedWord != null)
            {
                await _wordProviders.Delete(selectedWord?.Id ?? 0);
                LoadWords();
            }
        }

        /// <summary>
        /// Handles the click event of the Add Word button.
        /// </summary>
        private async void OnAddWordButtonClick(object parameter)
        {
            if (SelectedChapterName != null)
            {
                await _wordProviders.CreateWord(SelectedChapterName);
                LoadWords();
            }
        }

        /// <summary>
        /// Sets the ComboBox with available chapter names.
        /// </summary>
        private async Task SetComboBoxAsync()
        {
            _nameChapters = await _chapterProviders.GetAllName();
            ChapterNames.Clear();

            foreach (var name in _nameChapters)
            {
                ChapterNames.Add(name);
            }

            if (_nameChapters.Any())
            {
                SelectedChapterName = _nameChapters.First();
            }
        }

        /// <summary>
        /// Loads words for the currently selected chapter.
        /// </summary>
        private async void LoadWords()
        {
            InputText = string.Empty;
            var modelWords = await _wordProviders.GetByChapter(SelectedChapterName);
            Words = GetWordsCollection(modelWords);
        }

        /// <summary>
        /// Converts a collection of Word models to an ObservableCollection of Word.
        /// </summary>
        private ObservableCollection<Word> GetWordsCollection(IEnumerable<Word> modelWords)
        {
            return new ObservableCollection<Word>(modelWords.Select(w => new Word
            {
                EnglishWord = w.EnglishWord,
                TranslateWord = w.TranslateWord,
                Description = w.Description,
                Id = w.Id
            }).Reverse());
        }

        /// <summary>
        /// Handles the event when the selected chapter name changes.
        /// </summary>
        private async void OnSelectedChapterNameChanged()
        {
            LoadWords();
        }

        /// <summary>
        /// Handles the editing of a row in the DataGrid.
        /// </summary>
        public async void HandleRowEdit(Word editedWord)
        {
            await _wordProviders.UpdateWord(editedWord, editedWord.Id);
        }
    }
}
