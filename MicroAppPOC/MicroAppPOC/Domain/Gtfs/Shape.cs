namespace MicroAppPOC.Domain.Gtfs
{
    public class Shape
    {
        public string ShapeBaseId { get; set; }
        public double ShapePtLat { get; set; }
        public double ShapePtLon { get; set; }
        public int ShapePtSequence { get; set; }
    }
}