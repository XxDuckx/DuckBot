using System.Windows.Controls;
using DuckBot.Core.Services;

namespace DuckBot.GUI.Pages
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            UserLabel.Text = AuthService.CurrentUser?.Username ?? "Unknown";
            SubLabel.Text = (AuthService.CurrentUser?.IsPremium ?? false) ? "Premium" : "Free";
        }
    }
}
