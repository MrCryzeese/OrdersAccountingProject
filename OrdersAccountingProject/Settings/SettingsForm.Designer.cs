namespace OrdersAccountingProject
{
    partial class SettingsForm
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
            this.button4 = new System.Windows.Forms.Button();
            this.PracticeTypesViewButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.StudentsViewButton = new System.Windows.Forms.Button();
            this.DepartmentsViewButton = new System.Windows.Forms.Button();
            this.EmployeesViewButton = new System.Windows.Forms.Button();
            this.FacultiesViewButton = new System.Windows.Forms.Button();
            this.EmployeesPositionsButton = new System.Windows.Forms.Button();
            this.SpecialtiesViewButton = new System.Windows.Forms.Button();
            this.DeaneryStatusesButton = new System.Windows.Forms.Button();
            this.EducationFormsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(13, 353);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(402, 35);
            this.button4.TabIndex = 6;
            this.button4.Text = "Быстрое добавление студента";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // PracticeTypesViewButton
            // 
            this.PracticeTypesViewButton.Location = new System.Drawing.Point(13, 194);
            this.PracticeTypesViewButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PracticeTypesViewButton.Name = "PracticeTypesViewButton";
            this.PracticeTypesViewButton.Size = new System.Drawing.Size(300, 35);
            this.PracticeTypesViewButton.TabIndex = 4;
            this.PracticeTypesViewButton.Text = "Типы практики";
            this.PracticeTypesViewButton.UseVisualStyleBackColor = true;
            this.PracticeTypesViewButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(13, 308);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(402, 35);
            this.button2.TabIndex = 5;
            this.button2.Text = "Быстрое добавление сотрудника вуза";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // StudentsViewButton
            // 
            this.StudentsViewButton.Location = new System.Drawing.Point(321, 14);
            this.StudentsViewButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.StudentsViewButton.Name = "StudentsViewButton";
            this.StudentsViewButton.Size = new System.Drawing.Size(300, 35);
            this.StudentsViewButton.TabIndex = 2;
            this.StudentsViewButton.Text = "Студенты";
            this.StudentsViewButton.UseVisualStyleBackColor = true;
            this.StudentsViewButton.Click += new System.EventHandler(this.crudStudentsGroupsButton_Click);
            // 
            // DepartmentsViewButton
            // 
            this.DepartmentsViewButton.Location = new System.Drawing.Point(13, 59);
            this.DepartmentsViewButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DepartmentsViewButton.Name = "DepartmentsViewButton";
            this.DepartmentsViewButton.Size = new System.Drawing.Size(300, 35);
            this.DepartmentsViewButton.TabIndex = 1;
            this.DepartmentsViewButton.Text = "Кафедры";
            this.DepartmentsViewButton.UseVisualStyleBackColor = true;
            this.DepartmentsViewButton.Click += new System.EventHandler(this.crudDepartmentsButton_Click);
            // 
            // EmployeesViewButton
            // 
            this.EmployeesViewButton.Location = new System.Drawing.Point(321, 59);
            this.EmployeesViewButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EmployeesViewButton.Name = "EmployeesViewButton";
            this.EmployeesViewButton.Size = new System.Drawing.Size(300, 35);
            this.EmployeesViewButton.TabIndex = 3;
            this.EmployeesViewButton.Text = "Сотрудники";
            this.EmployeesViewButton.UseVisualStyleBackColor = true;
            this.EmployeesViewButton.Click += new System.EventHandler(this.crudEmployeesButton_Click);
            // 
            // FacultiesViewButton
            // 
            this.FacultiesViewButton.Location = new System.Drawing.Point(13, 14);
            this.FacultiesViewButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FacultiesViewButton.Name = "FacultiesViewButton";
            this.FacultiesViewButton.Size = new System.Drawing.Size(300, 35);
            this.FacultiesViewButton.TabIndex = 7;
            this.FacultiesViewButton.Text = "Факультеты";
            this.FacultiesViewButton.UseVisualStyleBackColor = true;
            this.FacultiesViewButton.Click += new System.EventHandler(this.FacultiesViewButton_Click);
            // 
            // EmployeesPositionsButton
            // 
            this.EmployeesPositionsButton.Location = new System.Drawing.Point(321, 104);
            this.EmployeesPositionsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EmployeesPositionsButton.Name = "EmployeesPositionsButton";
            this.EmployeesPositionsButton.Size = new System.Drawing.Size(300, 35);
            this.EmployeesPositionsButton.TabIndex = 12;
            this.EmployeesPositionsButton.Text = "Должности сотрудников";
            this.EmployeesPositionsButton.UseVisualStyleBackColor = true;
            this.EmployeesPositionsButton.Click += new System.EventHandler(this.EmployeesPositionsButton_Click);
            // 
            // SpecialtiesViewButton
            // 
            this.SpecialtiesViewButton.Location = new System.Drawing.Point(13, 104);
            this.SpecialtiesViewButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SpecialtiesViewButton.Name = "SpecialtiesViewButton";
            this.SpecialtiesViewButton.Size = new System.Drawing.Size(300, 35);
            this.SpecialtiesViewButton.TabIndex = 10;
            this.SpecialtiesViewButton.Text = "Специальности / направления";
            this.SpecialtiesViewButton.UseVisualStyleBackColor = true;
            this.SpecialtiesViewButton.Click += new System.EventHandler(this.SpecialtiesViewButton_Click);
            // 
            // DeaneryStatusesButton
            // 
            this.DeaneryStatusesButton.Location = new System.Drawing.Point(321, 149);
            this.DeaneryStatusesButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DeaneryStatusesButton.Name = "DeaneryStatusesButton";
            this.DeaneryStatusesButton.Size = new System.Drawing.Size(300, 35);
            this.DeaneryStatusesButton.TabIndex = 8;
            this.DeaneryStatusesButton.Text = "Статусы в деканате";
            this.DeaneryStatusesButton.UseVisualStyleBackColor = true;
            this.DeaneryStatusesButton.Click += new System.EventHandler(this.DeaneryStatusesButton_Click);
            // 
            // EducationFormsButton
            // 
            this.EducationFormsButton.Location = new System.Drawing.Point(13, 149);
            this.EducationFormsButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.EducationFormsButton.Name = "EducationFormsButton";
            this.EducationFormsButton.Size = new System.Drawing.Size(300, 35);
            this.EducationFormsButton.TabIndex = 9;
            this.EducationFormsButton.Text = "Формы обучения";
            this.EducationFormsButton.UseVisualStyleBackColor = true;
            this.EducationFormsButton.Click += new System.EventHandler(this.EducationFormsButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 402);
            this.Controls.Add(this.EmployeesPositionsButton);
            this.Controls.Add(this.SpecialtiesViewButton);
            this.Controls.Add(this.DeaneryStatusesButton);
            this.Controls.Add(this.EducationFormsButton);
            this.Controls.Add(this.FacultiesViewButton);
            this.Controls.Add(this.EmployeesViewButton);
            this.Controls.Add(this.DepartmentsViewButton);
            this.Controls.Add(this.StudentsViewButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PracticeTypesViewButton);
            this.Controls.Add(this.button4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button PracticeTypesViewButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button StudentsViewButton;
        private System.Windows.Forms.Button DepartmentsViewButton;
        private System.Windows.Forms.Button EmployeesViewButton;
        private System.Windows.Forms.Button FacultiesViewButton;
        private System.Windows.Forms.Button EmployeesPositionsButton;
        private System.Windows.Forms.Button SpecialtiesViewButton;
        private System.Windows.Forms.Button DeaneryStatusesButton;
        private System.Windows.Forms.Button EducationFormsButton;
    }
}