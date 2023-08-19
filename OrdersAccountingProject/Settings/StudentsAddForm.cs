using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace OrdersAccountingProject.Settings
{
    public partial class StudentsAddForm : Form
    {
        private Context db;
        private int course, specialtyId;

        public StudentsAddForm(Context db, int course, int specialtyId)
        {
            InitializeComponent();
            this.db = db;
            this.course = course;
            this.specialtyId = specialtyId;
        }

        private void addStudentsButton_Click(object sender, EventArgs e)
        {
            List<Student> students = new List<Student>();
            try
            {
                foreach (string name in textBox1.Text.Split('\n'))
                {
                    string[] nameParts = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    Student student = new Student
                    {
                        LastName = nameParts[0],
                        FirstName = nameParts[1],
                        MiddleName = nameParts[2],
                        CourseId = course,
                        SpecialtyId = specialtyId
                    };

                    students.Add(student);
                }

                db.Students.AddRange(students);
                db.SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                students.Clear();
                MessageBox.Show(ex.ToString());
            }
        }
    }
}