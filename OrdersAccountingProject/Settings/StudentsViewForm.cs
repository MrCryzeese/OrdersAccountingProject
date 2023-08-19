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
    public partial class StudentsViewForm : Form
    {
        private Context db;

        public StudentsViewForm(Context db)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
        }

        private void CrudStudentsGroupForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            
            var educationForms = db.EducationForms
                .Select(x => x.Name)
                .ToArray();

            educationFormComboBox.Items.Clear();
            educationFormComboBox.Items.AddRange(educationForms);

            var faculties = db.Faculties
                .Select(f => f.Name)
                .ToArray();

            facultyComboBox.Items.Clear();
            facultyComboBox.Items.AddRange(faculties);

            LoadingForm.Close();
        }

        private void educationFormComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            facultyComboBox.SelectedIndex = -1;
            specialtyComboBox.SelectedIndex = -1;
            courseComboBox.SelectedIndex = -1;

            dataGridView1.Columns.Clear();
        }

        private int courseId = 0, specialtyId = 0;
        private int[] specialtyIds;

        private void facultyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadingForm.Open();

            dataGridView1.Columns.Clear();

            specialtyComboBox.SelectedIndex = -1;
            courseComboBox.SelectedIndex = -1;

            string educationFormName = educationFormComboBox.Text;
            string facultyName = facultyComboBox.Text;

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

            specialtyIds = db.Specialties
                .Where(s => s.EducationFormId == educationFormId && departmentIds.Contains(s.DepartmentId))
                .Select(s => s.Id)
                .ToArray();

            specialtyComboBox.Items.Clear();
            specialtyComboBox.Items.AddRange(specialties);

            LoadingForm.Close();
        }

        private void specialtyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            specialtyId = specialtyIds[specialtyComboBox.SelectedIndex];
            courseComboBox.SelectedIndex = -1;

            dataGridView1.Columns.Clear();
        }

        private void courseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
        }

        private void FillDataGridView()
        {
            LoadingForm.Open();

            var students = db.Students
                .Where(s => s.SpecialtyId == specialtyId && s.CourseId == courseId)
                .ToList();

            students.Sort((s1, s2) => s1.LastName.CompareTo(s2.LastName));

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = students;

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["LastName"].HeaderText = "Фамилия";
            dataGridView1.Columns["FirstName"].HeaderText = "Имя";
            dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
            dataGridView1.Columns["CourseId"].Visible = false;
            dataGridView1.Columns["SpecialtyId"].Visible = false;

            DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn();
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "Update";
            updateColumn.Text = "Изменить";
            updateColumn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Delete";
            deleteColumn.Text = "Удалить";
            deleteColumn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.AddRange(new DataGridViewButtonColumn[] { updateColumn, deleteColumn });

            LoadingForm.Close();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            courseId = db.Courses.AsEnumerable()
                .Where(c => c.Number == int.Parse(courseComboBox.Text))
                .Select(c => c.Id)
                .FirstOrDefault();

            FillDataGridView();
        }

        private bool dataGridViewIsNotClear()
        {
            if (dataGridView1.ColumnCount == 0)
            {
                MessageBox.Show(
                    "Пожалуйста, выберите группу\nи нажмите кнопку \"Поиск\"",
                    "Группа не выбрана",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);

                return false;
            }
            return true;
        }

        private void addStudentButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewIsNotClear())
            {
                new StudentAddEditForm(db, courseId, specialtyId).ShowDialog();
                FillDataGridView();
            }
        }

        private void addStudentsButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewIsNotClear())
            {
                new StudentsAddForm(db, courseId, specialtyId).ShowDialog();
                FillDataGridView();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Update")
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                    new StudentAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string
                        lastName = dataGridView1.Rows[e.RowIndex].Cells["LastName"].Value.ToString(),
                        firstName = dataGridView1.Rows[e.RowIndex].Cells["FirstName"].Value.ToString(),
                        middleName = dataGridView1.Rows[e.RowIndex].Cells["MiddleName"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить студента\n{0} {1} {2}?", lastName, firstName, middleName),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        db.Students.Remove(db.Students.Find(id));
                        db.SaveChanges();

                        FillDataGridView();
                    }
                }
            }
        }
    }
}
