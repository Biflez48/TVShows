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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TVshows.Pages
{
    /// <summary>
    /// Interaction logic for ChannelPageDlg.xaml
    /// </summary>
    public partial class ChannelPageDlg : Page
    {
        private Database.Channels Channel { get; set; }
        private Page Page;
        public ChannelPageDlg(Page Page)
        {
            InitializeComponent();
            Channel = new Database.Channels();
            Channel.idCh = Core.VOID;
            this.Page = Page;
        }
        private bool CheckInfo()
        {
            if (NameChannelTextBox.Text == "")
            {
                MessageBox.Show("Не указано название канала",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                NameChannelTextBox.Focus();
                return false;
            }

            string Name = NameChannelTextBox.Text.ToLower();

            Func<Database.Channels, bool> Predicate;
            if (Channel.idCh == Core.VOID)
            {
                Predicate = C =>
                C.NameCh.ToLower() == Name;
            }
            else
            {
                Predicate = C =>
                C.NameCh.ToLower() == Name
                && C.idCh != Channel.idCh;
            }

            int Count = Core.Database.Channels.Where(C =>
                C.NameCh.ToLower() == Name).Count();
            if (Count > 0)
            {
                MessageBox.Show("Такой канал уже есть",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                NameChannelTextBox.Focus();
                return false;
            }
            return true;
        }
        private bool SaveInfo()
        {
            try
            {
                if (Channel.idCh == Core.VOID)
                {
                    Channel.NameCh = NameChannelTextBox.Text;
                    Channel.DescriptionCh = DescrTextBox.Text;
                    Core.Database.Channels.Add(Channel);
                }
                Core.Database.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить канал в базе данных",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                return false;
            }
            return true;
        }
        private void CloseInfo(bool NeedUpdate)
        {
            if (Page is ChannelPage)
            {
                ChannelPage ChannelPage = Page as ChannelPage;
                ChannelPage.HideDialog();
                if (NeedUpdate)
                {
                    ChannelPage.UpdateChannelTiles(Channel);
                }
                else
                {
                    ChannelPage.ChnlsListBox.Items.Refresh();
                }
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            OkButton.Focus();
            if (CheckInfo() && SaveInfo())
            {
                CloseInfo(true);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelButton.Focus();
            if (Channel.idCh != Core.VOID)
            {
                Core.CancelChanges(Channel);

            }
            CloseInfo(false);
        }
    }
}
