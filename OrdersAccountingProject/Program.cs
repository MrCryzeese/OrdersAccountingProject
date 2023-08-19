using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OrdersAccountingProject
{
    static class Program
    {
        public static int? userId = null;

        [STAThread]
        static void Main()
        {
            Context db = new Context();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(db));

            if (userId != null && db.Users.Find(userId).Role == "admin")
                Application.Run(new MainForm(db));

            db.Dispose();
        }
    }
}
