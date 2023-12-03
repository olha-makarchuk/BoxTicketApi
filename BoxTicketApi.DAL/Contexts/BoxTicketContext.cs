using System;
using System.Collections.Generic;
using BoxTicketApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxTicketApi.DAL.Contexts;

public partial class BoxTicketContext : DbContext
{
    public BoxTicketContext()
    {
    }

    public BoxTicketContext(DbContextOptions<BoxTicketContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllTicket> AllTickets { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Performance> Performances { get; set; }

    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<StatusTicket> StatusTickets { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<TypeOfTicket> TypeOfTickets { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdAllTickets");

            entity.HasOne(d => d.IdPerformanceNavigation).WithMany(p => p.AllTickets)
                .HasForeignKey(d => d.IdPerformance)
                .HasConstraintName("FK_AllTickets_Performance");

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.AllTickets)
                .HasForeignKey(d => d.IdType)
                .HasConstraintName("FK_AllTickets_TypeOfTickets");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdAuthor");

            entity.ToTable("Author");

            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdGenre");

            entity.ToTable("Genre");

            entity.Property(e => e.NameGenre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Performance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdPerformance");

            entity.ToTable("Performance");

            entity.Property(e => e.DateTimeEvent).HasColumnType("datetime");
            entity.Property(e => e.PerformanceName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.Performances)
                .HasForeignKey(d => d.IdAuthor)
                .HasConstraintName("FK_Performance_Author");

            entity.HasOne(d => d.IdGenreNavigation).WithMany(p => p.Performances)
                .HasForeignKey(d => d.IdGenre)
                .HasConstraintName("FK_Performance_Genre");
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdRole");

            entity.ToTable("RoleUser");

            entity.Property(e => e.NameRole)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdStatus");

            entity.ToTable("StatusTicket");

            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdTicket");

            entity.ToTable("Ticket");

            entity.HasOne(d => d.IdAllTicketsNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdAllTickets)
                .HasConstraintName("FK_Ticket_AllTickets");

            entity.HasOne(d => d.IdPerformanceNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdPerformance)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Performance");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_StatusTicket");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Ticket_UserInfo");
        });

        modelBuilder.Entity<TypeOfTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdType");

            entity.Property(e => e.TypeName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_IdUser");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MiddleName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_UserInfo_RoleUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
