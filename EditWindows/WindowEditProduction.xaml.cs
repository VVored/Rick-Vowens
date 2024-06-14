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
    /// Логика взаимодействия для WindowEditProduction.xaml
    /// </summary>
    public partial class WindowEditProduction : Window
    {
        Production production;
        public WindowEditProduction(Production production)
        {
            InitializeComponent();
            this.production = production;
            DataContext = production;
            List<int> IDDepartaments = new List<int>();
            foreach(Departaments prod in MainWindow.shoesKursovoiEntities.Departaments.ToList())
            {
                IDDepartaments.Add(prod.IDDepartment);
            }
            cbDepartaments.ItemsSource = IDDepartaments;
        }

        private void btSaveChanges(object sender, RoutedEventArgs e)
        {
            if (production.IDProduction == 0)
            {
                if (production.CountOfWorkers > 0 && production.DateProduction != null && production.IDDepartment != 0)
                {
                    MainWindow.shoesKursovoiEntities.Production.Add(production);
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
}
