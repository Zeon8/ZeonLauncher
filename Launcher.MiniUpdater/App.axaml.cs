using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Launcher.MiniUpdater.Services;
using Launcher.MiniUpdater.ViewModels;
using Launcher.MiniUpdater.Views;
using System;
using System.Threading.Tasks;

namespace Launcher.MiniUpdater;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var checker = new UpdateChecker();
        var gameStarter = new GameStarter();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            try
            {
                Task.Run(checker.CheckUpdates).Wait();
            }
            catch(Exception)
            {
                StartGame(gameStarter, desktop);
                throw;
            }

            if (checker.UpdateAvailable)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowViewModel(checker, gameStarter)
                }; 
            }
            else
                StartGame(gameStarter, desktop);
        }
        base.OnFrameworkInitializationCompleted();
    }

    private static void StartGame(GameStarter gameStarter, IClassicDesktopStyleApplicationLifetime desktop)
    {
        gameStarter.StartGame();
        desktop.Shutdown();
    }
}