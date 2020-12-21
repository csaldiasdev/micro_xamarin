using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class CalendarDateMap : ClassMap<CalendarDate>
    {
        public CalendarDateMap()
        {
            Map(x => x.ServiceId).Name("service_id");
            Map(x => x.Date).Name("date");
            Map(x => x.ExceptionType).Name("exception_type");
        }
    }
}