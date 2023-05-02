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
using WpfApp1.Entities1;
using System.Windows.Threading;
//using Rul.Entities1; // Подключение папки с моделью БД

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        private int countUnsuccessful = 1; //Количество неверных попыток входа
        public Autho()
        {
            InitializeComponent();
            txtCapcha.Visibility = Visibility.Hidden;
            textBlockCaptcha.Visibility = Visibility.Hidden;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null));
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim(); // Объявляем переменную, в которую будет записываться значения с TextBlock'a логина
            string password = txtPassword.Text.Trim(); // Объявляем переменную, в которую будет записываться значения с TextBlock'a пароля
            //UserControl user = new UserControl(); // создаем пустой объект пользователя
            var user = RulEntities2.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).FirstOrDefault(); // Условиена на нахождение пользователя с введеным логином ипаролем 
            int userCount = RulEntities2.GetContext().User.Where(p => p.UserLogin == login && p.UserPassword == password).Count(); // Находим кол-во пользователей
            if (countUnsuccessful < 1)
            {
                if (userCount > 0) // Если кол-во пользователей с веденными даннымибольше 0, то ->
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString()); // Появл. окно информации
                    LoadForm(user.Role.RoleName.ToString(),user); // И передается роль в метод загрузки страицы по ролям
                }
                else 
                {
                    MessageBox.Show("Вы ввели неверно логин или пароль!");
                    countUnsuccessful++;
                    if (countUnsuccessful == 1) // Если количество неверных попыток равно 1
                        GenerateCapcha();       // Генерируем капчу
                }
            }
            else
            {
                if (userCount > 0 && textBlockCaptcha.Text == txtCapcha.Text)
                {
                    MessageBox.Show("Вы вошли под: " + user.Role.RoleName.ToString());
                    LoadForm(user.Role.RoleName.ToString(), user);
                }
            }
           
        }
        private void GenerateCapcha()
        {
            txtCapcha.Visibility = Visibility.Visible; // Показать запись и
            textBlockCaptcha.Visibility = Visibility.Visible; // Поле для ввода капчи
            Random random = new Random();
            int randmNum = random.Next(0, 5); // Генерируется случайное чило от 1 до 3
            switch (randmNum)
            {
                case 1:
                    textBlockCaptcha.Text = "ju2sT8Cbs";
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 2:
                    textBlockCaptcha.Text = "iNwK2cl";
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 3:
                    textBlockCaptcha.Text = "uOozGk95";
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 4:
                    textBlockCaptcha.Text = "tb04Gh2r";
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case 5:
                    textBlockCaptcha.Text = "Ko98FaBn";
                    textBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
                    break;
            }
        }
        private void LoadForm(string _role, User user)
        {
            switch (_role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client(user)); // Если рольпользователя "Клиент", переходим на страницу клиента
                    break;
            }
        }
    }
}
