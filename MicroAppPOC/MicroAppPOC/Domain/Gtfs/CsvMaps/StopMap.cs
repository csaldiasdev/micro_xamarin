using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class StopMap : ClassMap<Stop>
    {
        public StopMap()
        {
            Map(x => x.StopId).Name("stop_id");
            Map(x => x.StopCode).Name("stop_code");
            Map(x => x.StopName).Name("stop_name");
            Map(x => x.StopLat).Name("stop_lat");
            Map(x => x.StopLon).Name("stop_lon");
            Map(x => x.StopUrl).Name("stop_url");
        }
    }
}