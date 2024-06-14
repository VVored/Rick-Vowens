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
    /// Логика взаимодействия для WindowEditMaterial.xaml
    /// </summary>
    public partial class WindowEditMaterial : Window
    {
        MaterialOfProduct material;
        Product shoe;
        public WindowEditMaterial(Product shoe, MaterialOfProduct material)
        {
            InitializeComponent();
            this.material = material;
            this.shoe = shoe;
            cbNamesMaterials.ItemsSource = MainWindow.shoesKursovoiEntities.Material.ToList();
            DataContext = material;
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            if (material.ID == 0)
            {
                List<MaterialOfProduct> materialsOfProduct = shoe.MaterialOfProduct.ToList();
                bool duplicateMaterial = false;
                foreach(MaterialOfProduct mat in materialsOfProduct)
                {
                    if (mat.IDMaterial == material.IDMaterial)
                    {
                        duplicateMaterial = true;
                    }
                }
                if (!duplicateMaterial && material.CountOfMaterial > 0)
                {
                    MainWindow.shoesKursovoiEntities.MaterialOfProduct.Add(material);
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

        private void btAddNewMaterial(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                Material newMaterial = new Material();
                newMaterial.IDMaterial = 0;
                EditWindows.WindowAddMaterial windowAddMaterial = new WindowAddMaterial(newMaterial);
                windowAddMaterial.ShowDialog();
                cbNamesMaterials.ItemsSource = null;
                cbNamesMaterials.ItemsSource = MainWindow.shoesKursovoiEntities.Material.ToList();
            }
            else
            {
                MessageBox.Show("Это может делать только администратор");
            }
        }
    }
}
