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
using LiveCharts;
using LiveCharts.Wpf;

namespace RickVowens.Stats
{
    /// <summary>
    /// Логика взаимодействия для PageProductionStat.xaml
    /// </summary>
    public partial class PageProductionStat : Page
    {
        Product shoe = null;
        public PageProductionStat()
        {
            InitializeComponent();
            cbShoes.ItemsSource = MainWindow.shoesKursovoiEntities.Product.ToList();
            cbShoes.SelectedIndex = 0;
            cbPeriod.SelectedIndex = 0;
        }
        public LineSeries ShoeSeries { get; set; }
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
                workSheet.Cells[1, 1] = "Артикул";
                workSheet.Cells[1, 2] = "Наименование";
                workSheet.Cells[1, 3] = "Произведено";
                List<Production> productions = new List<Production>();
                List<Product> shoeList = MainWindow.shoesKursovoiEntities.Product.ToList();
                if (cbPeriod.SelectedIndex == 1)
                {
                    productions = MainWindow.shoesKursovoiEntities.Production.Where(prod => prod.DateProduction.Month == DateTime.Now.Month && prod.DateProduction.Year == DateTime.Now.Year).ToList();
                }
                else if (cbPeriod.SelectedIndex == 2)
                {
                    productions = MainWindow.shoesKursovoiEntities.Production.Where(prod => prod.DateProduction.Year == DateTime.Now.Year).ToList();
                }
                else
                {
                    productions = MainWindow.shoesKursovoiEntities.Production.ToList();
                }
                for (int i = 0; i < shoeList.Count; i++)
                {
                    workSheet.Cells[i + 2, 1] = shoeList[i].Articul;
                    workSheet.Cells[i + 2, 2] = shoeList[i].Name;
                    int countOfProdShoe = 0;
                    foreach(Production prod in productions)
                    {
                        var contain = prod.ProductionContain.ToList();
                        for (int j = 0; j < contain.Count; j++)
                        {
                            if (shoeList[i].Articul == contain[j].Articul)
                            {
                                countOfProdShoe += contain[j].CountOfProduct;
                            }
                        }
                    }
                    workSheet.Cells[i + 2, 3] = countOfProdShoe;
                }
                excelApp.Visible = true;
            }
        }

        private void cbShoes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = sender as ComboBox;
            var shoe = comboBoxItem.SelectedValue as Product;
            this.shoe = shoe;
            var converter = new ImageSourceConverter();
            shoeImage.Source = (ImageSource)converter.ConvertFromString(shoe.ImagePath);
            nameOfShoe.Text = shoe.Name;
            List<Production> productionOfSelectedShoe = new List<Production>();
            List<Production> allProductions = MainWindow.shoesKursovoiEntities.Production.ToList();
            foreach(Production prod in allProductions)
            {
                var productionContain = prod.ProductionContain.ToList();
                for(int i = 0; i < productionContain.Count; i++)
                {
                    if (productionContain[i].Articul == shoe.Articul)
                    {
                        productionOfSelectedShoe.Add(prod);
                    }
                }
            }
            ChartValues<int> values = new ChartValues<int>();
            for (int i = 1; i < 13; i++)
            {
                var productionMonth = productionOfSelectedShoe.Where(prod => prod.DateProduction.Month == i);
                int resultPerMonth = 0;
                foreach (Production prod in productionMonth)
                {
                    var contain = prod.ProductionContain.ToList();
                    for (int j = 0; j < contain.Count; j++)
                    {
                        if (contain[j].Articul == shoe.Articul)
                        {
                            resultPerMonth += contain[j].CountOfProduct;
                        }
                    }
                }
                values.Add(resultPerMonth);
            }
            Labels = new[] { "Янв", "Фев", "Мар", "Апр", "Май", "Июн", "Июл", "Авг", "Сен", "Окт", "Ноя", "Дек", "Фев" };
            ShoeSeries = new LineSeries
            {
                Values = values,
                Title = shoe.Name
            };
            cartesianChart1.Series = new SeriesCollection
            {
                ShoeSeries
            };
            DataContext = this;
        }
    }
}
