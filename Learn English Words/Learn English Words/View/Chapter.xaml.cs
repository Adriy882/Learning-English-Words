using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Learn_English_Words.View
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Chapter : UserControl
    {
        public Chapter()
        {
            InitializeComponent();
        }

        private void DeleteChapterButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationButtons.Visibility = Visibility.Visible;
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationButtons.Visibility = Visibility.Hidden;
        }
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationButtons.Visibility = Visibility.Hidden;
        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                DescriptionConfirmationButtons.Visibility = Visibility.Visible;
            }
            else
            {
                DescriptionConfirmationButtons.Visibility = Visibility.Collapsed;
            }
        }
        private void NoDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            DescriptionConfirmationButtons.Visibility = Visibility.Collapsed;
        }

        private void YesDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            DescriptionConfirmationButtons.Visibility = Visibility.Collapsed;
        }


    }
}
