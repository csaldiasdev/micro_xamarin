using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class CalendarMap : ClassMap<Calendar>
    {
        public CalendarMap()
        {
            Map(x => x.ServiceId).Name("service_id");
            Map(x => x.Monday).Name("monday");
            Map(x => x.Tuesday).Name("tuesday");
            Map(x => x.Wednesday).Name("wednesday");
            Map(x => x.Thursday).Name("thursday");
            Map(x => x.Friday).Name("friday");
            Map(x => x.Saturday).Name("saturday");
            Map(x => x.Sunday).Name("sunday");
            Map(x => x.StartDate).Name("start_date");
            Map(x => x.EndDate).Name("end_date");
        }
    }
}