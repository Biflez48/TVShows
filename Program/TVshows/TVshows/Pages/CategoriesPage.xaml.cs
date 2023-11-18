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

namespace TVshows.Pages
{
    /// <summary>
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        private Database.Categories Category { get; set; }
        private CollectionViewSource CategViewModel { get; set; }
        public CategoriesPage(MainWindow.LogUser logUser)
        {
            InitializeComponent();
            CategViewModel = new CollectionViewSource();
            NewNameDockPanel.Visibility = Visibility.Hidden;
            if (logUser.idRol == 1)
            {
                DeleteCategButton.Visibility = Visibility.Visible;
                AddCategButton.Visibility = Visibility.Visible;
            }
            else
            {
                DeleteCategButton.Visibility = Visibility.Hidden;
                AddCategButton.Visibility = Visibility.Hidden;
            }
            UpdateCategTiles(null);
        }

        private void ResetData()
        {
            Category = new Database.Categories();
            Category.idCat = Core.VOID;
        }

        public void UpdateCategTiles(Database.Categories Category)
        {
            if (Category == null && CategListBox.SelectedItem != null)
            {
                Category = CategListBox.SelectedItem as Database.Categories;
            }

            ObservableCollection<Database.Categories> Categories =
                new ObservableCollection<Database.Categories>(
                Core.Database.Categories
                .Where(C => C.idCat >= 0)
                );

            CategViewModel.Source = Categories;
            CategListBox.ItemsSource = CategViewModel.View;

            if (Categories.Count > 0)
            {
                CategListBox.SelectedItem = Category;
                if (CategListBox.SelectedIndex < 0)
                {
                    CategListBox.SelectedIndex = 0;
                }
            }
        }
        private bool CheckInfo()
        {
            if (NewNameTextBox.Text == "")
            {
                MessageBox.Show("Не указано название категории",
                              "Предупреждение",
                              MessageBoxButton.OK,
                              MessageBoxImage.Information,
                              MessageBoxResult.None
                          );
                NewNameTextBox.Focus();
                return false;
            }

            string Name = NewNameTextBox.Text.ToLower();

            Func<Database.Categories, bool> Predicate;
            if (Category.idCat == Core.VOID)
            {
                Predicate = C =>
                C.NameCat.ToLower() == Name;
            }
            else
            {
                Predicate = C =>
                C.NameCat.ToLower() == Name
                && C.idCat != Category.idCat;
            }

            int Count = Core.Database.Categories.Where(C =>
                C.NameCat.ToLower() == Name).Count();

            if (Count > 0)
            {
                MessageBox.Show("Такая категория уже есть", 
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                NewNameTextBox.Focus();
                return false;
            }
            return true;
        }
        private bool SaveInfo()
        {
            try
            {
                if (Category.idCat == Core.VOID)
                {
                    Category.NameCat = NewNameTextBox.Text;
                    Core.Database.Categories.Add(Category);
                }
                Core.Database.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Не удалось добавить новую категорию в базу данных",
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information,
                                MessageBoxResult.None
                            );
                return false;
            }
            return true;
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
            NewNameDockPanel.Visibility = Visibility.Visible;
        }

        private void CancelNewNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (Category.idCat != Core.VOID)
            {
                Core.CancelChanges(Category);

            }
            NewNameTextBox.Text = "";
            NewNameDockPanel.Visibility = Visibility.Hidden;
        }

        private void OkNewNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInfo() && SaveInfo())
            {
                NewNameTextBox.Text = "";
                NewNameDockPanel.Visibility = Visibility.Hidden;
                UpdateCategTiles(Category);
            }
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            Database.Categories Category = (CategListBox.SelectedItem) as Database.Categories;
            if (Category != null)
            {
                try
                {
                    if (Category.idCat != Core.VOID)
                    {
                        Core.Database.Categories.Remove(Category);
                    }
                    Core.Database.SaveChanges();
                    UpdateCategTiles(Category);
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
    }
}
