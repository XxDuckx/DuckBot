using System.Windows;
using DuckBot.GUI.Pages;

namespace DuckBot.GUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new InstancesPage());
        }

        private void OnInstances(object sender, RoutedEventArgs e) => MainFrame.Navigate(new InstancesPage());
        private void OnLogs(object sender, RoutedEventArgs e) => MainFrame.Navigate(new LogsPage());
        private void OnScriptBuilder(object sender, RoutedEventArgs e) => MainFrame.Navigate(new ScriptBuilderPage());
        private void OnSettings(object sender, RoutedEventArgs e) => MainFrame.Navigate(new SettingsPage());
        private void OnUpdates(object sender, RoutedEventArgs e) => MainFrame.Navigate(new UpdatesPage());
        private void OnHelp(object sender, RoutedEventArgs e) => MainFrame.Navigate(new HelpPage());
    }
}
