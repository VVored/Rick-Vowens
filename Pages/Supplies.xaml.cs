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
    /// Логика взаимодействия для Supplies.xaml
    /// </summary>
    public partial class Supplies : Page
    {
        public static ListView lvShoesLink;
        public static ListView lvMaterialLink;
        public static Button btAddContainLink;
        public Supplies()
        {
            InitializeComponent();
            lvShoesLink = lvShoes;
            btAddContainLink = btAddContain;
            lvMaterialLink = lvMaterial;
        }

        private void btChooseTableAndDate(object sender, RoutedEventArgs e)
        {
            DateTime? fromDate = tbDateFrom.SelectedDate;
            DateTime? toDate = tbDateTo.SelectedDate;
            if (rbProductInProductStock.IsChecked == true)
            {
                frame.NavigationService.Navigate(new suppliesTables.ProductToProductStockTable(fromDate, toDate));
                CurrentTableSource.currentTable = "ProductToProductStock";
                lvMaterial.Visibility = Visibility.Collapsed;
                lvShoes.Visibility = Visibility.Visible;
            }
            else if (rbMaterialInMaterialStock.IsChecked == true)
            {
                frame.NavigationService.Navigate(new suppliesTables.MaterialToMaterialStockTable(fromDate, toDate));
                CurrentTableSource.currentTable = "MaterialToMaterialStock";
                lvMaterial.Visibility = Visibility.Visible;
                lvShoes.Visibility = Visibility.Collapsed;
            }
            else if(rbProductInShops.IsChecked == true)
            {
                frame.NavigationService.Navigate(new suppliesTables.ProductToShopsTable(fromDate, toDate));
                CurrentTableSource.currentTable = "ProductToShop";
                lvMaterial.Visibility = Visibility.Collapsed;
                lvShoes.Visibility = Visibility.Visible;
            }
            lvShoes.ItemsSource = null;
            lvMaterial.ItemsSource = null;
            btAddContain.Visibility = Visibility.Collapsed;
        }

        private void btDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentTableSource.currentTable == "ProductToShop")
                {
                    var button = sender as Button;
                    var shoe = button.DataContext as SuppliesProductsInShopsContains;
                    shoe.Product.CountInStock += shoe.CountOfShoe;
                    MainWindow.shoesKursovoiEntities.SuppliesProductsInShopsContains.Remove(shoe);
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    lvShoes.ItemsSource = null;
                    lvShoes.ItemsSource = suppliesTables.ProductToShopsTable.selectedSupply.SuppliesProductsInShopsContains.ToList();
                }
                else if (CurrentTableSource.currentTable == "ProductToProductStock")
                {
                    var button = sender as Button;
                    var shoe = button.DataContext as SuppliesProductsInProductStockContains;
                    shoe.Product.CountInStock -= shoe.CountOfShoe;
                    MainWindow.shoesKursovoiEntities.SuppliesProductsInProductStockContains.Remove(shoe);
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    lvShoes.ItemsSource = null;
                    lvShoes.ItemsSource = suppliesTables.ProductToProductStockTable.selectedSupply.SuppliesProductsInProductStockContains.ToList();
                }
                else
                {
                    var button = sender as Button;
                    var material = button.DataContext as SuppliesMaterialsInMaterialStockContains;
                    material.Material.CountInStock -= material.CountOfMaterial;
                    MainWindow.shoesKursovoiEntities.SuppliesMaterialsInMaterialStockContains.Remove(material);
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    lvMaterial.ItemsSource = null;
                    lvMaterial.ItemsSource = suppliesTables.MaterialToMaterialStockTable.selectedSupply.SuppliesMaterialsInMaterialStockContains.ToList();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btAddContainClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentTableSource.currentTable == "ProductToShop")
                {
                    int selectedIndex = suppliesTables.ProductToShopsTable.tableLink.SelectedIndex;
                    SuppliesProductsInShopsContains shoe = new SuppliesProductsInShopsContains();
                    shoe.ID = 0;
                    shoe.IDSupply = suppliesTables.ProductToShopsTable.selectedSupply.IDSupply;
                    EditWindows.WindowAddContainSupplyProductInShop windowAddContainSupplyProductInShop = new EditWindows.WindowAddContainSupplyProductInShop(shoe, suppliesTables.ProductToShopsTable.selectedSupply);
                    windowAddContainSupplyProductInShop.ShowDialog();
                    suppliesTables.ProductToShopsTable.tableLink.SelectedIndex = -1;
                    suppliesTables.ProductToShopsTable.tableLink.SelectedIndex = selectedIndex;
                }
                if (CurrentTableSource.currentTable == "ProductToProductStock")
                {
                    int selectedIndex = suppliesTables.ProductToProductStockTable.tableLink.SelectedIndex;
                    SuppliesProductsInProductStockContains shoe = new SuppliesProductsInProductStockContains();
                    shoe.ID = 0;
                    shoe.IDSupply = suppliesTables.ProductToProductStockTable.selectedSupply.IDSupply;
                    EditWindows.WindowAddContainSupplyProductInShop windowAddContainSupplyProductInShop = new EditWindows.WindowAddContainSupplyProductInShop(shoe, suppliesTables.ProductToProductStockTable.selectedSupply);
                    windowAddContainSupplyProductInShop.ShowDialog();
                    suppliesTables.ProductToProductStockTable.tableLink.SelectedIndex = -1;
                    suppliesTables.ProductToProductStockTable.tableLink.SelectedIndex = selectedIndex;
                }
                if (CurrentTableSource.currentTable == "MaterialToMaterialStock")
                {
                    int selectedIndex = suppliesTables.MaterialToMaterialStockTable.tableLink.SelectedIndex;
                    SuppliesMaterialsInMaterialStockContains shoe = new SuppliesMaterialsInMaterialStockContains();
                    shoe.ID = 0;
                    shoe.IDSupply = suppliesTables.MaterialToMaterialStockTable.selectedSupply.IDSupply;
                    EditWindows.WindowAddContainSupplyMaterialStock windowAddContainSupplyProductInShop = new EditWindows.WindowAddContainSupplyMaterialStock(shoe, suppliesTables.MaterialToMaterialStockTable.selectedSupply);
                    windowAddContainSupplyProductInShop.ShowDialog();
                    suppliesTables.MaterialToMaterialStockTable.tableLink.SelectedIndex = -1;
                    suppliesTables.MaterialToMaterialStockTable.tableLink.SelectedIndex = selectedIndex;
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }
    }
}
