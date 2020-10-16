namespace GS_STB
{
    partial class WorkForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GB_UserData = new System.Windows.Forms.GroupBox();
            this.BT_LogOut = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.TB_RFIDIn = new System.Windows.Forms.TextBox();
            this.GB_Work = new System.Windows.Forms.GroupBox();
            this.Controllabel = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.SerialTextBox = new System.Windows.Forms.TextBox();
            this.DG_UpLog = new System.Windows.Forms.DataGridView();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.CurrrentDateLabel = new System.Windows.Forms.Label();
            this.CurrrentTimeLabel = new System.Windows.Forms.Label();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BT_OpenSettings = new System.Windows.Forms.Button();
            this.BT_CloseApp = new System.Windows.Forms.Button();
            this.Times = new System.Windows.Forms.Timer(this.components);
            this.GB_UserData.SuspendLayout();
            this.GB_Work.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_UpLog)).BeginInit();
            this.GroupBox5.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_UserData
            // 
            this.GB_UserData.BackColor = System.Drawing.Color.Tan;
            this.GB_UserData.Controls.Add(this.BT_LogOut);
            this.GB_UserData.Controls.Add(this.Label13);
            this.GB_UserData.Controls.Add(this.TB_RFIDIn);
            this.GB_UserData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GB_UserData.Location = new System.Drawing.Point(1290, 611);
            this.GB_UserData.Name = "GB_UserData";
            this.GB_UserData.Size = new System.Drawing.Size(429, 177);
            this.GB_UserData.TabIndex = 30;
            this.GB_UserData.TabStop = false;
            this.GB_UserData.Text = "Вход пользователя";
            this.GB_UserData.Visible = false;
            // 
            // BT_LogOut
            // 
            this.BT_LogOut.BackColor = System.Drawing.Color.Transparent;
            this.BT_LogOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BT_LogOut.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.BT_LogOut.FlatAppearance.BorderSize = 0;
            this.BT_LogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_LogOut.ForeColor = System.Drawing.Color.Transparent;
            this.BT_LogOut.Location = new System.Drawing.Point(371, 83);
            this.BT_LogOut.Name = "BT_LogOut";
            this.BT_LogOut.Size = new System.Drawing.Size(47, 41);
            this.BT_LogOut.TabIndex = 30;
            this.BT_LogOut.UseVisualStyleBackColor = false;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(7, 45);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(321, 25);
            this.Label13.TabIndex = 1;
            this.Label13.Text = "Отсканируйте свой бэйджик";
            // 
            // TB_RFIDIn
            // 
            this.TB_RFIDIn.Location = new System.Drawing.Point(11, 88);
            this.TB_RFIDIn.MaxLength = 10;
            this.TB_RFIDIn.Name = "TB_RFIDIn";
            this.TB_RFIDIn.PasswordChar = '*';
            this.TB_RFIDIn.Size = new System.Drawing.Size(345, 31);
            this.TB_RFIDIn.TabIndex = 0;
            this.TB_RFIDIn.Text = "0000181218";
            // 
            // GB_Work
            // 
            this.GB_Work.Controls.Add(this.Controllabel);
            this.GB_Work.Controls.Add(this.Label2);
            this.GB_Work.Controls.Add(this.SerialTextBox);
            this.GB_Work.Controls.Add(this.DG_UpLog);
            this.GB_Work.Controls.Add(this.GroupBox5);
            this.GB_Work.Controls.Add(this.GroupBox4);
            this.GB_Work.Location = new System.Drawing.Point(12, 12);
            this.GB_Work.Name = "GB_Work";
            this.GB_Work.Size = new System.Drawing.Size(1258, 629);
            this.GB_Work.TabIndex = 31;
            this.GB_Work.TabStop = false;
            this.GB_Work.Visible = false;
            // 
            // Controllabel
            // 
            this.Controllabel.AutoSize = true;
            this.Controllabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Controllabel.Location = new System.Drawing.Point(0, 275);
            this.Controllabel.Name = "Controllabel";
            this.Controllabel.Size = new System.Drawing.Size(217, 29);
            this.Controllabel.TabIndex = 29;
            this.Controllabel.Text = "CONTROLLABEL";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label2.Location = new System.Drawing.Point(0, 310);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(161, 25);
            this.Label2.TabIndex = 28;
            this.Label2.Text = "Serial Number";
            // 
            // SerialTextBox
            // 
            this.SerialTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SerialTextBox.Location = new System.Drawing.Point(1, 339);
            this.SerialTextBox.Name = "SerialTextBox";
            this.SerialTextBox.Size = new System.Drawing.Size(662, 31);
            this.SerialTextBox.TabIndex = 27;
            // 
            // DG_UpLog
            // 
            this.DG_UpLog.AllowUserToAddRows = false;
            this.DG_UpLog.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DG_UpLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DG_UpLog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DG_UpLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_UpLog.Location = new System.Drawing.Point(0, 383);
            this.DG_UpLog.Name = "DG_UpLog";
            this.DG_UpLog.ReadOnly = true;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DG_UpLog.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DG_UpLog.Size = new System.Drawing.Size(663, 246);
            this.DG_UpLog.TabIndex = 26;
            // 
            // GroupBox5
            // 
            this.GroupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.GroupBox5.Controls.Add(this.CurrrentDateLabel);
            this.GroupBox5.Controls.Add(this.CurrrentTimeLabel);
            this.GroupBox5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupBox5.Location = new System.Drawing.Point(669, 383);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(316, 128);
            this.GroupBox5.TabIndex = 25;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "Дата и Время на этикетке СН";
            // 
            // CurrrentDateLabel
            // 
            this.CurrrentDateLabel.AutoSize = true;
            this.CurrrentDateLabel.BackColor = System.Drawing.SystemColors.Control;
            this.CurrrentDateLabel.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrrentDateLabel.Location = new System.Drawing.Point(6, 28);
            this.CurrrentDateLabel.Name = "CurrrentDateLabel";
            this.CurrrentDateLabel.Size = new System.Drawing.Size(241, 41);
            this.CurrrentDateLabel.TabIndex = 6;
            this.CurrrentDateLabel.Text = "Current DATE";
            // 
            // CurrrentTimeLabel
            // 
            this.CurrrentTimeLabel.AutoSize = true;
            this.CurrrentTimeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.CurrrentTimeLabel.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrrentTimeLabel.Location = new System.Drawing.Point(6, 77);
            this.CurrrentTimeLabel.Name = "CurrrentTimeLabel";
            this.CurrrentTimeLabel.Size = new System.Drawing.Size(234, 41);
            this.CurrrentTimeLabel.TabIndex = 6;
            this.CurrrentTimeLabel.Text = "Current TIME";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.GridInfo);
            this.GroupBox4.Controls.Add(this.BT_OpenSettings);
            this.GroupBox4.Controls.Add(this.BT_CloseApp);
            this.GroupBox4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupBox4.Location = new System.Drawing.Point(669, 19);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(583, 358);
            this.GroupBox4.TabIndex = 11;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Информация о ЛОТе и станции";
            // 
            // GridInfo
            // 
            this.GridInfo.AllowUserToAddRows = false;
            this.GridInfo.AllowUserToDeleteRows = false;
            this.GridInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridInfo.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GridInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GridInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridInfo.ColumnHeadersVisible = false;
            this.GridInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.GridInfo.Location = new System.Drawing.Point(6, 31);
            this.GridInfo.Name = "GridInfo";
            this.GridInfo.RowHeadersVisible = false;
            this.GridInfo.Size = new System.Drawing.Size(571, 254);
            this.GridInfo.TabIndex = 26;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 5;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 5;
            // 
            // BT_OpenSettings
            // 
            this.BT_OpenSettings.FlatAppearance.BorderSize = 0;
            this.BT_OpenSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_OpenSettings.Image = global::GS_STB.Properties.Resources.settings__1_;
            this.BT_OpenSettings.Location = new System.Drawing.Point(418, 289);
            this.BT_OpenSettings.Name = "BT_OpenSettings";
            this.BT_OpenSettings.Size = new System.Drawing.Size(69, 63);
            this.BT_OpenSettings.TabIndex = 24;
            this.BT_OpenSettings.UseVisualStyleBackColor = true;
            // 
            // BT_CloseApp
            // 
            this.BT_CloseApp.FlatAppearance.BorderSize = 0;
            this.BT_CloseApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_CloseApp.Image = global::GS_STB.Properties.Resources.close;
            this.BT_CloseApp.Location = new System.Drawing.Point(493, 291);
            this.BT_CloseApp.Name = "BT_CloseApp";
            this.BT_CloseApp.Size = new System.Drawing.Size(84, 61);
            this.BT_CloseApp.TabIndex = 25;
            this.BT_CloseApp.UseVisualStyleBackColor = true;
            // 
            // Times
            // 
            this.Times.Tick += new System.EventHandler(this.Times_Tick);
            // 
            // WorkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1393, 702);
            this.Controls.Add(this.GB_Work);
            this.Controls.Add(this.GB_UserData);
            this.Name = "WorkForm";
            this.Text = "WorkForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.GB_UserData.ResumeLayout(false);
            this.GB_UserData.PerformLayout();
            this.GB_Work.ResumeLayout(false);
            this.GB_Work.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_UpLog)).EndInit();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GB_UserData;
        internal System.Windows.Forms.Button BT_LogOut;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox TB_RFIDIn;
        private System.Windows.Forms.GroupBox GB_Work;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.Button BT_OpenSettings;
        internal System.Windows.Forms.Button BT_CloseApp;
        private System.Windows.Forms.DataGridView GridInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        internal System.Windows.Forms.Label Controllabel;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox SerialTextBox;
        internal System.Windows.Forms.DataGridView DG_UpLog;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Label CurrrentDateLabel;
        internal System.Windows.Forms.Label CurrrentTimeLabel;
        private System.Windows.Forms.Timer Times;
    }
}