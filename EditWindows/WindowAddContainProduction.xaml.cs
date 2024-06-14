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
    /// Логика взаимодействия для WindowAddContainProduction.xaml
    /// </summary>
    public partial class WindowAddContainProduction : Window
    {
        Production production;
        ProductionContain productionContain;
        public WindowAddContainProduction(Production production, ProductionContain productionContain)
        {
            InitializeComponent();
            this.production = production;
            this.productionContain = productionContain;
            DataContext = productionContain;
            cbNamesShoes.ItemsSource = MainWindow.shoesKursovoiEntities.Product.ToList();
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            if (productionContain.ID == 0)
            {
                List<Material> materialsForManipulations = new List<Material>();
                List<int> values = new List<int>();
                foreach (MaterialOfProduct i in findProductByArticul().MaterialOfProduct.ToList())
                {
                    materialsForManipulations.Add(i.Material);
                    values.Add(i.CountOfMaterial);
                }
                for (int i = 0; i < materialsForManipulations.Count; i++)
                {
                    if (materialsForManipulations[i].CountInStock - values[i] >= 0)
                    {
                        materialsForManipulations[i].CountInStock -= values[i];
                    }
                    else
                    {
                        MessageBox.Show($"На складе не хватает {materialsForManipulations[i].Name} для данной операции.", "Внимание");
                        return;
                    }
                }
                List<ProductionContain> productionContains = production.ProductionContain.ToList();
                bool duplicate = false;
                foreach (var i in productionContains)
                {
                    if (i.Articul == productionContain.Articul)
                    {
                        duplicate = true;
                    }
                }
                if (!duplicate && productionContain.CountOfProduct > 0)
                {
                    MainWindow.shoesKursovoiEntities.ProductionContain.Add(productionContain);
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
        public Product findProductByArticul()
        {
            var product = new Product();
            foreach(Product i in MainWindow.shoesKursovoiEntities.Product.ToList())
            {
                if (productionContain.Articul == i.Articul)
                {
                    product = i;
                }
            }
            return product;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
