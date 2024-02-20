using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using virginactive.club.access.core;

namespace virginactive.club.access.repository
{
    public class AccessLogRepository : IAccessLogRepository
    {
        private readonly AppDbContext _context;

        public AccessLogRepository(AppDbContext context)
        {
            _context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Member> AddAccessLogAsync(AccessLog log)
        {
            if (log == null)
                throw new ArgumentNullException(nameof(log));

            // Add the access log
            await _context.AccessLogs.AddAsync(log);
            await _context.SaveChangesAsync();

            // Retrieve and return the associated Member details
            // Assumes that log.MemberId is set and valid
            var member = await _context
                .Members
                .FirstOrDefaultAsync(m => m.MemberId == log.MemberId);
            if (member == null)
            {
                throw new KeyNotFoundException($"No member found with ID {log.MemberId}");
            }

            return member; // Return the member details
        }

        public async Task<IEnumerable<AccessLog>> GetAccessLogsAsync()
        {
            return await _context.AccessLogs.ToListAsync();
        }

        public async Task<List<AccessLog>> GetUnsyncedAccessLogsAsync()
        {
            return await _context.AccessLogs.Where(x => !x.IsInSync).ToListAsync();
        }

        public async Task MarkLogAsSyncedAsync(int AccessLogId)
        {
            var currentLog = await _context.AccessLogs.FindAsync(AccessLogId);

            if (currentLog == null)
            {
                throw new NullReferenceException($"log id {AccessLogId} could not be found");
            }

            currentLog.IsInSync = true;
            await _context.SaveChangesAsync();
        }
    }
}
