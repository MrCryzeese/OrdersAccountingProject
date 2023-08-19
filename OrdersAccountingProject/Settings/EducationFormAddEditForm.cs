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
    public partial class EducationFormAddEditForm : Form
    {
        Context db;
        private int? id;

        public EducationFormAddEditForm(Context db, int? id = null)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
            this.id = id;
        }

        private void PracticeTypeAddEditForm_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                textBox1.Text = db.EducationForms.Find(id).Name;
                button1.Text = "Изменить форму обучения";
            }

            LoadingForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            if (id.HasValue)
            {
                db.EducationForms.Find(id).Name = textBox1.Text;
            }
            else
            {
                db.EducationForms.Add(new EducationForm { Name = textBox1.Text });
            }

            db.SaveChanges();

            LoadingForm.Close();

            this.Close();
        }
    }
}
