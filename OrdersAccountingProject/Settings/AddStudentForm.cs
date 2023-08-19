using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrdersAccountingProject
{
    public partial class AddingStudentForm : Form
    {
        MainForm form1;
        Context db;

        public AddingStudentForm(MainForm form1)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.form1 = form1;
            db = form1.db;
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {
            var educationForms = db.EducationForms
                .Select(x => x.Name)
                .ToArray();

            var faculties = db.Faculties
                .Select(f => f.Name)
                .ToArray();

            EducationFormBox.Items.AddRange(educationForms);
            FacultyBox.Items.AddRange(faculties);

            LoadingForm.Close();
        }

        private void EducationFormBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSpecialtyBox();
        }

        private void FacultyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSpecialtyBox();
        }

        private int specialtyId = 0;

        private void FillSpecialtyBox()
        {
            string educationFormName = EducationFormBox.Text;
            string facultyName = FacultyBox.Text;

            var educationFormId = db.EducationForms
                .Where(x => x.Name == educationFormName)
                .Select(x => x.Id)
                .FirstOrDefault();

            var facultyId = db.Faculties
                .Where(f => f.Name == facultyName)
                .Select(f => f.Id)
                .FirstOrDefault();

            var departmentIds = db.Departments
                .Where(d => d.FacultyId == facultyId)
                .Select(d => d.Id)
                .ToArray();

            var specialties = db.Specialties
                .Where(s => s.EducationFormId == educationFormId && departmentIds.Contains(s.DepartmentId))
                .Select(s => s.Code + " " + s.Name)
                .ToArray();

            specialtyId = db.Specialties
                .Where(s => s.EducationFormId == educationFormId && departmentIds.Contains(s.DepartmentId))
                .Select(s => s.Id)
                .FirstOrDefault();

            SpecialtyBox.Items.Clear();
            SpecialtyBox.Items.AddRange(specialties);
        }

        private void AddStudentButton_Click(object sender, EventArgs e)
        {
            try
            {
                var courseId = db.Courses.AsEnumerable()
                    .Where(c => c.Number == int.Parse(CourseBox.Text))
                    .Select(c => c.Id)
                    .FirstOrDefault();

                Student student = new Student
                {
                    LastName = LastNameBox.Text,
                    FirstName = FirstNameBox.Text,
                    MiddleName = MiddleNameBox.Text,
                    CourseId = courseId,
                    SpecialtyId = specialtyId
                };

                db.Students.Add(student);
                db.SaveChanges();

                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Убедитесь, что все поля заполнены.\n\n{0}", ex.ToString());
            }
        }

        private bool CheckTextBoxes()
        {
            /*
            int num;
            if (int.TryParse(textBox2.Text, out num))
            {
                textBox2.BackColor = Color.Empty;
                if (int.TryParse(textBox3.Text, out num))
                {
                    textBox3.BackColor = Color.Empty;
                    return true;
                }
                textBox3.BackColor = Color.Red;
                return false;
            }
            textBox2.BackColor = Color.Red;*/
            return false;
        }

        private void ClearTextBoxes()
        {
            LastNameBox.Clear();
            FirstNameBox.Clear();
            MiddleNameBox.Clear();
            CourseBox.ResetText();
            SpecialtyBox.ResetText();
            FacultyBox.ResetText();
            EducationFormBox.ResetText();
        }
    }
}
