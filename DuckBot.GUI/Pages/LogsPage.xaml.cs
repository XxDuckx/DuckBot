using System;
using System.IO;
using System.Windows.Controls;

namespace DuckBot.GUI.Pages
{
    public partial class LogsPage : Page
    {
        private readonly string _logFile;

        public LogsPage()
        {
            InitializeComponent();
            _logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "global.log");

            if (File.Exists(_logFile))
                LogBox.Text = File.ReadAllText(_logFile);
        }
    }
}
