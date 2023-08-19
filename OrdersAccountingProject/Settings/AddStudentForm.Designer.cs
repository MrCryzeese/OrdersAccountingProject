namespace OrdersAccountingProject
{
    partial class AddingStudentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LastNameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AddStudentButton = new System.Windows.Forms.Button();
            this.CourseBox = new System.Windows.Forms.ComboBox();
            this.EducationFormBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SpecialtyBox = new System.Windows.Forms.ComboBox();
            this.FacultyBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.FirstNameBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MiddleNameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // LastNameBox
            // 
            this.LastNameBox.Location = new System.Drawing.Point(284, 18);
            this.LastNameBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LastNameBox.Name = "LastNameBox";
            this.LastNameBox.Size = new System.Drawing.Size(572, 26);
            this.LastNameBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Фамилия:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 141);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Курс:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 289);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Направление / специальность:";
            // 
            // AddStudentButton
            // 
            this.AddStudentButton.Location = new System.Drawing.Point(18, 348);
            this.AddStudentButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AddStudentButton.Name = "AddStudentButton";
            this.AddStudentButton.Size = new System.Drawing.Size(840, 35);
            this.AddStudentButton.TabIndex = 6;
            this.AddStudentButton.Text = "Добавить студента";
            this.AddStudentButton.UseVisualStyleBackColor = true;
            this.AddStudentButton.Click += new System.EventHandler(this.AddStudentButton_Click);
            // 
            // CourseBox
            // 
            this.CourseBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CourseBox.FormattingEnabled = true;
            this.CourseBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.CourseBox.Location = new System.Drawing.Point(284, 138);
            this.CourseBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CourseBox.Name = "CourseBox";
            this.CourseBox.Size = new System.Drawing.Size(572, 28);
            this.CourseBox.TabIndex = 7;
            // 
            // EducationFormBox
            // 
            this.EducationFormBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EducationFormBox.FormattingEnabled = true;
            this.EducationFormBox.Location = new System.Drawing.Point(284, 188);
            this.EducationFormBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EducationFormBox.Name = "EducationFormBox";
            this.EducationFormBox.Size = new System.Drawing.Size(572, 28);
            this.EducationFormBox.TabIndex = 14;
            this.EducationFormBox.SelectedIndexChanged += new System.EventHandler(this.EducationFormBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 190);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Форма обучения:";
            // 
            // SpecialtyBox
            // 
            this.SpecialtyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpecialtyBox.FormattingEnabled = true;
            this.SpecialtyBox.Location = new System.Drawing.Point(284, 286);
            this.SpecialtyBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SpecialtyBox.Name = "SpecialtyBox";
            this.SpecialtyBox.Size = new System.Drawing.Size(572, 28);
            this.SpecialtyBox.TabIndex = 15;
            // 
            // FacultyBox
            // 
            this.FacultyBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FacultyBox.FormattingEnabled = true;
            this.FacultyBox.Location = new System.Drawing.Point(284, 237);
            this.FacultyBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FacultyBox.Name = "FacultyBox";
            this.FacultyBox.Size = new System.Drawing.Size(572, 28);
            this.FacultyBox.TabIndex = 17;
            this.FacultyBox.SelectedIndexChanged += new System.EventHandler(this.FacultyBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 240);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Факультет / колледж:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 61);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Имя:";
            // 
            // FirstNameBox
            // 
            this.FirstNameBox.Location = new System.Drawing.Point(284, 58);
            this.FirstNameBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FirstNameBox.Name = "FirstNameBox";
            this.FirstNameBox.Size = new System.Drawing.Size(572, 26);
            this.FirstNameBox.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 101);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 20);
            this.label7.TabIndex = 21;
            this.label7.Text = "Отчество:";
            // 
            // MiddleNameBox
            // 
            this.MiddleNameBox.Location = new System.Drawing.Point(284, 98);
            this.MiddleNameBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MiddleNameBox.Name = "MiddleNameBox";
            this.MiddleNameBox.Size = new System.Drawing.Size(572, 26);
            this.MiddleNameBox.TabIndex = 20;
            // 
            // AddStudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 402);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MiddleNameBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FirstNameBox);
            this.Controls.Add(this.FacultyBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SpecialtyBox);
            this.Controls.Add(this.EducationFormBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CourseBox);
            this.Controls.Add(this.AddStudentButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LastNameBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddStudentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddStudentForm";
            this.Load += new System.EventHandler(this.AddStudentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LastNameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AddStudentButton;
        private System.Windows.Forms.ComboBox CourseBox;
        private System.Windows.Forms.ComboBox EducationFormBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox SpecialtyBox;
        private System.Windows.Forms.ComboBox FacultyBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox FirstNameBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox MiddleNameBox;
    }
}