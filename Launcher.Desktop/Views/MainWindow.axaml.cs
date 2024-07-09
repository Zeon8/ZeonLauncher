using Avalonia.Controls;
using Launcher.Desktop.Services;
using Launcher.ViewModels;

namespace Launcher.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(new NewsLoader(), new UpdateChecker(), new UpdateInstaller());
    }
}