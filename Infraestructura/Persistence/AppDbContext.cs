﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Area> Area { get; set; }
        public DbSet<ProjectType> ProjectType { get; set; }
        public DbSet<ApprovalStatus> ApprovalStatus { get; set; }
        public DbSet<ApproverRole> ApproverRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ApprovalRule> ApprovalRule { get; set; }
        public DbSet<ProjectProposal> ProjectProposal { get; set; }
        public DbSet<ProjectApprovalStep> ProjectApprovalStep { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-ABRISS;Database=proyectos;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Area
            modelBuilder.Entity<Area>().Property(a => a.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Area>().Property(a => a.Name).HasColumnType("nvarchar(25)").IsRequired();
            modelBuilder.Entity<Area>().HasData(
                new Area { Id = 1, Name = "Finanzas" },
                new Area { Id = 2, Name = "Tecnología" },
                new Area { Id = 3, Name = "Recursos Humanos" },
                new Area { Id = 4, Name = "Operaciones" }
            );

            // ProjectType
            modelBuilder.Entity<ProjectType>().Property(p => p.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ProjectType>().Property(p => p.Name).HasColumnType("nvarchar(25)").IsRequired();
            modelBuilder.Entity<ProjectType>().HasData(
                 new ProjectType { Id = 1, Name = "Mejora de Procesos" },
                 new ProjectType { Id = 2, Name = "Innovación y Desarrollo" },
                 new ProjectType { Id = 3, Name = "Infrastructure" },
                 new ProjectType { Id = 4, Name = "Capacitación Interna" }
            );

            // ApprovalStatus
            modelBuilder.Entity<ApprovalStatus>().Property(a => a.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ApprovalStatus>().Property(a => a.Name).HasColumnType("nvarchar(25)").IsRequired();
            modelBuilder.Entity<ApprovalStatus>().HasData(
                new ApprovalStatus { Id = 1, Name = "Pending" },
                new ApprovalStatus { Id = 2, Name = "Approved" },
                new ApprovalStatus { Id = 3, Name = "Rejected" },
                new ApprovalStatus { Id = 4, Name = "Observed" }
            );

            // ApproverRole
            modelBuilder.Entity<ApproverRole>().Property(r => r.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ApproverRole>().Property(r => r.Name).HasColumnType("nvarchar(25)").IsRequired();
            modelBuilder.Entity<ApproverRole>().HasData(
                new ApproverRole { Id = 1, Name = "Líder de Área" },
                new ApproverRole { Id = 2, Name = "Gerente" },
                new ApproverRole { Id = 3, Name = "Director" },
                new ApproverRole { Id = 4, Name = "Comité Técnico" }
            );

            // User
            modelBuilder.Entity<User>()
                .Property(u => u.Id).HasColumnType("int").IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Name).HasColumnType("varchar(25)").IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email).HasColumnType("varchar(100)").IsRequired();
            modelBuilder.Entity<User>()
                .HasOne(u => u.ApproverRole)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.Role)
                .IsRequired();
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "José Ferreyra", Email = "jferreyra@unaj.com", Role = 2 },
                new User { Id = 2, Name = "Ana Lucero", Email = "alucero@unaj.com", Role = 1 },
                new User { Id = 3, Name = "Gonzalo Molinas", Email = "gmolinas@unaj.com", Role = 2 },
                new User { Id = 4, Name = "Lucas Olivera", Email = "lolivera@unaj.com", Role = 3 },
                new User { Id = 5, Name = "Danilo Fagundez", Email = "dfagundez@unaj.com", Role = 4 },
                new User { Id = 6, Name = "Gabriel Galli", Email = "ggalli@unaj.com", Role = 4 }
            );

            // ApprovalRule
            modelBuilder.Entity<ApprovalRule>()
                .Property(a => a.Id).HasColumnType("bigint").IsRequired();
            modelBuilder.Entity<ApprovalRule>()
                .Property(a => a.MinAmount).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<ApprovalRule>()
                .Property(a => a.MaxAmount).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<ApprovalRule>()
                .HasOne(a => a.AreaDetail)
                .WithMany(ar => ar.ApprovalRules)
                .HasForeignKey(a => a.Area);
            modelBuilder.Entity<ApprovalRule>()
                .HasOne(a => a.ProjectType)
                .WithMany(pt => pt.ApprovalRules)
                .HasForeignKey(a => a.Type);
            modelBuilder.Entity<ApprovalRule>()
                .Property(a => a.StepOrder).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ApprovalRule>()
                .HasOne(a => a.ApproverRole)
                .WithMany(r => r.ApprovalRules)
                .HasForeignKey(a => a.ApproverRoleId)
                .IsRequired();
            modelBuilder.Entity<ApprovalRule>().HasData(
                new ApprovalRule { Id = 1, MinAmount = 0, MaxAmount = 100000, StepOrder = 1, ApproverRoleId = 1 },
                new ApprovalRule { Id = 2, MinAmount = 5000, MaxAmount = 20000, StepOrder = 2, ApproverRoleId = 2 },
                new ApprovalRule { Id = 3, MinAmount = 0, MaxAmount = 20000, Area = 2, Type = 2, StepOrder = 1, ApproverRoleId = 2 },
                new ApprovalRule { Id = 4, MinAmount = 20000, MaxAmount = 0, StepOrder = 3, ApproverRoleId = 3 },
                new ApprovalRule { Id = 5, MinAmount = 5000, MaxAmount = 0, Area = 1, Type = 1, StepOrder = 2, ApproverRoleId = 2 },
                new ApprovalRule { Id = 6, MinAmount = 0, MaxAmount = 10000, Type = 2, StepOrder = 1, ApproverRoleId = 1 },
                new ApprovalRule { Id = 7, MinAmount = 0, MaxAmount = 10000, Area = 2, Type = 1, StepOrder = 1, ApproverRoleId = 4 },
                new ApprovalRule { Id = 8, MinAmount = 10000, MaxAmount = 30000, Area = 2, StepOrder = 2, ApproverRoleId = 2 },
                new ApprovalRule { Id = 9, MinAmount = 30000, MaxAmount = 0, Area = 3, StepOrder = 2, ApproverRoleId = 3 },
                new ApprovalRule { Id = 10, MinAmount = 0, MaxAmount = 50000, Type = 4, StepOrder = 1, ApproverRoleId = 4 }
            );

            // ProjectProposal
            modelBuilder.Entity<ProjectProposal>()
                .Property(p => p.Id).HasColumnType("uniqueidentifier").ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .Property(p => p.Title).HasColumnType("varchar(255)").IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .Property(p => p.Description).HasColumnType("varchar(max)").IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .HasOne(p => p.AreaDetail)
                .WithMany(a => a.ProjectProposals)
                .HasForeignKey(p => p.Area)
                .IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .HasOne(p => p.ProjectType)
                .WithMany(t => t.ProjectProposals)
                .HasForeignKey(p => p.Type)
                .IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .Property(p => p.EstimatedAmount).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .Property(p => p.EstimatedDuration).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .HasOne(p => p.ApprovalStatus)
                .WithMany(s => s.ProjectProposals)
                .HasForeignKey(p => p.Status)
                .IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .Property(p => p.CreateAt).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<ProjectProposal>()
                .HasOne(p => p.CreatedByUser)
                .WithMany(u => u.ProjectProposals)
                .HasForeignKey(p => p.CreatedBy)
                .IsRequired();

            // ProjectApprovalStep
            modelBuilder.Entity<ProjectApprovalStep>()
                .Property(s => s.Id).HasColumnType("bigint").IsRequired();
            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(s => s.ProjectProposal)
                .WithMany(p => p.ProjectApprovalSteps)
                .HasForeignKey(s => s.ProjectProposalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(s => s.ApproverUser)
                .WithMany(u => u.ProjectApprovalSteps)
                .HasForeignKey(s => s.ApproverUserId);
            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(s => s.ApproverRole)
                .WithMany(r => r.ProjectApprovalSteps)
                .HasForeignKey(s => s.ApproverRoleId)
                .IsRequired();
            modelBuilder.Entity<ProjectApprovalStep>()
                .HasOne(s => s.ApprovalStatus)
                .WithMany(s => s.ProjectApprovalSteps)
                .HasForeignKey(s => s.Status)
                .IsRequired();
            modelBuilder.Entity<ProjectApprovalStep>()
                .Property(s => s.StepOrder).HasColumnType("int").IsRequired();
            modelBuilder.Entity<ProjectApprovalStep>()
                .Property(s => s.DecisionDate).HasColumnType("datetime");
            modelBuilder.Entity<ProjectApprovalStep>()
                .Property(s => s.Observations).HasColumnType("varchar(max)");
        }
    }
}