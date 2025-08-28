using System.Windows;
using System.Windows.Controls;
using DuckBot.Core.Models;
using DuckBot.Core.Services;

namespace DuckBot.GUI.Windows
{
    public partial class InstanceSettingsWindow : Window
    {
        private InstanceConfig _config;

        public InstanceSettingsWindow(string instanceName)
        {
            InitializeComponent();
            _config = ConfigService.Load(instanceName);
            DataContext = _config;

            ScriptList.ItemsSource = _config.Scripts;
            EmulatorList.ItemsSource = _config.Emulators;
            MailGrid.ItemsSource = _config.MailAccounts;

            BreakMinBox.Text = _config.BreakMinMinutes.ToString();
            BreakMaxBox.Text = _config.BreakMaxMinutes.ToString();
            StopAfterLoopBox.IsChecked = _config.StopAfterLoop;
            IgnoreCooldownsBox.IsChecked = _config.IgnoreCooldowns;
        }

        private void OnAddScript(object sender, RoutedEventArgs e)
        {
            _config.Scripts.Add("New Script");
            ScriptList.Items.Refresh();
        }

        private void OnRemoveScript(object sender, RoutedEventArgs e)
        {
            if (ScriptList.SelectedItem != null)
            {
                _config.Scripts.Remove(ScriptList.SelectedItem.ToString());
                ScriptList.Items.Refresh();
            }
        }

        private void OnAssignEmulator(object sender, RoutedEventArgs e)
        {
            _config.Emulators.Add("New Emulator");
            EmulatorList.Items.Refresh();
        }

        private void OnRemoveEmulator(object sender, RoutedEventArgs e)
        {
            if (EmulatorList.SelectedItem != null)
            {
                _config.Emulators.Remove(EmulatorList.SelectedItem.ToString());
                EmulatorList.Items.Refresh();
            }
        }

        private void OnAddMail(object sender, RoutedEventArgs e)
        {
            _config.MailAccounts.Add(new MailAccount { Email = "user@mail.com", Password = "pass" });
            MailGrid.Items.Refresh();
        }

        private void OnRemoveMail(object sender, RoutedEventArgs e)
        {
            if (MailGrid.SelectedItem is MailAccount account)
            {
                _config.MailAccounts.Remove(account);
                MailGrid.Items.Refresh();
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BreakMinBox.Text, out int min)) _config.BreakMinMinutes = min;
            if (int.TryParse(BreakMaxBox.Text, out int max)) _config.BreakMaxMinutes = max;

            _config.StopAfterLoop = StopAfterLoopBox.IsChecked ?? false;
            _config.IgnoreCooldowns = IgnoreCooldownsBox.IsChecked ?? false;

            ConfigService.Save(_config);

            MessageBox.Show("Instance settings saved.", "DuckBot", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
