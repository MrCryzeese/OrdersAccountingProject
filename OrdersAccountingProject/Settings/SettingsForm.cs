using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OrdersAccountingProject.Settings;

namespace OrdersAccountingProject
{
    public partial class SettingsForm : Form
    {
        MainForm form1;

        public SettingsForm(MainForm form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void FacultiesViewButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FacultiesViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            new AddingStudentForm(form1).ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new PracticeTypesViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EmployeeAddEditForm(form1.db).ShowDialog();
            this.Show();
        }

        private void crudDepartmentsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DepartmentsViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void crudStudentsGroupsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new StudentsViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void crudEmployeesButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EmployeesViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void SpecialtiesViewButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SpecialtiesViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void EmployeesPositionsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EmployeePositionsViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void EducationFormsButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new EducationFormsViewForm(form1.db).ShowDialog();
            this.Show();
        }

        private void DeaneryStatusesButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DeaneryStatusesViewForm(form1.db).ShowDialog();
            this.Show();
        }
    }
}
