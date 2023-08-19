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
    public partial class DeaneryStatusesViewForm : Form
    {
        private Context db;

        public DeaneryStatusesViewForm(Context db)
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

            var deaneryStatuses = db.DeaneryStatuses
                .Select(ds => ds)
                .ToList();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = deaneryStatuses;

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Status"].HeaderText = "Статус";
            dataGridView1.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn();
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "Изменить статус";
            updateColumn.Text = "Изменить";
            updateColumn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Удалить статус";
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
                    new DeaneryStatusAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string status = dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить статус \"{0}\"?", status),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        if (db.Employees.Any(x => x.DeaneryStatusId == id))
                        {
                            MessageBox.Show(
                                "Существует сотрудник с таким статусом. Чтобы удалить выбранный статус, удалите или измените соответвующего сотрудника.",
                                "Удаление статуса не удалось",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            db.DeaneryStatuses.Remove(db.DeaneryStatuses.Find(id));
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
            new DeaneryStatusAddEditForm(db).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
