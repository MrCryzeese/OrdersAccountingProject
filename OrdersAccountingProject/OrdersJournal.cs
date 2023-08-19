using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.Entity;
using System.IO;
using System.Globalization;

namespace OrdersAccountingProject
{
    public partial class OrdersJournal : Form
    {
        private Context db;

        public OrdersJournal(Context db)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
        }

        private void OrdersJournal_Load(object sender, EventArgs e)
        {
            practiceTypeComboBox.Items.Clear();
            practiceTypeComboBox.Items.AddRange(db.PracticeTypes.Select(p => p.Type).ToArray());

            facultyComboBox.Items.Clear();
            facultyComboBox.Items.AddRange(db.Faculties.Select(f => f.Name).ToArray());

            userComboBox.Items.Clear();
            userComboBox.Items.AddRange(db.Users.AsEnumerable()
                .Join(db.Employees, u => u.EmployeeId, x => x.Id, (u, x) => new
                {
                    UserName = string.Format("{0} {1}.{2}.", x.LastName, x.FirstName[0], x.MiddleName[0])
                })
                .Select(ux => ux.UserName)
                .ToArray());

            FillDataGridView();

            LoadingForm.Close();
        }

        private void FillDataGridView()
        {
            LoadingForm.Open();

            var orders = db.Orders.AsEnumerable()
                .OrderByDescending(o => DateTime.ParseExact(o.Date, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                .ThenByDescending(o => o.Number)
                .Join(db.PracticeTypes, o => o.PracticeTypeId, p => p.Id, (o, p) => new
                {
                    o,
                    PracticeType = p.Type
                })
                .Join(db.Employees, op => op.o.PracticeManagerId, x => x.Id, (op, x) => new
                {
                    op,
                    PracticeManager = string.Format("{0} {1}.{2}.", x.LastName, x.FirstName[0], x.MiddleName[0])
                })
                .Join(db.Specialties, opx => opx.op.o.SpecialtyId, s => s.Id, (opx, s) => new
                {
                    opx,
                    Specialty = s.Code + " " + s.Name
                })
                .Join(db.Courses, opxs => opxs.opx.op.o.CourseId, c => c.Id, (opxs, c) => new
                {
                    opxs,
                    CourseNumber = c.Number
                })
                .Join(db.Users, opxsc => opxsc.opxs.opx.op.o.UserId, u => u.Id, (opxsc, u) => new
                {
                    opxsc,
                    UserEmployeeIdId = u.EmployeeId
                })
                .Join(db.Employees, ospxcu => ospxcu.UserEmployeeIdId, x => x.Id, (ospxcu, x) => new
                {
                    Id = ospxcu.opxsc.opxs.opx.op.o.Id,
                    Number = ospxcu.opxsc.opxs.opx.op.o.Number,
                    Date = ospxcu.opxsc.opxs.opx.op.o.Date,
                    PracticeType = ospxcu.opxsc.opxs.opx.op.PracticeType,
                    PracticeManager = ospxcu.opxsc.opxs.opx.PracticeManager,
                    Specialty = ospxcu.opxsc.opxs.Specialty,
                    CourseNumber = ospxcu.opxsc.CourseNumber,
                    Path = ospxcu.opxsc.opxs.opx.op.o.Path,
                    UserName = string.Format("{0} {1}.{2}.", x.LastName, x.FirstName[0], x.MiddleName[0])
                });

            if (practiceTypeComboBox.SelectedIndex != -1)
            {
                orders = orders.AsEnumerable()
                    .Where(o => o.PracticeType == practiceTypeComboBox.Text)
                    .Select(o => o);
            }

            if (facultyComboBox.SelectedIndex != -1)
            {
                var facultyId = db.Faculties.AsEnumerable()
                    .Where(f => f.Name == facultyComboBox.Text)
                    .Select(f => f.Id)
                    .FirstOrDefault();

                var departmentsIds = db.Departments
                    .Where(d => d.FacultyId == facultyId)
                    .Select(d => d.Id);

                var specialties = db.Specialties
                    .Where(s => departmentsIds.Contains(s.DepartmentId))
                    .Select(s => s.Code + " " + s.Name);

                orders = orders
                    .Where(o => specialties.Contains(o.Specialty))
                    .Select(o => o);
            }

            if (userComboBox.SelectedIndex != -1)
            {
                orders = orders.AsEnumerable()
                    .Where(o => o.UserName == userComboBox.Text)
                    .Select(o => o);
            }

            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = orders.ToList();

            if (practiceTypeComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["PracticeType"].Visible = false;
            }

            if (userComboBox.SelectedIndex != -1)
            {
                dataGridView1.Columns["UserName"].Visible = false;
            }

            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Number"].HeaderText = "№";
            dataGridView1.Columns["Number"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Date"].HeaderText = "Дата форм. приказа";
            dataGridView1.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridView1.Columns["PracticeType"].HeaderText = "Тип практики";
            dataGridView1.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["PracticeManager"].HeaderText = "Рук. практики от кафедры";
            dataGridView1.Columns["PracticeManager"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            dataGridView1.Columns["Specialty"].HeaderText = "Специальность";
            dataGridView1.Columns["CourseNumber"].HeaderText = "Курс";
            dataGridView1.Columns["CourseNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridView1.Columns["Path"].Visible = false;
            dataGridView1.Columns["UserName"].HeaderText = "Ответственный за форм. приказа";
            dataGridView1.Columns["UserName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            DataGridViewButtonColumn wordColumn = new DataGridViewButtonColumn();
            wordColumn.Name = "Word";
            wordColumn.HeaderText = "Word";
            wordColumn.Text = "Открыть";
            wordColumn.UseColumnTextForButtonValue = true;
            wordColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.Name = "Status";
            statusColumn.HeaderText = "Статус";
            statusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            DataGridViewButtonColumn pdfColumn = new DataGridViewButtonColumn();
            pdfColumn.Name = "PDF";
            pdfColumn.HeaderText = "PDF";
            pdfColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Удалить";
            deleteColumn.Text = "Удалить";
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;

            dataGridView1.Columns.Add(wordColumn);
            dataGridView1.Columns.Add(statusColumn);
            dataGridView1.Columns.Add(pdfColumn);
            dataGridView1.Columns.Add(deleteColumn);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string path = row.Cells["Path"].Value.ToString();
                path = path.Substring(0, path.Length - 4) + "pdf";

                if (File.Exists(path))
                {
                    row.Cells["Status"].Value = "PDF загружен";
                    row.Cells["Status"].Style.BackColor = Color.Green;
                    row.Cells["PDF"].Value = "Открыть";
                }
                else
                {
                    row.Cells["Status"].Value = "PDF не загружен";
                    row.Cells["Status"].Style.BackColor = Color.Red;
                    row.Cells["PDF"].Value = "Загрузить";
                }
            }

            LoadingForm.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Word")
                {
                    Process.Start(dataGridView1.Rows[e.RowIndex].Cells["Path"].Value.ToString());
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "PDF")
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["PDF"].Value.ToString() == "Открыть")
                    {
                        string path = dataGridView1.Rows[e.RowIndex].Cells["Path"].Value.ToString();
                        path = path.Substring(0, path.Length - 4) + "pdf";

                        Process.Start(path);
                    }
                    else if (dataGridView1.Rows[e.RowIndex].Cells["PDF"].Value.ToString() == "Загрузить")
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Title = "Выберите отсканированный приказ в формате PDF";
                        openFileDialog.Filter = "Файлы PDF (*.pdf)|*.pdf";
                        openFileDialog.RestoreDirectory = true;

                        DialogResult result = openFileDialog.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            string selectedFilePath = openFileDialog.FileName;

                            if (selectedFilePath.Substring(selectedFilePath.Length - 4, 4) == ".pdf")
                            {
                                int number = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Number"].Value);

                                string orderPath = string.Format(@"..\..\docs\Приказ №{0}.pdf", number);
                                File.Copy(selectedFilePath, orderPath, true);

                                FillDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("Выбранный файл не формата PDF.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Файл не выбран.");
                        }
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    this.Hide();
                    new OrderDeleteForm(db, Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value)).ShowDialog();
                    this.Show();
                    FillDataGridView();
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void clearFilterButton_Click(object sender, EventArgs e)
        {
            practiceTypeComboBox.SelectedIndex = -1;
            facultyComboBox.SelectedIndex = -1;
            userComboBox.SelectedIndex = -1;
        }
    }
}
