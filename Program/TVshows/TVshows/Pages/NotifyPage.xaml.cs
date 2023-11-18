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
    /// Interaction logic for NotifyPage.xaml
    /// </summary>
    public partial class NotifyPage : Page
    {
        private CollectionViewSource NotifyViewModel { get; set; }
        MainWindow.LogUser logUser;
        public NotifyPage(MainWindow.LogUser logUser)
        {
            InitializeComponent();
            NotifyViewModel = new CollectionViewSource();
            this.logUser = logUser;
            UpdateNotify();
        }

        private class TileItem
        {
            public int idSh { get; set; }
            public string NameSh { get; set; }
            public string NameCh { get; set; }
            public string tStart { get; set; }
            public string dStart { get; set; }

            public TileItem(int idsh, string time, string date, string show, string channel)
            {
                idSh = idsh;
                NameSh = show;
                NameCh = channel;
                tStart = time;
                dStart = date;
            }
        }

        // cделать связь Favorites -> Shows -> Television -> Stencil -> Channels используюя id пользователя и передачи
        private void UpdateNotify()
        {
            List<TileItem> Tiles = new List<TileItem>();

            ObservableCollection<Database.Favorites> Favorites =
                new ObservableCollection<Database.Favorites>(
                    Core.Database.Favorites.Where( F => F.idUs == logUser.idUs ));

            ObservableCollection<Database.Shows> ShowsInFav = new ObservableCollection<Database.Shows>();
            for (int i = 0; i < Favorites.Count; i++)
            {
                int idSh = Favorites[i].idSh;
                ShowsInFav.Add(new ObservableCollection<Database.Shows>(
                    Core.Database.Shows.Where(S => S.idSh == idSh))[0]);
            }

            ObservableCollection<Database.Television> TelevisionsInFav = new ObservableCollection<Database.Television>();
            for (int i = 0; i < ShowsInFav.Count; i++)
            {
                int idSh = ShowsInFav[i].idSh;
                DateTime dUpdate = Favorites[i].dtNotifyRefresh.Date;
                DateTime dtNow = DateTime.Now.Date;
                ObservableCollection<Database.Television> GetTv = new ObservableCollection<Database.Television>(
                    Core.Database.Television.Where(Tv => Tv.idSh == idSh 
                                                      && Tv.dStart >= dUpdate
                                                      && Tv.dStart <= dtNow ));
                for(int j = 0; j < GetTv.Count; j++)
                {
                    TelevisionsInFav.Add(GetTv[j]);
                }
            }
            if (TelevisionsInFav.Count == 0) return;
            TelevisionsInFav = new ObservableCollection<Database.Television>(TelevisionsInFav.OrderBy(Tv => Tv.dStart));

            ObservableCollection<Database.Stencil> StencilsInFav = new ObservableCollection<Database.Stencil>();
            DateTime firstDay = TelevisionsInFav[0].dStart;
            DateTime lastDay = TelevisionsInFav[TelevisionsInFav.Count-1].dStart;
            List<int> deleteIds = new List<int>();
            for (int i = 0; i < TelevisionsInFav.Count; i++)
            {
                TimeSpan minTime = new TimeSpan(0, 0, 0);
                TimeSpan maxTime = new TimeSpan(23, 59, 59);
                if (TelevisionsInFav[i].dStart == firstDay)
                {
                    for(int j = 0; j < Favorites.Count; j++)
                    {
                        if (TelevisionsInFav[i].idSh == Favorites[j].idSh)
                        {
                            minTime = Favorites[j].dtNotifyRefresh.TimeOfDay;
                        }
                    }
                }
                if (TelevisionsInFav[i].dStart == lastDay)
                {
                    maxTime = DateTime.Now.TimeOfDay;
                }
                int idSt = TelevisionsInFav[i].idSt;
                ObservableCollection<Database.Stencil> sten = new ObservableCollection<Database.Stencil>(
                    Core.Database.Stencil.Where(St => St.idSt == idSt
                                                   && St.tStart >= minTime
                                                   && St.tStart <= maxTime));
                if(sten.Count > 0)
                {
                    StencilsInFav.Add(sten[0]);
                }
                else
                {
                    deleteIds.Add(i);
                }
            }

            for(int i=deleteIds.Count - 1; i >= 0; i--)
            {
                TelevisionsInFav.RemoveAt(deleteIds[i]);
            }

            ObservableCollection<Database.Channels> ChannelsInFav = new ObservableCollection<Database.Channels>();
            for (int i = 0; i < StencilsInFav.Count; i++)
            {
                int idCh = StencilsInFav[i].idCh;
                ObservableCollection<Database.Channels> GetChannel = new ObservableCollection<Database.Channels>(
                    Core.Database.Channels.Where(C => C.idCh == idCh));
                for (int j = 0; j < GetChannel.Count; j++)
                {
                    ChannelsInFav.Add(GetChannel[j]);
                }
            }

            for(int t = 0; t < TelevisionsInFav.Count; t++)
            {
                for(int s = 0; s < ShowsInFav.Count; s++)
                {
                    if (TelevisionsInFav[t].idSh == ShowsInFav[s].idSh)
                    {
                        Tiles.Add(new TileItem(ShowsInFav[s].idSh,
                                               StencilsInFav[t].tStart.ToString(@"hh\:mm"),
                                               TelevisionsInFav[t].dStart.ToString("dd.MM.yyyy"),
                                               ShowsInFav[s].NameSh,
                                               ChannelsInFav[t].NameCh));
                    }
                }
            }

            NotifyViewModel.Source = Tiles;
            NotifyListBox.ItemsSource = NotifyViewModel.View;
        }

        private void SetReadedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<Database.Favorites> clearFavorites = new ObservableCollection<Database.Favorites>();
                for (int i = 0; i < NotifyListBox.Items.Count; i++)
                {
                    TileItem tile = NotifyListBox.Items[i] as TileItem;
                    Database.Favorites favorite = new ObservableCollection<Database.Favorites>(
                        Core.Database.Favorites.Where(F => F.idUs == logUser.idUs
                                                        && F.idSh == tile.idSh ))[0];
                    if (!clearFavorites.Contains(favorite))
                    {
                        clearFavorites.Add(favorite);
                    }
                }
                for(int i=0; i < clearFavorites.Count; i++)
                {
                    clearFavorites[i].dtNotifyRefresh = DateTime.Now;
                }
                Core.Database.SaveChanges();
                UpdateNotify();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить изменения в базе данных",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
            }
        }
    }
}
