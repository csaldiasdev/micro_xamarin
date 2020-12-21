using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Channels;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using CsvHelper;
using MicroAppPOC.Domain.Gtfs;
using MicroAppPOC.Domain.Gtfs.CsvMaps;
using MicroAppPOC.Infrastructure.Database;
using Calendar = MicroAppPOC.Domain.Gtfs.Calendar;

namespace MicroAppPOC.Services.GtfsLoader
{
    public class SqliteGtfsLoaderService : IGtfsLoaderService
    {
        private readonly IGtfsGenericRepository<Agency> _agencyRepository;
        private readonly IGtfsGenericRepository<Calendar> _calendarRepository;
        private readonly IGtfsGenericRepository<CalendarDate> _calendarDateRepository;
        private readonly IGtfsGenericRepository<FeedInfo> _feedInfoRepository;
        private readonly IGtfsGenericRepository<Frequency> _frequencyRepository;
        private readonly IGtfsGenericRepository<Route> _routeRepository;
        private readonly IGtfsGenericRepository<Shape> _shapeRepository;
        private readonly IGtfsGenericRepository<Stop> _stopRepository;
        private readonly IGtfsGenericRepository<StopTime> _stopTimeRepository;
        private readonly IGtfsGenericRepository<Trip> _tripRepository;

        private readonly Channel<string> _loadGtfsDataChannel;
        
        public SqliteGtfsLoaderService(
            IGtfsGenericRepository<Agency> agencyRepository,
            IGtfsGenericRepository<Calendar> calendarRepository,
            IGtfsGenericRepository<CalendarDate> calendarDateRepository,
            IGtfsGenericRepository<FeedInfo> feedInfoRepository,
            IGtfsGenericRepository<Frequency> frequencyRepository,
            IGtfsGenericRepository<Route> routeRepository,
            IGtfsGenericRepository<Shape> shapeRepository,
            IGtfsGenericRepository<Stop> stopRepository,
            IGtfsGenericRepository<StopTime> stopTimeRepository,
            IGtfsGenericRepository<Trip> tripRepository)
        {
            _agencyRepository = agencyRepository;
            _calendarRepository = calendarRepository;
            _calendarDateRepository = calendarDateRepository;
            _feedInfoRepository = feedInfoRepository;
            _frequencyRepository = frequencyRepository;
            _routeRepository = routeRepository;
            _shapeRepository = shapeRepository;
            _stopRepository = stopRepository;
            _stopTimeRepository = stopTimeRepository;
            _tripRepository = tripRepository;

            _loadGtfsDataChannel = Channel.CreateUnbounded<string>();
        }
        
        public async Task LoadGtfsData()
        {
            await _loadGtfsDataChannel.Writer.WriteAsync("Downloading GTFS data");
            
            using var client = new HttpClient();

            var content = await client.GetStringAsync("http://www.dtpm.cl/index.php/gtfs-vigente");

            using var document = await BrowsingContext
                .New(Configuration.Default)
                .OpenAsync(req => req.Content(content));

            var downloadLink = document
                .QuerySelectorAll("a")
                .OfType<IHtmlAnchorElement>()
                .Where(a => a.Href.ToLower().Contains("gtfs") && a.Href.ToLower().Contains(".zip"))
                .Select(a => a.Href)
                .FirstOrDefault();

            await using var gtfsStreamZippedFile = await client.GetStreamAsync(downloadLink);
            
            using var file = new ZipArchive(gtfsStreamZippedFile);
            
            foreach (var entry in file.Entries)
            {
                using var reader = new StreamReader(entry.Open());
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                switch (entry.Name)
                {
                    case "agency.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk agencies data");
                        csv.Configuration.RegisterClassMap<AgencyMap>();
                        var agencyRecords = csv.GetRecords<Agency>();
                        await _agencyRepository.Bulk(agencyRecords);
                        break;
                    case "calendar_dates.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk calendar dates data");
                        csv.Configuration.RegisterClassMap<CalendarDateMap>();
                        var calendarDatesRecords = csv.GetRecords<CalendarDate>();
                        await _calendarDateRepository.Bulk(calendarDatesRecords);
                        break;
                    case "calendar.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk calendar data");
                        csv.Configuration.RegisterClassMap<CalendarMap>();
                        var calendarRecords = csv.GetRecords<Calendar>();
                        await _calendarRepository.Bulk(calendarRecords);
                        break;
                    case "feed_info.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk feed info data");
                        csv.Configuration.RegisterClassMap<FeedInfoMap>();
                        var feedInfoRecords = csv.GetRecords<FeedInfo>();
                        await _feedInfoRepository.Bulk(feedInfoRecords);
                        break;
                    case "frequencies.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk frequencies data");
                        csv.Configuration.RegisterClassMap<FrequencyMap>();
                        var frequencyRecords = csv.GetRecords<Frequency>();
                        await _frequencyRepository.Bulk(frequencyRecords);
                        break;
                    case "routes.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk routes data");
                        csv.Configuration.RegisterClassMap<RouteMap>();
                        var routeRecords = csv.GetRecords<Route>();
                        await _routeRepository.Bulk(routeRecords);
                        break;
                    case "shapes.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk shapes data");
                        csv.Configuration.RegisterClassMap<ShapeMap>();
                        var shapeRecords = csv.GetRecords<Shape>();
                        await _shapeRepository.Bulk(shapeRecords);
                        break;
                    case "stop_times.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk stop times data");
                        csv.Configuration.RegisterClassMap<StopTimeMap>();
                        var stopTimeRecords = csv.GetRecords<StopTime>();
                        await _stopTimeRepository.Bulk(stopTimeRecords);
                        break;
                    case "stops.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk stops data");
                        csv.Configuration.RegisterClassMap<StopMap>();
                        var stopRecords = csv.GetRecords<Stop>();
                        await _stopRepository.Bulk(stopRecords);
                        break;
                    case "trips.txt" :
                        await _loadGtfsDataChannel.Writer.WriteAsync("Bulk trips data");
                        csv.Configuration.RegisterClassMap<TripMap>();
                        var tripRecords = csv.GetRecords<Trip>();
                        await _tripRepository.Bulk(tripRecords);
                        break;
                }
            }
        }

        public DateTime GtfsDateTime()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsGtfsInfo()
        {
            var query = await _feedInfoRepository.Find();
            return query.FirstOrDefault() != null;
        }

        public async Task<string> GetLoadGtfsDataStatus() 
            => await _loadGtfsDataChannel.Reader.ReadAsync();
    }
}