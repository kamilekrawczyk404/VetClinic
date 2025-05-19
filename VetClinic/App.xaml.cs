using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using VetClinic.Interfaces;
using VetClinic.MVVM.ViewModel;
using VetClinic.Services;
using NavigationService = VetClinic.Services.NavigationService;

namespace VetClinic;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection _services = new ServiceCollection();

        _services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainWindowViewModel>()
        });
        _services.AddSingleton<MainViewModel>();
        _services.AddSingleton<MainWindowViewModel>();
        _services.AddSingleton<INavigationService, NavigationService>();

        _services.AddSingleton<Func<Type, ViewModel>>(provider => viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

        _serviceProvider = _services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}

