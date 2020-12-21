namespace MicroAppPOC.Domain.Gtfs
{
    public class Stop
    {
        public string StopId { get; set; }
        public string StopCode { get; set; }
        public string StopName { get; set; }
        public double StopLat { get; set; }
        public double StopLon { get; set; }
        public string StopUrl { get; set; }
    }
}