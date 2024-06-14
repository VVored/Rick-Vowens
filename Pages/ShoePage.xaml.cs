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
using System.Runtime.Caching;
using System.Xml;
using System.Threading;

namespace RickVowens.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShoePage.xaml
    /// </summary>
    public partial class ShoePage : Page
    {
        IEnumerable<Product> shoes = MainWindow.shoesKursovoiEntities.Product.ToList();
        List<Product> listShoes;
        private int _currentPage = 1;
        private int _countShoes = 4;
        private int _maxPages;
        public ShoePage()
        {
            InitializeComponent();
            lvShoes.ItemsSource = MainWindow.shoesKursovoiEntities.Product.ToList();
            Refresh();
        }
        /// <summary>
        /// Фильтрация списка обуви на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSort(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            SortListOfShoe();
            Refresh();
        }
        /// <summary>
        /// Фильтрация списка обуви (Основная механика)
        /// </summary>
        public void SortListOfShoe()
        {
            List<int> gendersToSearch = new List<int>();
            List<string> ageToSearch = new List<string>();

            if (male.IsChecked == true)
            {
                gendersToSearch.Add(2);
            }
            if (female.IsChecked == true)
            {
                gendersToSearch.Add(1);
            }
            if (unisex.IsChecked == true)
            {
                gendersToSearch.Add(3);
            }
            if (old.IsChecked == true)
            {
                ageToSearch.Add("б");
            }
            if (child.IsChecked == true)
            {
                ageToSearch.Add("м");
            }
            if (gendersToSearch.Count == 0)
            {
                gendersToSearch = new List<int> { 1, 2, 3 };
            }
            if (ageToSearch.Count == 0)
            {
                ageToSearch = new List<string> { "б", "м" };
            }
            shoes = MainWindow.shoesKursovoiEntities.Product.ToList()
                .Where(elem => gendersToSearch.Contains(elem.IdGender) && ageToSearch.Contains(elem.Age));
            lvShoes.ItemsSource = shoes;
        }
        /// <summary>
        /// Добавление обуви
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAddClick(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                _currentPage = 1;
                Product shoe = new Product();
                shoe.Articul = 0;
                EditWindows.WindowEditShoe windowEditShoe = new EditWindows.WindowEditShoe(shoe);
                windowEditShoe.ShowDialog();
                SortListOfShoe();
                Refresh();
            }
            else
            {
                MessageBox.Show("Это может делать только администратор.");
            }
        }
        /// <summary>
        /// Редактирование обуви
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEditShoe(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                _currentPage = 1;
                var button = sender as Button;
                var shoe = button.DataContext as Product;
                EditWindows.WindowEditShoe windowEditShoe = new EditWindows.WindowEditShoe(shoe);
                windowEditShoe.ShowDialog();
                SortListOfShoe();
                Refresh();
            }
            else
            {
                MessageBox.Show("Это может делать только администратор.");
            }
        }
        /// <summary>
        /// Удаление обуви
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelete(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                _currentPage = 1;
                var button = sender as Button;
                var shoe = button.DataContext as Product;
                var cache = MemoryCache.Default;

                if (shoe != null)
                {
                    cache.Add(shoe.MemoryCacheKey, shoe, DateTimeOffset.Now.AddMinutes(2));
                    MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow.shoesKursovoiEntities.Product.Remove(shoe);
                        MainWindow.shoesKursovoiEntities.SaveChanges();
                        var data = cache.Get(shoe.MemoryCacheKey) as Product;
                        MessageBox.Show(data.Name);
                    }
                }
                SortListOfShoe();
                Refresh();
            }
            else
            {
                MessageBox.Show("Это может делать только администратор.");
            }
        }
        /// <summary>
        /// Обновление пагинации
        /// </summary>
        private void Refresh()
        {
            listShoes = shoes.ToList();
            _maxPages = (int)Math.Ceiling(listShoes.Count * 1.0 / _countShoes);

            var listHotelPage = listShoes.Skip((_currentPage - 1) * _countShoes).Take(_countShoes).ToList();

            TxtCurrentPage.Text = _currentPage.ToString();
            LblTotalPages.Text = "из " + _maxPages;
            lvShoes.ItemsSource = listHotelPage;
        }
        /// <summary>
        /// Переход на первую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToFirstPage(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            Refresh();
        }
        /// <summary>
        /// Переход на предыдущую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage <= 1) _currentPage = 1;
            else
                _currentPage--;
            Refresh();
        }
        /// <summary>
        /// Переход на следующую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToNextPage(object sender, RoutedEventArgs e)
        {
            if (_currentPage >= _maxPages) _currentPage = _maxPages;
            else
                _currentPage++;
            Refresh();
        }
        /// <summary>
        /// Переход на последнюю страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToLastPage(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPages;
            Refresh();
        }
        /// <summary>
        /// Просмотр списка материалов, используемых в обуви
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btClickMaterialsInShoe(object sender, RoutedEventArgs e)
        {
            if (DataBank.currentUser.Post == "администратор")
            {
                var button = sender as Button;
                var shoe = button.DataContext as Product;
                EditWindows.WindowMaterialsInShoe windowMaterialsInShoe = new EditWindows.WindowMaterialsInShoe(shoe);
                windowMaterialsInShoe.ShowDialog();
                lvShoes.ItemsSource = null;
                lvShoes.ItemsSource = shoes;
                Refresh();
            }
            else
            {
                MessageBox.Show("Это может делать только администратор.");
            }
        }
        /// <summary>
        /// Поиск обуви по наименованию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbSearch.Text != "" || tbSearch.Text != "Поиск...")
            {
                shoes = shoes
                    .Where(shoe => shoe.Name.ToLower().Contains(tbSearch.Text.ToLower()));
            }
            Refresh();
        }

        private void btImport(object sender, RoutedEventArgs e)
        {
            try
            {
                string filename = "";
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.DefaultExt = ".xml";
                dialog.Filter = "XML files (*.xml)|*.xml";

                bool? result = dialog.ShowDialog();

                if (result == true)
                {
                    List<Product> importedProducts = new List<Product>();
                    filename = dialog.FileName;
                    using (XmlReader reader = XmlReader.Create(filename))
                    {
                        while (reader.Read())
                        {
                            Product newProduct = new Product();
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "idtype")
                            {
                                newProduct.IDType = int.Parse(reader.ReadElementContentAsString()); 
                            }
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "name")
                            {
                                newProduct.Name = reader.ReadElementContentAsString(); 
                            }
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "countinstock")
                            {
                                newProduct.CountInStock = int.Parse(reader.ReadElementContentAsString()); 
                            }
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "image")
                            {
                                newProduct.Image = reader.ReadElementContentAsString(); 
                            }
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "idgender")
                            {
                                newProduct.IdGender = int.Parse(reader.ReadElementContentAsString()); 
                            }
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "age")
                            {
                                newProduct.Age = reader.ReadElementContentAsString(); 
                            }
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "costwithoutnds")
                            {
                                newProduct.CostWithoutNDS = decimal.Parse(reader.ReadElementContentAsString()); 
                            }
                            if (newProduct.CostWithoutNDS != 0)
                            {
                                MainWindow.shoesKursovoiEntities.Product.Add(newProduct);
                            }
                        }
                    }
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btExport(object sender, RoutedEventArgs e)
        {
            List<Product> selectedProducts = new List<Product>();
            if (lvShoes.SelectedItems.Count >= 1)
            {
                foreach (Product i in lvShoes.SelectedItems)
                {
                    selectedProducts.Add(i);
                }
                using (XmlWriter writer = XmlWriter.Create("../../xmlExport/output.xml"))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("products");
                    foreach (Product i in selectedProducts)
                    {
                        writer.WriteStartElement("product");

                        writer.WriteElementString("articul", i.Articul.ToString());
                        writer.WriteElementString("idtype", i.IDType.ToString());
                        writer.WriteElementString("name", i.Name);
                        writer.WriteElementString("countinstock", i.CountInStock.ToString());
                        writer.WriteElementString("image", i.Image);
                        writer.WriteElementString("idgender", i.IdGender.ToString());
                        writer.WriteElementString("age", i.Age);
                        writer.WriteElementString("costwithoutnds", i.CostWithoutNDS.ToString());

                        writer.WriteEndElement();
                    }
                    
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали элементы для экспорта.");
            }
            
        }
    }
}
