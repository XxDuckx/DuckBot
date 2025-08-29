using DuckBot.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace DuckBot.GUI.Pages
{
    public partial class ScriptBuilderPage : Page
    {
        private readonly ObservableCollection<ScriptStep> _steps = new();
        private string _currentFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts", "script.json");

        public ScriptBuilderPage()
        {
            InitializeComponent();
            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts"));
            ScriptList.ItemsSource = _steps;
        }

        private void OnAddTap(object sender, RoutedEventArgs e) =>
            _steps.Add(new ScriptStep { Type = StepType.Tap, Param1 = "100", Param2 = "200" });

        private void OnAddWait(object sender, RoutedEventArgs e) =>
            _steps.Add(new ScriptStep { Type = StepType.Wait, Param1 = "1000" });

        private void OnAddInput(object sender, RoutedEventArgs e) =>
            _steps.Add(new ScriptStep { Type = StepType.Input, Param1 = "hello" });

        private void OnSaveScript(object sender, RoutedEventArgs e)
        {
            var json = JsonSerializer.Serialize(_steps, new JsonSerializerOptions { WriteIndented = true });
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

        public static List<ScriptStep> LoadScript(string file)
        {
            if (!File.Exists(file)) return new List<ScriptStep>();
            string json = File.ReadAllText(file);
            return JsonSerializer.Deserialize<List<ScriptStep>>(json) ?? new List<ScriptStep>();
        }
    }
}
