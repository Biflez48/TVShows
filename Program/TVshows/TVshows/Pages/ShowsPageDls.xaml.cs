using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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
using TVshows.Database;

namespace TVshows.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowsPageDls.xaml
    /// </summary>
    public partial class ShowsPageDls : Page
    {
        private Database.Shows Show;
        private Page Page;
        public ShowsPageDls(Page Page, int? ShowId)
        
        {
            InitializeComponent();
            this.Page = Page;

            if (ShowId == null)
            {
                CaptionLabel.Content = "Добавление";
                Show = new Database.Shows();
                Show.idSh = Core.VOID;
                Show.idCat = Core.NONE;
            }
            else
            {
                CaptionLabel.Content = "Редактирование";
                Show = new ObservableCollection<Database.Shows>(Core.Database.Shows.Where(S => S.idSh == ShowId))[0];
            }
            LoadData();
        }
        private void LoadData()
        {
            ObservableCollection<Database.Categories> Categ = new ObservableCollection<Database.Categories>(Core.Database.Categories.OrderBy(C => C.NameCat));
            List<string> CategNames = new List<string>();
            for(int i=0; i < Categ.Count; i++)
            {
                CategNames.Add(Categ[i].NameCat);
            }
            CategShowComboBox.ItemsSource = CategNames;
            if (Categ.Count > 0)
            {
                CategShowComboBox.SelectedItem = CategNames[0];
            }
            for (int i = 0; i < 24; i++)
            {
                HDurationShowComboBox.Items.Add(i);
            }
            HDurationShowComboBox.SelectedItem = 0;
            for (int i = 0; i < 60; i++)
            {
                MDurationShowComboBox.Items.Add(i);
                SDurationShowComboBox.Items.Add(i);
            }
            MDurationShowComboBox.SelectedItem = 0;
            SDurationShowComboBox.SelectedItem = 0;

            if (Show.idSh != Core.VOID)
            {
                NameShowTextBox.Text = Show.NameSh;
                HDurationShowComboBox.SelectedItem = Show.tDurationSh.Hours;
                MDurationShowComboBox.SelectedItem = Show.tDurationSh.Minutes;
                SDurationShowComboBox.SelectedItem = Show.tDurationSh.Seconds;

                int num = 0;
                for(int i = 0; i < CategNames.Count; i++)
                {
                    if(CategNames[i] == new ObservableCollection<Database.Categories>(Core.Database.Categories.Where( C => C.idCat == Show.idCat))[0].NameCat)
                    {
                        num = i;
                        break;
                    }
                }

                CategShowComboBox.SelectedItem = CategNames[num];
                DescrTextBox.Text = Show.DescrioptionSh;
            }
        }
        private bool CheckInfo()
        {
            if (NameShowTextBox.Text == "")
            {
                MessageBox.Show("Не указано название передачи",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                NameShowTextBox.Focus();
                return false;
            }
            if((int)HDurationShowComboBox.SelectedItem < 1 && (int)MDurationShowComboBox.SelectedItem < 10) {
                MessageBox.Show("Минимальная продолжительность передачи: 10 минут",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                MDurationShowComboBox.Focus();
                return false;
            }

            string NameSh = NameShowTextBox.Text.ToLower();
            TimeSpan tDurationSh = new TimeSpan(
                (int)HDurationShowComboBox.SelectedItem,
                (int)MDurationShowComboBox.SelectedItem,
                (int)SDurationShowComboBox.SelectedItem
            );

            string SelectedNameCateg = CategShowComboBox.SelectedItem.ToString();
            int idCat = (new ObservableCollection<Database.Categories>(Core.Database.Categories.Where(C => C.NameCat == SelectedNameCateg)))[0].idCat;

            Func<Database.Shows, bool> Predicate;
            if (Show.idSh == Core.VOID)
            {
                Predicate = S =>
                S.NameSh.ToLower() == NameSh
                && S.tDurationSh == tDurationSh
                && S.idCat == idCat;
            }
            else
            {
                Predicate = S =>
                S.NameSh.ToLower() == NameSh
                && S.tDurationSh == tDurationSh
                && S.idCat == idCat
                && S.idSh != Show.idSh;
            }

            ObservableCollection<Database.Shows> show = new ObservableCollection<Database.Shows>(
                Core.Database.Shows.Where(S =>
                S.NameSh.ToLower() == NameSh
                && S.tDurationSh == tDurationSh
                && S.idCat == idCat));
            if (show.Count > 0 && show[0].idSh!=Show.idSh)
            {
                MessageBox.Show("Такая передача уже есть",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                NameShowTextBox.Focus();
                return false;
            }
            return true;
        }
        private bool SaveInfo()
        {
            try
            {
                Show.NameSh = NameShowTextBox.Text;
                Show.tDurationSh = new TimeSpan(
                    (int)HDurationShowComboBox.SelectedItem,
                    (int)MDurationShowComboBox.SelectedItem,
                    (int)SDurationShowComboBox.SelectedItem
                );
                Show.DescrioptionSh = DescrTextBox.Text;
                string SelectedNameCateg = CategShowComboBox.SelectedItem.ToString();
                ObservableCollection<Database.Categories> Categs = new ObservableCollection<Database.Categories>(
                    Core.Database.Categories.Where(C => C.NameCat == SelectedNameCateg)
                    );
                Show.idCat = Categs[0].idCat;
                if (Show.idSh == Core.VOID)
                {
                    Core.Database.Shows.Add(Show);
                }
                Core.Database.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить передачу в базе данных",
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
            if (Page is ShowsPage)
            {
                ShowsPage ShowsPage = Page as ShowsPage;
                ShowsPage.HideDialog();
                if (NeedUpdate)
                {
                    ShowsPage.UpdateShowTiles();
                }
                else
                {
                    ShowsPage.ShowListBox.Items.Refresh();
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
            if (Show.idSh != Core.VOID)
            {
                Core.CancelChanges(Show);

            }
            CloseInfo(false);
        }
    }
}
