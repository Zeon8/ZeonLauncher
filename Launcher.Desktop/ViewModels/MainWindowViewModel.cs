using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Downloader;
using Launcher.Common.Models;
using Launcher.Desktop;
using Launcher.Desktop.Models;
using Launcher.Desktop.Services;

namespace Launcher.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private IEnumerable<LoadedNews>? _news;

    [ObservableProperty]
    private bool _isInstalling = false;
    
    [ObservableProperty]
    private string? _buttonText;

    [ObservableProperty]
    private double _installProgress;

    private readonly NewsLoader _newsLoader;
    private readonly UpdateChecker _updateChecker;
    private readonly UpdateInstaller _updateInstaller;

    private CancellationTokenSource? _tokenSource;

    public MainWindowViewModel(NewsLoader newsLoader, UpdateChecker gameUpdater, UpdateInstaller updateInstaller)
    {
        _newsLoader = newsLoader;
        _updateChecker = gameUpdater;
        _updateInstaller = updateInstaller;
        _updateInstaller.DownloadProgressChanged += UpdateInstaller_DownloadProgressChanged;

        Task.Run(LoadNews);
        Task.Run(CheckUpdates);
    }

    private void UpdateInstaller_DownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
    {
        InstallProgress = e.ProgressPercentage;
    }

    public async Task LoadNews()
    {
        try
        {
            News = await _newsLoader.GetNews();
        }
        catch(Exception)
        {
            News = [new LoadedNews()
            {
                Title = "Failed to load news",
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Failed to load news, make sure you have internet connection or server went down for maintaince."
            }];
        }
    }

    private async Task CheckUpdates()
    {
        try
        {
            await _updateChecker.CheckUpdates();
        }
        catch(Exception)
        {
            ButtonText = "PLAY";
        }

        if (!_updateChecker.IsGameInstalled)
        {
            ButtonText = "INSTALL";
        }
        else if (_updateChecker.UpdateAvailable)
        {
            ButtonText = "UPDATE";
        }
        else
            ButtonText = "PLAY";
    }

    public async Task PlayOrInstall()
    {
        if (!_updateChecker.IsGameInstalled)
        {
            StartInstalling();
            await _updateInstaller.Install(_updateChecker.FullGameDownloadUrl, _tokenSource!.Token);
            FinishInstalling();
        }
        else if (_updateChecker.UpdateAvailable)
        {
            StartInstalling();
            await _updateInstaller.InstallUpdates(_updateChecker.NeededPatches.Select(p => p.DownloadUrl), _tokenSource!.Token );
            FinishInstalling();
        }
        else
        {
            Process.Start(Globals.ExebutableFileName);
        }
    }
    private void StartInstalling()
    {
        _tokenSource?.Dispose();
        _tokenSource = new CancellationTokenSource();
        IsInstalling = true;
    }

    private void FinishInstalling()
    { 
        IsInstalling = false;
        ButtonText = "PLAY";
    }

    public void Stop()
    {
        _tokenSource?.Cancel();
        IsInstalling = false;
    }
}
