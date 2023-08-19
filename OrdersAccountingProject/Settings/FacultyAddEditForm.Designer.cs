namespace OrdersAccountingProject.Settings
{
    partial class FacultyAddEditForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.FacultyTextBox = new System.Windows.Forms.TextBox();
            this.FacultyAddEditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 216);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FacultyTextBox
            // 
            this.FacultyTextBox.Location = new System.Drawing.Point(13, 230);
            this.FacultyTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FacultyTextBox.Multiline = true;
            this.FacultyTextBox.Name = "FacultyTextBox";
            this.FacultyTextBox.Size = new System.Drawing.Size(400, 113);
            this.FacultyTextBox.TabIndex = 1;
            // 
            // FacultyAddEditButton
            // 
            this.FacultyAddEditButton.Location = new System.Drawing.Point(13, 353);
            this.FacultyAddEditButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FacultyAddEditButton.Name = "FacultyAddEditButton";
            this.FacultyAddEditButton.Size = new System.Drawing.Size(400, 35);
            this.FacultyAddEditButton.TabIndex = 2;
            this.FacultyAddEditButton.Text = "Добавить";
            this.FacultyAddEditButton.UseVisualStyleBackColor = true;
            this.FacultyAddEditButton.Click += new System.EventHandler(this.FacultyAddEditButton_Click);
            // 
            // FacultyAddEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 402);
            this.Controls.Add(this.FacultyAddEditButton);
            this.Controls.Add(this.FacultyTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FacultyAddEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FacultyAddEditForm";
            this.Load += new System.EventHandler(this.FacultyAddEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FacultyTextBox;
        private System.Windows.Forms.Button FacultyAddEditButton;
    }
}