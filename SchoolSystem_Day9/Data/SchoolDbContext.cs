using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;

namespace SchoolSystem.Data
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<StuCrsRes> StuCrsRes => Set<StuCrsRes>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StuCrsRes>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StuCrsRes)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StuCrsRes>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StuCrsRes)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teachers)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.Course)
                .WithMany(c => c.Teachers)
                .HasForeignKey(t => t.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
