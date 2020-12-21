using MicroAppPOC.Infrastructure.Mvvm;
using Prism.Navigation;

namespace MicroAppPOC.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}