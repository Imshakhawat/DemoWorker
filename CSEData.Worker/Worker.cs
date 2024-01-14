using CSEData.Worker;
using Persistance;

namespace DemoWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IStockDbContext _db;

        public Worker(ILogger<Worker> logger, IStockDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int index = 0;
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // Console.WriteLine("Worker running at: {0}", DateTimeOffset.Now);
                var scrapData = await Scraper.CseScraper();
                var dbHander = new DbHandler(_db);
                dbHander.DataHandler(scrapData);
                // Tracker.PrintRotation();
                _logger.LogInformation("Rotation {index} done ", index);
                index++;
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}