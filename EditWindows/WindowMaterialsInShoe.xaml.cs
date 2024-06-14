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

namespace RickVowens.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для WindowMaterialsInShoe.xaml
    /// </summary>
    public partial class WindowMaterialsInShoe : Window
    {
        Product shoe;
        public WindowMaterialsInShoe(Product shoe)
        {
            InitializeComponent();
            this.shoe = shoe;
            DataContext = shoe;
            dgMaterialsInShoe.ItemsSource = shoe.MaterialOfProduct.ToList();
        }

        private void btAddMaterialForShoe(object sender, RoutedEventArgs e)
        {
            try
            {
                MaterialOfProduct material = new MaterialOfProduct();
                material.ID = 0;
                material.Articul = shoe.Articul;
                WindowEditMaterial windowEditMaterial = new WindowEditMaterial(shoe, material);
                windowEditMaterial.ShowDialog();
                dgMaterialsInShoe.ItemsSource = null;
                dgMaterialsInShoe.ItemsSource = shoe.MaterialOfProduct.ToList();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btEditMaterial(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var material = button.DataContext as MaterialOfProduct;
            WindowEditMaterial windowEditMaterial = new WindowEditMaterial(shoe, material);
            windowEditMaterial.ShowDialog();
            dgMaterialsInShoe.ItemsSource = null;
            dgMaterialsInShoe.ItemsSource = shoe.MaterialOfProduct.ToList();
        }

        private void btDeleteMaterialInShoe(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var material = button.DataContext as MaterialOfProduct;
            if (material != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    MainWindow.shoesKursovoiEntities.MaterialOfProduct.Remove(material);
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    dgMaterialsInShoe.ItemsSource = null;
                    dgMaterialsInShoe.ItemsSource = shoe.MaterialOfProduct.ToList();
                }
            }
        }
    }
}
