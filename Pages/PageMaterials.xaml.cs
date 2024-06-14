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

namespace RickVowens.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageMaterials.xaml
    /// </summary>
    public partial class PageMaterials : Page
    {
        public PageMaterials()
        {
            InitializeComponent();
            dgMaterialsInShoe.ItemsSource = MainWindow.shoesKursovoiEntities.Material.ToList();
        }

        private void btAddMaterial(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                Material newMaterial = new Material();
                newMaterial.IDMaterial = 0;
                EditWindows.WindowAddMaterial windowAddMaterial = new EditWindows.WindowAddMaterial(newMaterial);
                windowAddMaterial.ShowDialog();
                dgMaterialsInShoe.ItemsSource = null;
                dgMaterialsInShoe.ItemsSource = MainWindow.shoesKursovoiEntities.Material.ToList();
            }
            else
            {
                MessageBox.Show("Это может делать только администратор");
            }
        }

        private void btEditMaterial(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                var button = sender as Button;
                var material = button.DataContext as Material;
                EditWindows.WindowAddMaterial windowAddMaterial = new EditWindows.WindowAddMaterial(material);
                windowAddMaterial.ShowDialog();
                dgMaterialsInShoe.ItemsSource = null;
                dgMaterialsInShoe.ItemsSource = MainWindow.shoesKursovoiEntities.Material.ToList();
            }
            else
            {
                MessageBox.Show("Это может делать только администратор");
            }
        }

        private void btDeleteMaterial(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                var button = sender as Button;
                var material = button.DataContext as Material;
                MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить данную запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (material != null)
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.shoesKursovoiEntities.Material.Remove(material);
                        MainWindow.shoesKursovoiEntities.SaveChanges();
                        dgMaterialsInShoe.ItemsSource = null;
                        dgMaterialsInShoe.ItemsSource = MainWindow.shoesKursovoiEntities.Material.ToList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Это может делать только администратор");
            }
        }
    }
}
