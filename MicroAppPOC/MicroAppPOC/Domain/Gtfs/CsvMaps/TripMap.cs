using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class TripMap : ClassMap<Trip>
    {
        public TripMap()
        {
            Map(x => x.RouteId).Name("route_id");
            Map(x => x.ServiceId).Name("service_id");
            Map(x => x.TripId).Name("trip_id");
            Map(x => x.TripHeadSign).Name("trip_headsign");
            Map(x => x.DirectionId).Name("direction_id");
            Map(x => x.ShapeId).Name("shape_id");
        }
    }
}