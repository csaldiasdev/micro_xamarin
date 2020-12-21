using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class FrequencyMap : ClassMap<Frequency>
    {
        public FrequencyMap()
        {
            Map(x => x.TripId).Name("trip_id");
            Map(x => x.StartTime).Name("start_time");
            Map(x => x.EndTime).Name("end_time");
            Map(x => x.HeadwaySecs).Name("headway_secs");
            Map(x => x.ExactTimes).Name("exact_times");
        }
    }
}