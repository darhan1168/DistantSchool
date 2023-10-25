using System;
using System.Collections.Generic;
using DistantSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace DistantSchool.DataBase;

public partial class AppContext : DbContext
{
    public AppContext()
    {
    }

    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeachersClassesSubject> TeachersClassesSubjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=School;TrustServerCertificate=True;User=sa;Password=reallyStrongPwd123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(400);

            entity.HasOne(d => d.Lesson).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assignments_Lessons ");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.Property(e => e.Comment).HasMaxLength(150);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Grade1).HasColumnName("Grade");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Grades)
                .HasForeignKey(d => d.AssignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Assignments");

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId);

            entity.ToTable("Lessons ");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.URL).HasMaxLength(200);

            entity.HasOne(d => d.TeacherClassSubject).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.TeacherClassSubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lessons _TeachersClassesSubjects");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.UserID, "UQ_UserID").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassID)
                .HasConstraintName("FK_Students_Classes");

            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Users");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasIndex(e => e.UserID, "UQ_TeacherUserID").IsUnique();

            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.UserID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teachers_Users");
        });

        modelBuilder.Entity<TeachersClassesSubject>(entity =>
        {
            entity.HasKey(e => e.TeacherClassSubjectId);

            entity.HasOne(d => d.Class).WithMany(p => p.TeachersClassesSubjects)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeachersClassesSubjects_Classes");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeachersClassesSubjects)
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeachersClassesSubjects_Subjects");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeachersClassesSubjects)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeachersClassesSubjects_Teachers");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
