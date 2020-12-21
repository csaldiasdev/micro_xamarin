using MicroAppPOC.Infrastructure.Mvvm;
using Prism.Navigation;

namespace MicroAppPOC.ViewModels
{
    public class GeolocationStopSearchTabViewModel : TabViewModelBase
    {
        public GeolocationStopSearchTabViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}