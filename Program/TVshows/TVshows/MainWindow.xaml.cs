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
using TVshows.Pages;
using static TVshows.MainWindow;

namespace TVshows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public LogUser logUser;
        public MainWindow(int UserID=1)
        {
            InitializeComponent();
            logUser = new LogUser();
            logUser.idUs = UserID;
            LoadData();
        }

        public class LogUser
        {
            public int idUs;
            public string NameUs;
            public int idRol;
        }

        private void LoadData()
        {
            ObservableCollection<Database.Users> user = new ObservableCollection<Database.Users>(
            Core.Database.Users
                .Where(U => U.idUs == logUser.idUs)
                );
            logUser.NameUs = user[0].NameUs;
            logUser.idRol = user[0].idRol;
            UserNameMainWndLabel.Content = logUser.NameUs;
        }

        public void ShowPage(Type PageType)
        {
            if(PageType!= null)
            {
                Page Page;
                Page = (Page)Activator.CreateInstance(PageType);
                RootFrame.Navigate(Page);
            }
        }

        private void RemovePagesFromFrame()
        {
            while (RootFrame.CanGoBack)
            {
                RootFrame.RemoveBackEntry();
            }
        }

        private void ChannelBtn_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new ChannelPage(logUser,this));
            // ShowPage(typeof(Pages.ChannelPage));
        }

        private void ShowsBtn_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new ShowsPage(logUser));
            // ShowPage(typeof(Pages.ShowsPage));
        }

        private void CategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new CategoriesPage(logUser));
            // ShowPage(typeof(Pages.CategoriesPage));
        }

        private void NotifyBtn_Click(object sender, RoutedEventArgs e)
        {
            RootFrame.Navigate(new NotifyPage(logUser));
            // ShowPage(typeof(Pages.NotifyPage));
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow LogWnd = new LoginWindow();
            LogWnd.Show();
            Close();
        }

        private void RootFrame_LoadCompleted(object sender, NavigationEventArgs e)
        {
            RemovePagesFromFrame();
        }
    }
}
