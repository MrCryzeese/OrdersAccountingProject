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
    public partial class FacultiesViewForm : Form
    {
        private Context db;

        public FacultiesViewForm(Context db)
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

            var faculties = db.Faculties
                .Select(f => f)
                .ToList();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = faculties;

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Name"].HeaderText = "Название факультета";
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn();
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "Изменить название факультета";
            updateColumn.Text = "Изменить";
            updateColumn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Удалить факультет";
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
                    int? id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                    this.Hide();
                    new FacultyAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string name = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить факультет {0}?", name.ToLower()),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        if (db.Departments.Any(d => d.FacultyId == id))
                        {
                            MessageBox.Show(
                                "Существует кафедра, к которой привязан этот факультет. Чтобы удалить факультет, удалите сначала соответвующую кафедру.",
                                "Удаление факультета не удалось",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            db.Faculties.Remove(db.Faculties.Find(id));
                            db.SaveChanges();

                            FillDataGridView();
                        }
                    }
                }
            }
        }

        private void addDepartmentButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FacultyAddEditForm(db).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
