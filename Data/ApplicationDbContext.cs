using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
        public DbSet<UniFile> UniFiles { get; set; }
        public DbSet<BifurcationRequest> BifurcationRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Faculty>()
                .HasOne(f => f.HighBoard)
                .WithOne(hb => hb.Faculty)
                .HasForeignKey<Faculty>(f => f.DeanId)
                .HasPrincipalKey<HighBoard>(hb => hb.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HighBoard>()
                .HasOne(hb => hb.Department)
                .WithOne(d => d.HighBoard)
                .HasForeignKey<Department>(hb => hb.HeadId)
                .HasPrincipalKey<HighBoard>(d => d.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AcademicRecords>()
                .HasOne(e => e.Student)
                .WithOne(e => e.AcademicRecords)
                .HasForeignKey<AcademicRecords>(e => e.StudentId)
                .HasPrincipalKey<Student>(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // One To Many

            modelBuilder.Entity<Department>()
                .HasOne(e => e.Faculty)
                .WithMany(e => e.Departments)
                .HasForeignKey(e => e.FacultyId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Subjects)
                .WithOne(e => e.Doctor)
                .HasForeignKey(e => e.DoctorId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasOne(e => e.Department)
                .WithMany(e => e.Students)
                .HasForeignKey(e => e.DepartmentId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.BifurcationRequests)
                .WithOne(br => br.Student)
                .HasForeignKey(br => br.StudentId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.HighBoard)
                .WithOne(h => h.Department)
                .HasForeignKey<Department>(d => d.HeadId)
                .HasPrincipalKey<HighBoard>(h => h.Id)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Department>()
                .HasMany(d => d.BifurcationRequests)
                .WithOne(br => br.Department)
                .HasForeignKey(br => br.DepartmentId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction);

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
                          .HasPrincipalKey(m => m.Id)
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne(m => m.Department)
                          .WithMany(b => b.DepartmentSubjects)
                          .HasForeignKey(m => m.DepartmentId)
                          .HasPrincipalKey(m => m.Id)
                          .OnDelete(DeleteBehavior.NoAction),
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
                          .HasPrincipalKey(m => m.Id)
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne(m => m.Subject)
                          .WithMany(b => b.StudentSubjects)
                          .HasForeignKey(m => m.SubjectId)
                          .HasPrincipalKey(m => m.Id)
                          .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey(t => new { t.StudentId, t.SubjectId });
                    });

            modelBuilder.Entity<StudentSubjects>()
                .Property(ar => ar.Degree)
                .HasDefaultValue(null);
        }
    }
}
