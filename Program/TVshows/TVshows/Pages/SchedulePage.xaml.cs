using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        ObservableCollection<TileItem> StencilTiles;
        bool ShowsToCateg = true;
        int IdChannel;
        public SchedulePage(int idChan)
        {
            InitializeComponent();
            IdChannel = idChan;
            ShowInfoChannel();
            ShowTiles();
        }

        private class TileItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string TimeStart { get; set; }
            public string DateStart { get; set; }
        }

        private void ShowInfoChannel()
        {
            ObservableCollection<Database.Channels> Channels =
               new ObservableCollection<Database.Channels>(
                   Core.Database.Channels
                   .Where(C => C.idCh== IdChannel));
            ChannelName.Content = Channels[0].NameCh;
            DescriptName.Content = Channels[0].DescriptionCh;
        }

        private void ShowTiles()
        {
            StencilTiles = new ObservableCollection<TileItem>();

            ObservableCollection<Database.Stencil> StencilList =
                   new ObservableCollection<Database.Stencil>(
                       Core.Database.Stencil
                       .Where(Sc => Sc.idCh == IdChannel));

            StencilList = new ObservableCollection<Database.Stencil>(StencilList.OrderBy(item => item.dtStart));

            int j = 0;
            for(int i = 0; i < StencilList.Count || StencilList[i].dtStart>=DateTime.Now; i++)
            {
                j = i;
            }
            j--;
            while (j >= 0)
            {
                StencilList.RemoveAt(j);
                j--;
            }

            ObservableCollection<Database.Categories> StencilCat = new ObservableCollection<Database.Categories>();
            for (int i = 0; i < StencilList.Count(); i++)
            {
                StencilCat.Concat(Core.Database.Categories
                           .Where(Sc => Sc.idCat == StencilList[i].idCat));
                TileItem tile = new TileItem();
                if (ShowsToCateg)
                {
                    tile.Id = StencilList[i].idCat;
                    tile.Name = StencilCat[i].NameCat;
                }
                tile.TimeStart = StencilList[i].dtStart.ToString("HH:mm:ss");
                tile.DateStart = StencilList[i].dtStart.ToString("dd-MM");
                StencilTiles.Add(tile);
            }

            if (!ShowsToCateg)
            {
                ObservableCollection<Database.Television> TvList = new ObservableCollection<Database.Television>(); 
                for(int i = 0; i < StencilList.Count(); i++)
                {
                    TvList.Concat(Core.Database.Television
                               .Where(Tv => Tv.idSt == StencilList[i].idSt));
                }

                ObservableCollection<Database.Shows> StencilShow = new ObservableCollection<Database.Shows>();
                for (int i = 0; i < TvList.Count(); i++)
                {
                    StencilShow.Concat(Core.Database.Shows
                               .Where(S => S.idSh == TvList[i].idSh));
                    StencilTiles[i].Id = TvList[i].idSh;
                    StencilTiles[i].Name = StencilShow[i].NameSh;
                }
                
            }

            SchlListBox.ItemsSource = StencilTiles;
        }

        private void SwitchShOrCatBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowsToCateg = !ShowsToCateg;
            if (ShowsToCateg)
            {
                SwitchShOrCatBtn.Content = "Показать передачи";
            }
            else
            {
                SwitchShOrCatBtn.Content = "Показать категории";
            }
            ShowTiles();
        }
    }
}
