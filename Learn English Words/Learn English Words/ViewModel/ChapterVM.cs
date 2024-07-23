using Learn_English_Words.Models;
using Learn_English_Words.Services.ChapterProviders;
using Learn_English_Words.Utilities;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Learn_English_Words.ViewModel
{
    public class ChapterVM : ViewModelBase
    {
        private readonly IChapterProviders _providers;

        // String info
        private const string StrNameChapter = "Name chapter: ";
        private const string StrWordCount = "Word Count: ";

        // Binding properties
        public ObservableCollection<string> ChapterNames { get; } = new ObservableCollection<string>();

        private string _labelNameChapter;
        public string LabelNameChapter
        {
            get => _labelNameChapter;
            set
            {
                _labelNameChapter = value;
                OnPropertyChanged();
            }
        }

        private string _labelCountWord;
        public string LabelCountWord
        {
            get => _labelCountWord;
            set
            {
                _labelCountWord = value;
                OnPropertyChanged();
            }
        }

        private bool _isDescriptionChanged;
        public bool IsDescriptionChanged
        {
            get => _isDescriptionChanged;
            set
            {
                _isDescriptionChanged = value;
                OnPropertyChanged();
            }
        }

        private bool _isDeletedChapter;
        public bool IsDeletedChapter
        {
            get => _isDeletedChapter;
            set
            {
                _isDeletedChapter = value;
                OnPropertyChanged();
            }
        }

        private string _tbDescription;
        public string tbDescription
        {
            get => _tbDescription;
            set
            {
                _tbDescription = value;
                OnPropertyChanged();
                OnDescriptionTextChanged();
            }
        }

        private string _tbAddChapter;
        public string tbAddChapter
        {
            get => _tbAddChapter;
            set
            {
                if (_tbAddChapter != value)
                {
                    _tbAddChapter = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tbRenameChapter;
        public string tbRenameChapter
        {
            get => _tbRenameChapter;
            set
            {
                if (_tbRenameChapter != value)
                {
                    _tbRenameChapter = value;
                    OnPropertyChanged();
                }
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

        private List<string> _nameChapters;
        private Chapter _selectedChapter;

        // Commands
        public ICommand AddChapterCommand { get; }
        public ICommand YesDeleteCommand { get; }
        public ICommand NoDeleteCommand { get; }
        public ICommand DeleteChapterCommand { get; }
        public ICommand RenameChapterCommand { get; }
        public ICommand YesChangeCommand { get; }
        public ICommand NoChangeCommand { get; }

        public ChapterVM(IChapterProviders providers)
        {
            _providers = providers;
            AddChapterCommand = new RelayCommand(AddChapter);
            YesDeleteCommand = new RelayCommand(OnYesDeleteButtonClick);
            RenameChapterCommand = new RelayCommand(OnRenameButtonClick);
            YesChangeCommand = new RelayCommand(OnYesChangeButtonClick);
            NoChangeCommand = new RelayCommand(OnNoChangeButtonClick);
            NoDeleteCommand = new RelayCommand(NoDeleteButtonClick);
            DeleteChapterCommand = new RelayCommand(DeleteChapterButtonClick);
            InitializationAsync().ConfigureAwait(false); // Initialize asynchronously
        }

        #region Initialization Methods

        /// <summary>
        /// Method for asynchronous initialization of the ViewModel.
        /// </summary>
        public async Task InitializationAsync()
        {
            IsDeletedChapter = false;
            await SetComboBoxAsync();
        }

        /// <summary>
        /// Method to set ComboBox values with chapter names.
        /// </summary>
        public async Task SetComboBoxAsync()
        {
            _nameChapters = await _providers.GetAllName();
            ChapterNames.Clear();
            foreach (var name in _nameChapters)
            {
                ChapterNames.Add(name);
            }

            if (_nameChapters.Any())
            {
                SelectedChapterName = _nameChapters.First();
            }
            else
            {
                await SetInfoChapterAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Method to set chapter information.
        /// </summary>
        public async Task SetInfoChapterAsync()
        {
            LabelNameChapter = StrNameChapter + SelectedChapterName;
            LabelCountWord = StrWordCount + await GetCountWordsOnSelectedChapterAsync();
            tbDescription = await GetDescriptionOnSelectedChapterAsync();
            IsDescriptionChanged = false;
        }

        #endregion

        #region Description Change Methods

        /// <summary>
        /// Method called when the description text changes.
        /// </summary>
        public async void OnDescriptionTextChanged()
        {
            var currentDescription = await GetDescriptionOnSelectedChapterAsync();
            IsDescriptionChanged = currentDescription != tbDescription;
        }

        /// <summary>
        /// Method to hide delete confirmation panel without deleting.
        /// </summary>
        private void NoDeleteButtonClick(object parameter)
        {
            IsDeletedChapter = false;
        }

        /// <summary>
        /// Method to toggle delete confirmation panel.
        /// </summary>
        private void DeleteChapterButtonClick(object parameter)
        {
            IsDeletedChapter = IsDeletedChapter == true ? false : true;
        }

        /// <summary>
        /// Method to confirm changes in the description.
        /// </summary>
        private async void OnYesChangeButtonClick(object parameter)
        {
            if (!string.IsNullOrEmpty(SelectedChapterName))
            {
                await _providers.UpdateDescription(SelectedChapterName, tbDescription);
                IsDescriptionChanged = false;
            }
            else
            {
                tbDescription = "";
            }
            IsDescriptionChanged = false;
        }

        /// <summary>
        /// Method to cancel changes in the description.
        /// </summary>
        private async void OnNoChangeButtonClick(object parameter)
        {
            if (!String.IsNullOrEmpty(SelectedChapterName))
            {
                var chapter = await _providers.GetByName(SelectedChapterName);
                tbDescription = chapter.Description;
            }
            else
            {
                tbDescription = "";
            }
            IsDescriptionChanged = false;
        }

        #endregion

        #region Chapter Management Methods

        /// <summary>
        /// Method to handle the deletion of a chapter.
        /// </summary>
        private async void OnYesDeleteButtonClick(object parameter)
        {
            await _providers.Delete(SelectedChapterName);
            await SetComboBoxAsync();
            IsDeletedChapter = false;
        }

        /// <summary>
        /// Method to handle the renaming of a chapter.
        /// </summary>
        private async void OnRenameButtonClick(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(tbRenameChapter))
            {
                if (ChapterNames.Contains(tbRenameChapter) || string.IsNullOrEmpty(SelectedChapterName))
                {
                    // Handle the case where the name already exists
                    MessageBox.Show("A chapter with this name already exists. Please choose a different name.", "Duplicate Chapter Name", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update the chapter name
                await _providers.UpdateChapterName(SelectedChapterName, tbRenameChapter);

                // Refresh the ComboBox and clear the rename text box
                await SetComboBoxAsync();
            }
            tbRenameChapter = "";
        }

        /// <summary>
        /// Method to add a new chapter.
        /// </summary>
        private async void AddChapter(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(tbAddChapter))
            {
                await _providers.CreateChapter(tbAddChapter);
                await SetComboBoxAsync(); // Refresh the ComboBox after adding a chapter
            }
            tbAddChapter = "";
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Method to handle the change of selected chapter name.
        /// </summary>
        private async void OnSelectedChapterNameChanged()
        {
            _selectedChapter = await _providers.GetByName(SelectedChapterName);
            await SetInfoChapterAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Method to get the description of the selected chapter.
        /// </summary>
        private async Task<string> GetDescriptionOnSelectedChapterAsync()
        {
            return _selectedChapter?.Description ?? ""; // Handle case where chapter might be null
        }

        /// <summary>
        /// Method to get the word count of the selected chapter.
        /// </summary>
        private async Task<string> GetCountWordsOnSelectedChapterAsync()
        {
            return _selectedChapter?.Words.Count.ToString() ?? ""; // Handle case where chapter might be null
        }

        #endregion
    }
}
