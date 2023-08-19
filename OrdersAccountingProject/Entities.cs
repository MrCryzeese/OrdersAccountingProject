using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OrdersAccountingProject
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
    }

    public class DeaneryStatus
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HeadId { get; set; }
        public int FacultyId { get; set; }
    }

    public class EducationForm
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EmployeePosition
    {
        [Key]
        public int Id { get; set; }
        public string Position { get; set; }
    }

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int? PositionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DeaneryStatusId { get; set; }
    }

    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Date { get; set; }
        public int? PracticeTypeId { get; set; }
        public int? PracticeManagerId { get; set; }
        public int? SpecialtyId { get; set; }
        public int? CourseId { get; set; }
        public string Path { get; set; }
        public int? UserId { get; set; }
    }

    public class PracticeType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }

    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int GroupNumber { get; set; }
        public int DepartmentId { get; set; }
        public int EducationFormId { get; set; }
    }

    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int CourseId { get; set; }
        public int SpecialtyId { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? EmployeeId { get; set; }
    }
}
