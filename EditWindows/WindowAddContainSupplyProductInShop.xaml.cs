using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RickVowens.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для WindowAddContainSupplyProductInShop.xaml
    /// </summary>
    public partial class WindowAddContainSupplyProductInShop : Window
    {
        SuppliesProductsInShopsContains shoe;
        SuppliesProductsInShops supply;
        SuppliesProductsInProductStockContains shoeProduct;
        SuppliesProductsInProductStock supplyProduct;
        public WindowAddContainSupplyProductInShop(SuppliesProductsInShopsContains shoe, SuppliesProductsInShops supply)
        {
            InitializeComponent();
            this.shoe = shoe;
            this.supply = supply;
            DataContext = shoe;
            cbShoe.ItemsSource = MainWindow.shoesKursovoiEntities.Product.ToList();
        }
        public WindowAddContainSupplyProductInShop(SuppliesProductsInProductStockContains shoeProduct, SuppliesProductsInProductStock supplyProduct)
        {
            InitializeComponent();
            this.shoeProduct = shoeProduct;
            this.supplyProduct = supplyProduct;
            DataContext = shoeProduct;
            cbShoe.ItemsSource = MainWindow.shoesKursovoiEntities.Product.ToList();
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            try
            {
                if (shoe != null)
                {
                    if (shoe.ID == 0)
                    {
                        removeCountOfProduct(shoe.CountOfShoe, shoe.ShoeArticul);
                        List<SuppliesProductsInShopsContains> suppliesProductsInShopsContains = supply.SuppliesProductsInShopsContains.ToList();
                        bool duplicate = false;
                        foreach(var i in suppliesProductsInShopsContains)
                        {
                            if (i.ShoeArticul == shoe.ShoeArticul)
                            {
                                duplicate = true;
                            }
                        }
                        if (!duplicate)
                        {
                            supply.SuppliesProductsInShopsContains.Add(shoe);
                        }
                        else
                        {
                            MessageBox.Show("Товар уже присутствует");
                        }
                        if (shoe.CountOfShoe <= 0)
                        {
                            MessageBox.Show("Введите корректное количество товара");
                        }
                    }
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    Close();
                }
                else
                {
                    if (shoeProduct.ID == 0)
                    {
                        addCountOfProduct(shoeProduct.CountOfShoe, shoeProduct.ShoeArticul);
                        List<SuppliesProductsInProductStockContains> suppliesProductsInProductStockContains = supplyProduct.SuppliesProductsInProductStockContains.ToList();
                        bool duplicate = false;
                        foreach (var i in suppliesProductsInProductStockContains)
                        {
                            if (i.ShoeArticul == shoeProduct.ShoeArticul)
                            {
                                duplicate = true;
                            }
                        }
                        if (!duplicate && shoeProduct.CountOfShoe > 0)
                        {
                            supplyProduct.SuppliesProductsInProductStockContains.Add(shoeProduct);
                        }
                        else
                        {
                            MessageBox.Show("Некорректный ввод.");
                            return;
                        }
                    }
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        public void addCountOfProduct(int count, int articul)
        {
            Product neededProduct = findProductByArticul(articul);
            neededProduct.CountInStock += count;
        }
        public void removeCountOfProduct(int count, int articul)
        {
            Product neededProduct = findProductByArticul(articul);
            neededProduct.CountInStock -= count;
        }
        public Product findProductByArticul(int articul)
        {
            var product = new Product();
            foreach (Product i in MainWindow.shoesKursovoiEntities.Product.ToList())
            {
                if (articul == i.Articul)
                {
                    product = i;
                }
            }
            return product;
        }
    }
}
