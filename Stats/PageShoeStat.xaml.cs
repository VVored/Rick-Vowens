using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using excel = Microsoft.Office.Interop.Excel;

namespace RickVowens.Stats
{
    /// <summary>
    /// Логика взаимодействия для PageShoeStat.xaml
    /// </summary>
    public partial class PageShoeStat : Page
    {
        Product shoe = null;
        public PageShoeStat()
        {
            InitializeComponent();
            cbShoes.ItemsSource = MainWindow.shoesKursovoiEntities.Product.ToList();
            cbShoes.SelectedIndex = 0;
            cbPeriod.SelectedIndex = 0;
        }

        public LineSeries Shop1Series { get; set; }
        public LineSeries Shop2Series { get; set; }
        public string[] Labels { get; set; }
        /// <summary>
        /// Смена товара, по которому просматривается график
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = sender as ComboBox;
            var shoe = comboBoxItem.SelectedValue as Product;
            this.shoe = shoe;
            var converter = new ImageSourceConverter();
            shoeImage.Source = (ImageSource)converter.ConvertFromString(shoe.ImagePath);
            nameOfShoe.Text = shoe.Name;
            List<SuppliesProductsInShops> supShopWithSelectedShoes = new List<SuppliesProductsInShops>();
            List<SuppliesProductsInShops> allSup = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList();
            foreach (SuppliesProductsInShops sup in allSup)
            {
                var containInSup = sup.SuppliesProductsInShopsContains.ToList();
                for (int i = 0; i < containInSup.Count; i++)
                {
                    if (containInSup[i].ShoeArticul == shoe.Articul)
                    {
                        supShopWithSelectedShoes.Add(sup);
                    }
                }
            }
            ChartValues<int> values1 = new ChartValues<int>();
            ChartValues<int> values2 = new ChartValues<int>();
            for (int i = 1; i < 13; i++)
            {
                var supplyShop1 = supShopWithSelectedShoes.Where(sup => sup.Date.Month == i && sup.IDShop == 1);
                var supplyShop2 = supShopWithSelectedShoes.Where(sup => sup.Date.Month == i && sup.IDShop == 2);
                int resultPerMonth1 = 0;
                int resultPerMonth2 = 0;
                foreach (SuppliesProductsInShops sup in supplyShop1)
                {
                    var contain = sup.SuppliesProductsInShopsContains.ToList();
                    for (int j = 0; j < contain.Count; j++)
                    {
                        if (contain[j].ShoeArticul == shoe.Articul)
                            resultPerMonth1 += contain[j].CountOfShoe;
                    }
                }
                foreach (SuppliesProductsInShops sup in supplyShop2)
                {
                    var contain = sup.SuppliesProductsInShopsContains.ToList();
                    for (int j = 0; j < contain.Count; j++)
                    {
                        if (contain[j].ShoeArticul == shoe.Articul)
                            resultPerMonth2 += contain[j].CountOfShoe;
                    }
                }
                values1.Add(resultPerMonth1);
                values2.Add(resultPerMonth2);
            }
            Labels = new[] { "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек", "Фев"};
            Shop1Series = new LineSeries
            {
                Values = values1
            };
            Shop1Series.Title = MainWindow.shoesKursovoiEntities.Shops.ToList()[0].Adress;
            Shop2Series = new LineSeries
            {
                Values = values2
            };
            Shop2Series.Title = MainWindow.shoesKursovoiEntities.Shops.ToList()[1].Adress;
            cartesianChart1.Series = new SeriesCollection
            {
                Shop1Series,
                Shop2Series
            };
            DataContext = this;
        }
        /// <summary>
        /// Открытие Excel-таблицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void excelStat(object sender, RoutedEventArgs e)
        {
            if (cbPeriod.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите период.");
            }
            else
            {
                excel.Application excelApp = new excel.Application();
                excel.Workbook workBook;
                workBook = excelApp.Workbooks.Add();
                excel.Worksheet workSheet;
                workSheet = workBook.Worksheets[1];
                workSheet.Cells[2, 1] = "Наименование";
                workSheet.Range[workSheet.Cells[1, 2], workSheet.Cells[1, 4]].Merge();
                workSheet.Cells[1, 2] = "Точки отправки";
                workSheet.Cells[1, 2].Style.HorizontalAlignment = excel.XlHAlign.xlHAlignCenter;
                for (int i = 0; i < MainWindow.shoesKursovoiEntities.Shops.ToList().Count; i++)
                {
                    workSheet.Cells[2, i + 2] = MainWindow.shoesKursovoiEntities.Shops.ToList()[i].Adress;
                }
                workSheet.Cells[2, 4] = "Склад ГП";
                List<Product> products = MainWindow.shoesKursovoiEntities.Product.ToList();
                for (int i = 0; i < products.Count; i++)
                {
                    workSheet.Cells[i + 3, 1] = products[i].Name;
                    decimal result = moneyForShoesInShops(1, products, i);
                    workSheet.Cells[i + 3, 2] = result;
                    result = moneyForShoesInShops(2, products, i);
                    workSheet.Cells[i + 3, 3] = result;
                    result = moneyForShoesInStock(products, i);
                    workSheet.Cells[i + 3, 4] = result;
                }
                excel.Range rangeAllTable = workSheet.Range[$"A1:D{products.Count + 2}"];
                rangeAllTable.EntireColumn.AutoFit();
                rangeAllTable.EntireRow.AutoFit();
                excelApp.Visible = true;
            }
        }
        /// <summary>
        /// Расчет общей стоимости для обуви в магазинах (для Excel-таблицы)
        /// </summary>
        /// <param name="IDShop"></param>
        /// <param name="products"></param>
        /// <param name="i"></param>
        public decimal moneyForShoesInShops(int IDShop, List<Product> products, int i)
        {
            decimal resultShop = 0;
            List<SuppliesProductsInShops> supplies = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList();
            var query = supplies.Where(sup => sup.IDShop == IDShop).ToList();
            if (cbPeriod.SelectedIndex == 1)
            {
                query = query.Where(sup => sup.Date.Year == DateTime.Now.Year).ToList();
            }
            else if (cbPeriod.SelectedIndex == 0)
            {
                query = query.Where(sup => sup.Date.Year == DateTime.Now.Year && sup.Date.Month == DateTime.Now.Month).ToList();
            }
            foreach (var j in query)
            {
                List<SuppliesProductsInShopsContains> contain = j.SuppliesProductsInShopsContains.ToList();
                for (int cont = 0; cont < contain.Count; cont++)
                {
                    if (contain[cont].ShoeArticul == products[i].Articul)
                    {
                        resultShop += contain[cont].CountOfShoe * products[i].CostWithNDS;
                    }
                }
            }

            return resultShop;
        }
        /// <summary>
        /// Расчет общей стоимости для обуви на складе (для Excel-таблицы)
        /// </summary>
        /// <param name="products"></param>
        /// <param name="i"></param>
        public decimal moneyForShoesInStock(List<Product> products, int i)
        {
            List<SuppliesProductsInProductStock> supplies = MainWindow.shoesKursovoiEntities.SuppliesProductsInProductStock.ToList();
            if (cbPeriod.SelectedIndex == 1)
            {
                supplies = supplies.Where(sup => sup.Date.Year == DateTime.Now.Year).ToList();
            }
            else if (cbPeriod.SelectedIndex == 0)
            {
                supplies = supplies.Where(sup => sup.Date.Year == DateTime.Now.Year && sup.Date.Month == DateTime.Now.Month).ToList();
            }
            decimal result = 0;
            foreach (var j in supplies)
            {
                List<SuppliesProductsInProductStockContains> contain = j.SuppliesProductsInProductStockContains.ToList();
                for (int cont = 0; cont < contain.Count; cont++)
                {
                    if (contain[cont].ShoeArticul == products[i].Articul)
                    {
                        result += contain[cont].CountOfShoe * products[i].CostWithNDS;
                    }
                }
            }
            return result;
        }
    }
}
