using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using DuckBot.Core.Models;
using DuckBot.Core.Services;
using DuckBot.GUI.Windows;

namespace DuckBot.GUI.Pages
{
    public partial class InstancesPage : Page
    {
        private ObservableCollection<InstanceConfig> _instances;
        private string _instancesDir;

        public InstancesPage()
        {
            InitializeComponent();

            _instancesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "instances");
            Directory.CreateDirectory(_instancesDir);

            _instances = new ObservableCollection<InstanceConfig>();
            LoadInstances();

            InstanceList.ItemsSource = _instances;
        }

        private void LoadInstances()
        {
            _instances.Clear();
            foreach (var file in Directory.GetFiles(_instancesDir, "*.json"))
            {
                string name = Path.GetFileNameWithoutExtension(file);
                _instances.Add(ConfigService.Load(name));
            }
        }

        private void OnAddInstance(object sender, RoutedEventArgs e)
        {
            string newName = $"Instance {_instances.Count + 1}";
            var config = new InstanceConfig { InstanceName = newName };
            ConfigService.Save(config);
            _instances.Add(config);
        }

        private void OnRemoveInstance(object sender, RoutedEventArgs e)
        {
            if (InstanceList.SelectedItem is InstanceConfig config)
            {
                string path = Path.Combine(_instancesDir, $"{config.InstanceName}.json");
                if (File.Exists(path))
                    File.Delete(path);
                _instances.Remove(config);
            }
        }

        private void OnEditInstance(object sender, RoutedEventArgs e)
        {
            if (InstanceList.SelectedItem is InstanceConfig config)
            {
                var window = new InstanceSettingsWindow(config.InstanceName);
                window.Owner = Application.Current.MainWindow;
                window.ShowDialog();
                LoadInstances();
            }
        }

        private void OnStartInstance(object sender, RoutedEventArgs e)
        {
            if (InstanceList.SelectedItem is InstanceConfig config)
            {
                LogService.Log(config.InstanceName, "Started instance.");
                // Hook into ScriptRunner here
            }
        }

        private void OnStopInstance(object sender, RoutedEventArgs e)
        {
            if (InstanceList.SelectedItem is InstanceConfig config)
            {
                LogService.Log(config.InstanceName, "Stopped instance.");
            }
        }
    }
}
