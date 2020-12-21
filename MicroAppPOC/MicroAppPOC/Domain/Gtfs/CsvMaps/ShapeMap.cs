using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class ShapeMap : ClassMap<Shape>
    {
        public ShapeMap()
        {
            Map(x => x.ShapeBaseId).Name("shape_id");
            Map(x => x.ShapePtLat).Name("shape_pt_lat");
            Map(x => x.ShapePtLon).Name("shape_pt_lon");
            Map(x => x.ShapePtSequence).Name("shape_pt_sequence");
        }
    }
}