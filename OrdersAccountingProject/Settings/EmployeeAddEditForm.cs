using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrdersAccountingProject.Settings
{
    public partial class EmployeeAddEditForm : Form
    {
        private Context db;
        private string position, faculty, department, deaneryStatus;
        private int id;

        public EmployeeAddEditForm(Context db, string position = "", string faculty = "", string department = "", string deaneryStatus = "")
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
            this.position = position;
            this.faculty = faculty;
            this.department = department;
            this.deaneryStatus = deaneryStatus;
        }

        public EmployeeAddEditForm(Context db, int id)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
            this.id = id;
            addButton.Text = "Изменить";
        }

        private void AddEmployeeForm_Load(object sender, EventArgs e)
        {
            var positions = db.EmployeePositions
                .Select(p => p.Position)
                .ToArray();

            positionComboBox.Items.Clear();
            positionComboBox.Items.AddRange(positions);
            positionComboBox.Text = position;

            var faculties = db.Faculties
                .Select(f => f.Name)
                .ToArray();

            facultyComboBox.Items.Clear();
            facultyComboBox.Items.AddRange(faculties);
            facultyComboBox.Text = faculty;

            var departments = db.Departments
                .Where(d => d.FacultyId == db.Faculties.FirstOrDefault(f => f.Name == faculty).Id)
                .Select(d => d.Name)
                .ToArray();

            departmentComboBox.Items.Clear();
            departmentComboBox.Items.AddRange(departments);
            departmentComboBox.Text = department;

            var deaneryStatuses = db.DeaneryStatuses
                .Select(ds => ds.Status)
                .ToArray();

            deaneryStatusComboBox.Items.Clear();
            deaneryStatusComboBox.Items.AddRange(deaneryStatuses);
            deaneryStatusComboBox.Text = deaneryStatus;

            if (addButton.Text == "Изменить")
            {
                var employee = db.Employees.Find(id);

                lastNameTextBox.Text = employee.LastName;
                firstNameTextBox.Text = employee.FirstName;
                middleNameTextBox.Text = employee.MiddleName;

                positionComboBox.Text = db.EmployeePositions
                    .Where(ep => ep.Id == employee.PositionId)
                    .Select(ep => ep.Position)
                    .FirstOrDefault();

                facultyComboBox.Text = db.Faculties
                    .Where(f => f.Id == db.Departments.FirstOrDefault(d => d.Id == employee.DepartmentId).FacultyId)
                    .Select(f => f.Name)
                    .FirstOrDefault();

                departmentComboBox.Text = db.Departments
                    .Where(d => d.Id == employee.DepartmentId)
                    .Select(d => d.Name)
                    .FirstOrDefault();

                deaneryStatusComboBox.Text = db.DeaneryStatuses
                    .Where(ds => ds.Id == employee.DeaneryStatusId)
                    .Select(ds => ds.Status)
                    .FirstOrDefault();
            }
            
            LoadingForm.Close();
        }

        private void facultyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var departments = db.Departments.AsEnumerable()
                .Where(d => d.FacultyId == db.Faculties.FirstOrDefault(f => f.Name == facultyComboBox.Text).Id)
                .Select(d => d.Name)
                .ToArray();

            departmentComboBox.Items.Clear();
            departmentComboBox.Items.AddRange(departments);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            var positionId = db.EmployeePositions.AsEnumerable()
                .Where(x => x.Position == positionComboBox.Text)
                .Select(x => x.Id)
                .FirstOrDefault();

            var departmentId = db.Departments.AsEnumerable()
                .Where(d => d.Name == departmentComboBox.Text && d.FacultyId == db.Faculties.FirstOrDefault(f => f.Name == facultyComboBox.Text).Id)
                .Select(d => d.Id)
                .FirstOrDefault();

            var deaneryStatusId = db.DeaneryStatuses.AsEnumerable()
                .Where(ds => ds.Status == deaneryStatusComboBox.Text)
                .Select(ds => ds.Id)
                .FirstOrDefault();

            if (addButton.Text == "Добавить")
            {
                Employee employee = new Employee
                {
                    LastName = lastNameTextBox.Text,
                    FirstName = firstNameTextBox.Text,
                    MiddleName = middleNameTextBox.Text,
                    PositionId = positionId != 0 ? (int?)positionId : null,
                    DepartmentId = departmentId != 0 ? (int?)departmentId : null,
                    DeaneryStatusId = deaneryStatusId != 0 ? (int?)deaneryStatusId : null
                };

                db.Employees.Add(employee);
            }
            else if (addButton.Text == "Изменить")
            {
                var employee = db.Employees.Find(id);

                employee.LastName = lastNameTextBox.Text;
                employee.FirstName = firstNameTextBox.Text;
                employee.MiddleName = middleNameTextBox.Text;
                employee.PositionId = positionId != 0 ? (int?)positionId : null;
                employee.DepartmentId = departmentId != 0 ? (int?)departmentId : null;
                employee.DeaneryStatusId = deaneryStatusId != 0 ? (int?)deaneryStatusId : null;
            }

            db.SaveChanges();
            LoadingForm.Close();
            this.Close();
        }
    }
}
