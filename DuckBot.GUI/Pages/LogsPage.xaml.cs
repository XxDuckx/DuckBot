using System.IO;
using System.Windows;
using System.Windows.Controls;
using DuckBot.Core.Services;

namespace DuckBot.GUI.Pages
{
    public partial class LogsPage : UserControl
    {
        public LogsPage()
        {
            InitializeComponent();

            // Populate bot list + global
            BotSelector.Items.Add("Global Logs");

            foreach (var file in Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "logs"), "*.log"))
            {
                BotSelector.Items.Add(Path.GetFileNameWithoutExtension(file));
            }

            BotSelector.SelectionChanged += OnBotSelected;
            BotSelector.SelectedIndex = 0;
        }

        private void OnBotSelected(object sender, SelectionChangedEventArgs e)
        {
            string selected = BotSelector.SelectedItem?.ToString() ?? "";
            if (selected == "Global Logs")
            {
                LogBox.Text = File.Exists("logs/global.log") ? File.ReadAllText("logs/global.log") : "";
            }
            else
            {
                string path = $"logs/{selected}.log";
                LogBox.Text = File.Exists(path) ? File.ReadAllText(path) : "";
            }
        }
    }
}
