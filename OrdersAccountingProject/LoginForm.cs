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
    public partial class LoginForm : Form
    {
        private Context db;

        public LoginForm(Context db)
        {
            InitializeComponent();
            this.db = db;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            var user = db.Users
                .FirstOrDefault(u => u.Login == textBox1.Text && u.Password == textBox2.Text);

            LoadingForm.Close();

            if (user != null)
            {
                Program.userId = user.Id;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                "Неверный логин или пароль, попробуйте заново.",
                "Ошибка входа",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button1);
            }
        }
    }
}
