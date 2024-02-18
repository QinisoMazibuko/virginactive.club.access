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
            _context = context;
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
            // This returns all access logs. will be adding filtering and pagination for performance.
        }
    }
}
