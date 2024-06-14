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
using word = Microsoft.Office.Interop.Word;

namespace RickVowens.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageProduction.xaml
    /// </summary>
    public partial class PageProduction : Page
    {
        Production selectedProduction = null;
        List<Production> tableSource = null;
        public PageProduction()
        {
            InitializeComponent();
        }

        private void btWord(object sender, RoutedEventArgs e)
        {
            if (selectedProduction.ProductionContain.Count != 0)
            {
                word.Application wordApp = new word.Application();
                word.Document wordDoc = wordApp.Documents.Add();
                wordDoc.PageSetup.Orientation = word.WdOrientation.wdOrientPortrait;
                wordDoc.PageSetup.LeftMargin = wordApp.CentimetersToPoints(1f);
                wordDoc.Paragraphs.SpaceAfter = wordApp.CentimetersToPoints(0f);
                wordDoc.PageSetup.LeftMargin = wordApp.CentimetersToPoints(3f);
                wordDoc.PageSetup.RightMargin = wordApp.CentimetersToPoints(1.5f);
                wordDoc.PageSetup.TopMargin = wordApp.CentimetersToPoints(2f);
                wordDoc.PageSetup.BottomMargin = wordApp.CentimetersToPoints(2f);
                word.Paragraph p1 = wordDoc.Paragraphs[1];
                p1.Range.Text = $"Отчет производства за смену с нормами №{selectedProduction.IDProduction} от {selectedProduction.DateProduction.ToShortDateString()}";
                p1.Alignment = word.WdParagraphAlignment.wdAlignParagraphLeft;
                p1.Range.Font.Size = 12f;
                p1.Range.Font.Color = word.WdColor.wdColorBlack;
                p1.Range.Font.Name = "Times New Roman";
                wordDoc.Paragraphs.Add();
                wordDoc.Paragraphs.Add();
                word.Paragraph p3 = wordDoc.Content.Paragraphs[3];
                p3.Range.Text = "Организация: ООО «РИК ВОВЕНС»";
                wordDoc.Paragraphs.Add();
                word.Paragraph p4 = wordDoc.Content.Paragraphs[4];
                p4.Range.Text = "Склад: Склад ГП";
                wordDoc.Paragraphs.Add();
                wordDoc.Paragraphs.Add();
                word.Paragraph tableParagraph = wordDoc.Content.Paragraphs[6];
                tableParagraph.Range.Font.Size = 12f;
                tableParagraph.Range.Font.Color = word.WdColor.wdColorBlack;
                tableParagraph.Range.Font.Bold = 0;
                tableParagraph.Range.Font.Name = "Times New Roman";
                word.Range tableRange = tableParagraph.Range;
                word.Table productionContainTable = wordDoc.Tables.Add(tableRange, selectedProduction.ProductionContain.Count + 2, 7);
                productionContainTable.Borders.InsideLineStyle = productionContainTable.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
                productionContainTable.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                word.Range cellRange;
                productionContainTable.Cell(1, 1).Merge(productionContainTable.Cell(2, 1));
                cellRange = productionContainTable.Cell(1, 1).Range;
                cellRange.Text = "№";
                cellRange = productionContainTable.Cell(1, 2).Range;
                cellRange.Text = "Продукция, товарно-материальные ценности";
                cellRange = productionContainTable.Cell(2, 2).Range;
                cellRange.Text = "Наименование";
                productionContainTable.Cell(1, 3).Merge(productionContainTable.Cell(2, 3));
                cellRange = productionContainTable.Cell(1, 3).Range;
                cellRange.Text = "Код";
                productionContainTable.Cell(1, 4).Merge(productionContainTable.Cell(2, 4));
                cellRange = productionContainTable.Cell(1, 4).Range;
                cellRange.Text = "Ед. изм.";
                productionContainTable.Cell(1, 5).Merge(productionContainTable.Cell(1, 7));
                cellRange = productionContainTable.Cell(1, 5).Range;
                cellRange.Text = "Произведено";
                cellRange = productionContainTable.Cell(2, 5).Range;
                cellRange.Text = "Количество";
                cellRange = productionContainTable.Cell(2, 6).Range;
                cellRange.Text = "Цена плановая";
                cellRange = productionContainTable.Cell(2, 7).Range;
                cellRange.Text = "Итого";
                var productionList = selectedProduction.ProductionContain.ToList();
                for (int i = 0; i < selectedProduction.ProductionContain.Count; i++)
                {
                    cellRange = productionContainTable.Cell(i + 3, 1).Range;
                    cellRange.Text = (i + 1).ToString();
                    cellRange = productionContainTable.Cell(i + 3, 2).Range;
                    cellRange.Text = productionList[i].Product.Name;
                    cellRange = productionContainTable.Cell(i + 3, 3).Range;
                    cellRange.Text = productionList[i].Articul.ToString();
                    cellRange = productionContainTable.Cell(i + 3, 4).Range;
                    cellRange.Text = "шт";
                    cellRange = productionContainTable.Cell(i + 3, 5).Range;
                    cellRange.Text = productionList[i].CountOfProduct.ToString();
                    cellRange = productionContainTable.Cell(i + 3, 6).Range;
                    cellRange.Text = productionList[i].Product.CostWithNDS.ToString();
                    cellRange = productionContainTable.Cell(i + 3, 7).Range;
                    cellRange.Text = (productionList[i].Product.CostWithNDS * productionList[i].CountOfProduct).ToString();
                }
                wordApp.Visible = true;
            }
        }

        private void btAddContainClick(object sender, RoutedEventArgs e)
        {
            if (selectedProduction.SendStatus == "да")
            {
                MessageBox.Show("Товар уже отправлен.");
            }
            else
            {
                ProductionContain productionContain = new ProductionContain();
                productionContain.ID = 0;
                productionContain.IDProduction = selectedProduction.IDProduction;
                EditWindows.WindowAddContainProduction windowAddContainProduction = new EditWindows.WindowAddContainProduction(selectedProduction, productionContain);
                windowAddContainProduction.ShowDialog();
                lvShoes.ItemsSource = null;
                lvShoes.ItemsSource = selectedProduction.ProductionContain.ToList();
            }  
        }

        private void btDelete(object sender, RoutedEventArgs e)
        {
            if (selectedProduction.SendStatus == "да")
            {
                MessageBox.Show("Товар уже отправлен.");
            }
            else
            {
                var button = sender as Button;
                var productionContain = button.DataContext as ProductionContain;
                if (productionContain != null)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        List<Material> materialsForManipulations = new List<Material>();
                        List<int> values = new List<int>();
                        foreach (MaterialOfProduct i in productionContain.Product.MaterialOfProduct)
                        {
                            materialsForManipulations.Add(i.Material);
                            values.Add(i.CountOfMaterial);
                        }
                        for (int i = 0; i < materialsForManipulations.Count; i++)
                        {
                            materialsForManipulations[i].CountInStock += values[i];
                        }
                        MainWindow.shoesKursovoiEntities.ProductionContain.Remove(productionContain);
                        MainWindow.shoesKursovoiEntities.SaveChanges();
                        table.ItemsSource = null;
                        table.ItemsSource = tableSource;
                    }
                }
            }

        }

        private void btAddProduction(object sender, RoutedEventArgs e)
        {
            Production newProduction = new Production();
            newProduction.IDProduction = 0;
            newProduction.DateProduction = DateTime.Now;
            newProduction.SendStatus = "нет";
            EditWindows.WindowEditProduction windowEditProduction = new EditWindows.WindowEditProduction(newProduction);
            windowEditProduction.ShowDialog();
            tbDateFrom.SelectedDate = null;
            tbDateTo.SelectedDate = null;
            ChooseDate();
            table.ItemsSource = null;
            table.ItemsSource = tableSource;
        }

        private void btEditProduction(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var production = button.DataContext as Production;
            EditWindows.WindowEditProduction windowEditProduction = new EditWindows.WindowEditProduction(production);
            windowEditProduction.ShowDialog();
            tbDateFrom.SelectedDate = null;
            tbDateTo.SelectedDate = null;
            ChooseDate();
            table.ItemsSource = null;
            table.ItemsSource = tableSource;
        }

        private void deleteProduction(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var production = button.DataContext as Production;
            if (production != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Вы точно хотите удалить запись?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    MainWindow.shoesKursovoiEntities.Production.Remove(production);
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    ChooseDate();
                    table.ItemsSource = null;
                    table.ItemsSource = tableSource;
                }
            }
        }

        private void table_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedItem = table.SelectedItem as Production;
            if (selectedItem != null)
            {
                selectedProduction = selectedItem;
                btAddContain.Visibility = Visibility.Visible;
                lvShoes.ItemsSource = selectedItem.ProductionContain.ToList();
            }
            else
            {
                selectedProduction = null;
                btAddContain.Visibility = Visibility.Collapsed;
                lvShoes.ItemsSource = null;
            }
        }

        private void btChooseDate(object sender, RoutedEventArgs e)
        {
            ChooseDate();
            table.ItemsSource = tableSource;
        }
        
        public void ChooseDate()
        {
            if (DataBank.currentUser.Post == "бухгалтер на производстве")
            {
                DateTime? fromDate = tbDateFrom.SelectedDate;
                DateTime? toDate = tbDateTo.SelectedDate;
                if (fromDate == null && toDate == null)
                {
                    tableSource = MainWindow.shoesKursovoiEntities.Production.ToList();
                }
                else if (fromDate != null && toDate == null)
                {
                    tableSource = MainWindow.shoesKursovoiEntities.Production
                        .Where(s => s.DateProduction >= fromDate).ToList();
                }
                else if (fromDate == null && toDate != null)
                {
                    tableSource = MainWindow.shoesKursovoiEntities.Production
                        .Where(s => s.DateProduction <= toDate).ToList();
                }
                else
                {
                    tableSource = MainWindow.shoesKursovoiEntities.Production
                        .Where(s => s.DateProduction >= fromDate && s.DateProduction <= toDate).ToList();
                }
            }
            else
            {
                MessageBox.Show("Вы не бухгалтер на производстве");
            }
        }

        private void sendToStock(object sender, RoutedEventArgs e)
        {
            if (selectedProduction.SendStatus == "нет")
            {
                if (selectedProduction.ProductionContain.Count != 0)
                {
                    SuppliesProductsInProductStock suppliesProductsInProductStock = new SuppliesProductsInProductStock();
                    suppliesProductsInProductStock.Date = selectedProduction.DateProduction;
                    suppliesProductsInProductStock.IDSupply = 0;
                    MainWindow.shoesKursovoiEntities.SuppliesProductsInProductStock.Add(suppliesProductsInProductStock);
                    foreach (ProductionContain productionContain in selectedProduction.ProductionContain.ToList())
                    {
                        SuppliesProductsInProductStockContains suppliesProductsInProductStockContains = new SuppliesProductsInProductStockContains();
                        suppliesProductsInProductStockContains.ID = 0;
                        suppliesProductsInProductStockContains.IDSupply = suppliesProductsInProductStock.IDSupply;
                        suppliesProductsInProductStockContains.CountOfShoe = productionContain.CountOfProduct;
                        suppliesProductsInProductStockContains.ShoeArticul = productionContain.Articul;
                        MainWindow.shoesKursovoiEntities.SuppliesProductsInProductStockContains.Add(suppliesProductsInProductStockContains);
                        productionContain.Product.CountInStock += productionContain.CountOfProduct;
                    }
                    selectedProduction.SendStatus = "да";
                    MainWindow.shoesKursovoiEntities.SaveChanges();
                    ChooseDate();
                    table.ItemsSource = null;
                    table.ItemsSource = tableSource;
                }
                else
                {
                    MessageBox.Show("Нет содержимого.");
                }
            }
            else
            {
                MessageBox.Show("Уже отправлено.");
            }
        }
    }
}
