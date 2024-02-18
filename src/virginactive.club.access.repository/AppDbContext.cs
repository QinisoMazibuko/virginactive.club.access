namespace virginactive.club.access.repository;

using Microsoft.EntityFrameworkCore;
using virginactive.club.access.core;

public class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<AccessLog> AccessLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=ClubAccess.db");
    }
}
