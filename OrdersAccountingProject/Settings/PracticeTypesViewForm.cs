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
    public partial class PracticeTypesViewForm : Form
    {
        private Context db;

        public PracticeTypesViewForm(Context db)
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

            var practiceTypes = db.PracticeTypes
                .Select(pt => pt)
                .ToList();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = practiceTypes;

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Type"].HeaderText = "Наименование";
            dataGridView1.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn();
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "Изменить тип практики";
            updateColumn.Text = "Изменить";
            updateColumn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Удалить тип практики";
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
                    new PracticeTypeAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string name = dataGridView1.Rows[e.RowIndex].Cells["Type"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить тип практики \"{0}\"?", name),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        if (db.Orders.Any(o => o.PracticeTypeId == id))
                        {
                            MessageBox.Show(
                                "Существует приказ, к которому привязан этот тип практики. Чтобы удалить тип практики, удалите сначала соответвующий приказ.",
                                "Удаление типа практики не удалось",
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
            new PracticeTypeAddEditForm(db).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
