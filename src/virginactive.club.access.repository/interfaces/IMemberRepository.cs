namespace virginactive.club.access.repository;

using virginactive.club.access.core;

public interface IMemberRepository
{
    Task<Member> AddMemberAsync(Member member);
    Task<Member> GetMemberAsync(string memberCode);
}
