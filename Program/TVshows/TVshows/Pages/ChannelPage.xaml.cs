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
    /// Interaction logic for ChannelPage.xaml
    /// </summary>
    public partial class ChannelPage : Page
    {
        ObservableCollection<TileItem> ChannelsTiles = new ObservableCollection<TileItem>();
        public ChannelPage()
        {
            InitializeComponent();
            ShowTiles();
        }

        private class TileItem
        {
            public string NameCh { get; set; }
            public string DescCh { get; set; }
        }

        private void ShowTiles()
        {
            ObservableCollection<Database.Channels> Channels =
               new ObservableCollection<Database.Channels>(
                   Core.Database.Channels
                   .Where(C => C.NameCh!="" && C.DescriptionCh!=""));

            for (int i = 0; i < Channels.Count(); i++)
            {
                TileItem tile = new TileItem();
                tile.NameCh = Channels[i].NameCh;
                tile.DescCh = Channels[i].DescriptionCh;
                ChannelsTiles.Add(tile);
            }

            ChnlsListBox.ItemsSource = ChannelsTiles;
        }

        private void ShowScheduleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChnlsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
