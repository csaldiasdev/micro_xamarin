using System;
using Prism;
using Prism.Navigation;
using ReactiveUI;

namespace MicroAppPOC.Infrastructure.Mvvm
{
    public class ReactiveTabViewModelBase : ReactiveObject, IActiveAware
    {
        protected readonly INavigationService NavigationService;
        protected virtual void RaiseIsActiveChanged() => IsActiveChanged?.Invoke(this, EventArgs.Empty);
        
        private bool _isActive;

        public ReactiveTabViewModelBase(INavigationService navigationService) => NavigationService = navigationService;

        public event EventHandler IsActiveChanged;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                this.RaiseAndSetIfChanged(ref _isActive, value);
                RaiseIsActiveChanged();
            }
        }
    }
}