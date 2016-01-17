namespace Employees.Views
{
    partial class CertificationsOfEmployeeItemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CertificationsOfEmployeeItemForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxCertification = new System.Windows.Forms.ComboBox();
            this.labelCertification = new System.Windows.Forms.Label();
            this.comboBoxEmployee = new System.Windows.Forms.ComboBox();
            this.labelEmployee = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddCertification = new System.Windows.Forms.Button();
            this.buttonAddEmployees = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxCertification);
            this.groupBox1.Controls.Add(this.labelCertification);
            this.groupBox1.Controls.Add(this.comboBoxEmployee);
            this.groupBox1.Controls.Add(this.labelEmployee);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(25, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 240);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // comboBoxCertification
            // 
            this.comboBoxCertification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCertification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCertification.FormattingEnabled = true;
            this.comboBoxCertification.Location = new System.Drawing.Point(120, 123);
            this.comboBoxCertification.Name = "comboBoxCertification";
            this.comboBoxCertification.Size = new System.Drawing.Size(339, 24);
            this.comboBoxCertification.TabIndex = 5;
            this.comboBoxCertification.SelectedIndexChanged += new System.EventHandler(this.CertificationSelectedHandler);
            // 
            // labelCertification
            // 
            this.labelCertification.AutoSize = true;
            this.labelCertification.Location = new System.Drawing.Point(15, 123);
            this.labelCertification.Name = "labelCertification";
            this.labelCertification.Size = new System.Drawing.Size(94, 16);
            this.labelCertification.TabIndex = 4;
            this.labelCertification.Text = "Certification:";
            // 
            // comboBoxEmployee
            // 
            this.comboBoxEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEmployee.FormattingEnabled = true;
            this.comboBoxEmployee.Location = new System.Drawing.Point(120, 27);
            this.comboBoxEmployee.Name = "comboBoxEmployee";
            this.comboBoxEmployee.Size = new System.Drawing.Size(339, 24);
            this.comboBoxEmployee.TabIndex = 3;
            this.comboBoxEmployee.SelectedIndexChanged += new System.EventHandler(this.EmployeeSelectedHandler);
            // 
            // labelEmployee
            // 
            this.labelEmployee.AutoSize = true;
            this.labelEmployee.Location = new System.Drawing.Point(33, 30);
            this.labelEmployee.Name = "labelEmployee";
            this.labelEmployee.Size = new System.Drawing.Size(76, 16);
            this.labelEmployee.TabIndex = 2;
            this.labelEmployee.Text = "Employee:";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOk.Location = new System.Drawing.Point(393, 266);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(110, 40);
            this.buttonOk.TabIndex = 8;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(531, 266);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(110, 40);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonAddCertification
            // 
            this.buttonAddCertification.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddCertification.Location = new System.Drawing.Point(531, 143);
            this.buttonAddCertification.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddCertification.Name = "buttonAddCertification";
            this.buttonAddCertification.Size = new System.Drawing.Size(110, 40);
            this.buttonAddCertification.TabIndex = 9;
            this.buttonAddCertification.Text = "Certifications";
            this.buttonAddCertification.UseVisualStyleBackColor = true;
            this.buttonAddCertification.Click += new System.EventHandler(this.AddCertificationHandler);
            // 
            // buttonAddEmployees
            // 
            this.buttonAddEmployees.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddEmployees.Location = new System.Drawing.Point(531, 47);
            this.buttonAddEmployees.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddEmployees.Name = "buttonAddEmployees";
            this.buttonAddEmployees.Size = new System.Drawing.Size(110, 40);
            this.buttonAddEmployees.TabIndex = 10;
            this.buttonAddEmployees.Text = "Employees";
            this.buttonAddEmployees.UseVisualStyleBackColor = true;
            this.buttonAddEmployees.Click += new System.EventHandler(this.AddEmployeeHandler);
            // 
            // CertificationsOfEmployeeItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 331);
            this.Controls.Add(this.buttonAddEmployees);
            this.Controls.Add(this.buttonAddCertification);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CertificationsOfEmployeeItemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.LoadedHandler);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxCertification;
        private System.Windows.Forms.Label labelCertification;
        private System.Windows.Forms.ComboBox comboBoxEmployee;
        private System.Windows.Forms.Label labelEmployee;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAddCertification;
        private System.Windows.Forms.Button buttonAddEmployees;
    }
}