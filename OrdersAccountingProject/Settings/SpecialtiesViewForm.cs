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
    public partial class SpecialtiesViewForm : Form
    {
        private Context db;

        public SpecialtiesViewForm(Context db)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
        }

        private void CrudStudentsGroupForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

            var educationForms = db.EducationForms
                .Select(ef => ef.Name)
                .ToArray();

            educationFormComboBox.Items.Clear();
            educationFormComboBox.Items.AddRange(educationForms);

            var faculties = db.Faculties
                .Select(f => f.Name)
                .ToArray();

            facultyComboBox.Items.Clear();
            facultyComboBox.Items.AddRange(faculties);

            var departments = db.Departments
                .Select(d => d.Name)
                .ToArray();

            departmentComboBox.Items.Clear();
            departmentComboBox.Items.AddRange(departments);

            FillDataGridView();

            LoadingForm.Close();
        }

        private void employeePositionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void facultyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var facultyId = db.Faculties.AsEnumerable()
                .Where(f => f.Name == facultyComboBox.Text)
                .Select(f => f.Id)
                .FirstOrDefault();

            var departments = db.Departments
                .Where(d => d.FacultyId == facultyId)
                .Select(d => d.Name)
                .ToArray();

            departmentComboBox.Items.Clear();
            departmentComboBox.Items.AddRange(departments);

            departmentComboBox.Enabled = true;

            FillDataGridView();
        }

        private void departmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void FillDataGridView()
        {
            LoadingForm.Open();

            var specialties = db.Specialties.AsEnumerable()
                .Select(s => new
                {
                    s.Id,
                    s.Code,
                    s.Name,
                    s.GroupNumber,
                    Faculty = db.Faculties.FirstOrDefault(f => f.Id == db.Departments.FirstOrDefault(d => d.Id == s.DepartmentId).FacultyId).Name,
                    Department = db.Departments.FirstOrDefault(d => d.Id == s.DepartmentId).Name,
                    EducationForm = db.EducationForms.FirstOrDefault(ef => ef.Id == s.EducationFormId).Name
                });

            if (educationFormComboBox.SelectedIndex != -1)
            {
                specialties = specialties
                    .Where(s => s.EducationForm == educationFormComboBox.Text);
            }

            if (facultyComboBox.SelectedIndex != -1)
            {
                specialties = specialties
                    .Where(s => s.Faculty == facultyComboBox.Text);
            }

            if (departmentComboBox.SelectedIndex != -1)
            {
                specialties = specialties
                    .Where(s => s.Department == departmentComboBox.Text);
            }

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = specialties.OrderBy(s => s.Name).ToList();

            if (educationFormComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["EducationForm"].Visible = false;
            }

            if (facultyComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["Faculty"].Visible = false;
            }

            if (departmentComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["Department"].Visible = false;
            }

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Code"].HeaderText = "Код";
            dataGridView1.Columns["Name"].HeaderText = "Наименование";
            dataGridView1.Columns["GroupNumber"].HeaderText = "Соотв. номер группы";
            dataGridView1.Columns["Faculty"].HeaderText = "Факультет";
            dataGridView1.Columns["Department"].HeaderText = "Кафедра";
            dataGridView1.Columns["EducationForm"].HeaderText = "Форма обучения";

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

            dataGridView1.Columns.Add(updateColumn);
            dataGridView1.Columns.Add(deleteColumn);

            LoadingForm.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Update")
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                    this.Hide();
                    new SpecialtyAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string
                        code = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString(),
                        name = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить специальность\"{0} {1}\"?", code, name),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        if (db.Orders.Any(o => o.SpecialtyId == id))
                        {
                            MessageBox.Show(
                                "Существует приказ, к которому привязана эта специальность. Чтобы удалить специальность, удалите сначала соответвующий приказ.",
                                "Удаление типа практики не удалось",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            db.Specialties.Remove(db.Specialties.Find(id));
                            db.SaveChanges();

                            FillDataGridView();
                        }                        
                    }
                }
            }
        }

        private void filtersClearButton_Click(object sender, EventArgs e)
        {
            educationFormComboBox.SelectedIndex = -1;
            facultyComboBox.SelectedIndex = -1;
            departmentComboBox.SelectedIndex = -1;

            departmentComboBox.Enabled = false;

            FillDataGridView();
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SpecialtyAddEditForm(db).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
