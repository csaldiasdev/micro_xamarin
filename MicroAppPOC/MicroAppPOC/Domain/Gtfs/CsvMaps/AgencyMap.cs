using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class AgencyMap : ClassMap<Agency>
    {
        public AgencyMap()
        {
            Map(x => x.AgencyId).Name("agency_id");
            Map(x => x.AgencyName).Name("agency_name");
            Map(x => x.AgencyUrl).Name("agency_url");
            Map(x => x.AgencyTimezone).Name("agency_timezone");
        }
    }
}