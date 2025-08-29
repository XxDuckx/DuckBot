using System.Windows;
using System.Windows.Controls;
using DuckBot.Core.Models;
using DuckBot.Core.Services;

namespace DuckBot.GUI.Pages
{
    public partial class AccountsPage : Page
    {
        public AccountsPage()
        {
            InitializeComponent();
            AccountGrid.ItemsSource = AccountCreator.Accounts;
        }

        private void OnAddAccount(object sender, RoutedEventArgs e)
        {
            var acc = new GameAccount { Email = "new@mail.com", Password = "1234" };
            AccountCreator.AddAccount(acc);
            AccountGrid.Items.Refresh();
        }

        private void OnMarkTutorial(object sender, RoutedEventArgs e)
        {
            if (AccountGrid.SelectedItem is GameAccount acc)
            {
                acc.TutorialComplete = true;
                AccountCreator.Save();
                AccountGrid.Items.Refresh();
            }
        }
    }
}
