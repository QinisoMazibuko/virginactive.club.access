﻿namespace virginactive.club.access.repository;

using Microsoft.EntityFrameworkCore;
using virginactive.club.access.core;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Member> Members { get; set; }
    public DbSet<AccessLog> AccessLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public async Task SeedDatabaseAsync()
    {
        if (!Members.Any())
        {
            var dummyMembers = new List<Member>
            {
                new Member
                {
                    Name = "Qiniso",
                    Surname = "Mazibuko",
                    QRCode = "X000TFH1TF",
                    Email = "Qiniso.mazibuko@example.com"
                },
                new Member
                {
                    Name = "Jane",
                    Surname = "Doe",
                    QRCode = "QR002",
                    Email = "jane.doe@example.com"
                },
                new Member
                {
                    Name = "Jim",
                    Surname = "Beam",
                    QRCode = "QR003",
                    Email = "jim.beam@example.com"
                },
                new Member
                {
                    Name = "Jack",
                    Surname = "Daniels",
                    QRCode = "QR004",
                    Email = "jack.daniels@example.com"
                },
                new Member
                {
                    Name = "Johnny",
                    Surname = "Walker",
                    QRCode = "QR005",
                    Email = "johnny.walker@example.com"
                },
                new Member
                {
                    Name = "Jameson",
                    Surname = "Irish",
                    QRCode = "QR006",
                    Email = "jameson.irish@example.com"
                },
                new Member
                {
                    Name = "Josie",
                    Surname = "Wales",
                    QRCode = "QR007",
                    Email = "josie.wales@example.com"
                },
                new Member
                {
                    Name = "Jasper",
                    Surname = "Newton",
                    QRCode = "QR008",
                    Email = "jasper.newton@example.com"
                },
                new Member
                {
                    Name = "Julie",
                    Surname = "Bean",
                    QRCode = "QR009",
                    Email = "julie.bean@example.com"
                },
                new Member
                {
                    Name = "Jolene",
                    Surname = "Dolly",
                    QRCode = "QR010",
                    Email = "jolene.dolly@example.com"
                }
            };

            await Members.AddRangeAsync(dummyMembers);
            await SaveChangesAsync();
        }
    }
}
