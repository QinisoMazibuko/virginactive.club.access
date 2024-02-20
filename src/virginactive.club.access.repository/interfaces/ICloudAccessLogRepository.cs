using virginactive.club.access.core;

namespace virginactive.club.access.repository;

public interface ICloudAccessLogRepository
{
    Task<bool> UploadAccessLogAsync(AccessLog log);
}
