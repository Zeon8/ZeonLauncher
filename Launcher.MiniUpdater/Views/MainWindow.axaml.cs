using Avalonia.Controls;
using Avalonia.Threading;
using Launcher.MiniUpdater.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Launcher.MiniUpdater.Views;

public partial class MainWindow : Window
{

    public MainWindow()
    {
        InitializeComponent();
        Opened += MainWindow_Opened;
    }

    private void MainWindow_Opened(object? sender, System.EventArgs e)
    {
        var viewModel = (MainWindowViewModel)DataContext!;
        Task.Run(viewModel.Install).GetAwaiter().OnCompleted(Close);
        Task.Run(SwitchSlides);
    }

    private async Task SwitchSlides()
    {
        while (true)
        {
            await Task.Delay(3000);
            Dispatcher.UIThread.Invoke(() =>
            {
                if (slides.SelectedIndex < slides.ItemCount-1)
                    slides.Next();
                else
                    slides.SelectedIndex = 0;
            });
        }

    }

    
}