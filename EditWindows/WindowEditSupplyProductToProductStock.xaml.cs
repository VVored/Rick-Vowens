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
    /// Логика взаимодействия для WindowEditSupplyProductToProductStock.xaml
    /// </summary>
    public partial class WindowEditSupplyProductToProductStock : Window
    {
        SuppliesProductsInProductStock supply;
        SuppliesMaterialsInMaterialStock supplyMaterial;
        public WindowEditSupplyProductToProductStock(SuppliesProductsInProductStock supply)
        {
            InitializeComponent();
            this.supply = supply;
            DataContext = supply;
        }
        public WindowEditSupplyProductToProductStock(SuppliesMaterialsInMaterialStock supplyMaterial)
        {
            InitializeComponent();
            this.supplyMaterial = supplyMaterial;
            DataContext = supplyMaterial;
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            try
            {
                if (supply != null)
                {
                    if (supply.IDSupply == 0)
                    {
                        MainWindow.shoesKursovoiEntities.SuppliesProductsInProductStock.Add(supply);
                    }
                }
                else
                {
                    if (supplyMaterial.IDSupply == 0)
                    {
                        MainWindow.shoesKursovoiEntities.SuppliesMaterialsInMaterialStock.Add(supplyMaterial);
                    }
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
