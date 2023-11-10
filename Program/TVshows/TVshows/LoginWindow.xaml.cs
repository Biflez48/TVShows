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
using System.Windows.Shapes;

namespace TVshows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OkLogBtn_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Database.Users> User = new ObservableCollection<Database.Users>(Core.Database.Users.Where(U => U.NameUs == LoginLogTextBox.Text && U.PasswordUs== PasswordLogTextBox.Text));

            if (User.Count()>0)
            {
                MainWindow mWnd = new MainWindow(User[0].NameUs);
                mWnd.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK);
                LoginLogTextBox.Focus();
            }
        }

        private void RehLogBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow regWnd = new RegistrationWindow();
            regWnd.Show();
            Close();
        }
    }
}
