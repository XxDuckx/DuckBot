using System.Windows;
using System.Windows.Controls;

namespace DuckBot.GUI.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;

            if (username == "Ducko" && password == "test123") // Master account for testing
            {
                StatusText.Text = "Login successful!";
                DialogResult = true;
                Close();
            }
            else
            {
                StatusText.Text = "Invalid credentials. Try again.";
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
