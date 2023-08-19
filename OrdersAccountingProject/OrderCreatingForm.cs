using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace OrdersAccountingProject
{
    public partial class OrderCreatingForm : Form
    {
        private MainForm form1;
        private StudentsChoosingForm studentChoosingForm;
        public Context db;
        private int educationFormId, practiceTypeId, practiceManagerId, specialtyId, courseId;

        public OrderCreatingForm(MainForm form1)
        {
            LoadingForm.Open();

            InitializeComponent();
            this.form1 = form1;
            db = form1.db;
        }

        private void OrderCreatingForm_Load(object sender, EventArgs e)
        {
            var educationForms = db.EducationForms
                .Select(x => x.Name)
                .ToArray();

            educationFormComboBox.Items.Clear();
            educationFormComboBox.Items.AddRange(educationForms);

            var faculties = db.Faculties
                .Select(f => f.Name)
                .ToArray();

            facultyComboBox.Items.Clear();
            facultyComboBox.Items.AddRange(faculties);

            var practiceTypes = db.PracticeTypes
                .Select(t => t.Type)
                .ToArray();

            practiceTypeComboBox.Items.Clear();
            practiceTypeComboBox.Items.AddRange(practiceTypes);

            if (db.Orders.Count() != 0)
            {
                var orderNumber = db.Orders
                    .Max(x => x.Number);

                numericUpDown1.Value = Convert.ToDecimal(orderNumber + 1);
            }

            LoadingForm.Close();
        }

        private void educationFormComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            educationFormId = db.EducationForms.AsEnumerable()
                .Where(x => x.Name == educationFormComboBox.Text)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        private void facultyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string educationFormName = educationFormComboBox.Text;

            var facultyId = db.Faculties.AsEnumerable()
                .Where(f => f.Name == facultyComboBox.Text)
                .Select(f => f.Id)
                .FirstOrDefault();

            var departments = db.Departments
                .Where(d => d.FacultyId == facultyId)
                .Select(d => d.Name)
                .ToArray();

            departmentComboBox.Items.Clear();
            departmentComboBox.Items.AddRange(departments);
        }

        private void departmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadingForm.Open();

            var educationFormId = db.EducationForms.AsEnumerable()
                .Where(x => x.Name == educationFormComboBox.Text)
                .Select(x => x.Id)
                .FirstOrDefault();

            var departmentId = db.Departments.AsEnumerable()
                .Where(d => d.Name == departmentComboBox.Text)
                .Select(d => d.Id)
                .FirstOrDefault();

            var specialties = db.Specialties
                .Where(s => s.DepartmentId == departmentId && s.EducationFormId == educationFormId)
                .Select(s => s.Code + " " + s.Name)
                .ToArray();

            specialtyComboBox.Items.Clear();
            specialtyComboBox.Items.AddRange(specialties);

            var employees = db.Employees.AsEnumerable()
                .Where(x => x.DepartmentId == departmentId)
                .Select(x => string.Format("{0} {1}.{2}.", x.LastName, x.FirstName[0], x.MiddleName[0]))
                .ToArray();

            practiceManagerComboBox.Items.Clear();
            practiceManagerComboBox.Items.AddRange(employees);

            LoadingForm.Close();
        }

        private void courseComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            courseId = db.Courses.AsEnumerable()
                .Where(c => c.Number == int.Parse(courseComboBox.Text))
                .Select(c => c.Id)
                .FirstOrDefault();
        }

        private void specialtyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            specialtyId = db.Specialties.AsEnumerable()
                .Where(s => s.Code + " " + s.Name == specialtyComboBox.Text && s.EducationFormId == educationFormId)
                .Select(s => s.Id)
                .FirstOrDefault();
        }

        private void practiceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            practiceTypeId = db.PracticeTypes.AsEnumerable()
                .Where(p => p.Type == practiceTypeComboBox.Text)
                .Select(p => p.Id)
                .FirstOrDefault();
        }

        private void practiceManagerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            practiceManagerId = db.Employees.AsEnumerable()
                .Where(x => string.Format("{0} {1}.{2}.", x.LastName, x.FirstName[0], x.MiddleName[0]) == practiceManagerComboBox.Text)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                numericUpDown1.Enabled = true;
                dateTimePicker3.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingForm.Open();

            CreateOrderWithSavingInDb();

            studentChoosingForm.Close();
            studentChoosingForm = null;

            label13.Visible = false;
            label14.Visible = false;

            LoadingForm.Close();
        }

        public void CreateOrderWithSavingInDb()
        {
            string orderTemplatePath = @"..\..\docs\Шаблоны\Шаблон1.docx";
            string orderPath = string.Format(@"..\..\docs\Приказ №{0}.docx", numericUpDown1.Value);
            File.Copy(orderTemplatePath, orderPath, true);

            object filename = Path.GetFullPath(orderPath);
            object nullobject = Type.Missing;

            Word.Application wordApp;

            bool WordWasOpen = false;
            try
            {
                wordApp = (Word.Application) Marshal.GetActiveObject("Word.Application");
                WordWasOpen = true;
            }
            catch
            {
                wordApp = new Word.Application();
            }

            Word.Documents wordDocs = wordApp.Documents;
            Word.Document wordDoc = wordDocs.Open(ref filename);

            WordsReplace(ref wordDoc);

            Word.Table table = wordDoc.Tables[1]; // отчёт начинается с 1, а не с 0
            TableFilling(ref table);

            wordDoc.Save();

            if (checkBox2.Checked)
            {
                try
                {
                    wordDoc.SaveAs2(Path.GetFullPath(orderPath.Substring(0, orderPath.Length - 4) + "pdf"), Word.WdExportFormat.wdExportFormatPDF);
                }
                catch
                {
                    MessageBox.Show(
                        "Пожалуйста, установите Microsoft Word 2010 или новее; или сохраните документ в формате PDF вручную.",
                        "Сохранение в PDF не удалось",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1);
                }
            }

            ((Microsoft.Office.Interop.Word._Document)wordDoc).Close(ref nullobject, ref nullobject, ref nullobject);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wordDoc);

            if (!WordWasOpen)
            {
                ((Microsoft.Office.Interop.Word._Application)wordApp).Quit(ref nullobject, ref nullobject, ref nullobject);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Order order = new Order
            {
                Number = Convert.ToInt32(numericUpDown1.Value),
                Date = dateTimePicker3.Value.ToString("dd.MM.yyyy"),
                PracticeTypeId = practiceTypeId,
                PracticeManagerId = practiceManagerId,
                SpecialtyId = specialtyId,
                CourseId = courseId,
                Path = orderPath,
                UserId = Program.userId
            };

            db.Orders.Add(order);
            db.SaveChanges();

            numericUpDown1.Value += 1;

            if (checkBox3.Checked)
                Process.Start(orderPath);

            if (checkBox4.Checked)
            {
                try
                {
                    Process.Start(orderPath.Substring(0, orderPath.Length - 4) + "pdf");
                }
                catch { }
            }
        }

        public void WordsReplace(ref Word.Document wordDoc)
        {
            var department = db.Departments.AsEnumerable()
                .Where(d => d.Name == departmentComboBox.Text)
                .Select(d => d)
                .FirstOrDefault();

            #region Заполнение дат и номеров

            string day = dateTimePicker3.Value.Day.ToString();
            string month = "";

            wordDoc.Content.Find.Execute(FindText: "<Day>", ReplaceWith: day);
            wordDoc.Content.Find.Execute(FindText: "<Day>", ReplaceWith: day);

            if (dateTimePicker3.Value.Month == 1)
                month = "января";
            else if (dateTimePicker3.Value.Month == 2)
                month = "февраля";
            else if (dateTimePicker3.Value.Month == 3)
                month = "марта";
            else if (dateTimePicker3.Value.Month == 4)
                month = "апреля";
            else if (dateTimePicker3.Value.Month == 5)
                month = "мая";
            else if (dateTimePicker3.Value.Month == 6)
                month = "июня";
            else if (dateTimePicker3.Value.Month == 7)
                month = "июля";
            else if (dateTimePicker3.Value.Month == 8)
                month = "августа";
            else if (dateTimePicker3.Value.Month == 9)
                month = "сентября";
            else if (dateTimePicker3.Value.Month == 10)
                month = "октября";
            else if (dateTimePicker3.Value.Month == 11)
                month = "ноября";
            else if (dateTimePicker3.Value.Month == 12)
                month = "декабря";

            wordDoc.Content.Find.Execute(FindText: "<Month>", ReplaceWith: month);
            wordDoc.Content.Find.Execute(FindText: "<Month>", ReplaceWith: month);

            string number = numericUpDown1.Value.ToString();

            wordDoc.Content.Find.Execute(FindText: "<Number>", ReplaceWith: number);
            wordDoc.Content.Find.Execute(FindText: "<Number>", ReplaceWith: number);

            #endregion

            #region Заполнение п.1 приказа
            string facultyOrColledge;
            string educationForm = educationFormComboBox.Text.Replace("ая", "ой").ToLower();

            if (facultyComboBox.Text == "Социально-гуманитарный" || facultyComboBox.Text == "Инженерно-технологический")
            {
                facultyOrColledge = facultyComboBox.Text
                    .Substring(0, facultyComboBox.Text.Length - 2)
                    .ToLower()
                    + "ого факультета";
            }
            else if (facultyComboBox.Text == "Колледж (СПО)")
            {
                facultyOrColledge = "колледжа";
            }
            else
            {
                facultyOrColledge = "факультета " + facultyComboBox.Text.ToLower();
            }

            wordDoc.Content.Find.Execute(FindText: "<FacultyOrColledgeName>", ReplaceWith: facultyOrColledge);

            wordDoc.Content.Find.Execute(FindText: "<Course>", ReplaceWith: courseComboBox.Text);
            wordDoc.Content.Find.Execute(FindText: "<Direction>", ReplaceWith: specialtyComboBox.Text);
            wordDoc.Content.Find.Execute(FindText: "<EducationForm>", ReplaceWith: educationForm);
            wordDoc.Content.Find.Execute(FindText: "<PracticeType>", ReplaceWith: practiceTypeComboBox.Text);

            string
                startDate = dateTimePicker1.Value.ToString("dd.MM.yyyy"),
                finishDate = dateTimePicker2.Value.ToString("dd.MM.yyyy");

            wordDoc.Content.Find.Execute(FindText: "<StartDate>", ReplaceWith: startDate);
            wordDoc.Content.Find.Execute(FindText: "<FinishDate>", ReplaceWith: finishDate);

            #endregion

            #region Заполнение п.2 приказа

            if (facultyComboBox.Text != "Колледж (СПО)")
            {
                wordDoc.Content.Find.Execute(FindText: "<FacultyOrColledge>", ReplaceWith: "факультета");
                wordDoc.Content.Find.Execute(FindText: "<DeanOrDirector>", ReplaceWith: "декана");
            }
            else
            {
                wordDoc.Content.Find.Execute(FindText: "<FacultyOrColledge>", ReplaceWith: "колледжа");
                wordDoc.Content.Find.Execute(FindText: "<DeanOrDirector>", ReplaceWith: "директора");
            }

            var depultyDeanId = db.DeaneryStatuses
                .Where(d => d.Status == "Заместитель декана по учебной работе")
                .Select(d => d.Id)
                .FirstOrDefault();

            var depultyDean = db.Employees.AsEnumerable()
                .Join(db.Departments, x => x.DepartmentId, d => d.Id, (x, d) => new
                {
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    Position = db.EmployeePositions.Find(x.PositionId).Position.ToLower(),
                    FacultyId = d.FacultyId,
                    DeaneryStatusId = x.DeaneryStatusId
                })
                .Where(x => x.FacultyId != null && x.FacultyId == department.FacultyId && x.DeaneryStatusId != null && x.DeaneryStatusId == depultyDeanId)
                .Select(x => x)
                .FirstOrDefault();

            string depultyDeanPosition = depultyDean.Position;

            if (depultyDeanPosition[depultyDeanPosition.Length - 1] != 'ь')
            {
                depultyDeanPosition += "a";
            }
            else
            {
                depultyDeanPosition = depultyDeanPosition.Substring(0, depultyDeanPosition.Length - 1) + "я";
            }

            string depultyDeanName;

            if (depultyDean.LastName.EndsWith("а"))
            {
                depultyDeanName = depultyDean.LastName.Substring(0, depultyDean.LastName.Length - 1) + "у";
            }
            else
            {
                depultyDeanName = depultyDean.LastName + "а";
            }

            depultyDeanName = string.Format("{0} {1}.{2}",
                depultyDeanName,
                depultyDean.FirstName[0],
                depultyDean.MiddleName[0]);

            wordDoc.Content.Find.Execute(FindText: "<EmpoyeePosition>", ReplaceWith: depultyDeanPosition);
            wordDoc.Content.Find.Execute(FindText: "<EmployeeName>", ReplaceWith: depultyDeanName);

            #endregion

            #region Заполнение п.3 приказа

            var departmentHead = db.Employees.AsEnumerable()
                .Where(d => d.Id == department.HeadId)
                .Select(d => d)
                .FirstOrDefault();

            string departmentHeadName;

            if (departmentHead.LastName.EndsWith("а"))
            {
                departmentHeadName = departmentHead.LastName.Substring(0, departmentHead.LastName.Length - 1) + "у";
            }
            else
            {
                departmentHeadName = departmentHead.LastName + "а";
            }

            departmentHeadName = string.Format("{0} {1}.{2}.",
                departmentHeadName,
                departmentHead.FirstName[0],
                departmentHead.MiddleName[0]);

            wordDoc.Content.Find.Execute(FindText: "<DepartmentHeadName>", ReplaceWith: departmentHeadName);

            string departmentOrColledge;
            if (facultyComboBox.Text != "Колледж (СПО)")
            {
                departmentOrColledge = "кафедры";
            }
            else
            {
                departmentOrColledge = "колледжа";
            }

            wordDoc.Content.Find.Execute(FindText: "<DepartmentOrColledge>", ReplaceWith: departmentOrColledge);

            string[] practiceManagerNames = practiceManagerComboBox.Text
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (practiceManagerNames[0].EndsWith("а"))
            {
                practiceManagerNames[0] = practiceManagerNames[0]
                    .Substring(0, practiceManagerNames[0].Length - 1)
                    + "у";
            }
            else
            {
                practiceManagerNames[0] += "а";
            }

            string practiceManagerName = string.Format("{0} {1}",
                practiceManagerNames[0],
                practiceManagerNames[1]);

            wordDoc.Content.Find.Execute(FindText: "<PracticeManagerName>", ReplaceWith: practiceManagerName);

            #endregion

            #region Заполнение п.5 приказа

            string deanOrDirector, deanOrDirectorName = "";
            if (facultyComboBox.Text != "Колледж (СПО)")
            {
                deanOrDirector = "декана";
            }
            else
            {
                deanOrDirector = "директора";
            }

            var deanId = db.DeaneryStatuses
                .Where(d => d.Status == "Декан")
                .Select(d => d.Id)
                .FirstOrDefault();

            var dean = db.Employees
                .Join(db.Departments, x => x.DepartmentId, d => d.Id, (x, d) => new
                {
                    LastName = x.LastName,
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    PositionId = x.PositionId,
                    FacultyId = d.FacultyId,
                    DeaneryStatusId = x.DeaneryStatusId
                })
                .Where(x => x.FacultyId != null && x.FacultyId == department.FacultyId && x.DeaneryStatusId != null && x.DeaneryStatusId == deanId)
                .Select(x => x)
                .FirstOrDefault();

            if (dean.LastName.EndsWith("а"))
            {
                deanOrDirectorName = dean.LastName.Substring(0, dean.LastName.Length - 1) + "у";
            }
            else
            {
                deanOrDirectorName = dean.LastName + "а";
            }

            deanOrDirectorName = string.Format("{0} {1}.{2}",
                deanOrDirectorName,
                dean.FirstName[0],
                dean.MiddleName[0]);

            wordDoc.Content.Find.Execute(FindText: "<DeanOrDirector>", ReplaceWith: deanOrDirector);
            wordDoc.Content.Find.Execute(FindText: "<FacultyOrColledge>", ReplaceWith: facultyOrColledge);
            wordDoc.Content.Find.Execute(FindText: "<DeanOrDirectorName>", ReplaceWith: deanOrDirectorName);

            #endregion

            #region Заполнение "подписей"

            var branchDirectorName = db.Employees.AsEnumerable()
                .Join(db.EmployeePositions, em => em.PositionId, emp => emp.Id, (em, emp) => new
                {
                    em,
                    emp.Position
                })
                .Where(x => x.Position != null && x.Position.ToLower() == "директор")
                .Select(x => string.Format("{0}.{1}. {2}", x.em.FirstName[0], x.em.MiddleName[0], x.em.LastName))
                .FirstOrDefault();

            wordDoc.Content.Find.Execute(FindText: "<BranchDirectorName>", ReplaceWith: branchDirectorName);

            deanOrDirectorName = string.Format("{0}.{1}. {2}", dean.FirstName[0], dean.MiddleName[0], dean.LastName);

            if (facultyComboBox.Text != "Колледж (СПО)")
            {
                deanOrDirector = "Декан";
            }
            else
            {
                deanOrDirector = "Директор";
            }

            wordDoc.Content.Find.Execute(FindText: "<DeanOrDirector>", ReplaceWith: deanOrDirector);
            wordDoc.Content.Find.Execute(FindText: "<FacultyOrColledge>", ReplaceWith: facultyOrColledge);
            wordDoc.Content.Find.Execute(FindText: "<DeanOrDirectorName>", ReplaceWith: deanOrDirectorName);

            #endregion
        }

        public void TableFilling(ref Word.Table table)
        {
            var groupNumber = db.Specialties
                .Where(x => x.Id == specialtyId)
                .Select(x => x.GroupNumber)
                .FirstOrDefault()
                .ToString();

            if (data[3].Count == 0)
            {
                table.Rows[10].Delete();
                table.Rows[10].Delete();
            }
            else
            {
                for (int i = 0; i < data[3].Count - 1; i++)
                {
                    table.Rows.Add(table.Rows[11]);
                }

                for (int i = 0; i < data[3].Count; i++)
                {
                    Word.Row row = table.Rows[i + 11];

                    row.Cells[1].Range.Text = (i + data[0].Count + data[1].Count + data[2].Count + 1).ToString().Trim();
                    row.Cells[2].Range.Text = data[3][i][0].ToString().Trim();
                    row.Cells[3].Range.Text = groupNumber.Trim();
                    row.Cells[4].Range.Text = data[3][i][1].ToString().Trim();
                    row.Cells[5].Range.Text = data[3][i][2].ToString().Trim();
                }
            }

            if (data[2].Count == 0)
            {
                table.Rows[8].Delete();
                table.Rows[8].Delete();

                if (data[3].Count == 0)
                {
                    table.Rows[7].Delete();
                }
            }
            else
            {
                for (int i = 0; i < data[2].Count - 1; i++)
                {
                    table.Rows.Add(table.Rows[9]);
                }

                for (int i = 0; i < data[2].Count; i++)
                {
                    Word.Row row = table.Rows[i + 9];

                    row.Cells[1].Range.Text = (i + data[0].Count + data[1].Count + 1).ToString().Trim();
                    row.Cells[2].Range.Text = data[2][i][0].ToString().Trim();
                    row.Cells[3].Range.Text = groupNumber.Trim();
                    row.Cells[4].Range.Text = data[2][i][1].ToString().Trim();
                    row.Cells[5].Range.Text = data[2][i][2].ToString().Trim();
                }
            }

            if (data[1].Count == 0)
            {
                table.Rows[5].Delete();
                table.Rows[5].Delete();
            }
            else
            {
                for (int i = 0; i < data[1].Count - 1; i++)
                {
                    table.Rows.Add(table.Rows[6]);
                }

                for (int i = 0; i < data[1].Count; i++)
                {
                    Word.Row row = table.Rows[i + 6];

                    row.Cells[1].Range.Text = (i + data[0].Count + 1).ToString().Trim();
                    row.Cells[2].Range.Text = data[1][i][0].ToString().Trim();
                    row.Cells[3].Range.Text = groupNumber.Trim();
                    row.Cells[4].Range.Text = data[1][i][1].ToString().Trim();
                    row.Cells[5].Range.Text = data[1][i][2].ToString().Trim();
                }
            }

            if (data[0].Count == 0)
            {
                table.Rows[3].Delete();
                table.Rows[3].Delete();

                if (data[1].Count == 0)
                {
                    table.Rows[2].Delete();
                }
            }
            else
            {
                for (int i = 0; i < data[0].Count - 1; i++)
                {
                    table.Rows.Add(table.Rows[4]);
                }

                for (int i = 0; i < data[0].Count; i++)
                {
                    Word.Row row = table.Rows[i + 4];

                    row.Cells[1].Range.Text = (i + 1).ToString().Trim();
                    row.Cells[2].Range.Text = data[0][i][0].ToString().Trim();
                    row.Cells[3].Range.Text = groupNumber.Trim();
                    row.Cells[4].Range.Text = data[0][i][1].ToString().Trim();
                    row.Cells[5].Range.Text = data[0][i][2].ToString().Trim();
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox4.Enabled = true;
            }
            else
            {
                checkBox4.Checked = false;
                checkBox4.Enabled = false;
            }
        }

        public List<List<object>>[] data = new List<List<object>>[4];

        private void StudentsChoosingButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (studentChoosingForm == null)
            {
                studentChoosingForm = new StudentsChoosingForm(this, specialtyId, courseId, practiceManagerComboBox.Text);
            }
            studentChoosingForm.ShowDialog();

            int studentsCount = db.Students
                .Where(s => s.SpecialtyId == specialtyId && s.CourseId == courseId)
                .Count();

            int dataCount = data[0].Count + data[1].Count + data[2].Count + data[3].Count;

            label13.Visible = true;
            label14.Visible = true;

            if (dataCount == 0)
            {
                label13.ForeColor = Color.Red;
                label14.Text = "Студенты не распределены";
            }

            if (dataCount > 0 && dataCount < studentsCount)
            {
                label13.ForeColor = Color.Yellow;
                label14.Text = "Не все студенты распределены";
            }
            else if (dataCount == studentsCount)
            {
                label13.ForeColor = Color.Green;
                label14.Text = "Все студенты распределены";
            }

            this.Show();
        }
    }
}
