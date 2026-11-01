using System.Windows.Controls;
using _3practsheynemanv.Models; // Подключаем модель

namespace _3practsheynemanv.Pages
{
    public partial class Client : Page
    {
        private User _currentUser; // Сохраняем пользователя

        public Client(object user)
        {
            InitializeComponent();

            // Приводим object к User (если это не гость)
            _currentUser = user as User;

            if (_currentUser != null)
            {
                // Здесь можно вывести приветствие, если на странице есть TextBlock
                // Title = "Клиент: " + _currentUser.Login;
            }
            else
            {
                // Title = "Гость";
            }
        }
    }
}