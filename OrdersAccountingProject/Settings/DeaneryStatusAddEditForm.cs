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
    public partial class DeaneryStatusAddEditForm : Form
    {
        Context db;
        private int? id;

        public DeaneryStatusAddEditForm(Context db, int? id = null)
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
                textBox1.Text = db.DeaneryStatuses.Find(id).Status;
                button1.Text = "Изменить статус";
            }

            LoadingForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            if (id.HasValue)
            {
                db.DeaneryStatuses.Find(id).Status = textBox1.Text;
            }
            else
            {
                db.DeaneryStatuses.Add(new DeaneryStatus { Status = textBox1.Text });
            }

            db.SaveChanges();

            LoadingForm.Close();

            this.Close();
        }
    }
}
