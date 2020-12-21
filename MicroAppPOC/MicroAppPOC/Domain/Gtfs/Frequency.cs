namespace MicroAppPOC.Domain.Gtfs
{
    public class Frequency
    {
        public string TripId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int HeadwaySecs { get; set; }
        public int ExactTimes { get; set; }
    }
}