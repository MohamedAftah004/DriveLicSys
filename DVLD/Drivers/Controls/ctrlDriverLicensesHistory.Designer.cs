namespace DVLD.Drivers.Controls
{
    partial class ctrlDriverLicensesHistory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvLocalList = new System.Windows.Forms.DataGridView();
            this.tcLicensesHistory = new System.Windows.Forms.TabControl();
            this.tbLocal = new System.Windows.Forms.TabPage();
            this.lblLocalRecordCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLocalLicenseHistory = new System.Windows.Forms.Label();
            this.tbInternational = new System.Windows.Forms.TabPage();
            this.lblInternationalRecordCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvInternationalList = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalList)).BeginInit();
            this.tcLicensesHistory.SuspendLayout();
            this.tbLocal.SuspendLayout();
            this.tbInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLocalList
            // 
            this.dgvLocalList.AllowUserToAddRows = false;
            this.dgvLocalList.AllowUserToDeleteRows = false;
            this.dgvLocalList.AllowUserToOrderColumns = true;
            this.dgvLocalList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvLocalList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalList.Location = new System.Drawing.Point(17, 58);
            this.dgvLocalList.Name = "dgvLocalList";
            this.dgvLocalList.ReadOnly = true;
            this.dgvLocalList.Size = new System.Drawing.Size(1080, 180);
            this.dgvLocalList.TabIndex = 0;
            // 
            // tcLicensesHistory
            // 
            this.tcLicensesHistory.Controls.Add(this.tbLocal);
            this.tcLicensesHistory.Controls.Add(this.tbInternational);
            this.tcLicensesHistory.Font = new System.Drawing.Font("Arial", 12.25F);
            this.tcLicensesHistory.Location = new System.Drawing.Point(13, 28);
            this.tcLicensesHistory.Name = "tcLicensesHistory";
            this.tcLicensesHistory.SelectedIndex = 0;
            this.tcLicensesHistory.Size = new System.Drawing.Size(1123, 311);
            this.tcLicensesHistory.TabIndex = 1;
            this.tcLicensesHistory.SelectedIndexChanged += new System.EventHandler(this.tcLicensesHistory_SelectedIndexChanged);
            // 
            // tbLocal
            // 
            this.tbLocal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbLocal.Controls.Add(this.lblLocalRecordCount);
            this.tbLocal.Controls.Add(this.label2);
            this.tbLocal.Controls.Add(this.lblLocalLicenseHistory);
            this.tbLocal.Controls.Add(this.dgvLocalList);
            this.tbLocal.Font = new System.Drawing.Font("Arial", 12.25F);
            this.tbLocal.Location = new System.Drawing.Point(4, 27);
            this.tbLocal.Name = "tbLocal";
            this.tbLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tbLocal.Size = new System.Drawing.Size(1115, 280);
            this.tbLocal.TabIndex = 0;
            this.tbLocal.Text = "Local";
            // 
            // lblLocalRecordCount
            // 
            this.lblLocalRecordCount.AutoSize = true;
            this.lblLocalRecordCount.Font = new System.Drawing.Font("Arial", 12.25F);
            this.lblLocalRecordCount.Location = new System.Drawing.Point(114, 255);
            this.lblLocalRecordCount.Name = "lblLocalRecordCount";
            this.lblLocalRecordCount.Size = new System.Drawing.Size(27, 19);
            this.lblLocalRecordCount.TabIndex = 136;
            this.lblLocalRecordCount.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 19);
            this.label2.TabIndex = 135;
            this.label2.Text = "# Records:";
            // 
            // lblLocalLicenseHistory
            // 
            this.lblLocalLicenseHistory.AutoSize = true;
            this.lblLocalLicenseHistory.Font = new System.Drawing.Font("Arial", 12.25F, System.Drawing.FontStyle.Bold);
            this.lblLocalLicenseHistory.Location = new System.Drawing.Point(15, 33);
            this.lblLocalLicenseHistory.Name = "lblLocalLicenseHistory";
            this.lblLocalLicenseHistory.Size = new System.Drawing.Size(189, 19);
            this.lblLocalLicenseHistory.TabIndex = 1;
            this.lblLocalLicenseHistory.Text = "Local License History: ";
            // 
            // tbInternational
            // 
            this.tbInternational.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbInternational.Controls.Add(this.lblInternationalRecordCount);
            this.tbInternational.Controls.Add(this.label3);
            this.tbInternational.Controls.Add(this.label4);
            this.tbInternational.Controls.Add(this.dgvInternationalList);
            this.tbInternational.Font = new System.Drawing.Font("Arial", 8.25F);
            this.tbInternational.Location = new System.Drawing.Point(4, 27);
            this.tbInternational.Name = "tbInternational";
            this.tbInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tbInternational.Size = new System.Drawing.Size(1115, 280);
            this.tbInternational.TabIndex = 1;
            this.tbInternational.Text = "International";
            this.tbInternational.Click += new System.EventHandler(this.tbInternational_Click);
            // 
            // lblInternationalRecordCount
            // 
            this.lblInternationalRecordCount.AutoSize = true;
            this.lblInternationalRecordCount.Font = new System.Drawing.Font("Arial", 12.25F);
            this.lblInternationalRecordCount.Location = new System.Drawing.Point(114, 253);
            this.lblInternationalRecordCount.Name = "lblInternationalRecordCount";
            this.lblInternationalRecordCount.Size = new System.Drawing.Size(27, 19);
            this.lblInternationalRecordCount.TabIndex = 140;
            this.lblInternationalRecordCount.Text = "??";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 19);
            this.label3.TabIndex = 139;
            this.label3.Text = "# Records:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(15, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(245, 19);
            this.label4.TabIndex = 138;
            this.label4.Text = "International License History: ";
            // 
            // dgvInternationalList
            // 
            this.dgvInternationalList.AllowUserToAddRows = false;
            this.dgvInternationalList.AllowUserToDeleteRows = false;
            this.dgvInternationalList.AllowUserToOrderColumns = true;
            this.dgvInternationalList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvInternationalList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalList.Location = new System.Drawing.Point(17, 56);
            this.dgvInternationalList.Name = "dgvInternationalList";
            this.dgvInternationalList.ReadOnly = true;
            this.dgvInternationalList.Size = new System.Drawing.Size(1080, 180);
            this.dgvInternationalList.TabIndex = 137;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tcLicensesHistory);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 11.6F);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1144, 348);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Driver Licenses";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // ctrlDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.groupBox1);
            this.Name = "ctrlDriverLicensesHistory";
            this.Size = new System.Drawing.Size(1153, 371);
            this.Load += new System.EventHandler(this.ctrlDriverLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalList)).EndInit();
            this.tcLicensesHistory.ResumeLayout(false);
            this.tbLocal.ResumeLayout(false);
            this.tbLocal.PerformLayout();
            this.tbInternational.ResumeLayout(false);
            this.tbInternational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalList;
        private System.Windows.Forms.TabControl tcLicensesHistory;
        private System.Windows.Forms.TabPage tbLocal;
        private System.Windows.Forms.Label lblLocalLicenseHistory;
        private System.Windows.Forms.TabPage tbInternational;
        private System.Windows.Forms.Label lblLocalRecordCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblInternationalRecordCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvInternationalList;
    }
}
