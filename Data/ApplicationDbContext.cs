using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentSubjects> DepartmentSubjects { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<HighBoard> HighBoards { get; set; }
        public DbSet<StudentSubjects> StudentSubjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<AcademicRecords> academicRecords { get; set; }
        public DbSet<UniPhotos> UniPhotos { get; set; }
        public DbSet<BifurcationRequest> BifurcationRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One to One
            modelBuilder.Entity<Faculty>()
                .HasOne(f => f.HighBoard)
                .WithOne(hb => hb.Faculty)
                .HasForeignKey<Faculty>(f => f.DeanId)
                .HasPrincipalKey<HighBoard>(hb => hb.Id);

            modelBuilder.Entity<HighBoard>()
                .HasOne(hb => hb.Department)
                .WithOne(d => d.HighBoard)
                .HasForeignKey<Department>(hb => hb.HeadId)
                .HasPrincipalKey<HighBoard>(d => d.Id);

            modelBuilder.Entity<AcademicRecords>()
                .HasOne(e => e.Student)
                .WithOne(e => e.AcademicRecords)
                .HasForeignKey<AcademicRecords>(e => e.StudentId)
                .HasPrincipalKey<Student>(e => e.Id);

            // One To Many

            modelBuilder.Entity<Department>()
                .HasOne(e => e.Faculty)
                .WithMany(e => e.Departments)
                .HasForeignKey(e => e.FacultyId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Subjects)
                .WithOne(e => e.Doctor)
                .HasForeignKey(e => e.DoctorId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Student>()
                .HasOne(e => e.Department)
                .WithMany(e => e.Students)
                .HasForeignKey(e => e.DepartmentId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.BifurcationRequests)
                .WithOne(br => br.Student)
                .HasForeignKey(br => br.StudentId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.BifurcationRequests)
                .WithOne(br => br.Department)
                .HasForeignKey(br => br.DepartmentId)
                .HasPrincipalKey(e=>e.Id);

            modelBuilder.Entity<BifurcationRequest>()
                .HasKey(br => new { br.StudentId, br.DepartmentId });

            // Many To Many
            modelBuilder.Entity<Department>()
                .HasMany(e => e.Subjects)
                .WithMany(e => e.Departments)
                .UsingEntity<DepartmentSubjects>(
                    j => j.HasOne(m => m.Subject)
                          .WithMany(b => b.DepartmentSubjects)
                          .HasForeignKey(m => m.SubjectId)
                          .HasPrincipalKey(m => m.Id),
                    j => j.HasOne(m => m.Department)
                          .WithMany(b => b.DepartmentSubjects)
                          .HasForeignKey(m => m.DepartmentId)
                          .HasPrincipalKey(m => m.Id),
                    j =>
                    {
                        j.HasKey(t => new { t.SubjectId, t.DepartmentId });
                    });

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Subjects)
                .UsingEntity<StudentSubjects>(
                    j => j.HasOne(m => m.Student)
                          .WithMany(b => b.StudentSubjects)
                          .HasForeignKey(m => m.StudentId)
                          .HasPrincipalKey(m => m.Id),
                    j => j.HasOne(m => m.Subject)
                          .WithMany(b => b.StudentSubjects)
                          .HasForeignKey(m => m.SubjectId)
                          .HasPrincipalKey(m => m.Id),
                    j =>
                    {
                        j.HasKey(t => new { t.StudentId, t.SubjectId });
                    });

            // Computed Columns
            modelBuilder.Entity<AcademicRecords>()
                .Property(ar => ar.SemesterPoints)
                .HasComputedColumnSql(@"
            (
                SELECT SUM(ss.DegreePoints * s.SubjectHours)
                FROM StudentSubjects ss
                JOIN Subject s ON ss.SubjectId = s.Id
                WHERE ss.StudentId = ar.StudentId AND s.Semester = ar.Semester
            )").ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(ar => ar.TotalPoints)
                .HasComputedColumnSql(@"
            (
                SELECT SUM(ss.DegreePoints * s.SubjectHours)
                FROM StudentSubjects ss
                JOIN Subject s ON ss.SubjectId = s.Id
                WHERE ss.StudentId = ar.StudentId
            )").ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(ar => ar.CreditHours)
                .HasComputedColumnSql(@"
            (
                SELECT SUM(s.SubjectHours)
                FROM Subject s
                JOIN StudentSubjects ss ON s.Id = ss.SubjectId
                WHERE ss.Degree >= 60
                AND ss.StudentId = ar.StudentId
            )").ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(ar => ar.RecordedHours)
                .HasComputedColumnSql(@"
            (
                SELECT SUM(s.SubjectHours)
                FROM Subject s
                JOIN StudentSubjects ss ON s.Id = ss.SubjectId
                WHERE s.Semester = ar.Semester
                AND ss.StudentId = ar.StudentId
            )").ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(ar => ar.TotalHours)
                .HasComputedColumnSql(@"
            (
                SELECT SUM(s.SubjectHours)
                FROM Subject s
                JOIN StudentSubjects ss ON s.Id = ss.SubjectId
                WHERE ss.StudentId = ar.StudentId
            )").ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(ar => ar.Level)
                .HasComputedColumnSql("CASE " +
                    "WHEN CreditHours < 36 AND CreditHours >= 0 THEN 0 " +
                    "WHEN CreditHours >= 36  AND CreditHours < 72 THEN 1 " +
                    "WHEN CreditHours >= 72  AND CreditHours < 108 THEN 2 " +
                    "WHEN CreditHours >= 108  AND CreditHours <= 144 THEN 3 " +
                    "ELSE 0 END")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<StudentSubjects>()
                .Property(ar => ar.Grade)
                .HasComputedColumnSql("CASE " +
                    "WHEN Degree <= 100 AND Degree >= 90 THEN 0 " +
                    "WHEN Degree >= 85  AND Degree < 90 THEN 1 " +
                    "WHEN Degree >= 80  AND Degree < 85 THEN 2 " +
                    "WHEN Degree >= 75  AND Degree < 80 THEN 3 " +
                    "WHEN Degree >= 70  AND Degree < 75 THEN 4 " +
                    "WHEN Degree >= 65  AND Degree < 70 THEN 5 " +
                    "WHEN Degree >= 60  AND Degree < 65 THEN 6 " +
                    "ELSE 7 END")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(t => t.GPASemester)
                .HasComputedColumnSql("CASE WHEN [RecordedHours] = 0 THEN 0 ELSE ([SemesterPoints] / [RecordedHours]) END")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(t => t.GPATotal)
                .HasComputedColumnSql("CASE WHEN [TotalHours] = 0 THEN 0 ELSE ([TotalPoints] / [TotalHours]) END")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>()
                .Property(ar => ar.Semester)
                .HasComputedColumnSql("CASE " +
                    "WHEN CreditHours < 18 AND CreditHours >= 0 THEN 0 " +
                    "WHEN CreditHours >= 18  AND CreditHours < 36 THEN 1 " +
                    "WHEN CreditHours >= 36  AND CreditHours < 54 THEN 2 " +
                    "WHEN CreditHours >= 54  AND CreditHours < 72 THEN 3 " +
                    "WHEN CreditHours >= 72  AND CreditHours < 90 THEN 4 " +
                    "WHEN CreditHours >= 90  AND CreditHours < 108 THEN 5 " +
                    "WHEN CreditHours >= 108  AND CreditHours < 126 THEN 6 " +
                    "WHEN CreditHours >= 126  AND CreditHours <= 144 THEN 7 " +
                    "ELSE 0 END")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<StudentSubjects>()
                .Property(ar => ar.DegreePoints)
                .HasComputedColumnSql(
                    "CASE " +
                    "WHEN Grade IS NULL THEN NULL " +
                    "WHEN Grade = 0 THEN 4.0 " +
                    "WHEN Grade = 1 THEN 3.667 " +
                    "WHEN Grade = 2 THEN 3.333 " +
                    "WHEN Grade = 3 THEN 3.0 " +
                    "WHEN Grade = 4 THEN 2.667 " +
                    "WHEN Grade = 5 THEN 2.333 " +
                    "WHEN Grade = 6 THEN 2.0 " +
                    "ELSE 0.0 END")
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<StudentSubjects>()
                .Property(ar => ar.Degree)
                .HasDefaultValue(null);
        }

    }
}
