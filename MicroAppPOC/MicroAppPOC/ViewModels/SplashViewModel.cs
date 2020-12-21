using System.Threading;
using System.Threading.Tasks;
using MicroAppPOC.Infrastructure.Mvvm;
using MicroAppPOC.Services.GtfsLoader;
using Prism.Navigation;

namespace MicroAppPOC.ViewModels
{
    public class SplashViewModel : ViewModelBase
    {
        private readonly IGtfsLoaderService _gtfsLoaderService;

        #region Data Binding

        private string _labelProgressInfo = string.Empty;
        
        public string LabelProgressInfo
        {
            get => _labelProgressInfo;
            set => SetProperty(ref _labelProgressInfo, value);
        }

        #endregion
        
        public SplashViewModel(INavigationService navigationService, IGtfsLoaderService gtfsLoaderService) : base(navigationService)
        {
            _gtfsLoaderService = gtfsLoaderService;
        }
        
        public override async void OnAppearing()
        {
            var existsInfo = await _gtfsLoaderService.ExistsGtfsInfo();
            
            if (!existsInfo)
            {
                var cancelTokenSource = new CancellationTokenSource();
                var cancelToken = cancelTokenSource.Token;

                Task.Factory.StartNew(async () =>
                {
                    while (!cancelToken.IsCancellationRequested)
                    {
                        LabelProgressInfo = await _gtfsLoaderService.GetLoadGtfsDataStatus();
                    }
                }, cancelToken);
                
                await _gtfsLoaderService.LoadGtfsData();
                cancelTokenSource.Cancel();
            }
            
            var navigationArgs = string.Concat(
                "NavigationPage/", 
                "MainView",
                "?createTab=StopSearchHistoryTabView",
                "&createTab=StopSearchTabView",
                "&createTab=GeolocationStopSearchTabView",
                "&selectedTab=StopSearchTabView"
            );
            
            var navigationResult = await NavigationService.NavigateAsync(navigationArgs);
        }
    }
}