using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using VetClinic.Database;
using VetClinic.Models;
using VetClinic.MVVM.ViewModel;
using VetClinic.MVVM.ViewModel.Auth;
using VetClinic.MVVM.ViewModel.Dashboard;
using VetClinic.Services;
using NavigationService = VetClinic.Services.NavigationService;

namespace VetClinic;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(context.Configuration, services);
            })
            .Build();
    }

    private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        string connectionString = configuration.GetConnectionString("MySqlConnection");

        // Register your services here
        services.AddDbContextFactory<VeterinaryClinicContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );

        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IUserSessionService, UserSessionService>();
        services.AddSingleton<UserService>();
        services.AddSingleton<Func<Type, ViewModel>>(provider => viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));

        services.AddTransient<LoginViewModel>();
        services.AddTransient<RegisterViewModel>();
        services.AddTransient<NavigationViewModel>();

        services.AddTransient<DoctorDashboardViewModel>();
        services.AddTransient<DoctorListViewModel>();

        services.AddTransient<AdminDashboardViewModel>();
        services.AddTransient<DoctorEditViewModel>();

        services.AddTransient<ClientDashboardViewModel>();
        services.AddTransient<ViewOpinionsViewModel>();
        services.AddTransient<PetListViewModel>();
        services.AddTransient<PetEditViewModel>();
        services.AddTransient<AppointmentListViewModel>();
        services.AddTransient<PrescriptionListViewModel>();
        services.AddTransient<BookAppointmentViewModel>();








        services.AddTransient<DrugListViewModel>();
        services.AddTransient<AppointmentViewModel>();
        services.AddTransient<MostPopularServicesViewModel>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var dbContext = _host.Services.GetRequiredService<VeterinaryClinicContext>();


        try
        {
            await dbContext.Database.OpenConnectionAsync();

            //MessageBox.Show("Database connection successful", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        } catch(Exception ex)
        {
            //MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(1);
        }

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }
        base.OnExit(e);
    }
}

