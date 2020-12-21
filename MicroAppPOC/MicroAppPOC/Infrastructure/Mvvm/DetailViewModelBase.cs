using Prism.AppModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace MicroAppPOC.Infrastructure.Mvvm
{
    public abstract class DetailViewModelBase : BindableBase, INavigationAware, IPageLifecycleAware
    {
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