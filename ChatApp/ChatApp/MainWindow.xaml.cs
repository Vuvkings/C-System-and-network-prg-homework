using ChatApp.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text.Trim();
            var password = PasswordBox.Password;

            if (UserService.Authenticate(login, password))
            {
                var chatWindow = new ChatWindow(login);
                chatWindow.Show();
                this.Close();
            }
            else
            {
                StatusText.Text = "Неверный логин или пароль";
            }
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text.Trim();
            var password = PasswordBox.Password;

            if (UserService.Register(login, password))
            {
                StatusText.Text = "Регистрация успешна! Теперь войдите.";
            }
            else
            {
                StatusText.Text = "Логин занят или данные некорректны";
            }
        }
    }
}