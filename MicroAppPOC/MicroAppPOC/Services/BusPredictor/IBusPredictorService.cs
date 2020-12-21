using System.Collections.Generic;
using System.Threading.Tasks;
using MicroAppPOC.Domain.Predictor;

namespace MicroAppPOC.Services.BusPredictor
{
    public interface IBusPredictorService
    {
        Task<IEnumerable<Prediction>> GetStopPredictions(string StopId);
    }
}