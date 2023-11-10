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

namespace TVshows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string UserName="UnknownUser")
        {
            InitializeComponent();
            UserNameMainWndLabel.Content = UserName;
        }

        private void ShowPage(Type PageType)
        {
            if(PageType!= null)
            {
                Page Page;
                Page = (Page)Activator.CreateInstance(PageType);
                RootFrame.Navigate(Page);
            }
        }

        private void ChannelBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.ChannelPage));
        }

        private void ShowsBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.ShowsPage));
        }

        private void CategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.CategoriesPage));
        }

        private void NotifyBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowPage(typeof(Pages.NotifyPage));
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow LogWnd = new LoginWindow();
            LogWnd.Show();
            Close();
        }

        private void RootFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {

        }
    }
}
