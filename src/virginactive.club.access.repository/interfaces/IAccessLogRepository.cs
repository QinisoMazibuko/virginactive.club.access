namespace virginactive.club.access.repository;

using virginactive.club.access.core;

public interface IAccessLogRepository
{
    Task<Member> AddAccessLogAsync(AccessLog log); // should return member details for confirmation
    Task<IEnumerable<AccessLog>> GetAccessLogsAsync();
    Task<List<AccessLog>> GetUnsyncedAccessLogsAsync();
    Task MarkLogAsSyncedAsync(int AccessLogId);
}
