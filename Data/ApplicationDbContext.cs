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
                        SELECT SUM(ss.DegreePoints * s.CreditHour)
                        FROM StudentSubjects ss
                        JOIN Subjects s ON ss.SubjectId = s.Id
                        WHERE ss.StudentId = ar.StudentId AND s.Semester = ar.Semester
                    )");

            modelBuilder.Entity<AcademicRecords>().Property(t => t.GPASemester)
                .HasComputedColumnSql("([Semesterpoints] / [RecordedHours])");

            modelBuilder.Entity<AcademicRecords>().Property(t => t.GPATotal)
                .HasComputedColumnSql("([Totalpoints] / [TotalHours])");



        }
    }
}
