using System.Windows;
using Wpf_ITStaffManager.ViewModels;
using Wpf_ITStaffManager.Services;

namespace Wpf_ITStaffManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow() : this(new MainViewModel(new WindowService())) { }

        public MainWindow(BaseViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;
        }
    }
}