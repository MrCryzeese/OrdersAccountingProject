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
    public partial class DepartmentHeadUpdateForm : Form
    {
        private Context db;
        private int id;
        private string departmentName;

        public DepartmentHeadUpdateForm(Context db, int id, string departmentName)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
            this.id = id;
            this.departmentName = departmentName;
        }

        private void UpdateDepartmentHeadForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Выберите заведующего для кафедры " + departmentName.ToLower();

            var employees = db.Employees.AsEnumerable()
                .Where(x => x.DepartmentId == id)
                .Select(x => string.Format("{0} {1} {2}", x.LastName, x.FirstName, x.MiddleName))
                .ToArray();

            employeeComboBox.Items.Clear();
            employeeComboBox.Items.AddRange(employees);

            LoadingForm.Close();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            string[] name = employeeComboBox.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var employeeId = db.Employees.AsEnumerable()
                .Where(x => x.LastName == name[0] && x.FirstName == name[1] && x.MiddleName == name[2])
                .Select(x => x.Id)
                .FirstOrDefault();

            db.Departments.Find(id).HeadId = employeeId;
            db.SaveChanges();

            LoadingForm.Close();
            this.Close();
        }
    }
}
