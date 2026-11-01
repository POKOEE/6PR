using System;
using System.Windows;
using System.Windows.Controls;
using _3practsheynemanv.Services;
using _3practsheynemanv.Models;

namespace _3practsheynemanv.Pages
{
    public partial class Autho : Page
    {
        private int clickCount = 0;
        private string currentCaptcha = "";

        public Autho()
        {
            InitializeComponent();
            clickCount = 0;
            HideCaptcha(); // Скрываем капчу при старте
        }

        // Метод для скрытия капчи и очистки её полей
        private void HideCaptcha()
        {
            txtBlockCaptcha.Visibility = Visibility.Hidden;
            txtBoxCaptcha.Visibility = Visibility.Hidden;
            inputCaptcha.Visibility = Visibility.Hidden;

            inputCaptcha.Text = "";
            txtBoxCaptcha.Text = "";
        }

        // Метод для генерации и показа капчи
        private void GenerateCaptcha()
        {
            currentCaptcha = CaptchaGenerator.GenerateCaptchaText(6);

            txtBoxCaptcha.Text = currentCaptcha;

            txtBlockCaptcha.Visibility = Visibility.Visible;
            txtBoxCaptcha.Visibility = Visibility.Visible;
            inputCaptcha.Visibility = Visibility.Visible;

            inputCaptcha.Clear();
        }

        // Кнопка "Войти"
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            clickCount++;
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Password.Trim(); // У PasswordBox свойство Password, а не Text

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            // Хэшируем введенный пароль
            string hashedPassword = Hash.HashPassword(password);

            // Имитация БД (логин: client, пароль: 123)
            User user = null;
            if (login == "client" && hashedPassword == Hash.HashPassword("123"))
            {
                user = new User { Login = login, PasswordHash = hashedPassword, RoleName = "Клиент" };
            }

            // Логика с капчей
            if (clickCount == 1)
            {
                if (user != null)
                {
                    MessageBox.Show("Вы вошли под: " + user.RoleName);
                    LoadPage(user.RoleName, user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно! Введите капчу.");
                    GenerateCaptcha(); // Показываем капчу
                    txtPassword.Password = ""; // Очищаем пароль
                }
            }
            else if (clickCount > 1)
            {
                // Проверяем капчу
                if (user != null && inputCaptcha.Text.Trim().Equals(currentCaptcha, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Вы вошли под: " + user.RoleName);
                    LoadPage(user.RoleName, user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново! Капча неверна или пароль не подходит.");
                    ResetForm();
                }
            }
        }

        // Кнопка "Войти как гость"
        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null));
        }

        // Переход на нужную страницу в зависимости от роли
        private void LoadPage(string role, User user)
        {
            ResetForm();
            switch (role)
            {
                case "Клиент":
                    NavigationService.Navigate(new Client(user));
                    break;
                default:
                    MessageBox.Show("Неизвестная роль");
                    break;
            }
        }

        // Полная очистка формы
        private void ResetForm()
        {
            clickCount = 0;
            txtLogin.Clear();
            txtPassword.Password = ""; // Очистка PasswordBox
            HideCaptcha();
        }
    }
}