using System.Collections.Generic;
using System.Linq;
using MicroAppPOC.Domain.Models;
using MicroAppPOC.Domain.Predictor;
using MicroAppPOC.Infrastructure.Mvvm;
using MicroAppPOC.Services.BusPredictor;
using Prism.Navigation;

namespace MicroAppPOC.ViewModels
{
    public class StopViewModel : DetailViewModelBase
    {
        private readonly IBusPredictorService _busPredictorService;
        
        private string _stopTitle;
        private string _stopSubtitle;
        private string _stopId;
        private IList<Prediction> _predictions;
        
        public StopViewModel(IBusPredictorService busPredictorService) => _busPredictorService = busPredictorService;

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var model = parameters.GetValue<StopModel>("model");

            StopTitle = model.StopTitle;
            StopSubtitle = model.StopSubtitle;
            StopId = model.StopId;

            var result = await _busPredictorService
                .GetStopPredictions(StopId);
            
            Predictions = result.ToList();
        }

        public string StopTitle
        {
            get => _stopTitle;
            set => SetProperty(ref _stopTitle, value);
        }

        public string StopSubtitle
        {
            get => _stopSubtitle;
            set => SetProperty(ref _stopSubtitle, value);
        }

        public string StopId
        {
            get => _stopId;
            set => SetProperty(ref _stopId, value);
        }

        public IList<Prediction> Predictions
        {
            get => _predictions;
            set => SetProperty(ref _predictions, value);
        }
    }
}