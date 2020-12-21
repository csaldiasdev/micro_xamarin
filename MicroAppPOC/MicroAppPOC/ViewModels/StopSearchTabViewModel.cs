using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using MicroAppPOC.Domain.Gtfs;
using MicroAppPOC.Domain.Models;
using MicroAppPOC.Domain.Predictor;
using MicroAppPOC.Infrastructure.Database;
using MicroAppPOC.Infrastructure.Mvvm;
using Prism.Commands;
using Prism.Navigation;
using ReactiveUI;

namespace MicroAppPOC.ViewModels
{
    public class StopSearchTabViewModel : ReactiveTabViewModelBase
    {
        private string _entryStopCode;
        private string _totalResultsMessage;
        private bool _isVisibleStopsListView;
        private IList<StopModel> _stops;
        
        public StopSearchTabViewModel(
            INavigationService navigationService, 
            IGtfsGenericRepository<Stop> stopRepository) : base(navigationService)
        {
            ItemTapped = new DelegateCommand<object>(async args => await ItemTappedEvent(args));
            
            var searchStopCommand = ReactiveCommand.Create<string>(async s =>
            {
                Stops = new List<StopModel>();

                if (string.IsNullOrEmpty(s))
                {
                    TotalResultsMessage = string.Empty;
                    IsVisibleStopsListView = false;
                    return;
                }
                
                var tempStops = new List<StopModel>();
                
                var results = stopRepository
                    .Find(x => x.StopId.StartsWith(s));

                foreach (var stop in await results)
                {
                    var stopId = stop.StopId;
                    var stopTitles = stop.StopName.Split("/", 2);

                    var stopTitle = stopTitles[0].Replace($"{stopId}-", "").Trim();
                    var stopSubtitle = (stopTitles.Length < 2 ? string.Empty : stopTitles[1].Trim());
                    tempStops.Add(new StopModel(stopId, stopTitle, stopSubtitle));
                }

                Stops = tempStops;
                
                TotalResultsMessage = $"Found {Stops.Count()} results.";
                IsVisibleStopsListView = true;
            });

            this.WhenAnyValue(x => x.EntryStopCode)
                .Throttle(TimeSpan.FromSeconds(0.8), RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .InvokeCommand(searchStopCommand);
        }
        
        #region DataBinding

        public string EntryStopCode
        {
            get => _entryStopCode;
            set => this.RaiseAndSetIfChanged(ref _entryStopCode, value);
        }

        public string TotalResultsMessage
        {
            get => _totalResultsMessage;
            set => this.RaiseAndSetIfChanged(ref _totalResultsMessage, value);
        }

        public bool IsVisibleStopsListView
        {
            get => _isVisibleStopsListView;
            set => this.RaiseAndSetIfChanged(ref _isVisibleStopsListView, value);
        }

        public IList<StopModel> Stops
        {
            get => _stops;
            set => this.RaiseAndSetIfChanged(ref _stops, value);
        }

        #endregion

        #region Commands

        public DelegateCommand<object> ItemTapped { get; }
        
        #endregion

        #region Command Resolvers

        private async Task ItemTappedEvent(object args)
        {
            if (!(args is StopModel))
                return;

            await NavigationService.NavigateAsync("StopView", new NavigationParameters {{"model", args}});
        }

        #endregion
    }
}