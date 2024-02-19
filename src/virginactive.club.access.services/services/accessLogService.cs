namespace virginactive.club.access.services;

using System.Collections.Generic;
using virginactive.club.access.core;
using virginactive.club.access.repository;

public class accessLogService : IAccessLogService
{
    private readonly IAccessLogRepository _repository;

    public accessLogService(IAccessLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AccessLog>> GetAllAccessLogsAsync()
    {
        return await _repository.GetAccessLogsAsync();
    }

    public async Task RecordAccessAsync(int memberId, accessType accessType)
    {
        var log = new AccessLog
        {
            MemberId = memberId,
            AccessTime = DateTime.UtcNow, // Assuming UTC for simplicity
            AccessType = accessType.ToString(),
            ClubLocation = "Claremont" // fixed  for simplicity
        };

        await _repository.AddAccessLogAsync(log);
    }
}
