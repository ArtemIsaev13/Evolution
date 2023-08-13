using CommunityToolkit.Mvvm.DependencyInjection;
using EvolutionWPF.ViewModels;
using EvolutionWPF.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace EvolutionWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly IHost _host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services
        .AddSingleton<MainViewModel>()
        .AddSingleton<MainView>(_ =>
        {
            MainView loginView = new();
            MainViewModel loginViewModel = Ioc.Default.GetService<MainViewModel>();
            loginView.DataContext = loginViewModel;
            return loginView;
        });
    }).Build();


    public App()
    {
        Ioc.Default.ConfigureServices(_host.Services);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        MainView mainView = Ioc.Default.GetService<MainView>();
        mainView.Show();
    }
}

