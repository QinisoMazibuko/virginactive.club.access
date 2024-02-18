using virginactive.club.access.core;
using virginactive.club.access.repository;

namespace virginactive.club.access.services;

public class memberService : IMemberService
{
    private readonly IMemberRepository _repository;

    public memberService(IMemberRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Member> GetMemberAsync(string membercode)
    {
        return await _repository.GetMemberAsync(membercode);
    }
}
