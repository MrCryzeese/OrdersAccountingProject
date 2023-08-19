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
    public partial class DepartmentAddEditForm : Form
    {
        private Context db;

        public DepartmentAddEditForm(Context db)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;

            var faculties = db.Faculties
                .Select(f => f.Name)
                .ToArray();

            facultyComboBox.Items.Clear();
            facultyComboBox.Items.AddRange(faculties);

            /*
            var employees = db.Employees.AsEnumerable()
                .Select(x => string.Format("{0} {1} {2}", x.LastName, x.FirstName, x.MiddleName))
                .ToArray();

            employeeComboBox.Items.Clear();
            employeeComboBox.Items.AddRange(employees);*/

            LoadingForm.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            var facultyId = db.Faculties
                .Where(f => f.Name == facultyComboBox.Text)
                .Select(f => f.Id)
                .FirstOrDefault();

            /*
            string[] name = employeeComboBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var employeeId = db.Employees.AsEnumerable()
                .Where(x => x.LastName == name[0] && x.FirstName == name[1] && x.MiddleName == name[2])
                .Select(x => x.Id)
                .FirstOrDefault();*/

            Department department = new Department
            {
                Name = nameTextBox.Text,
                //HeadId = employeeId,
                FacultyId = facultyId
            };

            db.Departments.Add(department);
            db.SaveChanges();

            LoadingForm.Close();

            this.Close();
        }
    }
}
