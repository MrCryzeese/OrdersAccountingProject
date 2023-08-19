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
    public partial class StudentAddEditForm : Form
    {
        private Context db;
        private int id, courseId, specialtyId;

        public StudentAddEditForm(Context db, int courseId, int specialtyId)
        {
            InitializeComponent();
            this.db = db;
            this.courseId = courseId;
            this.specialtyId = specialtyId;
        }

        public StudentAddEditForm(Context db, int id)
        {
            InitializeComponent();
            this.db = db;
            this.id = id;

            var student = db.Students.Find(id);

            lastNameTextBox.Text = student.LastName;
            firstNameTextBox.Text = student.FirstName;
            middleNameTextBox.Text = student.MiddleName;

            addButton.Text = "Изменить";
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (addButton.Text == "Добавить")
            {
                Student student = new Student 
                { 
                    LastName = lastNameTextBox.Text,
                    FirstName = firstNameTextBox.Text,
                    MiddleName = middleNameTextBox.Text,
                    CourseId = courseId, 
                    SpecialtyId = specialtyId
                };
                db.Students.Add(student);
            }
            else
            {
                var student = db.Students.Find(id);

                student.LastName = lastNameTextBox.Text;
                student.FirstName = firstNameTextBox.Text;
                student.MiddleName = middleNameTextBox.Text;
            }

            db.SaveChanges();
            this.Close();
        }
    }
}
