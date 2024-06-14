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
    /// Логика взаимодействия для WindowAddContainSupplyMaterialStock.xaml
    /// </summary>
    public partial class WindowAddContainSupplyMaterialStock : Window
    {
        SuppliesMaterialsInMaterialStock supply;
        SuppliesMaterialsInMaterialStockContains material;
        int oldCount;
        public WindowAddContainSupplyMaterialStock(SuppliesMaterialsInMaterialStockContains material, SuppliesMaterialsInMaterialStock supply)
        {
            InitializeComponent();
            this.material = material;
            this.supply = supply;
            DataContext = material;
            if (material.ID != 0)
            {
                oldCount = material.CountOfMaterial;
            }
            cbNamesMaterials.ItemsSource = MainWindow.shoesKursovoiEntities.Material.ToList();
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            if (material.ID == 0)
            {
                AddCountOfMaterial(material.CountOfMaterial, material.IDMaterial);
                List<SuppliesMaterialsInMaterialStockContains> suppliesMaterialsInMaterialStockContains = supply.SuppliesMaterialsInMaterialStockContains.ToList();
                bool duplicate = false;
                foreach(var i in suppliesMaterialsInMaterialStockContains)
                {
                    if (i.IDMaterial == material.IDMaterial)
                    {
                        duplicate = true;
                    }
                }
                if (!duplicate && material.CountOfMaterial > 0)
                {
                    supply.SuppliesMaterialsInMaterialStockContains.Add(material);
                }
                else
                {
                    MessageBox.Show("Некорректный ввод.");
                    return;
                }
            }
            else
            {
                changeCountOfMaterialWhenEdit(material.CountOfMaterial, material.IDMaterial, oldCount);
            }
            MainWindow.shoesKursovoiEntities.SaveChanges();
            Close();
        }

        public void changeCountOfMaterialWhenEdit(int count, int materialID, int oldCount)
        {
            var neededMaterial = FindMaterialByID(materialID);
            if (oldCount > count)
            {
                neededMaterial.CountInStock -= oldCount - count;
            }
            else if(count > oldCount)
            {
                neededMaterial.CountInStock += count - oldCount;
            }
        }

        public void AddCountOfMaterial(int count, int materialID)
        {
            var neededMaterial = FindMaterialByID(materialID);
            neededMaterial.CountInStock += count;
        }

        public Material FindMaterialByID(int materialID)
        {
            Material newMaterial = new Material();
            var listOfMaterial = MainWindow.shoesKursovoiEntities.Material.ToList();
            foreach (Material i in listOfMaterial)
            {
                if (i.IDMaterial == materialID)
                {
                    newMaterial = i;
                    break;
                }
            }
            return newMaterial;
        }
    }
}
