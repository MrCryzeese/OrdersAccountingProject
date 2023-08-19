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
    public partial class EducationFormsViewForm : Form
    {
        private Context db;

        public EducationFormsViewForm(Context db)
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

            var educationForms = db.EducationForms
                .Select(ef => ef)
                .ToList();

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = educationForms;

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Name"].HeaderText = "Название формы обучения";
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            DataGridViewButtonColumn updateColumn = new DataGridViewButtonColumn();
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "Изменить форму обучения";
            updateColumn.Text = "Изменить";
            updateColumn.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Удалить форму обучения";
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
                    new EducationFormAddEditForm(db, id).ShowDialog();
                    FillDataGridView();
                    this.Show();
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string name = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                    DialogResult result = MessageBox.Show(
                        string.Format("Удалить форму обучения \"{0}\"?", name),
                        "Подтвердите действие",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value);

                        if (db.Specialties.Any(s => s.EducationFormId == id))
                        {
                            MessageBox.Show(
                                "Существует специальность, к которому привязан эта форма обучения. Чтобы удалить форму обучения, удалите или измените соответвующую специальность.",
                                "Удаление формы обучения не удалось",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                        }
                        else
                        {
                            db.EducationForms.Remove(db.EducationForms.Find(id));
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
            new EducationFormAddEditForm(db).ShowDialog();
            FillDataGridView();
            this.Show();
        }
    }
}
