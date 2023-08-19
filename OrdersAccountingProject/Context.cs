using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace OrdersAccountingProject
{
    public class Context : DbContext
    {
        public Context() : base("SQLiteConnection") { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<DeaneryStatus> DeaneryStatuses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PracticeType> PracticeTypes { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeaneryStatus>().ToTable("DeaneryStatuses");
        }
    }
}