using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace MicroAppPOC.Infrastructure.Mvvm
{
    public abstract class ViewModelBase : BindableBase, INavigationAware, IPageLifecycleAware
    {
        protected readonly INavigationService NavigationService;
        
        protected ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }
    }
}