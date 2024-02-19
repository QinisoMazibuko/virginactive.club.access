using virginactive.club.access.core;

namespace virginactive.club.access.services;

public interface IAccessLogService
{
    Task RecordAccessAsync(int memberId, accessType accessType);

    Task<IEnumerable<AccessLog>> GetAllAccessLogsAsync();
}
