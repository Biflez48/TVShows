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
using System.Windows.Markup;
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
        private MainWindow.LogUser logUser;
        private MainWindow wnd;
        private CollectionViewSource ChannelsViewModel { get; set; }
        public ChannelPage(MainWindow.LogUser logUser,MainWindow wnd)
        {
            InitializeComponent();
            this.wnd = wnd;
            this.logUser = new MainWindow.LogUser();
            this.logUser = logUser;
            if (this.logUser.idRol == 1)
            {
                AddChannelButton.Visibility = Visibility.Visible;
                DeleteChannelButton.Visibility = Visibility.Visible;
            }
            else
            {
                AddChannelButton.Visibility = Visibility.Hidden;
                DeleteChannelButton.Visibility = Visibility.Hidden;
            }
            ChannelsViewModel = new CollectionViewSource();
            UpdateChannelTiles(null);
        }

        private void ShowDialog(Page Page)
        {
            Grid.SetColumnSpan(ChannelsStackPanel, 1);
            DialogGridSplitter.Visibility = Visibility.Visible;
            DialogScrollViewer.Visibility = Visibility.Visible;
            DialogFrame.Navigate(Page);
        }

        public void HideDialog()
        {
            Grid.SetColumnSpan(ChannelsStackPanel, 3);
            DialogGridSplitter.Visibility = Visibility.Hidden;
            DialogScrollViewer.Visibility = Visibility.Hidden;
            DialogFrame.Navigate(null);
            while (DialogFrame.CanGoBack)
            {
                DialogFrame.RemoveBackEntry();
            }
        }

        public void UpdateChannelTiles(Database.Channels Channel)
        {
            if (Channel == null && ChnlsListBox.SelectedItem != null)
            {
                Channel = ChnlsListBox.SelectedItem as Database.Channels;
            }
            
            ObservableCollection<Database.Channels> Channels =
                new ObservableCollection<Database.Channels>(
                    Core.Database.Channels
                    .Where(C => C.idCh >= 0)
                    );

            ChannelsViewModel.Source = Channels;
            ChnlsListBox.ItemsSource = ChannelsViewModel.View;
            
            if (Channels.Count > 0)
            {
                ChnlsListBox.SelectedItem = Channel;
                if (ChnlsListBox.SelectedIndex < 0)
                {
                    ChnlsListBox.SelectedIndex = 0;
                }
            }
        }

        private void ChnlsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Database.Channels SelItem = (Database.Channels)ChnlsListBox.SelectedItem;
            wnd.RootFrame.Navigate(new SchedulePage(logUser,SelItem.idCh));
        }

        private void AddChannelButton_Click(object sender, RoutedEventArgs e)
        {
            ShowDialog(new ChannelPageDlg(this));
        }

        private void DeleteChannelButton_Click(object sender, RoutedEventArgs e)
        {
            Database.Channels Channel = ChnlsListBox.SelectedItem as Database.Channels;
            if (Channel != null)
            {
                try
                {
                    if (Channel.idCh != Core.VOID)
                    {
                        Core.Database.Channels.Remove(Channel);
                    }
                    Core.Database.SaveChanges();
                    UpdateChannelTiles(null);
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить канал из базы данных",
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
