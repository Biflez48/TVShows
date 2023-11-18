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
    /// Логика взаимодействия для ScheduleShowPageDlg.xaml
    /// </summary>
    public partial class ScheduleShowPageDlg : Page
    {
        private Page Page;
        private int IdChannel;
        private DateTime ShowedDay;
        private int IdDayOfWeek;
        private Database.Television Television { get; set; }
        public ScheduleShowPageDlg(Page Page, int IdChannel, DateTime ShowedDay)
        {
            InitializeComponent();
            SelTimeStackPanel.Visibility = Visibility.Hidden;
            Television = new Database.Television();
            Television.idTv = Core.VOID;
            Television.idSh = Core.NONE;
            Television.idSt = Core.NONE;
            this.Page = Page;
            this.IdChannel = IdChannel;
            this.ShowedDay = ShowedDay;
            IdDayOfWeek = 0;
            LoadData();
        }

        private class CmbbItem
        {
            public int IdStencil { get; set; }
            public string tStart { get; set; }
            public CmbbItem(int IdStencil, string tStart)
            {
                this.IdStencil = IdStencil;
                this.tStart = tStart;
            }
        }
        private void LoadData()
        {

            string NameDayOfWeek = ShowedDay.DayOfWeek.ToString();
            IdDayOfWeek = new ObservableCollection<Database.DaysOfWeek>(
                       Core.Database.DaysOfWeek
                       .Where(D => D.NameDayOfWeek == NameDayOfWeek))[0].idDayOfWeek;
            ObservableCollection<Database.Stencil> StencilList =
                   new ObservableCollection<Database.Stencil>(
                       Core.Database.Stencil
                       .Where(Sc => Sc.idCh == IdChannel && Sc.idDayOfWeek == IdDayOfWeek));

            ObservableCollection<Database.Shows> ShowsList = new ObservableCollection<Database.Shows>();
            List<int> Ids = new List<int>();
            for (int i = 0; i < StencilList.Count; i++)
            {
                int idcat = (int)StencilList[i].idCat;
                if (!Ids.Contains(idcat))
                {
                    ObservableCollection<Database.Shows> givedShows = new ObservableCollection<Shows>(Core.Database.Shows
                        .Where(S => S.idCat == idcat));
                    for (int j = 0; j < givedShows.Count; j++)
                    {
                        ShowsList.Add(givedShows[j]);
                    }
                    Ids.Add(idcat);
                }
            }

            List<string> ShowsNames = new List<string>();
            for (int i = 0; i < ShowsList.Count; i++)
            {
                ShowsNames.Add(ShowsList[i].NameSh);
            }

            CategShowComboBox.ItemsSource = ShowsNames;
        }
        private bool CheckInfo()
        {
            if (CategShowComboBox.SelectedItem == null)
            {
                MessageBox.Show("Передача не выбрана",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                CategShowComboBox.Focus();
                return false;
            }
            if (TimeShowComboBox.SelectedItem == null)
            {
                MessageBox.Show("Время начала не выбрано",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                CategShowComboBox.Focus();
                return false;
            }

            string SelectedShowName = CategShowComboBox.SelectedValue.ToString();
            int IdStencil = (TimeShowComboBox.SelectedItem as CmbbItem).IdStencil;

            int IdShow = (new ObservableCollection<Database.Shows>(Core.Database.Shows.Where(S => S.NameSh == SelectedShowName)))[0].idSh;

            Func<Database.Television, bool> Predicate;
            if (Television.idTv == Core.VOID)
            {
                Predicate = T =>
                T.idSh == IdShow
                && T.idSt == IdStencil
                && T.dStart == ShowedDay;
            }

            int Count = Core.Database.Television.Where(T =>
                T.idSh == IdShow
                && T.idSt == IdStencil
                && T.dStart == ShowedDay).Count();
            if (Count > 0)
            {
                MessageBox.Show("Такая передача в расписании на сегодня уже есть",
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
                if (Television.idTv == Core.VOID)
                {
                    string SelectedShowName = CategShowComboBox.SelectedValue.ToString();
                    int IdStencil = (TimeShowComboBox.SelectedItem as CmbbItem).IdStencil;

                    Television.idSh = (new ObservableCollection<Database.Shows>(Core.Database.Shows.Where(S => S.NameSh == SelectedShowName)))[0].idSh;
                    Television.idSt = IdStencil;
                    Television.dStart = ShowedDay;

                    Core.Database.Television.Add(Television);
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

        private void CategShowComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelItem = CategShowComboBox.SelectedValue.ToString();
            ObservableCollection<Database.Shows> ShowsList = new ObservableCollection<Database.Shows>(
                Core.Database.Shows.Where( S => S.NameSh == SelItem ));
            int IdShow = ShowsList[0].idSh;
            int IdCat = (int)ShowsList[0].idCat;

            ObservableCollection<Database.Stencil> StencilList = new ObservableCollection<Database.Stencil>(
                Core.Database.Stencil.Where(S => S.idCh == IdChannel
                                              && S.idCat == IdCat
                                              && S.idDayOfWeek == IdDayOfWeek));

            for(int i = StencilList.Count-1; i >= 0; i--)
            {
                int IdSt = StencilList[i].idSt;
                if(new ObservableCollection<Database.Television>(Core.Database.Television
                    .Where( Tv => Tv.idSt == IdSt && Tv.dStart == ShowedDay)).Count > 0)
                {
                    StencilList.RemoveAt(i);
                }
            }

            List<CmbbItem> StencilItems = new List<CmbbItem>();
            for(int i=0; i < StencilList.Count; i++)
            {
                StencilItems.Add(new CmbbItem(StencilList[i].idSt, StencilList[i].tStart.ToString(@"hh\:mm\:ss")));
            }

            TimeShowComboBox.ItemsSource = StencilItems;
            TimeShowComboBox.DisplayMemberPath = "tStart";

            SelTimeStackPanel.Visibility = Visibility.Visible;
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
            if (Television.idTv != Core.VOID)
            {
                Core.CancelChanges(Television);

            }
            CloseInfo(false);
        }
    }
}
