using System;
using Prism;
using Prism.Mvvm;
using Prism.Navigation;

namespace MicroAppPOC.Infrastructure.Mvvm
{
    public abstract class TabViewModelBase : BindableBase, IActiveAware
    {
        protected readonly INavigationService NavigationService;

        private bool _isActive;

        public TabViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public event EventHandler IsActiveChanged;
        
        protected virtual void RaiseIsActiveChanged() => IsActiveChanged?.Invoke(this, EventArgs.Empty);
        
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, RaiseIsActiveChanged);
        }
    }
}