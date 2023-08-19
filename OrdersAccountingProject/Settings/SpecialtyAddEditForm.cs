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
    public partial class SpecialtyAddEditForm : Form
    {
        private Context db;
        private int? id;

        public SpecialtyAddEditForm(Context db, int? id = null)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
            this.id = id;
        }

        private void AddEmployeeForm_Load(object sender, EventArgs e)
        {
            var faculties = db.Faculties
                .Select(f => f.Name)
                .ToArray();

            facultyComboBox.Items.Clear();
            facultyComboBox.Items.AddRange(faculties);

            var educationForms = db.EducationForms
                .Select(ef => ef.Name)
                .ToArray();

            educationFormComboBox.Items.Clear();
            educationFormComboBox.Items.AddRange(educationForms);

            if (id.HasValue)
            {
                var specialty = db.Specialties.Find(id);

                codeTextBox.Text = specialty.Code;
                nameTextBox.Text = specialty.Name;
                groupNumberTextBox.Text = specialty.GroupNumber.ToString();
                facultyComboBox.Text = db.Faculties.FirstOrDefault(f => f.Id == db.Departments.FirstOrDefault(d => d.Id == specialty.DepartmentId).FacultyId).Name;
                departmentComboBox.Text = db.Departments.FirstOrDefault(d => d.Id == specialty.DepartmentId).Name;
                educationFormComboBox.Text = db.EducationForms.FirstOrDefault(ef => ef.Id == specialty.EducationFormId).Name;

                addButton.Text = "Изменить";
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

            var departmentId = db.Departments.AsEnumerable()
                .Where(d => d.Name == departmentComboBox.Text && d.FacultyId == db.Faculties.FirstOrDefault(f => f.Name == facultyComboBox.Text).Id)
                .Select(d => d.Id)
                .FirstOrDefault();

            var educationFormId = db.EducationForms.AsEnumerable()
                .Where(ef => ef.Name == educationFormComboBox.Text)
                .Select(ef => ef.Id)
                .FirstOrDefault();

            if (id.HasValue)
            {
                var specialty = db.Specialties.Find(id);

                specialty.Code = codeTextBox.Text;
                specialty.Name = nameTextBox.Text;
                specialty.GroupNumber = int.Parse(groupNumberTextBox.Text);
                specialty.DepartmentId = departmentId;
                specialty.EducationFormId = educationFormId;
            }
            else
            {
                Specialty specialty = new Specialty
                {
                    Code = codeTextBox.Text,
                    Name = nameTextBox.Text,
                    GroupNumber = int.Parse(groupNumberTextBox.Text),
                    DepartmentId = departmentId,
                    EducationFormId = educationFormId
                };

                db.Specialties.Add(specialty);
            }

            db.SaveChanges();

            LoadingForm.Close();

            this.Close();
        }
    }
}
