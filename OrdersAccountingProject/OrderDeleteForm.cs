using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OrdersAccountingProject
{
    public partial class OrderDeleteForm : Form
    {
        private Context db;
        private int id;
        private string wordPath, pdfPath;

        public OrderDeleteForm(Context db, int id)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.db = db;
            this.id = id;
        }

        private void OrderDeleteForm_Load(object sender, EventArgs e)
        {
            label1.Text = "Приказ №" + db.Orders.Find(id).Number;

            wordPath = db.Orders.Find(id).Path;
            pdfPath = wordPath.Substring(0, wordPath.Length - 4) + "pdf";

            if (!File.Exists(pdfPath))
            {
                button1.Enabled = false;
            }

            LoadingForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete(pdfPath);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(wordPath);

                if (button1.Enabled)
                {
                    File.Delete(pdfPath);
                }

                db.Orders.Remove(db.Orders.Find(id));
                db.SaveChanges();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
