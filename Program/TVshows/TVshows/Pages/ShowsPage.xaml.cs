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
using TVshows.Database;

namespace TVshows.Pages
{
    /// <summary>
    /// Interaction logic for ShowsPage.xaml
    /// </summary>
    public partial class ShowsPage : Page
    {
        private ObservableCollection<TileItem> ShowsTiles;
        private MainWindow.LogUser logUser;
        public ShowsPage(MainWindow.LogUser logUser)
        {
            InitializeComponent();
            this.logUser = logUser;
            if (logUser.idRol == 1)
            {
                AddShowButton.Visibility = Visibility.Visible;
                EditShowButton.Visibility = Visibility.Visible;
                DeleteShowButton.Visibility = Visibility.Visible;
            }
            else
            {
                AddShowButton.Visibility = Visibility.Hidden;
                EditShowButton.Visibility = Visibility.Hidden;
                DeleteShowButton.Visibility = Visibility.Hidden;
            }
            UpdateShowTiles();
        }

        private class TileItem
        {
            public int idSh { get; set; }
            public string NameSh { get; set; }
            public string tDurationSh { get; set; }
            public string DescrioptionSh { get; set; }
            public int idCat { get; set; }
            public bool AddFavorite { get; set; }

            public TileItem(Database.Shows show, bool inFavorite)
            {
                idSh = show.idSh;
                NameSh = show.NameSh;
                tDurationSh = show.tDurationSh.ToString();
                DescrioptionSh = show.DescrioptionSh;
                idCat = (int)show.idCat;
                AddFavorite = inFavorite;
            }
        }

        public void UpdateShowTiles()
        {
            ShowsTiles = new ObservableCollection<TileItem>();
            ObservableCollection<Database.Shows> Shows =
                new ObservableCollection<Database.Shows>(
                    Core.Database.Shows);

            for (int i=0; i<Shows.Count; i++)
            {
                int idSh = Shows[i].idSh;
                ObservableCollection<Database.Favorites> Favorites =
                new ObservableCollection<Database.Favorites>(
                    Core.Database.Favorites.Where(
                            F => F.idSh == idSh
                              && F.idUs == logUser.idUs
                        ));
                ShowsTiles.Add(new TileItem(Shows[i], Favorites.Count>0));
            }

            ShowListBox.ItemsSource = ShowsTiles;
        }
        private Database.Shows TileToShow(TileItem tile)
        {
            Database.Shows Show = new Database.Shows();
            Show.idSh = tile.idSh;
            Show.NameSh = tile.NameSh;
            Show.tDurationSh = TimeSpan.Parse(tile.tDurationSh);
            Show.DescrioptionSh = tile.DescrioptionSh;
            Show.idCat = tile.idCat;
            ObservableCollection<Database.Categories> Categ =
                new ObservableCollection<Database.Categories>(
                    Core.Database.Categories.Where(C => C.idCat == Show.idCat));
            Show.Categories = Categ[0];
            return Show;
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
            TileItem SelectTile = ShowListBox.SelectedItem as TileItem;
            if (SelectTile != null)
            {
                ShowDialog(new ShowsPageDls(this, SelectTile.idSh));
            }
        }

        private void DeleteShowButton_Click(object sender, RoutedEventArgs e)
        {
            TileItem tile = (ShowListBox.SelectedItem) as TileItem;
            if (tile != null)
            {
                try
                {
                    Core.Database.Shows.Remove(
                            new ObservableCollection<Database.Shows>(
                                Core.Database.Shows
                                .Where(S => S.idSh == tile.idSh))[0]);
                    Core.Database.SaveChanges();
                    UpdateShowTiles();
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

        private void HeartCheckBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Database.Shows Show = TileToShow(ShowListBox.SelectedItem as TileItem);
            int idSh = Show.idSh;
            if (Show != null)
            {
                try
                {
                    if (!(ShowListBox.SelectedItem as TileItem).AddFavorite)
                    {
                        Database.Favorites newFavorite = new Favorites();
                        newFavorite.idFav = Core.VOID;
                        newFavorite.idUs = logUser.idUs;
                        newFavorite.idSh = idSh;
                        newFavorite.dtNotifyRefresh = DateTime.Now;
                        Core.Database.Favorites.Add(newFavorite);
                    }
                    else
                    {
                        
                        Core.Database.Favorites.Remove(
                            new ObservableCollection<Database.Favorites>(
                                Core.Database.Favorites
                                .Where( F => F.idSh == idSh
                                          && F.idUs == logUser.idUs ))[0]);
                    }
                    Core.Database.SaveChanges();
                    UpdateShowTiles();
                }
                catch
                {
                    MessageBox.Show("Упс! Кажется не получилось обновить список избранного...",
                                    "Предупреждение",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information,
                                    MessageBoxResult.None
                                );
                }
            }
            else
            {
                MessageBox.Show("ListBox не успел обновиться",
                                    "Предупреждение",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information,
                                    MessageBoxResult.None
                                );
            }
        }
    }
}
