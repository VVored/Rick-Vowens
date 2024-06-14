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
    /// Логика взаимодействия для WindowEditSupplyProductToShop.xaml
    /// </summary>
    public partial class WindowEditSupplyProductToShop : Window
    {
        SuppliesProductsInShops supply;
        public WindowEditSupplyProductToShop(SuppliesProductsInShops supply)
        {
            InitializeComponent();
            this.supply = supply;
            DataContext = supply;
            cbShopAdress.ItemsSource = MainWindow.shoesKursovoiEntities.Shops.ToList();
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            try
            {
                if (supply.IDSupply == 0)
                {
                    MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.Add(supply);
                }
                MainWindow.shoesKursovoiEntities.SaveChanges();
                this.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
