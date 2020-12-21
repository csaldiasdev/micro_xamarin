using System;
using System.Threading.Tasks;

namespace MicroAppPOC.Services.GtfsLoader
{
    public interface IGtfsLoaderService
    {
        Task LoadGtfsData();
        DateTime GtfsDateTime();
        Task<bool> ExistsGtfsInfo();
        Task<string> GetLoadGtfsDataStatus();
    }
}