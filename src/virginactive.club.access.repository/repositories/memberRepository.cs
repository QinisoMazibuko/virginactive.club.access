using Microsoft.EntityFrameworkCore;
using virginactive.club.access.core;

namespace virginactive.club.access.repository;

public class memberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public memberRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Member> AddMemberAsync(Member member)
    {
        if (member == null)
            throw new ArgumentNullException(nameof(member));

        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task<Member> GetMemberAsync(string memberCode)
    {
        return await _context.Members.FirstOrDefaultAsync(m => m.QRCode == memberCode);
    }
}
