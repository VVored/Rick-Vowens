using LiveCharts;
using LiveCharts.Wpf;
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
using excel = Microsoft.Office.Interop.Excel;


namespace RickVowens.Stats
{
    /// <summary>
    /// Логика взаимодействия для PageSuppliesStat.xaml
    /// </summary>
    public partial class PageSuppliesStat : Page
    {
        public PageSuppliesStat()
        {
            InitializeComponent();
            cbPeriod.SelectedIndex = 0;
            cbSupplies.SelectedIndex = 0;
        }
        public LineSeries Shop1Series { get; set; }
        public LineSeries Shop2Series { get; set; }
        public LineSeries ProdToProdSeries { get; set; }
        public LineSeries MaterialToMaterialSeries { get; set; }
        public string[] Labels { get; set; }

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
                workSheet.Cells[1, 1] = "Вид отгрузки/поставки";
                workSheet.Cells[1, 2] = "Количество";

                workSheet.Cells[2, 1] = "Магазин";
                workSheet.Cells[3, 1] = "Склад ГП";
                workSheet.Cells[4, 1] = "Склад материалов";

                List<SuppliesProductsInShops> prodToShop = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList();
                List<SuppliesProductsInProductStock> prodToProd = MainWindow.shoesKursovoiEntities.SuppliesProductsInProductStock.ToList();
                List<SuppliesMaterialsInMaterialStock> materialToMaterial = MainWindow.shoesKursovoiEntities.SuppliesMaterialsInMaterialStock.ToList();

                if (cbPeriod.SelectedIndex == 0)
                {
                    prodToShop = prodToShop
                        .Where(sup => sup.Date.Month == DateTime.Now.Month).ToList();
                    prodToProd = prodToProd
                        .Where(sup => sup.Date.Month == DateTime.Now.Month).ToList();
                    materialToMaterial = materialToMaterial
                        .Where(sup => sup.Date.Month == DateTime.Now.Month).ToList();
                }
                else if (cbPeriod.SelectedIndex == 1)
                {
                    prodToShop = prodToShop
                        .Where(sup => sup.Date.Month == DateTime.Now.Month && sup.Date.Year == DateTime.Now.Year).ToList();
                    prodToProd = prodToProd
                        .Where(sup => sup.Date.Month == DateTime.Now.Month && sup.Date.Year == DateTime.Now.Year).ToList();
                    materialToMaterial = materialToMaterial
                        .Where(sup => sup.Date.Month == DateTime.Now.Month && sup.Date.Year == DateTime.Now.Year).ToList();
                }
                workSheet.Cells[2, 2] = prodToShop.Count();
                workSheet.Cells[3, 2] = prodToProd.Count();
                workSheet.Cells[4, 2] = materialToMaterial.Count();

                excel.Range rangeAllTable = workSheet.Range[$"A1:B3"];
                rangeAllTable.EntireColumn.AutoFit();
                rangeAllTable.EntireRow.AutoFit();

                 excelApp.Visible = true;
            }
        }

        private void cbSupplies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSupplies.SelectedIndex == 0)
            {
                var prodToShopAll = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList();
                ChartValues<int> perMonth1 = new ChartValues<int>();
                ChartValues<int> perMonth2 = new ChartValues<int>();
                for (int i = 1; i < 13; i++)
                {
                    var prodToShop1 = prodToShopAll.Where(sup => sup.Date.Month == i && sup.IDShop == 1).ToList();
                    var prodToShop2 = prodToShopAll.Where(sup => sup.Date.Month == i && sup.IDShop == 2).ToList();
                    perMonth1.Add(prodToShop1.Count);
                    perMonth2.Add(prodToShop2.Count);
                }
                Shop1Series = new LineSeries
                {
                    Values = perMonth1,
                    Title = MainWindow.shoesKursovoiEntities.Shops.ToList()[0].Adress
                };
                Shop2Series = new LineSeries
                {
                    Values = perMonth2,
                    Title = MainWindow.shoesKursovoiEntities.Shops.ToList()[1].Adress
                };
                cartesianChart1.Series = new SeriesCollection
                {
                    Shop1Series,
                    Shop2Series
                };
            }
            else if (cbSupplies.SelectedIndex == 1)
            {
                var prodToProd = MainWindow.shoesKursovoiEntities.SuppliesProductsInProductStock.ToList();
                ChartValues<int> perMonth = new ChartValues<int>();
                for (int i = 1; i < 13; i++)
                {
                    var prodToProdPerMonth = prodToProd.Where(sup => sup.Date.Month == i).ToList();
                    perMonth.Add(prodToProdPerMonth.Count);
                }
                ProdToProdSeries = new LineSeries
                {
                    Values = perMonth,
                    Title = "Склад ГП"
                };
                cartesianChart1.Series = new SeriesCollection
                {
                    ProdToProdSeries
                };
            }
            else if (cbSupplies.SelectedIndex == 2)
            {
                var materialToMaterial = MainWindow.shoesKursovoiEntities.SuppliesMaterialsInMaterialStock.ToList();
                ChartValues<int> perMonth = new ChartValues<int>();
                for (int i = 1; i < 13; i++)
                {
                    var materialToMaterialMonth = materialToMaterial.Where(sup => sup.Date.Month == i).ToList();
                    perMonth.Add(materialToMaterialMonth.Count);
                }
                MaterialToMaterialSeries = new LineSeries
                {
                    Values = perMonth,
                    Title = "Склад материалов"
                };
                cartesianChart1.Series = new SeriesCollection
                {
                    MaterialToMaterialSeries
                };
            }
            Labels = new[] { "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек", "Фев" };
            DataContext = this;
        }
    }
}
