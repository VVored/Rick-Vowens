using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace RickVowens.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для WindowEditShoe.xaml
    /// </summary>
    public partial class WindowEditShoe : Window
    {
        Product shoe;
        public WindowEditShoe(Product shoe)
        {
            InitializeComponent();
            this.shoe = shoe;
            DataContext = shoe;
            cbGender.ItemsSource = MainWindow.shoesKursovoiEntities.Gender.ToList();
            cbTypeShoe.ItemsSource = MainWindow.shoesKursovoiEntities.TypeOfProduct.ToList();
            cbImage.ItemsSource = MainWindow.shoesKursovoiEntities.Product.ToList();
        }

        private void btSave(object sender, RoutedEventArgs e)
        {
            try
            {
                if (shoe.Articul == 0)
                {
                    MainWindow.shoesKursovoiEntities.Product.Add(shoe);
                }
                if (shoe.CostWithoutNDS > 0 && shoe.CountInStock > 0 && shoe.IdGender != 0 && shoe.Age != null && shoe.IDType != 0 && shoe.Image != null && shoe.Name != null)
                {
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    Close();
                }
                else
                {
                    MessageBox.Show("Некорректный ввод.");
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btLoadImage(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = "";
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.DefaultExt = ".jpg";
                dialog.Filter = "Images (*.png;*.jpg;*jpeg)|*.png;*.jpg;*jpeg";

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    filename = dialog.FileName;
                    string fileTitle = System.IO.Path.GetFileName(filename);
                    string path = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + "/imgs/" + fileTitle;
                    if (!File.Exists(path))
                        File.Copy(filename, path, true);

                    var newImgs = MainWindow.shoesKursovoiEntities.Product.ToList();
                    shoe.Image = "imgs/" + fileTitle;
                    newImgs.Add(shoe);
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    cbImage.ItemsSource = newImgs;
                    cbImage.SelectedItem = shoe;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
