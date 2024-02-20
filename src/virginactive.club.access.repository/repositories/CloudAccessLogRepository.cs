using virginactive.club.access.core;

namespace virginactive.club.access.repository;

public class CloudAccessLogRepository : ICloudAccessLogRepository
{
    private readonly CloudDbContext _context;

    public CloudAccessLogRepository(CloudDbContext cloudDbContext)
    {
        _context = cloudDbContext ?? throw new ArgumentNullException(nameof(cloudDbContext));
    }

    public async Task<bool> UploadAccessLogAsync(AccessLog log)
    {
        if (log == null)
            throw new ArgumentNullException(nameof(log));

        await _context
            .AccessLogs
            .AddAsync(
                new AccessLog()
                {
                    AccessLogId = log.AccessLogId,
                    MemberId = log.MemberId,
                    AccessTime = log.AccessTime,
                    ClubLocation = log.ClubLocation,
                    AccessType = log.AccessType,
                    IsInSync = true,
                }
            );
        return await _context.SaveChangesAsync() > 0;
    }
}
