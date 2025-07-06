using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using TourMate.MembershipService.Repositories.Models;

namespace TourMate.MembershipService.Repositories.Context;

public partial class TourMateMembershipContext : DbContext
{
    public TourMateMembershipContext()
    {
    }

    public TourMateMembershipContext(DbContextOptions<TourMateMembershipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountMembership> AccountMemberships { get; set; }

    public virtual DbSet<MembershipPackage> MembershipPackages { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountMembership>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AccountMembership");

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.AccountMembershipId)
                .ValueGeneratedOnAdd()
                .HasColumnName("accountMembershipId");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.MembershipPackageId).HasColumnName("membershipPackageId");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
        });

        modelBuilder.Entity<MembershipPackage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MembershipPackage");

            entity.Property(e => e.BenefitDesc).HasColumnName("benefitDesc");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.MembershipPackageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("membershipPackageId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
