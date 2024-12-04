using System;

namespace Task3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new APPDBCONTEXT();

            var depart = new Department 
            { 
                DepartmentName = "CS" 
            };
            context.Departments.Add(depart);
            context.SaveChanges();

            var teach = new Teacher
            { 
                Name = "Habiba", 
                Department = depart
            };
            context.Teachers.Add(teach);
            context.SaveChanges();

            var cours = new Course 
            { 
                CourseName = "Back-end",
                Credits = 2, 
                Teacher = teach
            };
            context.Courses.Add(cours);
            context.SaveChanges();

            var stud = new Student
            { 
                FirstName = "Habiba",
                LastName = "Nahrawy",
                Email = "habibanahr@gmail.com" 
            };
            context.Students.Add(stud);
            context.SaveChanges();

            var grade = new Grade 
            { 
                GradeName = "B+"
            };
            context.Grades.Add(grade);
            context.SaveChanges();

            var enroll = new Enrollment
            {
                Student = stud,
                Course = cours,
                Grade = grade
            };
            context.Enrollments.Add(enroll);
            context.SaveChanges();
        }
    }
}
