using System.IO;
using MicroAppPOC.Infrastructure.Database;
using MicroAppPOC.Services.BusPredictor;
using MicroAppPOC.Services.GtfsLoader;
using MicroAppPOC.ViewModels;
using MicroAppPOC.Views;
using Prism.Ioc;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MicroAppPOC
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SplashView, SplashViewModel>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();

            containerRegistry.RegisterForNavigation<StopSearchHistoryTabView, StopSearchHistoryTabViewModel>();
            containerRegistry.RegisterForNavigation<StopSearchTabView, StopSearchTabViewModel>();
            containerRegistry.RegisterForNavigation<GeolocationStopSearchTabView, GeolocationStopSearchTabViewModel>();
            
            containerRegistry.RegisterForNavigation<StopView, StopViewModel>();
            
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, $"micro_app_data.db");
            
            containerRegistry.RegisterSingleton<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(databasePath));
            containerRegistry.RegisterScoped(typeof(IGtfsGenericRepository<>), typeof(SqliteGtfsGenericRepository<>));
            containerRegistry.RegisterScoped<IGtfsLoaderService, SqliteGtfsLoaderService>();
            containerRegistry.RegisterScoped<IBusPredictorService, BusPredictorService>();
        }
        
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("SplashView");
        }
    }
}