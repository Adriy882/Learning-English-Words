using Learn_English_Words.DBContext;
using Learn_English_Words.Services.ChapterProviders;
using Learn_English_Words.Services.WordProviders;
using Learn_English_Words.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Learn_English_Words
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_ST = "Data Source=mywords.db";
        private readonly MyWordsDbContextFactory _myWordsDbContextFactory;

        private readonly IChapterProviders _chapterProviders;
        private readonly IWordProviders _wordProviders;
        public App()
        {
            _myWordsDbContextFactory = new MyWordsDbContextFactory(CONNECTION_ST);
            _chapterProviders = new DatabaseChapterProvider(_myWordsDbContextFactory);
            _wordProviders = new DatabaseWordProvider(_myWordsDbContextFactory);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using (MyWordsContext dbContext = _myWordsDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            var navigationVM = new NavigationVM(_chapterProviders, _wordProviders);

            MainWindow = new MainWindow
            {
                DataContext = navigationVM
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
