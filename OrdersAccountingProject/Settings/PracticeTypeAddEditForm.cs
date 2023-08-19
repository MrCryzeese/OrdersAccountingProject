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
    public partial class PracticeTypeAddEditForm : Form
    {
        Context db;
        private int? id;

        public PracticeTypeAddEditForm(Context db, int? id = null)
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
                textBox1.Text = db.PracticeTypes.Find(id).Type;
                button1.Text = "Изменить тип практики";
            }

            LoadingForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            if (id.HasValue)
            {
                db.PracticeTypes.Find(id).Type = textBox1.Text;
            }
            else
            {
                db.PracticeTypes.Add(new PracticeType { Type = textBox1.Text });
            }

            db.SaveChanges();

            LoadingForm.Close();

            this.Close();
        }
    }
}
