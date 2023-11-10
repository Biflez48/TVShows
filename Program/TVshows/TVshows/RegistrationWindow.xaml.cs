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
using System.Windows.Shapes;

namespace TVshows
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private string Captcha;
        private const int CaptchaLenght = 5;
        public RegistrationWindow()
        {
            InitializeComponent();
            CreateCaptcha();
        }

        private void CreateCaptcha()
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            //разделитель
            char[] a = { ',' };
            //расщепление массива по разделителю
            String[] ar = allowchar.Split(a);
            String Captcha = "";
            string temp = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                Captcha += temp;
            }
            CaptchaRegTextBox.Text = Captcha;
        }

        private void OkRegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInfo() && SaveInfo())
            {
                LoginWindow logWnd = new LoginWindow();
                logWnd.Show();
                Close();
            }
        }

        private void CancelRegBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow logWnd = new LoginWindow();
            logWnd.Show();
            Close();
        }

        private bool SaveInfo()
        {
            try
            {
                Database.Users User = new Database.Users();
                User.idUs = Core.VOID;
                User.NameUs = LoginRegTextBox.Text;
                User.PasswordUs = PasswordRegTextBox.Text;
                User.idRol = 2;
                Core.Database.Users.Add(User);
                Core.Database.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Не удалось зарегистрировать пользователя",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK);
                Core.CancelChanges(Core.Database.Users);
                return false;
            }
            return true;
        }

        private bool CheckInfo()
        {
            if (CaptchRegTextBox.Text != Captcha)
            {
                MessageBox.Show("Неверная капча",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK);
                CaptchRegTextBox.Focus();
                return false;
            }
            if(LoginRegTextBox.Text==""
                || PasswordRegTextBox.Text==""
                || RepeatePasswordRegTextBox.Text == "")
            {
                MessageBox.Show("Заполните поля логина и пароля",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK);
                LoginRegTextBox.Focus();
                return false;
            }
            if(PasswordRegTextBox.Text!= RepeatePasswordRegTextBox.Text)
            {
                MessageBox.Show("Пароли должны совпадать",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK);
                RepeatePasswordRegTextBox.Focus();
            }
            if(LoginRegTextBox.Text.Length<5 || PasswordRegTextBox.Text.Length < 5)
            {
                MessageBox.Show("Логин и пароль должны быть длинее 5 символов",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK);
                LoginRegTextBox.Focus();
            }
            int cntUsers = Core.Database.Users
                .Where(U => U.NameUs == LoginRegTextBox.Text)
                .Count();
            if (cntUsers > 0)
            {
                MessageBox.Show("Такой пользователь уже есть",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult.OK);
                LoginRegTextBox.Focus();
            }
            return true;
        }
    }
}
