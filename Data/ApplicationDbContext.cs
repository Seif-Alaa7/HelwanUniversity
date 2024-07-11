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
    public class ApplicationDbContext : DbContext
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
        public DbSet<University> Universities { get; set; }
        public DbSet<AcademicRecords> academicRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                           WHERE ss.DegreePoints >= 60
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


            modelBuilder.Entity<AcademicRecords>().Property(ar=>ar.Level).HasComputedColumnSql("CASE " +
                                      "WHEN CreditHours < 36 AND CreditHours >= 0 THEN 0 " +
                                      "WHEN CreditHours >= 36  AND CreditHours < 72 THEN 1 " +
                                      "WHEN CreditHours >= 72  AND CreditHours < 108 THEN 2 " +
                                      "WHEN CreditHours >= 108  AND CreditHours <= 144 THEN 3 " +
                                      "ELSE 0 END")
                                      .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<StudentSubjects>().Property(ar => ar.Grade).HasComputedColumnSql("CASE " +
                                      "WHEN DegreePoints <= 100  AND DegreePoints >= 85 THEN 0 " +
                                      "WHEN DegreePoints >= 75   AND DegreePoints < 85 THEN 1 " +
                                      "WHEN DegreePoints >= 65   AND DegreePoints < 75 THEN 2 " +
                                      "WHEN DegreePoints >= 60   AND DegreePoints < 65 THEN 3 " +
                                      "ELSE 4 END")
                                      .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>().Property(t => t.GPASemester)
                .HasComputedColumnSql("([Semesterpoints] / [RecordedHours])").ValueGeneratedOnAddOrUpdate();


            modelBuilder.Entity<AcademicRecords>().Property(t => t.GPATotal)
                .HasComputedColumnSql("([Totalpoints] / [TotalHours])").ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AcademicRecords>().Property(ar => ar.Semester).HasComputedColumnSql("CASE " +
                                    "WHEN CreditHours < 18 AND CreditHours >= 0 THEN 0 " +
                                    "WHEN CreditHours >= 18  AND CreditHours < 36 THEN 1 " +
                                    "WHEN CreditHours >= 36  AND CreditHours < 54 THEN 2 " +
                                    "WHEN CreditHours >= 54  AND CreditHours < 72 THEN 3 " +
                                    "WHEN CreditHours >= 72  AND CreditHours < 90 THEN 4 " +
                                    "WHEN CreditHours >= 90  AND CreditHours < 108 THEN 5 " +
                                    "WHEN CreditHours >= 108  AND CreditHours < 126 THEN 6 " +
                                    "WHEN CreditHours >= 126  AND CreditHours <= 144 THEN 7 " +
                                    "ELSE 0 END").ValueGeneratedOnAddOrUpdate();


        }
    }
}
