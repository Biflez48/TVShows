using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TVshows.Pages
{
    /// <summary>
    /// Interaction logic for ShowsPage.xaml
    /// </summary>
    public partial class ShowsPage : Page
    {
        private CollectionViewSource ShowsViewModel { get; set; }
        public ShowsPage()
        {
            InitializeComponent();
            ShowsViewModel = new CollectionViewSource();
            UpdateShowTiles(null);
        }

        public void UpdateShowTiles(Database.Shows Show)
        {
            if (Show == null && ShowListBox.SelectedItem != null)
            {
                Show = ShowListBox.SelectedItem as Database.Shows;
            }

            // string time = (new Database.Shows()).tDurationSh.ToString(); 

            ObservableCollection<Database.Shows> Shows =
                new ObservableCollection<Database.Shows>(
                    Core.Database.Shows
                    .Where(S => S.idSh >= 0)
                    );

            ShowsViewModel.Source = Shows;
            ShowListBox.ItemsSource = ShowsViewModel.View;

            if (Shows.Count > 0)
            {
                ShowListBox.SelectedItem = Show;
                if (ShowListBox.SelectedIndex < 0)
                {
                    ShowListBox.SelectedIndex = 0;
                }
            }
        }
        private void ShowDialog(Page Page)
        {
            Grid.SetColumnSpan(ShowsStackPanel, 1);
            DialogGridSplitter.Visibility = Visibility.Visible;
            DialogScrollViewer.Visibility = Visibility.Visible;
            DialogFrame.Navigate(Page);
        }

        public void HideDialog()
        {
            Grid.SetColumnSpan(ShowsStackPanel, 3);
            DialogGridSplitter.Visibility = Visibility.Hidden;
            DialogScrollViewer.Visibility = Visibility.Hidden;
            DialogFrame.Navigate(null);
            while (DialogFrame.CanGoBack)
            {
                DialogFrame.RemoveBackEntry();
            }
        }

        private void AddShowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDialog(new ShowsPageDls(this,null));
        }

        private void EditShowButton_Click(object sender, RoutedEventArgs e)
        {
            Database.Shows SelectShow = ShowListBox.SelectedItem as Database.Shows;
            if (SelectShow != null)
            {
                ShowDialog(new ShowsPageDls(this, SelectShow));
            }
        }

        private void DeleteShowButton_Click(object sender, RoutedEventArgs e)
        {
            Database.Shows Show = ShowListBox.SelectedItem as Database.Shows;
            if (Show != null)
            {
                try
                {
                    if (Show.idSh == Core.VOID)
                    {
                        Core.Database.Shows.Remove(Show);
                    }
                    Core.Database.SaveChanges();
                    UpdateShowTiles(null);
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить передачу из базы данных",
                                    "Предупреждение",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information,
                                    MessageBoxResult.None
                                );
                }
            }
        }
    }
}
