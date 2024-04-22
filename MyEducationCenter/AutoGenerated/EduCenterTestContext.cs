﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyEducationCenter;

public partial class EduCenterTestContext : DbContext
{
    public EduCenterTestContext()
    {
    }

    public EduCenterTestContext(DbContextOptions<EduCenterTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleGroup> ModuleGroups { get; set; }

    public virtual DbSet<ModuleSubGroup> ModuleSubGroups { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<PlasticCard> PlasticCards { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleModule> RoleModules { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Timesheet> Timesheets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=edu_center_test;Username=postgres;Password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("country_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("currency_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("district_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Region).WithMany(p => p.Districts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("district_region_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Districts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("district_state_id_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employee_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.WorkInTime).HasDefaultValueSql("CURRENT_TIME");
            entity.Property(e => e.WorkOutTime).HasDefaultValueSql("CURRENT_TIME");

            entity.HasOne(d => d.District).WithMany(p => p.Employees).HasConstraintName("employee_district_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_organization_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_position_id_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Employees).HasConstraintName("employee_region_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_status_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_user_id_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("language_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("module_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.SubGroup).WithMany(p => p.Modules).HasConstraintName("module_sub_group_id_fkey");
        });

        modelBuilder.Entity<ModuleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("module_group_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<ModuleSubGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("module_sub_group_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Group).WithMany(p => p.ModuleSubGroups).HasConstraintName("module_sub_group_group_id_fkey");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_organization");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.District).WithMany(p => p.Organizations).HasConstraintName("organization_district_id_fkey");

            entity.HasOne(d => d.Region).WithMany(p => p.Organizations).HasConstraintName("organization_region_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Organizations).HasConstraintName("organization_state_id_fkey");
        });

        modelBuilder.Entity<PlasticCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plastic_card_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Organization).WithMany(p => p.PlasticCards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plastic_card_organization_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.PlasticCards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plastic_card_state_id_fkey");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("position_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsReciever).HasDefaultValue(false);
            entity.Property(e => e.IsSender).HasDefaultValue(false);

            entity.HasOne(d => d.Organization).WithMany(p => p.Positions).HasConstraintName("position_organization_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Positions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("position_status_id_fkey");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("region_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Country).WithMany(p => p.Regions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("region_country_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Regions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("region_state_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<RoleModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_module_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.CreatedUser).WithMany(p => p.RoleModules).HasConstraintName("fk_created_user_id");

            entity.HasOne(d => d.Module).WithMany(p => p.RoleModules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_module_id");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleModules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_id");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("status_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("table_pkey");
        });

        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("timesheet_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.DocDate).HasDefaultValueSql("now()");
            entity.Property(e => e.DontWork).HasDefaultValue(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Timesheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timesheet_employee_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Timesheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timesheet_organization_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Timesheets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("timesheet_status_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
            entity.Property(e => e.StateId).HasDefaultValue(1);
            entity.Property(e => e.UserTypeId).HasDefaultValue(3);

            entity.HasOne(d => d.Language).WithMany(p => p.Users).HasConstraintName("user_language_id_fkey");

            entity.HasOne(d => d.Organization).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_organization_id");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users).HasConstraintName("user_user_type_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_role_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_type_pkey");

            entity.Property(e => e.DateOfCreated).HasDefaultValueSql("now()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
