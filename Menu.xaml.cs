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

namespace RickVowens
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            objectSelectionView(btShoePage);
            frame.NavigationService.Navigate(new Pages.ShoePage());
        }
        /// <summary>
        /// Переход на страницу "Обувь"
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btShoePageClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                objectSelectionView(sender);
            }
            frame.NavigationService.Navigate(new Pages.ShoePage());
        }
        /// <summary>
        /// Выделение выбранной вкладки
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        public void objectSelectionView(object sender)
        {
            List<Button> buttons = new List<Button>() { btShoePage, btSupplies, btStat, btProduction, btMaterials };

            foreach (Button i in buttons)
            {
                i.FontWeight = FontWeights.Normal;
            }

            var button = sender as Button;

            button.FontWeight = FontWeights.Bold;
        }
        /// <summary>
        /// Переход на страницу "Поставки"
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btSuppliesClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                objectSelectionView(sender);
            }
            frame.NavigationService.Navigate(new Pages.Supplies());
        }
        /// <summary>
        /// Выход в окно авторизации
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void backToLogin(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            DataBank.currentUser = null;
            mainWindow.Show();
            this.Close();
        }
        /// <summary>
        /// Переход на страницу "Статистика"
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btStatClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                objectSelectionView(sender);
            }
            frame.NavigationService.Navigate(new Pages.PageStat());
        }
        /// <summary>
        /// Переход на страницу "Производство"
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btProductionClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                objectSelectionView(sender);
            }
            frame.NavigationService.Navigate(new Pages.PageProduction());
        }
        /// <summary>
        /// Переход на страницу "Материалы"
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void btMaterialsClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                objectSelectionView(sender);
            }
            frame.NavigationService.Navigate(new Pages.PageMaterials());
        }
        /// <summary>
        /// Показ справки
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Справка.chm");
            }
        }
    }
}
