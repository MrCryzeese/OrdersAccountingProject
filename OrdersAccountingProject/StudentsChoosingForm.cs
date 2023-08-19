using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrdersAccountingProject
{
    public partial class StudentsChoosingForm : Form
    {
        private OrderCreatingForm form;
        private Context db;
        private int specialtyId, courseId, groupNumber, dataIndex;
        private string practiceManagerName;
        private List<object> selectedItems;

        public StudentsChoosingForm(OrderCreatingForm form, int specialtyId, int courseId, string practiceManagerName)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.form = form;
            this.db = form.db;
            this.specialtyId = specialtyId;
            this.courseId = courseId;
            this.practiceManagerName = practiceManagerName;

            var students = db.Students.AsEnumerable()
                .Where(s => s.SpecialtyId == specialtyId && s.CourseId == courseId)
                .OrderBy(s => s.LastName)
                .Select(s => string.Format("{0} {1} {2}", s.LastName, s.FirstName, s.MiddleName))
                .ToArray();

            listBox1.Items.Clear();
            listBox1.Items.AddRange(students);

            groupNumber = db.Specialties
                .Where(s => s.Id == specialtyId)
                .Select(s => s.GroupNumber)
                .FirstOrDefault();

            form.data[0] = new List<List<object>>();
            form.data[1] = new List<List<object>>();
            form.data[2] = new List<List<object>>();
            form.data[3] = new List<List<object>>();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dataIndex = 0;

            LoadingForm.Close();
        }

        private void StudentsChoosingForm_Load(object sender, EventArgs e)
        {

        }

        private void ListBoxSort(ref ListBox listBox)
        {
            List<object> tempList = new List<object>();

            foreach (var item in listBox.Items)
            {
                tempList.Add(item);
            }

            tempList.Sort();

            listBox.Items.Clear();
            listBox.Items.AddRange(tempList.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedItems = new List<object>();

            foreach (var item in listBox1.SelectedItems)
            {
                selectedItems.Add(item);

                if (dataIndex == 0)
                {
                    dataGridView1.Rows.Add(item, "Бирский филиал УУНиТ", practiceManagerName);
                }
                else
                {
                    dataGridView1.Rows.Add(item, "", "");
                }
            }

            dataGridView1.Sort(dataGridView1.Columns["StudentName"], ListSortDirection.Ascending);

            foreach (var item in selectedItems)
            {
                listBox1.Items.Remove(item);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedItems = new List<object>();

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                listBox1.Items.Add(row.Cells["StudentName"].Value);
                dataGridView1.Rows.Remove(row);
            }

            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                if (cell.OwningColumn.Name == "StudentName")
                {
                    listBox1.Items.Add(cell.Value);
                    DataGridViewRow row = cell.OwningRow;
                    dataGridView1.Rows.Remove(row);
                }
            }

            ListBoxSort(ref listBox1);
        }

        private void SaveData()
        {
            form.data[dataIndex].Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                List<object> rowData = new List<object>();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    rowData.Add(cell.Value);
                }

                form.data[dataIndex].Add(rowData);
            }

            dataIndex = comboBox1.SelectedIndex * 2 + comboBox2.SelectedIndex;

            if (dataIndex == -1)
                dataIndex = 0;

            dataGridView1.Rows.Clear();

            foreach (List<object> rowData in form.data[dataIndex])
            {
                dataGridView1.Rows.Add(rowData.ToArray());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveData();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Hide();
        }

        private void StudentsChoosingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }
    }
}
