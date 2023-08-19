using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

using System.Data.Entity;
using System.Data.SQLite;

namespace OrdersAccountingProject
{
    public partial class MainForm : Form
    {
        public Context db;

        public MainForm(Context db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            textBox1.AppendText("Список студентов:" + Environment.NewLine);
            foreach (Student s in db.Students)
            {
                var groupNumber = db.Specialties
                    .Where(x => x.Id == s.SpecialtyId)
                    .Select(x => x.GroupNumber)
                    .FirstOrDefault();
                
                textBox1.AppendText(string.Format("{0}. {1} - {2}{3}", s.Id, s.Name, s.Course, groupNumber) + Environment.NewLine);
            }*/
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            new OrderCreatingForm(this).ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SettingsForm(this).ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            new OrdersJournal(db).ShowDialog();
            this.Show();
        }
    }
}
