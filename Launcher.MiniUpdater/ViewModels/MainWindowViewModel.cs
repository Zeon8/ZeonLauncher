using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Launcher.Desktop.Services;
using Launcher.MiniUpdater.Services;
using System.Threading.Tasks;

namespace Launcher.MiniUpdater.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private float _installProgress;

    private readonly UpdateChecker _updateCheker;
    private readonly GameStarter _gameStarter;
    private readonly UpdateInstaller _updateInstaller = new();

    public MainWindowViewModel(UpdateChecker updater, GameStarter gameStarter)
    {
        _updateCheker = updater;
        _gameStarter = gameStarter;
        _updateInstaller.DownloadProgressChanged += UpdateInstaller_DownloadProgressChanged;
    }

    public MainWindowViewModel() { }

    private void UpdateInstaller_DownloadProgressChanged(object? sender, Downloader.DownloadProgressChangedEventArgs e)
    {
        InstallProgress = (float)e.ProgressPercentage;
    }

    [RelayCommand]
    private async Task Install()
    {
        try
        {
            await _updateInstaller.InstallUpdates(_updateCheker.NeededPatches);
        }
        finally
        {
            _gameStarter.StartGame();
        }

    }
}
