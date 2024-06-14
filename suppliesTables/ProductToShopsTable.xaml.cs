using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using word = Microsoft.Office.Interop.Word;

namespace RickVowens.suppliesTables
{
    /// <summary>
    /// Логика взаимодействия для ProductToShopsTable.xaml
    /// </summary>
    public partial class ProductToShopsTable : Page
    {
        DateTime? fromDate = null;
        DateTime? toDate = null;
        public static SuppliesProductsInShops selectedSupply;
        public static DataGrid tableLink;

        public ProductToShopsTable(DateTime? fromDate, DateTime? toDate)
        {
            InitializeComponent();
            this.fromDate = fromDate;
            this.toDate = toDate;
            InitializeTable(this.fromDate, this.toDate);
            tableLink = table;
        }

        private void addSupply(object sender, RoutedEventArgs e)
        {
            SuppliesProductsInShops supply = new SuppliesProductsInShops();
            supply.IDSupply = 0;
            supply.Date = DateTime.Now;
            EditWindows.WindowEditSupplyProductToShop windowEditSupplyProductToShop = new EditWindows.WindowEditSupplyProductToShop(supply);
            windowEditSupplyProductToShop.ShowDialog();
            InitializeTable(fromDate, toDate);
        }

        public void InitializeTable(DateTime? fromDate, DateTime? toDate)
        {
            if (DataBank.currentUser.Post == "бухгалтер склада ГП")
            {
                table.ItemsSource = null;
                if (fromDate == null && toDate == null)
                {
                    table.ItemsSource = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList();
                }
                else if (fromDate != null && toDate == null)
                {
                    table.ItemsSource = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList()
                        .Where(s => s.Date >= fromDate);
                }
                else if (fromDate == null && toDate != null)
                {
                    table.ItemsSource = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList()
                        .Where(s => s.Date <= toDate);
                }
                else
                {
                    table.ItemsSource = MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.ToList()
                        .Where(s => s.Date >= fromDate && s.Date <= toDate);
                }
            }
            else
            {
                MessageBox.Show("Вы не бухгалтер склада ГП");
            } 
        }

        private void btEditSupply(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int selectedIndex = table.SelectedIndex;
            var supply = button.DataContext as SuppliesProductsInShops;
            EditWindows.WindowEditSupplyProductToShop windowEditSupplyProductToShop = new EditWindows.WindowEditSupplyProductToShop(supply);
            windowEditSupplyProductToShop.ShowDialog();
            InitializeTable(fromDate, toDate);
            table.SelectedIndex = selectedIndex;
        }

        private void deleteSupply(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                var supply = button.DataContext as SuppliesProductsInShops;
                if (supply != null)
                {
                    var contain = supply.SuppliesProductsInShopsContains.ToList();
                    for (int i = 0; i < contain.Count; i++)
                    {
                        contain[i].Product.CountInStock += contain[i].CountOfShoe;
                    }
                    MainWindow.shoesKursovoiEntities.SuppliesProductsInShops.Remove(supply);
                }
                MainWindow.shoesKursovoiEntities.SaveChanges();
                InitializeTable(fromDate, toDate);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void table_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedSupply = table.SelectedItem as SuppliesProductsInShops;
            if (selectedSupply == null)
            {
                Pages.Supplies.lvShoesLink.ItemsSource = null;
                table.SelectedItem = null;
                Pages.Supplies.lvShoesLink.Visibility = Visibility.Collapsed;
                Pages.Supplies.btAddContainLink.Visibility = Visibility.Collapsed;
            }
            else
            {
                Pages.Supplies.btAddContainLink.Visibility = Visibility.Visible;
                Pages.Supplies.lvShoesLink.Visibility = Visibility.Visible;
                Pages.Supplies.lvShoesLink.ItemsSource = null;
                Pages.Supplies.lvShoesLink.ItemsSource = selectedSupply.SuppliesProductsInShopsContains.ToList();
            }
        }

        private void btWord(object sender, RoutedEventArgs e)
        {
            int resultCount = 0;
            decimal allCost = 0;
            var supplyContainList = selectedSupply.SuppliesProductsInShopsContains.ToList();
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
            p1.Range.Text = "Приложение №1";
            p1.Alignment = word.WdParagraphAlignment.wdAlignParagraphRight;
            p1.Range.Font.Size = 12f;
            p1.Range.Font.Color = word.WdColor.wdColorBlack;
            p1.Range.Font.Name = "Times New Roman";
            wordDoc.Paragraphs.Add();
            word.Paragraph p2 = wordDoc.Content.Paragraphs[2];
            p2.Range.Text = "к договору №" + selectedSupply.IDSupply;
            p2.Alignment = word.WdParagraphAlignment.wdAlignParagraphRight;
            p2.Range.Font.Size = 12f;
            p2.Range.Font.Name = "Times New Roman";
            p2.Range.Font.Color = word.WdColor.wdColorBlack;
            wordDoc.Paragraphs.Add();
            word.Paragraph p3 = wordDoc.Content.Paragraphs[3];
            p3.Range.Text = "от " + selectedSupply.Date.ToLongDateString();
            p3.Alignment = word.WdParagraphAlignment.wdAlignParagraphRight;
            p3.Range.Font.Size = 12f;
            p3.Range.Font.Color = word.WdColor.wdColorBlack;
            p3.Range.Font.Name = "Times New Roman";
            wordDoc.Paragraphs.Add();
            wordDoc.Paragraphs.Add();
            word.Paragraph p5 = wordDoc.Content.Paragraphs[5];
            p5.Range.Text = "АКТ";
            p5.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
            p5.Range.Font.Size = 12f;
            p5.Range.Bold = 1;
            p5.Range.Font.Name = "Times New Roman";
            p5.Range.Font.Color = word.WdColor.wdColorBlack;
            wordDoc.Paragraphs.Add();
            word.Paragraph p6 = wordDoc.Content.Paragraphs[6];
            p6.Range.Text = "приема-передачи";
            p6.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
            p6.Range.Font.Size = 12f;
            p6.Range.Bold = 1;
            p6.Range.Font.Color = word.WdColor.wdColorBlack;
            p6.Range.Font.Name = "Times New Roman";
            wordDoc.Paragraphs.Add();
            wordDoc.Paragraphs.Add();
            word.Paragraph p8 = wordDoc.Content.Paragraphs[8];
            p8.Range.Text = "ООО «РИК ВОВЕНС», в лице директора Мучкина В.Д., действующего на основании Устава, именуемое в дальнейшем Продавец, с одной стороны и ООО «РИК ВОВЕНС», в лице директора Мучкина В.Д., действующего на основании Устава, именуемое в дальнейшем Покупатель, с другой стороны (в дальнейшем вместе именуемые «Стороны» и по отдельности «Сторона»), составили настоящий Акт о нижеследующем:";
            p8.Alignment = word.WdParagraphAlignment.wdAlignParagraphJustify;
            p8.Range.Font.Size = 12f;
            p8.Range.Font.Color = word.WdColor.wdColorBlack;
            p8.Range.Font.Bold = 0;
            p8.Range.Font.Name = "Times New Roman";
            wordDoc.Paragraphs.Add();
            wordDoc.Paragraphs.Add();
            word.Paragraph p10 = wordDoc.Content.Paragraphs[10];
            p10.Range.Text = $"1. В соответствии с п. 2.3 Договора между Сторонами № 17 от {selectedSupply.Date.ToLongDateString()} Продавец передает, а Покупатель принимает Товар следующего ассортимента и количества:";
            p10.Alignment = word.WdParagraphAlignment.wdAlignParagraphJustify;
            p10.Range.Font.Size = 12f;
            p10.Range.Font.Color = word.WdColor.wdColorBlack;
            p10.Range.Font.Bold = 0;
            p10.Range.Font.Name = "Times New Roman";
            wordDoc.Paragraphs.Add();
            wordDoc.Paragraphs.Add();
            word.Paragraph tableParagraph = wordDoc.Content.Paragraphs[12];
            tableParagraph.Range.Font.Size = 12f;
            tableParagraph.Range.Font.Color = word.WdColor.wdColorBlack;
            tableParagraph.Range.Font.Bold = 0;
            tableParagraph.Range.Font.Name = "Times New Roman";
            word.Range tableRange = tableParagraph.Range;
            word.Table supplyContainTable = wordDoc.Tables.Add(tableRange, supplyContainList.Count + 2, 5);
            supplyContainTable.Borders.InsideLineStyle = supplyContainTable.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
            supplyContainTable.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            word.Range cellRange;
            cellRange = supplyContainTable.Cell(1, 1).Range;
            cellRange.Text = "№ п/п";
            cellRange = supplyContainTable.Cell(1, 2).Range;
            cellRange.Text = "Наименование";
            cellRange = supplyContainTable.Cell(1, 3).Range;
            cellRange.Text = "Кол-во, шт.";
            cellRange = supplyContainTable.Cell(1, 4).Range;
            cellRange.Text = "Цена, включая НДС, руб";
            cellRange = supplyContainTable.Cell(1, 5).Range;
            cellRange.Text = "Сумма, включая НДС, руб";
            supplyContainTable.Rows[1].Range.Bold = 1;
            supplyContainTable.Rows[1].Range.ParagraphFormat.Alignment = word.WdParagraphAlignment.wdAlignParagraphCenter;
            for (int i = 0; i < supplyContainList.Count; i++)
            {
                cellRange = supplyContainTable.Cell(i + 2, 1).Range;
                cellRange.Text = (i + 1).ToString();
                cellRange = supplyContainTable.Cell(i + 2, 2).Range;
                cellRange.Text = supplyContainList[i].Product.Name;
                cellRange = supplyContainTable.Cell(i + 2, 3).Range;
                cellRange.Text = supplyContainList[i].CountOfShoe.ToString();
                cellRange = supplyContainTable.Cell(i + 2, 4).Range;
                cellRange.Text = supplyContainList[i].Product.CostWithNDS.ToString();
                cellRange = supplyContainTable.Cell(i + 2, 5).Range;
                cellRange.Text = (supplyContainList[i].Product.CostWithNDS * supplyContainList[i].CountOfShoe).ToString();
                resultCount += supplyContainList[i].CountOfShoe;
                allCost += supplyContainList[i].Product.CostWithNDS * supplyContainList[i].CountOfShoe;
            }
            supplyContainTable.Cell(supplyContainList.Count + 2, 1).Merge(supplyContainTable.Cell(supplyContainList.Count + 2, 2));
            cellRange = supplyContainTable.Cell(supplyContainList.Count + 2, 1).Range;
            cellRange.Text = "Итого: ";
            cellRange = supplyContainTable.Cell(supplyContainList.Count + 2, 2).Range;
            cellRange.Text = resultCount.ToString();
            cellRange = supplyContainTable.Cell(supplyContainList.Count + 2, 3).Range;
            cellRange.Text = "-";
            cellRange = supplyContainTable.Cell(supplyContainList.Count + 2, 4).Range;
            cellRange.Text = allCost.ToString();
            supplyContainTable.Rows[supplyContainList.Count + 2].Range.Bold = 1;
            wordDoc.Paragraphs.Add();
            wordDoc.Paragraphs.Add();
            word.Paragraph p14 = wordDoc.Paragraphs[14 + (supplyContainList.Count + 2) * 5 + supplyContainList.Count];
            p14.Range.Text = "2. Принятый Покупателем товар обладает качеством и ассортиментом, соответствующим требованиям Договора. Товар поставлен в установленные в Договоре сроки. Покупатель не имеет никаких претензий к принятому товару.";
            p14.Alignment = word.WdParagraphAlignment.wdAlignParagraphJustify;
            p14.Range.Font.Size = 12f;
            p14.Range.Font.Color = word.WdColor.wdColorBlack;
            p14.Range.Font.Bold = 0;
            p14.Range.Font.Name = "Times New Roman";
            wordDoc.Paragraphs.Add();
            wordDoc.Paragraphs.Add();
            word.Paragraph p16 = wordDoc.Paragraphs[16 + (supplyContainList.Count + 2) * 5 + supplyContainList.Count];
            p16.Range.Text = "3. Настоящий Акт составлен в двух экземплярах, имеющих равную юридическую силу, по одному экземпляру для каждой из Сторон и является неотъемлемой частью Договора между Сторонами.";
            p16.Alignment = word.WdParagraphAlignment.wdAlignParagraphJustify;
            p16.Range.Font.Size = 12f;
            p16.Range.Font.Color = word.WdColor.wdColorBlack;
            p16.Range.Font.Bold = 0;
            p16.Range.Font.Name = "Times New Roman";
            wordDoc.Paragraphs.Add();
            wordDoc.Paragraphs.Add();
            word.Paragraph table2Paragraph = wordDoc.Paragraphs[18 + (supplyContainList.Count + 2) * 5 + supplyContainList.Count];
            word.Range table2Range = table2Paragraph.Range;
            word.Table table2 = wordDoc.Tables.Add(table2Range, 2, 2);
            table2.Borders.InsideLineStyle = table2.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleNone;
            table2.Range.Cells.VerticalAlignment = word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            table2Range.Font.Size = 12f;
            table2Range.Font.Color = word.WdColor.wdColorBlack;
            table2Range.Font.Bold = 0;
            table2Range.Font.Name = "Times New Roman";
            cellRange = table2.Cell(1, 1).Range;
            cellRange.Text = "ПОКУПАТЕЛЬ";
            cellRange = table2.Cell(1, 2).Range;
            cellRange.Text = "ПРОДАВЕЦ";
            for (int i = 1; i < 3; i++)
            {
                cellRange = table2.Cell(2, i).Range;
                cellRange.Text = "Директор     Мучкин   Мучкин В.Д. \nМ.П.";
            }

            wordApp.Visible = true;
        }
    }
}
