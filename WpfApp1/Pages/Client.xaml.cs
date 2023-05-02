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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        User user = new User(); // Создаем пустой обект пользователя

        public Client(User currentUser)
        {
            InitializeComponent();

            var product = RulEntities2.GetContext().Product.ToList(); // Обращение каблице - Товары
            LViewProduct.ItemsSource = product; // Передаем таблицу в лист
            DataContext = this;  // Привязываем контекст данных к коду, чтобы обратиться к массивам

            txtAllAmount.Text = product.Count.ToString();

            user = currentUser; // Передаем конкретного пользователя в пустой объект

            UpdateData(); // Вызываем метод 
            User();       // Вызываем метод 
        }
        public void User()
        {
            if (user != null)
                txtFullname.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
            else 
                txtFullname.Text = "Гость";
        }
        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "Стоимость по возрастанию",
            "Стоимость по убыванию"
        };
        public string[] FilterList { get; set; } =
        {
            "Все диапазоны",
            "0%-9,99%",
            "10%-14,99%",
            "15% и более"
        };
        private void UpdateData()
        {
            var result = RulEntities2.GetContext().Product.ToList();   // Вводим переменную, которая принимает данные из таблицы товаров
           

            if (cmbSoring.SelectedIndex == 1)                                       // Реализация сортировки
                result = result.OrderBy(p=>p.ProductCost).ToList();                 // С помощью запросов на сортировку по возрастанию
            if (cmbSoring.SelectedIndex==2)                                         // И убыванию цены
                result = result.OrderByDescending(p=>p.ProductCost).ToList();

            if(cmbFilter.SelectedIndex==1)
                result=result.Where(p=>p.ProductDiscountAmount>=0 && p.ProductDiscountAmount < 10).ToList();
            if (cmbFilter.SelectedIndex == 2)                                                                          // Реализация фильтрации
                result = result.Where(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15).ToList();    // С помощью запросов на выборку 
            if (cmbFilter.SelectedIndex == 3)                                                                          // По условия задания 
                result = result.Where(p => p.ProductDiscountAmount >=15).ToList();

            result = result.Where(p => p.ProductName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
            LViewProduct.ItemsSource = result; // Передаем результат в ListView

            txtResultAmount.Text = result.Count.ToString();
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData(); // Вызываем метод
        }
        private void cmbSorting_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateData(); // Вызываем метод
        }
        private void cmbFilter_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateData(); // Вызываем метод
        }
    }
}
