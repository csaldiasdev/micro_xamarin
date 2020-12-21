using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class StopTimeMap : ClassMap<StopTime>
    {
        public StopTimeMap()
        {
            Map(x => x.TripId).Name("trip_id");
            Map(x => x.ArrivalTime).Name("arrival_time");
            Map(x => x.DepartureTime).Name("departure_time");
            Map(x => x.StopId).Name("stop_id");
            Map(x => x.StopSequence).Name("stop_sequence");
        }
    }
}