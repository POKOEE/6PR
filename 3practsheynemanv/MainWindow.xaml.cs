using System.Windows;
using _3practsheynemanv.Pages;

namespace _3practsheynemanv
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Загружаем страницу авторизации при старте
            FrmMain.Navigate(new Autho());
        }

        // Обработчик кнопки "Назад"
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (FrmMain.CanGoBack)
            {
                FrmMain.GoBack();
            }
        }

        // Скрываем/показываем кнопку "Назад" в зависимости от истории навигации
        private void FrmMain_ContentRendered(object sender, System.EventArgs e)
        {
            if (FrmMain.CanGoBack)
                btnBack.Visibility = Visibility.Visible;
            else
                btnBack.Visibility = Visibility.Hidden;
        }
    }
}
