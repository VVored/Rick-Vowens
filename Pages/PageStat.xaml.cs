using System;
using System.Collections.Generic;
using System.IO;
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

namespace RickVowens.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageStat.xaml
    /// </summary>
    public partial class PageStat : Page
    {
        public PageStat()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader("D:\\3 курс\\RickVowens\\logs.txt", true);
            while (sr.EndOfStream != true)
            {
                lbLogs.Items.Add(sr.ReadLine());
            }
        }

        private void btShoeStatClick(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Stats.PageShoeStat());
        }

        private void clickBtSupplies(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Stats.PageSuppliesStat());
        }

        private void btProductionStatClick(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Stats.PageProductionStat());
        }
    }
}
