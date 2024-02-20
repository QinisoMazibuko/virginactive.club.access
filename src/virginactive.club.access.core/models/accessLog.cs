namespace virginactive.club.access.core;

public class AccessLog
{
    public int AccessLogId { get; set; }
    public int MemberId { get; set; }
    public DateTime AccessTime { get; set; }
    public string AccessType { get; set; } // Type of access, e.g., "Entry" or "Exit"
    public string ClubLocation { get; set; }
    public Member Member { get; set; }

     public bool IsInSync { get; set; }
}
