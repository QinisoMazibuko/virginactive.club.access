using System.ComponentModel.DataAnnotations.Schema;

namespace virginactive.club.access.core;

public class Member
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MemberId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string QRCode { get; set; }
    public string Email { get; set; }
}
