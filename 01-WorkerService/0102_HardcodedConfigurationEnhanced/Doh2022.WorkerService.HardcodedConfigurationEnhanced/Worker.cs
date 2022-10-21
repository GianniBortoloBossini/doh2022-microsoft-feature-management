public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly IFeatureAwareFactory featureManager;

    public Worker(ILogger<Worker> logger, IFeatureAwareFactory featureManager)
    {
        this.logger = logger;
        this.featureManager = featureManager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running");
            await Task.Delay(1000, stoppingToken);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            var logMsg = "Worker running";

            // *** TOGGLE ROUTER ***
            if(await featureManager.IncludeTimestampIntoLogMesssages)
                logMsg += $" at: {DateTimeOffset.Now}";
            // *********************

            logger.LogInformation(logMsg);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
