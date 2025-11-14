using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab4_DataFirst.Models;

public partial class Lab4CodeFirstContext : DbContext
{
    public Lab4CodeFirstContext()
    {
    }

    public Lab4CodeFirstContext(DbContextOptions<Lab4CodeFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Learner> Learners { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI;Database=Lab4_CodeFirst;uid=sa;pwd=262005; MultipleActiveResultSets=True; TrustServerCertificate=True ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("Enrollment");

            entity.HasIndex(e => e.CourseId, "IX_Enrollment_CourseId");

            entity.HasIndex(e => e.LearnerId, "IX_Enrollment_LearnerId");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments).HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Learner).WithMany(p => p.Enrollments).HasForeignKey(d => d.LearnerId);
        });

        modelBuilder.Entity<Learner>(entity =>
        {
            entity.ToTable("Learner");

            entity.HasIndex(e => e.MajorId, "IX_Learner_MajorId");

            entity.HasOne(d => d.Major).WithMany(p => p.Learners).HasForeignKey(d => d.MajorId);
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
