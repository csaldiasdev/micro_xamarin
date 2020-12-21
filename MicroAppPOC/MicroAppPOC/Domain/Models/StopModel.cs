namespace MicroAppPOC.Domain.Models
{
    public class StopModel
    {
        public StopModel(string stopId, string stopTitle, string stopSubtitle)
        {
            StopId = stopId;
            StopTitle = stopTitle;
            StopSubtitle = stopSubtitle;
        }

        public string StopId { get; }
        public string StopTitle { get; }
        public string StopSubtitle { get; }
    }
}