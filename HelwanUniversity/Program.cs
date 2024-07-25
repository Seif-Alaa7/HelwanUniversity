using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Data.Repository.IRepository;
using Data.Repository;
using CloudinaryDotNet;
using HelwanUniversity.Controllers;

namespace HelwanUniversity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            /*builder.Services.AddIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();*/

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
            builder.Services.AddScoped<IHighBoardRepository, HighBoardRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
            builder.Services.AddScoped<IAcademicRecordsRepository, AcademicRecordsRepository>();
            builder.Services.AddScoped<IUniFileRepository, UniFileRepository>();
            builder.Services.AddScoped<IStudentSubjectsRepository, StudentSubjectsRepository>();
            builder.Services.AddScoped<IDepartmentSubjectsRepository, DepartmentSubjectsRepository>();
            builder.Services.AddScoped<IBifurcationRequestRepository, BifurcationRequestRepository>();
            builder.Services.AddTransient<CloudinaryController>();

            var cloudinaryAccount = new Account(
                         builder.Configuration["Cloudinary:CloudName"],
                         builder.Configuration["Cloudinary:ApiKey"],
                         builder.Configuration["Cloudinary:ApiSecret"]
                             );
            var cloudinary = new Cloudinary(cloudinaryAccount);

            builder.Services.AddSingleton(cloudinary);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=University}/{action=Index}/{id?}");

            /*app.MapRazorPages();*/

            app.Run();
        }
    }
}
