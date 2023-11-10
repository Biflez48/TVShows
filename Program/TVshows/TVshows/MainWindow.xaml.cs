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

        private void ChannelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoriesBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotifyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow LogWnd = new LoginWindow();
            LogWnd.Show();
            Close();
        }
    }
}
