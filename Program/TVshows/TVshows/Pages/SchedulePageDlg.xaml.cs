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

namespace TVshows.Pages
{
    /// <summary>
    /// Логика взаимодействия для SchedulePageDlg.xaml
    /// </summary>
    public partial class SchedulePageDlg : Page
    {
        private Page Page;
        private Database.Stencil Stencil { get; set; }
        public SchedulePageDlg(Page Page, int IdChannel, DateTime ShowedDay)
        {
            InitializeComponent();
            this.Page = Page;

            string NameDayOfWeek = ShowedDay.DayOfWeek.ToString();
            int idDayOfWeek = new ObservableCollection<Database.DaysOfWeek>(
                       Core.Database.DaysOfWeek
                       .Where(D => D.NameDayOfWeek == NameDayOfWeek))[0].idDayOfWeek;

            Stencil = new Database.Stencil();
            Stencil.idSt = Core.VOID;
            Stencil.idCh = IdChannel;
            Stencil.idDayOfWeek = idDayOfWeek;
            Stencil.idCat = Core.NONE;
            LoadData();
        }
        private void LoadData()
        {
            ObservableCollection<Database.Categories> Categ = new ObservableCollection<Database.Categories>(Core.Database.Categories.OrderBy(C => C.NameCat));
            List<string> CategNames = new List<string>();
            for (int i = 0; i < Categ.Count; i++)
            {
                CategNames.Add(Categ[i].NameCat);
            }
            CategShowComboBox.ItemsSource = CategNames;
            if (Categ.Count > 0)
            {
                CategShowComboBox.SelectedItem = Core.Database.Categories.FirstOrDefault(C => C.idCat == Stencil.idCat);
            }
            for (int i = 0; i < 24; i++)
            {
                HStartCategComboBox.Items.Add(i);
            }
            HStartCategComboBox.SelectedItem = 0;
            for (int i = 0; i < 60; i++)
            {
                MStartCategComboBox.Items.Add(i);
                SStartCategComboBox.Items.Add(i);
            }
            MStartCategComboBox.SelectedItem = 0;
            SStartCategComboBox.SelectedItem = 0;
        }
        private bool CheckInfo()
        {

            TimeSpan tStartCateg = new TimeSpan(
                (int)HStartCategComboBox.SelectedItem,
                (int)MStartCategComboBox.SelectedItem,
                (int)SStartCategComboBox.SelectedItem
            );
            ObservableCollection<Database.Stencil> AllSten = new ObservableCollection<Database.Stencil>(Core.Database.Stencil
                .Where(As => As.idCh == Stencil.idCh && As.idDayOfWeek == Stencil.idDayOfWeek));

            bool shortTime = true;
            int i = 0;
            while(shortTime && i < AllSten.Count)
            {
                shortTime = (AllSten[i].tStart - tStartCateg).Duration() >= (new TimeSpan(0,10,0));
                i++;
            }

            if (!shortTime || (int)HStartCategComboBox.SelectedItem >= 24 && (int)MStartCategComboBox.SelectedItem >= 50)
            {
                MessageBox.Show("Минимальная продолжительность категории: 10 минут",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                MStartCategComboBox.Focus();
                return false;
            }

            string SelectedNameCateg = CategShowComboBox.SelectedItem.ToString();
            int idCat = (new ObservableCollection<Database.Categories>(Core.Database.Categories.Where(C => C.NameCat == SelectedNameCateg)))[0].idCat;

            Func<Database.Stencil, bool> Predicate;
            if (Stencil.idSt == Core.VOID)
            {
                Predicate = S =>
                S.idCh == Stencil.idCh
                && S.idCat == idCat
                && S.idDayOfWeek == Stencil.idDayOfWeek
                && S.tStart == tStartCateg;
            }

            int Count = Core.Database.Stencil.Where(S =>
                S.idCh == Stencil.idCh
                && S.idCat == idCat
                && S.idDayOfWeek == Stencil.idDayOfWeek
                && S.tStart == tStartCateg).Count();
            if (Count > 0)
            {
                MessageBox.Show("Такая категория в расписании уже есть",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                CategShowComboBox.Focus();
                return false;
            }
            return true;
        }
        private bool SaveInfo()
        {
            try
            {
                if (Stencil.idSt == Core.VOID)
                {
                    Stencil.tStart = new TimeSpan(
                        (int)HStartCategComboBox.SelectedItem,
                        (int)MStartCategComboBox.SelectedItem,
                        (int)SStartCategComboBox.SelectedItem
                    );
                    string SelectedNameCateg = CategShowComboBox.SelectedItem.ToString();
                    int idCat = (new ObservableCollection<Database.Categories>(Core.Database.Categories.Where(C => C.NameCat == SelectedNameCateg)))[0].idCat;
                    Stencil.idCat = idCat;
                    Core.Database.Stencil.Add(Stencil);
                    Core.Database.SaveChanges();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить изменения в базе данных",
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
            if (Page is SchedulePage)
            {
                SchedulePage SchedulePage = Page as SchedulePage;
                SchedulePage.HideDialog();
                if (NeedUpdate)
                {
                    SchedulePage.ShowTiles();
                }
                else
                {
                    SchedulePage.SchlListBox.Items.Refresh();
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
            if (Stencil.idSt != Core.VOID)
            {
                Core.CancelChanges(Stencil);

            }
            CloseInfo(false);
        }
    }
}
