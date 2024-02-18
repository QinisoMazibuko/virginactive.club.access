using virginactive.club.access.core;

namespace virginactive.club.access.services;

public interface IMemberService
{
    Task<Member> GetMemberAsync(string memberId);
}
