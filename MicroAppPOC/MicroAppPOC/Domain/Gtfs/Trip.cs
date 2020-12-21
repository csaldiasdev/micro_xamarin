namespace MicroAppPOC.Domain.Gtfs
{
    public class Trip
    {
        public string RouteId { get; set; }
        public string ServiceId { get; set; }
        public string TripId { get; set; }
        public string TripHeadSign { get; set; }
        public int DirectionId { get; set; }
        public string ShapeId { get; set; }
    }
}