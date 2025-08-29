using System.Collections.ObjectModel;
using System.Windows.Controls;
using DuckBot.Core.Models;

namespace DuckBot.GUI.Pages
{
    public partial class MyBotsPage : Page
    {
        public ObservableCollection<InstanceConfig> Instances { get; set; }

        public MyBotsPage()
        {
            InitializeComponent();

            // Example data until persistence is wired in
            Instances = new ObservableCollection<InstanceConfig>
            {
                new InstanceConfig { Name = "Duck 1", EmulatorId = "0", CurrentScript = "Resource Transport", Status = "Idle" },
                new InstanceConfig { Name = "RSS Farms", EmulatorId = "1", CurrentScript = "Gather Resources", Status = "Running" }
            };

            DataContext = this;
        }
    }
}
