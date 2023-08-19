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
    public partial class EmployeePositionsViewForm : Form
    {
        private Context db;

        public EmployeePositionsViewForm(Context db)
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

            var employeePositions = db.EmployeePositions
                .Select(ep => ep)
                .ToList();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = employeePositions;

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Position"].HeaderText = "Название должности";
            dataGridView1.Columns["Position"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn();
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "Изменить должность";
            updateColumn.Text = "Изменить";
            updateColumn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Удалить должность";
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
                    new EmployeePositionAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string position = dataGridView1.Rows[e.RowIndex].Cells["Position"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить должность \"{0}\"?", position),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        if (db.Employees.Any(x => x.PositionId == id))
                        {
                            MessageBox.Show(
                                "Существует сотрудник, имеющий данную должность. Чтобы удалить должность, удалите или измените соответвующего сотрудника.",
                                "Удаление должности не удалось",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            db.PracticeTypes.Remove(db.PracticeTypes.Find(id));
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
            new EmployeePositionAddEditForm(db).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
