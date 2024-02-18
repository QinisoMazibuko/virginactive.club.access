namespace virginactive.club.access.services;

public interface IAccessLogService
{
    Task RecordAccessAsync(int memberId, string accessType);
}
