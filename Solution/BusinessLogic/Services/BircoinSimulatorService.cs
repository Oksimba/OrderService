using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class BitcoinSimulatorService : BackgroundService
    {
        private readonly ILogger<BitcoinSimulatorService> _logger;
        public static double BitcoinValue = 5000;

        public BitcoinSimulatorService(ILogger<BitcoinSimulatorService> logger)
        {
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                CalculateNextValue();
                await Task.Delay(1000);
            }
        }

        private double CalculateNextValue()
        {
            Random random = new Random();
            double changePercentage = random.NextDouble() * 10 - 5;
            BitcoinValue *= (1 + changePercentage / 100);
            _logger.LogInformation($"Current Bitcoin Value: {BitcoinValue:C2}");

            return BitcoinValue;
        }
    }
}
