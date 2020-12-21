using CsvHelper.Configuration;

namespace MicroAppPOC.Domain.Gtfs.CsvMaps
{
    public class FeedInfoMap : ClassMap<FeedInfo>
    {
        public FeedInfoMap()
        {
            Map(x => x.FeedPublisherName).Name("feed_publisher_name");
            Map(x => x.FeedPublisherUrl).Name("feed_publisher_url");
            Map(x => x.FeedLang).Name("feed_lang");
            Map(x => x.FeedStartDate).Name("feed_start_date");
            Map(x => x.FeedEndDate).Name("feed_end_date");
            Map(x => x.FeedVersion).Name("feed_version");
        }
    }
}