using System.Diagnostics;
using virginactive.club.access.repository;

namespace virginactive.club.access.services;

public class DataSyncService : IDataSyncService
{
    private readonly IAccessLogService _accessLogService;
    private readonly ICloudAccessLogRepository _repository;
    private const int MaxRetryAttempts = 5;
    private readonly TimeSpan DelayBetweenRetries = TimeSpan.FromSeconds(10);

    public DataSyncService(
        IAccessLogService accessLogService,
        ICloudAccessLogRepository cloudAccessLogRepository
    )
    {
        _accessLogService =
            accessLogService ?? throw new ArgumentNullException(nameof(accessLogService));
        _repository =
            cloudAccessLogRepository
            ?? throw new ArgumentNullException(nameof(cloudAccessLogRepository));
    }

    /// <summary>
    /// Rudimentary syncing machanism but adresses the need for the purposes of a POC
    /// </summary>
    /// <returns></returns>
    public async Task SyncDataAsync()
    {
        var unsyncedLogs = await _accessLogService.GetUnsyncedAccessLogsAsync();
        foreach (var log in unsyncedLogs)
        {
            int retryAttempt = 0;
            while (retryAttempt < MaxRetryAttempts)
            {
                try
                {
                    if (await _repository.UploadAccessLogAsync(log))
                    {
                        await _accessLogService.MarkLogAsSynced(log.AccessLogId);
                        Debug.WriteLine($"sucessfully synced access log : {log.AccessLogId}.");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    retryAttempt++;
                    await Task.Delay(DelayBetweenRetries);
                    Debug.WriteLine($"error while performing access log sync : {ex}.");
                }
            }
        }
    }
}
