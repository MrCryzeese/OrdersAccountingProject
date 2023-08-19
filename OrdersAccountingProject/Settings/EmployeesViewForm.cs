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
    public partial class EmployeesViewForm : Form
    {
        private Context db;

        public EmployeesViewForm(Context db)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
        }

        private void CrudStudentsGroupForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

            var positions = db.EmployeePositions
                .Select(p => p.Position)
                .ToArray();

            employeePositionComboBox.Items.Clear();
            employeePositionComboBox.Items.AddRange(positions);

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

            var deaneryStatuses = db.DeaneryStatuses
                .Select(ds => ds.Status)
                .ToArray();

            deaneryStatusComboBox.Items.Clear();
            deaneryStatusComboBox.Items.AddRange(deaneryStatuses);

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

        private void deaneryStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void FillDataGridView()
        {
            LoadingForm.Open();

            var employees = db.Employees.AsEnumerable()
                .Select(e => new
                {
                    e.Id,
                    e.LastName,
                    e.FirstName,
                    e.MiddleName,
                    Position = e.PositionId.HasValue ? db.EmployeePositions.FirstOrDefault(ep => ep.Id == e.PositionId).Position : "",
                    Faculty = e.DepartmentId.HasValue ? db.Faculties.FirstOrDefault(f => f.Id == db.Departments.FirstOrDefault(d => d.Id == e.DepartmentId).FacultyId).Name : "",
                    Department = e.DepartmentId.HasValue ? db.Departments.FirstOrDefault(d => d.Id == e.DepartmentId).Name : "",
                    DeaneryStatus = e.DeaneryStatusId.HasValue ? db.DeaneryStatuses.FirstOrDefault(ds => ds.Id == e.DeaneryStatusId).Status : ""
                });

            if (employeePositionComboBox.SelectedIndex != -1)
            {
                employees = employees
                    .Where(e => e.Position == employeePositionComboBox.Text);
            }

            if (facultyComboBox.SelectedIndex != -1)
            {
                employees = employees
                    .Where(e => e.Faculty == facultyComboBox.Text);
            }

            if (departmentComboBox.SelectedIndex != -1)
            {
                employees = employees
                    .Where(e => e.Department == departmentComboBox.Text);
            }

            if (deaneryStatusComboBox.SelectedIndex != -1)
            {
                employees = employees
                    .Where(e => e.DeaneryStatus == deaneryStatusComboBox.Text);
            }

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = employees.OrderBy(e => e.LastName).ToList();

            if (employeePositionComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["Position"].Visible = false;
            }

            if (facultyComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["Faculty"].Visible = false;
            }

            if (departmentComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["Department"].Visible = false;
            }

            if (deaneryStatusComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["DeaneryStatus"].Visible = false;
            }

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["LastName"].HeaderText = "Фамилия";
            dataGridView1.Columns["FirstName"].HeaderText = "Имя";
            dataGridView1.Columns["MiddleName"].HeaderText = "Отчество";
            dataGridView1.Columns["Position"].HeaderText = "Должность";
            dataGridView1.Columns["Faculty"].HeaderText = "Факультет";
            dataGridView1.Columns["Department"].HeaderText = "Кафедра";
            dataGridView1.Columns["DeaneryStatus"].HeaderText = "Статус в деканате";

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Update")
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);
                    this.Hide();
                    new EmployeeAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string
                        lastName = dataGridView1.Rows[e.RowIndex].Cells["LastName"].Value.ToString(),
                        firstName = dataGridView1.Rows[e.RowIndex].Cells["FirstName"].Value.ToString(),
                        middleName = dataGridView1.Rows[e.RowIndex].Cells["MiddleName"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить сотрудника\n{0} {1} {2}?", lastName, firstName, middleName),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        db.Employees.Remove(db.Employees.Find(id));
                        db.SaveChanges();

                        FillDataGridView();
                    }
                }
            }
        }

        private void filtersClearButton_Click(object sender, EventArgs e)
        {
            employeePositionComboBox.SelectedIndex = -1;
            facultyComboBox.SelectedIndex = -1;
            departmentComboBox.SelectedIndex = -1;
            deaneryStatusComboBox.SelectedIndex = -1;

            departmentComboBox.Enabled = false;

            FillDataGridView();
        }

        private void addEmployeeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EmployeeAddEditForm(db, employeePositionComboBox.Text, facultyComboBox.Text, departmentComboBox.Text, deaneryStatusComboBox.Text).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
