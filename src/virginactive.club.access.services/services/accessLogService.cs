namespace virginactive.club.access.services;

using System.Collections.Generic;
using System.Diagnostics;
using virginactive.club.access.core;
using virginactive.club.access.repository;

public class accessLogService : IAccessLogService
{
    private readonly IAccessLogRepository _repository;

    public accessLogService(IAccessLogRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<AccessLog>> GetAllAccessLogsAsync()
    {
        return await _repository.GetAccessLogsAsync();
    }

    public Task<List<AccessLog>> GetUnsyncedAccessLogsAsync()
    {
        return _repository.GetUnsyncedAccessLogsAsync();
    }

    public async Task MarkLogAsSynced(int accessLogId)
    {
        try
        {
            await _repository.MarkLogAsSyncedAsync(accessLogId);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"error while syncing log with id {accessLogId} : {ex}.");
        }
    }

    public async Task RecordAccessAsync(int memberId, accessType accessType)
    {
        try
        {
            var log = new AccessLog
            {
                MemberId = memberId,
                AccessTime = DateTime.UtcNow, // Assuming UTC for simplicity
                AccessType = accessType.ToString(),
                ClubLocation = "Claremont", // fixed  for simplicity
                IsInSync = false,
            };

            await _repository.AddAccessLogAsync(log);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"error while adding access log for member {memberId} : {ex}.");
        }
    }
}
