using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class APPDBCONTEXT : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Task3;trustservercertificate = true ; Integrated Security = SSPI;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // One to One: Enrollment and Grade
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Grade)
                .WithOne(g => g.Enrollment)
                .HasForeignKey<Enrollment>(e => e.GradeId);

            // One to Many: Department and Teachers
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Teachers)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // One to Many: Teacher and Course
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many to Many
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.EnrollmentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } 
    }
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } 
    }
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection <Course> Courses { get; set; } 
    }
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public ICollection <Teacher> Teachers { get; set; }
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int GradeId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Grade Grade { get; set; }
    }

    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public Enrollment Enrollment { get; set; }
    }

}
