using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace DuckBot.GUI.Pages
{
    public partial class SettingsPage : Page
    {
        private string _licensePath;

        public SettingsPage()
        {
            InitializeComponent();
            _licensePath = Path.Combine(AppContext.BaseDirectory, "auth.json");
            LoadLicense();
        }

        /// <summary>
        /// Loads license key from auth.json if available.
        /// </summary>
        private void LoadLicense()
        {
            try
            {
                if (File.Exists(_licensePath))
                {
                    string json = File.ReadAllText(_licensePath);
                    var doc = JsonDocument.Parse(json);

                    if (doc.RootElement.TryGetProperty("licenseKey", out var key))
                    {
                        LicenseBox.Text = key.GetString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading license: {ex.Message}", "License Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Saves license key to auth.json.
        /// </summary>
        private void OnSaveLicense(object sender, RoutedEventArgs e)
        {
            string licenseKey = LicenseBox.Text.Trim();

            if (string.IsNullOrEmpty(licenseKey))
            {
                MessageBox.Show("Please enter a license key before saving.",
                                "License", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var obj = new { licenseKey = licenseKey };
                string json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(_licensePath, json);

                MessageBox.Show("License saved successfully!",
                                "License", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving license: {ex.Message}", "License Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
