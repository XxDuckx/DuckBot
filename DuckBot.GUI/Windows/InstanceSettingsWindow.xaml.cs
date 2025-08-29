using DuckBot.Core.Models;
using DuckBot.Core.Services;
using System.Windows;
using System.Windows.Controls;

namespace DuckBot.GUI.Windows
{
    public partial class InstanceSettingsWindow : Window
    {
        private InstanceConfig _config;
        private readonly string _fileName;

        public InstanceSettingsWindow(string fileName, InstanceConfig config)
        {
            InitializeComponent();
            _fileName = fileName;
            _config = config;

            // Load values into form
            InstanceNameBox.Text = _config.InstanceName;
            MailBox.Text = _config.MailAccount;

            EmulatorBox.SelectedItem = null;
            foreach (var item in EmulatorBox.Items)
            {
                if ((item as ComboBoxItem)?.Content.ToString() == _config.Emulator)
                {
                    EmulatorBox.SelectedItem = item;
                    break;
                }
            }

            ScriptList.Items.Clear();
            foreach (var script in _config.Scripts)
                ScriptList.Items.Add(script);
        }

        private void OnAddScript(object sender, RoutedEventArgs e)
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox("Enter script name:", "Add Script", "");
            if (!string.IsNullOrWhiteSpace(input))
            {
                ScriptList.Items.Add(input);
            }
        }

        private void OnRemoveScript(object sender, RoutedEventArgs e)
        {
            if (ScriptList.SelectedItem != null)
                ScriptList.Items.Remove(ScriptList.SelectedItem);
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            _config.InstanceName = InstanceNameBox.Text;
            _config.MailAccount = MailBox.Text;
            _config.Emulator = (EmulatorBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "LDPlayer";

            _config.Scripts = new string[ScriptList.Items.Count];
            for (int i = 0; i < ScriptList.Items.Count; i++)
                _config.Scripts[i] = ScriptList.Items[i].ToString();

            ConfigService.SaveConfig(_fileName, _config);

            MessageBox.Show("Settings saved!", "DuckBot", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
