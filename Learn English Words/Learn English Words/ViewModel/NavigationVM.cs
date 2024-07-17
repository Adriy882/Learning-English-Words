using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learn_English_Words.Utilities;
using System.Windows.Input;

namespace Learn_English_Words.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand DictionaryCommand { get; set; }
        public ICommand TestsCommand { get; set; }
 

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Dictionary(object obj) => CurrentView = new DictionaryVM();
        private void Test(object obj) => CurrentView = new TestVM();
 

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            DictionaryCommand = new RelayCommand(Dictionary);
            TestsCommand = new RelayCommand(Test);
 

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}
