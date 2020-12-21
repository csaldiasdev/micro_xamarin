using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class RouteMap : ClassMap<Route>
    {
        public RouteMap()
        {
            Map(x => x.RouteId).Name("route_id");
            Map(x => x.AgencyId).Name("agency_id");
            Map(x => x.RouteShortName).Name("route_short_name");
            Map(x => x.RouteLongName).Name("route_long_name");
            Map(x => x.RouteDesc).Name("route_desc");
            Map(x => x.RouteType).Name("route_type");
            Map(x => x.RouteUrl).Name("route_url");
            Map(x => x.RouteColor).Name("route_color");
            Map(x => x.RouteTextColor).Name("route_text_color");
        }
    }
}