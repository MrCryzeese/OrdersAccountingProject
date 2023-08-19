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
    public partial class DepartmentsViewForm : Form
    {
        private Context db;

        public DepartmentsViewForm(Context db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void CrudStudentsGroupForm_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;

            FillDataGridView();
        }

        private void FillDataGridView()
        {
            LoadingForm.Open();

            var departments = db.Departments.AsEnumerable()
                .Where(d => d.Name != "Без кафедры")
                .GroupJoin(db.Employees, d => d.HeadId, e => e.Id, (d, group) => new { d, group })
                .SelectMany(x => x.group.DefaultIfEmpty(), (x, e) => new
                {
                    x.d.Id,
                    x.d.Name,
                    FacultyName = db.Faculties.FirstOrDefault(f => f.Id == x.d.FacultyId).Name,
                    HeadName = e != null ? string.Format("{0} {1}. {2}.", e.LastName, e.FirstName[0], e.MiddleName[0]) : ""
                })
                .ToList();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = departments;

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Name"].HeaderText = "Название кафедры";
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["FacultyName"].HeaderText = "Название факультета";
            dataGridView1.Columns["FacultyName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["HeadName"].HeaderText = "ФИО зав. кафедрой";
            dataGridView1.Columns["HeadName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn();
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "Изменить зав. кафедрой";
            updateColumn.Text = "Изменить";
            updateColumn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Удалить кафедру";
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
                    string departmentName = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                    this.Hide();
                    new DepartmentHeadUpdateForm(db, id, departmentName).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string name = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить кафедру\n{0}?", name.ToLower()),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        // ПРОВЕРКА ЕСТЬ ЛИ КАФЕДРЫ С FACULTYID = ID выше
                        /*
                        if (db.Departments.Any(d => d.FacultyId == id))
                        {
                            MessageBox.Show(
                                "Существует хотя бы одна кафедра, к которой привязан этот факультет. Чтобы удалить факультет, удалите сначала соответвующую кафедру.",
                                "Удаление факультета не удалось",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                        }
                        else
                        {*/
                            db.Departments.Remove(db.Departments.Find(id));
                            db.SaveChanges();

                            FillDataGridView();
                        //}
                    }
                }
            }
        }

        private void addDepartmentButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DepartmentAddEditForm(db).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
