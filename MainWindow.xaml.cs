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

//**********************************************************************************
//* Название программы: "Rick Vowens"
//* 
//*Назначение программы: помогает автоматизировать работу производственной компании,
//* упрощая складской и производственный учет.
//*
//*Разработчик: студент группы ПР-330/б Мучкин В. Д.
//*
//* Версия: 1.0
//**********************************************************************************


namespace RickVowens
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ShoesKursovoiEntities shoesKursovoiEntities = new ShoesKursovoiEntities();
        string placeholderPassword = "Пароль";
        string placeholderLogin = "Логин";
        Employees user = null;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Авторизация пользователя и переход в главное меню
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            var employeesList = shoesKursovoiEntities.Employees.ToList();
            foreach (Employees i in employeesList)
            {
                if (tbLogin.Text == i.Login)
                {
                    if (tbPassword.Text == i.Password)
                    {
                        user = i;
                        DataBank.currentUser = user;
                        break;
                    }
                }
            }
            if (user != null)
            {
                Menu menu = new Menu();
                menu.Show();
                StreamWriter sw = new StreamWriter("D:\\3 курс\\RickVowens\\logs.txt", true);
                sw.Write($"{user.Login} {DateTime.Now}\n");
                sw.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ошиблись в логине или пароле.");
            }
        }
        /// <summary>
        /// Текст-заполнитель для текстового поля пароля
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>

        private void tbPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == placeholderPassword)
            {
                tbPassword.Text = "";
            }
        }
        /// <summary>
        /// Текст-заполнитель для текстового поля пароля
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void tbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Text == "")
            {
                tbPassword.Text = placeholderPassword;
            }
        }
        /// <summary>
        /// Текст-заполнитель для текстового поля логина
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void tbLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == placeholderLogin)
            {
                tbLogin.Text = "";
            }
        }
        /// <summary>
        /// Текст-заполнитель для текстового поля логина
        /// </summary>
        /// <param name="e"></param>
        /// <param name="sender"></param>
        private void tbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "")
            {
                tbLogin.Text = placeholderLogin;
            }
        }
    }
}
