using DuckBot.Auth;
using DuckBot.Core.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace DuckBot.GUI.Pages
{
    public partial class ScriptBuilderPage : UserControl
    {
        private string _currentFile = "script.json";
        private readonly ObservableCollection<ScriptStep> _steps = new();

        public ScriptBuilderPage()
        {
            InitializeComponent();

            if (!SubscriptionManager.IsPremium)
            {
                MessageBox.Show("Script Builder is a premium feature. Please upgrade your subscription.");
                this.IsEnabled = false;
                return;
            }

            ScriptList.ItemsSource = _steps;
        }

        private void OnAddTap(object sender, RoutedEventArgs e)
        {
            _steps.Add(new ScriptStep { Type = StepType.Tap, Param1 = "100", Param2 = "200" });
        }

        private void OnAddWait(object sender, RoutedEventArgs e)
        {
            _steps.Add(new ScriptStep { Type = StepType.Wait, Param1 = "1000" });
        }

        private void OnAddInput(object sender, RoutedEventArgs e)
        {
            _steps.Add(new ScriptStep { Type = StepType.Input, Param1 = "hello" });
        }

        private void OnSaveScript(object sender, RoutedEventArgs e)
        {
            string json = JsonSerializer.Serialize(_steps, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_currentFile, json);
            MessageBox.Show("Script saved!");
        }

        private void OnLoadScript(object sender, RoutedEventArgs e)
        {
            if (File.Exists(_currentFile))
            {
                string json = File.ReadAllText(_currentFile);
                var steps = JsonSerializer.Deserialize<ObservableCollection<ScriptStep>>(json);
                if (steps != null)
                {
                    _steps.Clear();
                    foreach (var s in steps) _steps.Add(s);
                }
            }
        }
    }
}
