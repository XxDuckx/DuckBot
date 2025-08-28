using System;
using System.Windows;
using System.Windows.Controls;

namespace DuckBot.GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateTo("MyBotsPage"); // Default page
        }

        private void OnNavClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string pageName)
            {
                NavigateTo(pageName);
            }
        }

        private void NavigateTo(string pageName)
        {
            Type? pageType = Type.GetType($"DuckBot.GUI.Pages.{pageName}");
            if (pageType != null)
            {
                MainFrame.Content = Activator.CreateInstance(pageType);
            }
            else
            {
                MessageBox.Show($"Page not found: {pageName}");
            }
        }
    }
}
