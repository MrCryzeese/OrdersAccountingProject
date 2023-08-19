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
    public partial class FacultyAddEditForm : Form
    {
        private Context db;
        private int? id;

        public FacultyAddEditForm(Context db, int? id = null)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
            this.id = id;
        }

        private void FacultyAddEditForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                var faculty = db.Faculties.Find(id);

                label1.Text = "Введите новое название для ";

                if (faculty.Name == "Социально-гуманитарный" || faculty.Name == "Инженерно-технологический")
                {
                    label1.Text += faculty.Name
                        .Substring(0, faculty.Name.Length - 2)
                        .ToLower()
                        + "ого факультета:";
                }
                else if (faculty.Name == "Колледж (СПО)")
                {
                    label1.Text += "колледжа:";
                }
                else
                {
                    label1.Text += "факультета " + faculty.Name.ToLower() + ":";
                }

                FacultyAddEditButton.Text = "Изменить";
            }

            LoadingForm.Close();
        }

        private void FacultyAddEditButton_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            if (id.HasValue)
            {
                var faculty = db.Faculties.Find(id);

                faculty.Name = FacultyTextBox.Text;
            }
            else
            {
                Faculty faculty = new Faculty
                {
                    Name = FacultyTextBox.Text
                };

                db.Faculties.Add(faculty);
            }

            db.SaveChanges();
            this.Close();

            LoadingForm.Close();
        }
    }
}
