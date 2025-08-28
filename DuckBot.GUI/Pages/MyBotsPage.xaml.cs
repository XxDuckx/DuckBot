using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DuckBot.Core.Models;
using DuckBot.Core.Services;
using DuckBot.GUI.Windows;
using DuckBot.Auth;

namespace DuckBot.GUI.Pages
{
    public partial class MyBotsPage : UserControl
    {
        public static ObservableCollection<BotConfig> Bots { get; set; } = new();

        public MyBotsPage()
        {
            InitializeComponent();
            BotList.ItemsSource = Bots;

            // Load existing bot configs
            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory(), "Duck *.json"))
            {
                var config = ConfigService.LoadConfig(file);
                Bots.Add(config);
            }
        }

        private void OnAddBot(object sender, RoutedEventArgs e)
        {
            var bot = new BotConfig { Name = $"Duck {Bots.Count + 1}" };
            Bots.Add(bot);
            ConfigService.SaveConfig(bot, $"{bot.Name}.json");
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is BotConfig bot)
            {
                Bots.Remove(bot);
                if (File.Exists($"{bot.Name}.json"))
                    File.Delete($"{bot.Name}.json");
                MessageBox.Show($"{bot.Name} removed");
            }
        }

        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is BotConfig bot)
            {
                var win = new InstanceSettingsWindow(bot.Name);
                win.ShowDialog();
            }
        }

        private void OnStartClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is BotConfig bot)
            {
                if (!SubscriptionManager.IsPremium && bot.Scripts.Exists(s => s.Type == StepType.IfImage || s.Type == StepType.OCRWait))
                {
                    MessageBox.Show("This script contains premium steps. Upgrade to Premium to run.");
                    return;
                }

                // For now, just log. Later: run ScriptRunner
                LogService.Log(bot.Name, "Bot started");
            }
        }

        private void OnStopClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.DataContext is BotConfig bot)
            {
                LogService.Log(bot.Name, "Bot stopped");
            }
        }
    }
}
