﻿using System;
using System.Collections.Generic;
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

namespace TVshows.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowsPageDls.xaml
    /// </summary>
    public partial class ShowsPageDls : Page
    {
        private Database.Shows Show { get; set; }
        private List<Database.Categories> Categories { get; set; }
        private Page Page;
        public ShowsPageDls(Page Page, Database.Shows Show)
        {
            InitializeComponent();
            this.Page = Page;
            if(Show == null)
            {
                CaptionLabel.Content = "Добавление";
                this.Show = new Database.Shows();
                this.Show.idSh = Core.VOID;
                this.Show.idCat = Core.NONE;
            }
            else
            {
                CaptionLabel.Content = "Редактирование";
                this.Show = Show;
            }
            LoadData();
        }
        private void LoadData()
        {
            Categories = new List<Database.Categories>(Core.Database.Categories.OrderBy(C => C.NameCat));
            CategShowComboBox.ItemsSource = Categories;
            if (Categories.Count > 0)
            {
                CategShowComboBox.SelectedItem = Core.Database.Categories.FirstOrDefault(C => C.idCat == Show.idCat);
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

            int idCat = (CategShowComboBox.SelectedItem as Database.Categories).idCat;

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

            int Count = Core.Database.Shows.Where(S =>
                S.NameSh.ToLower() == NameSh
                && S.tDurationSh == tDurationSh
                && S.idCat == idCat).Count();
            if (Count > 0)
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
                    ShowsPage.UpdateShowTiles(Show);
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
