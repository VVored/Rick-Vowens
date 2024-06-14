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
    /// Логика взаимодействия для WindowAddMaterial.xaml
    /// </summary>
    public partial class WindowAddMaterial : Window
    {
        Material newMaterial;
        public WindowAddMaterial(Material newMaterial)
        {
            InitializeComponent();
            this.newMaterial = newMaterial;
            DataContext = newMaterial;
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            if (newMaterial.IDMaterial == 0)
            {
                List<Material> materials = MainWindow.shoesKursovoiEntities.Material.ToList();
                bool duplicate = false;
                foreach (Material i in materials)
                {
                    if (i.IDMaterial == newMaterial.IDMaterial)
                    {
                        duplicate = true;
                    }
                }
                if (!duplicate && newMaterial.CountInStock > 0)
                {
                    MainWindow.shoesKursovoiEntities.Material.Add(newMaterial);
                }
                else
                {
                    MessageBox.Show("Некорретный ввод.");
                    return;
                }
            }
            MainWindow.shoesKursovoiEntities.SaveChanges();
            this.Close();
        }
    }
}
