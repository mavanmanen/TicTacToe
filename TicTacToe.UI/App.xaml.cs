using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Core;
using TicTacToe.UI.ViewModels;

namespace TicTacToe.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private IServiceProvider ServiceProvider { get; }

    public App()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IEngine, Engine>();
        services.AddSingleton<MainWindow>();
        services.AddSingleton<MainWindowViewModel>();
        ServiceProvider = services.BuildServiceProvider();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        MainWindow.Show();
        base.OnStartup(e);
    }
}