namespace virginactive.club.access.services;

using virginactive.club.access.core;
using virginactive.club.access.repository;

public class accessLogService : IAccessLogService
{
    private readonly IAccessLogRepository _repository;

    public accessLogService(IAccessLogRepository repository)
    {
        _repository = repository;
    }

    public async Task RecordAccessAsync(int memberId, string accessType)
    {
        var log = new AccessLog
        {
            MemberId = memberId,
            AccessTime = DateTime.UtcNow, // Assuming UTC for simplicity
            AccessType = accessType
        };

        await _repository.AddAccessLogAsync(log);
    }
}
