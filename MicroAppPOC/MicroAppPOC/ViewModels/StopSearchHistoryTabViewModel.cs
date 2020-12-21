using MicroAppPOC.Infrastructure.Mvvm;
using Prism.Navigation;

namespace MicroAppPOC.ViewModels
{
    public class StopSearchHistoryTabViewModel : TabViewModelBase
    {
        public StopSearchHistoryTabViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}