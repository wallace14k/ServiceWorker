using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Integration.Process.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Integration.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPointWorkService _pointWorkService;
        public Worker(ILogger<Worker> logger,
            IConfiguration configuration,
            IPointWorkService pointWorkService)
        {
            _logger = logger;
            _configuration = configuration;
            _pointWorkService = pointWorkService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _pointWorkService.IntegrationPointWork();
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
