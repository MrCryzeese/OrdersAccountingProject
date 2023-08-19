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
    public partial class LoadingForm : Form
    {
        private static LoadingForm loadingForm;

        private LoadingForm()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            
        }

        public static void Open()
        {
            if (loadingForm == null)
            {
                loadingForm = new LoadingForm();
            }

            loadingForm.Show();
            loadingForm.Refresh();
        }

        public new static void Close()
        {
            loadingForm.Hide();
        }
    }
}
