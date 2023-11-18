using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        private ObservableCollection<TileItem> StencilTiles;
        private bool ShowsToCateg = true;
        private Database.Channels Channel;
        private DateTime ShowedDay;
        private bool isEditableName = false;
        private bool isEditableDesc = false;
        public SchedulePage(MainWindow.LogUser logUser, int idChan)
        {
            InitializeComponent();
            if (logUser.idRol != 1)
            {
                AddButton.Visibility = Visibility.Hidden;
                EditNameButton.Visibility = Visibility.Hidden;
                EditDescrButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
            }
            ShowedDay = DateTime.Now.Date;
            PresDayButton.Visibility = Visibility.Hidden;
            CancelEdNameButton.Visibility = Visibility.Hidden;
            CancelEdDescrButton.Visibility = Visibility.Hidden;
            ShowInfoChannel(idChan);
            ShowTiles();
        }

        private class TileItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string TimeStart { get; set; }

            public TileItem(int id, string name, string timeStart)
            {
                Id = id;
                Name = name;
                TimeStart = timeStart;
            }
        }

        private void ShowInfoChannel(int idChan)
        {
            Channel = new ObservableCollection<Database.Channels>(
                   Core.Database.Channels
                   .Where(C => C.idCh == idChan))[0];
            ChannelName.Text = Channel.NameCh;
            ChannelDescr.Text = Channel.DescriptionCh;
            switch (ShowedDay.DayOfWeek.ToString())
            {
                case "Monday":
                    DayOfWeekLabel.Content = "Понедельник";
                    break;
                case "Tuesday":
                    DayOfWeekLabel.Content = "Вторник";
                    break;
                case "Wednesday":
                    DayOfWeekLabel.Content = "Среда";
                    break;
                case "Thursday":
                    DayOfWeekLabel.Content = "Четверг";
                    break;
                case "Friday":
                    DayOfWeekLabel.Content = "Пятница";
                    break;
                case "Saturday":
                    DayOfWeekLabel.Content = "Суббота";
                    break;
                case "Sunday":
                    DayOfWeekLabel.Content = "Воскресенье";
                    break;
            }
            ShowDayLabel.Content = ShowedDay.Date.ToString("dd.MM.yyyy");
        }

        public void ShowTiles()
        {
            StencilTiles = new ObservableCollection<TileItem>();

            string NameDayOfWeek = ShowedDay.DayOfWeek.ToString();
            int idDayOfWeek = new ObservableCollection<Database.DaysOfWeek>(
                       Core.Database.DaysOfWeek
                       .Where(D => D.NameDayOfWeek == NameDayOfWeek))[0].idDayOfWeek;
            ObservableCollection<Database.Stencil> StencilList =
                   new ObservableCollection<Database.Stencil>(
                       Core.Database.Stencil
                       .Where(Sc => Sc.idCh == Channel.idCh && Sc.idDayOfWeek == idDayOfWeek));
            StencilList = new ObservableCollection<Database.Stencil>(StencilList.OrderBy(item => item.tStart));

            if (ShowsToCateg)
            {
                ObservableCollection<Database.Categories> StencilCat;
                for (int i = 0; i < StencilList.Count(); i++)
                {
                    Database.Stencil stencil = StencilList[i];
                    StencilCat = new ObservableCollection<Database.Categories>(
                        Core.Database.Categories
                               .Where(Sc => Sc.idCat == stencil.idCat)
                        );
                    StencilTiles.Add(new TileItem((int)StencilList[i].idCat, StencilCat[0].NameCat, StencilList[i].tStart.ToString()));
                }

            }
            else
            {
                ObservableCollection<Database.Television> TvList = new ObservableCollection<Database.Television>();
                ObservableCollection<Database.Stencil> AddStencilList = new ObservableCollection<Database.Stencil>();
                for (int i = 0; i < StencilList.Count(); i++)
                {
                    int IdSt = StencilList[i].idSt;
                    ObservableCollection<Database.Television> AddTvList = new ObservableCollection<Database.Television>(
                        Core.Database.Television.Where(Tv => Tv.idSt == IdSt && Tv.dStart == ShowedDay));

                    if(AddTvList.Count > 0)
                    {
                        TvList.Add(AddTvList[0]);
                        AddStencilList.Add(StencilList[i]);
                    }
                }

                for (int i = 0; i < TvList.Count(); i++)
                {
                    int IdSh = TvList[i].idSh;
                    Database.Shows StencilShow = new ObservableCollection<Database.Shows>(
                        Core.Database.Shows.Where( S => S.idSh == IdSh )) [0];
                    StencilTiles.Add(new TileItem(TvList[i].idTv, StencilShow.NameSh, AddStencilList[i].tStart.ToString()));
                }
                
            }

            SchlListBox.ItemsSource = StencilTiles;
        }
        private void ShowDialog(Page Page)
        {
            Grid.SetColumnSpan(ChannelStackPanel, 1);
            DialogGridSplitter.Visibility = Visibility.Visible;
            DialogScrollViewer.Visibility = Visibility.Visible;
            DialogFrame.Navigate(Page);
        }

        public void HideDialog()
        {
            Grid.SetColumnSpan(ChannelStackPanel, 3);
            DialogGridSplitter.Visibility = Visibility.Hidden;
            DialogScrollViewer.Visibility = Visibility.Hidden;
            DialogFrame.Navigate(null);
            while (DialogFrame.CanGoBack)
            {
                DialogFrame.RemoveBackEntry();
            }
        }

        private void DeleteCategory()
        {
            TileItem tile = (SchlListBox.SelectedItem) as TileItem;
            if (tile != null)
            {
                try
                {
                    if (tile.Id != Core.VOID)
                    {
                        Core.Database.Stencil.Remove(
                            new ObservableCollection<Database.Stencil>(
                                Core.Database.Stencil
                                .Where(Sc => Sc.idSt == tile.Id))[0]);
                    }
                    Core.Database.SaveChanges();
                    ShowTiles();
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить категорию из базы данных",
                                    "Предупреждение",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information,
                                    MessageBoxResult.None
                                );
                }
            }
        }

        private void DeleteShow()
        {
            TileItem tile = (SchlListBox.SelectedItem) as TileItem;
            if (tile != null)
            {
                try
                {
                    if (tile.Id != Core.VOID)
                    {
                        Core.Database.Television.Remove(
                            new ObservableCollection<Database.Television>(
                                Core.Database.Television
                                .Where(Tv => Tv.idTv == tile.Id))[0]);
                    }
                    Core.Database.SaveChanges();
                    ShowTiles();
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить категорию из базы данных",
                                    "Предупреждение",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information,
                                    MessageBoxResult.None
                                );
                }
            }
        }
        private bool CheckInfo()
        {
            if (ChannelName.Text == "")
            {
                MessageBox.Show("Не указано название канала",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                ChannelName.Focus();
                return false;
            }

            string NewName = ChannelName.Text;

            Func<Database.Channels, bool> Predicate;
            Predicate = C =>
                C.NameCh.ToLower() == NewName
                && C.idCh != Channel.idCh;

            int Count = Core.Database.Channels.Where(C =>
                C.NameCh.ToLower() == NewName).Count();
            if (Count > 0)
            {
                MessageBox.Show("Такой канал уже есть",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                ChannelName.Focus();
                return false;
            }
            return true;
        }
        private bool SaveInfo()
        {
            try
            {
                if (isEditableName)
                {
                    Channel.NameCh = ChannelName.Text;
                }
                if (isEditableDesc)
                {
                    Channel.DescriptionCh = ChannelDescr.Text;
                }
                Core.Database.SaveChanges();
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

        private void SwitchShOrCatBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowsToCateg = !ShowsToCateg;
            if (ShowsToCateg)
            {
                SwitchShOrCatBtn.Content = "Показать передачи";
            }
            else
            {
                SwitchShOrCatBtn.Content = "Показать категории";
            }
            ShowTiles();
        }

        private void PastDayButton_Click(object sender, RoutedEventArgs e)
        {
            ShowedDay = ShowedDay.AddDays(1);
            PresDayButton.Visibility = Visibility.Visible;
            ShowInfoChannel(Channel.idCh);
            ShowTiles();
        }

        private void PresDayButton_Click(object sender, RoutedEventArgs e)
        {

            ShowedDay = ShowedDay.AddDays(-1);
            if(ShowedDay.Date <= DateTime.Now)
            {
                PresDayButton.Visibility = Visibility.Hidden;
            }
            ShowInfoChannel(Channel.idCh);
            ShowTiles();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowsToCateg)
            {
                ShowDialog(new SchedulePageDlg(this, Channel.idCh, ShowedDay));
            }
            else
            {
                ShowDialog(new ScheduleShowPageDlg(this, Channel.idCh, ShowedDay));
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShowsToCateg)
            {
                DeleteCategory();
            }
            else
            {
                DeleteShow();
            }
        }

        private void EditNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEditableName)
            {
                if (CheckInfo() && SaveInfo())
                {
                    ShowTiles();
                }
                ChannelName.IsEnabled = false;
                ChannelName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DBD2D4"));
                EditNameButton.Content = "Ред.";
                CancelEdNameButton.Visibility = Visibility.Hidden;
            }
            else
            {
                ChannelName.IsEnabled = true;
                ChannelName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B9B0B2"));
                EditNameButton.Content = "\\/";
                CancelEdNameButton.Visibility = Visibility.Visible;
            }
            ShowInfoChannel(Channel.idCh);
            isEditableName = !isEditableName;
        }

        private void CancelEdNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (Channel.idCh != Core.VOID)
            {
                Core.CancelChanges(Channel);

            }
            ChannelName.IsEnabled = false;
            ChannelName.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DBD2D4"));
            EditNameButton.Content = "Ред.";
            CancelEdNameButton.Visibility = Visibility.Hidden;
            ShowInfoChannel(Channel.idCh);
            isEditableName = !isEditableName;
        }

        private void EditDescrButton_Click(object sender, RoutedEventArgs e)
        {
            if (isEditableDesc)
            {
                if (SaveInfo())
                {
                    ShowTiles();
                }
                ChannelDescr.IsEnabled = false;
                ChannelDescr.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DBD2D4"));
                EditDescrButton.Content = "Ред.";
                CancelEdDescrButton.Visibility = Visibility.Hidden;
            }
            else
            {
                ChannelDescr.IsEnabled = true;
                ChannelDescr.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B9B0B2"));
                EditDescrButton.Content = "\\/";
                CancelEdDescrButton.Visibility = Visibility.Visible;
            }
            ShowInfoChannel(Channel.idCh);
            isEditableDesc = !isEditableDesc;
        }

        private void CancelEdDescrButton_Click(object sender, RoutedEventArgs e)
        {
            if (Channel.idCh != Core.VOID)
            {
                Core.CancelChanges(Channel);

            }
            ChannelDescr.IsEnabled = false;
            ChannelDescr.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DBD2D4"));
            EditDescrButton.Content = "Ред.";
            CancelEdDescrButton.Visibility = Visibility.Hidden;
            ShowInfoChannel(Channel.idCh);
            isEditableDesc = !isEditableDesc;
        }
    }
}
