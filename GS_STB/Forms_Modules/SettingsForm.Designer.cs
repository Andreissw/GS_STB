namespace GS_STB.Forms_Modules
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LineSettings = new System.Windows.Forms.GroupBox();
            this.Fas_Start = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CheckBoxSN = new System.Windows.Forms.CheckBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.TB_LabelSNCount = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.Label7 = new System.Windows.Forms.Label();
            this.RB_Local_DateTime = new System.Windows.Forms.RadioButton();
            this.RB_Server_Time = new System.Windows.Forms.RadioButton();
            this.DG_LOTList = new System.Windows.Forms.DataGridView();
            this.BT_OpenWorkForm = new System.Windows.Forms.Button();
            this.UploadStationGB = new System.Windows.Forms.GroupBox();
            this.CheckBoxID = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.CheckBoxDublicateSCID = new System.Windows.Forms.CheckBox();
            this.TB_LabelIDCount = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.DG_UplSt = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.Fas_Start.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_LOTList)).BeginInit();
            this.UploadStationGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_UplSt)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GridInfo);
            this.groupBox1.Location = new System.Drawing.Point(928, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(479, 217);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Информация о рабочей станции";
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridInfo.DefaultCellStyle = dataGridViewCellStyle3;
            this.GridInfo.Location = new System.Drawing.Point(6, 19);
            this.GridInfo.Name = "GridInfo";
            this.GridInfo.RowHeadersVisible = false;
            this.GridInfo.Size = new System.Drawing.Size(467, 192);
            this.GridInfo.TabIndex = 2;
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
            // LineSettings
            // 
            this.LineSettings.Location = new System.Drawing.Point(1373, 235);
            this.LineSettings.Name = "LineSettings";
            this.LineSettings.Size = new System.Drawing.Size(136, 50);
            this.LineSettings.TabIndex = 2;
            this.LineSettings.TabStop = false;
            this.LineSettings.Text = "Настройка Линии";
            this.LineSettings.Visible = false;
            // 
            // Fas_Start
            // 
            this.Fas_Start.Controls.Add(this.groupBox3);
            this.Fas_Start.Controls.Add(this.groupBox2);
            this.Fas_Start.Location = new System.Drawing.Point(1373, 291);
            this.Fas_Start.Name = "Fas_Start";
            this.Fas_Start.Size = new System.Drawing.Size(94, 25);
            this.Fas_Start.TabIndex = 3;
            this.Fas_Start.TabStop = false;
            this.Fas_Start.Text = "Fas_Start";
            this.Fas_Start.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CheckBoxSN);
            this.groupBox3.Controls.Add(this.Button2);
            this.groupBox3.Controls.Add(this.TB_LabelSNCount);
            this.groupBox3.Location = new System.Drawing.Point(6, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 108);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Печать этикеток SN ";
            // 
            // CheckBoxSN
            // 
            this.CheckBoxSN.AutoSize = true;
            this.CheckBoxSN.Location = new System.Drawing.Point(11, 31);
            this.CheckBoxSN.Name = "CheckBoxSN";
            this.CheckBoxSN.Size = new System.Drawing.Size(152, 17);
            this.CheckBoxSN.TabIndex = 16;
            this.CheckBoxSN.Text = "Количество этикеток SN";
            this.CheckBoxSN.UseVisualStyleBackColor = true;
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(9, 67);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(149, 23);
            this.Button2.TabIndex = 9;
            this.Button2.Text = "Настройки  этикетки";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Visible = false;
            // 
            // TB_LabelSNCount
            // 
            this.TB_LabelSNCount.Location = new System.Drawing.Point(169, 28);
            this.TB_LabelSNCount.Name = "TB_LabelSNCount";
            this.TB_LabelSNCount.Size = new System.Drawing.Size(33, 20);
            this.TB_LabelSNCount.TabIndex = 8;
            this.TB_LabelSNCount.Text = "3";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DateTimePicker);
            this.groupBox2.Controls.Add(this.Label7);
            this.groupBox2.Controls.Add(this.RB_Local_DateTime);
            this.groupBox2.Controls.Add(this.RB_Server_Time);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 122);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выбор режима даты";
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Enabled = false;
            this.DateTimePicker.Location = new System.Drawing.Point(6, 90);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(204, 20);
            this.DateTimePicker.TabIndex = 19;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(6, 72);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(209, 13);
            this.Label7.TabIndex = 20;
            this.Label7.Text = "Укажите требуемую дату производства";
            // 
            // RB_Local_DateTime
            // 
            this.RB_Local_DateTime.AutoSize = true;
            this.RB_Local_DateTime.Location = new System.Drawing.Point(6, 46);
            this.RB_Local_DateTime.Name = "RB_Local_DateTime";
            this.RB_Local_DateTime.Size = new System.Drawing.Size(136, 17);
            this.RB_Local_DateTime.TabIndex = 18;
            this.RB_Local_DateTime.TabStop = true;
            this.RB_Local_DateTime.Text = "Время локально с ПК";
            this.RB_Local_DateTime.UseVisualStyleBackColor = true;
            // 
            // RB_Server_Time
            // 
            this.RB_Server_Time.AutoSize = true;
            this.RB_Server_Time.Location = new System.Drawing.Point(6, 23);
            this.RB_Server_Time.Name = "RB_Server_Time";
            this.RB_Server_Time.Size = new System.Drawing.Size(62, 17);
            this.RB_Server_Time.TabIndex = 16;
            this.RB_Server_Time.TabStop = true;
            this.RB_Server_Time.Text = "Сервер";
            this.RB_Server_Time.UseVisualStyleBackColor = true;
            // 
            // DG_LOTList
            // 
            this.DG_LOTList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DG_LOTList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_LOTList.Location = new System.Drawing.Point(6, 8);
            this.DG_LOTList.Name = "DG_LOTList";
            this.DG_LOTList.Size = new System.Drawing.Size(910, 487);
            this.DG_LOTList.TabIndex = 4;
            this.DG_LOTList.Visible = false;
            // 
            // BT_OpenWorkForm
            // 
            this.BT_OpenWorkForm.FlatAppearance.BorderSize = 0;
            this.BT_OpenWorkForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_OpenWorkForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT_OpenWorkForm.Image = global::GS_STB.Properties.Resources.play;
            this.BT_OpenWorkForm.Location = new System.Drawing.Point(922, 514);
            this.BT_OpenWorkForm.Name = "BT_OpenWorkForm";
            this.BT_OpenWorkForm.Size = new System.Drawing.Size(89, 95);
            this.BT_OpenWorkForm.TabIndex = 24;
            this.BT_OpenWorkForm.Text = "Запуск";
            this.BT_OpenWorkForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.BT_OpenWorkForm.UseVisualStyleBackColor = true;
            // 
            // UploadStationGB
            // 
            this.UploadStationGB.Controls.Add(this.CheckBoxID);
            this.UploadStationGB.Controls.Add(this.checkBox1);
            this.UploadStationGB.Controls.Add(this.CheckBoxDublicateSCID);
            this.UploadStationGB.Controls.Add(this.TB_LabelIDCount);
            this.UploadStationGB.Controls.Add(this.textBox1);
            this.UploadStationGB.Location = new System.Drawing.Point(1373, 328);
            this.UploadStationGB.Name = "UploadStationGB";
            this.UploadStationGB.Size = new System.Drawing.Size(95, 22);
            this.UploadStationGB.TabIndex = 39;
            this.UploadStationGB.TabStop = false;
            this.UploadStationGB.Text = "UploadStation";
            this.UploadStationGB.Visible = false;
            // 
            // CheckBoxID
            // 
            this.CheckBoxID.AutoSize = true;
            this.CheckBoxID.Location = new System.Drawing.Point(6, 43);
            this.CheckBoxID.Name = "CheckBoxID";
            this.CheckBoxID.Size = new System.Drawing.Size(148, 17);
            this.CheckBoxID.TabIndex = 24;
            this.CheckBoxID.Text = "Количество этикеток ID";
            this.CheckBoxID.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(152, 17);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Количество этикеток SN";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // CheckBoxDublicateSCID
            // 
            this.CheckBoxDublicateSCID.AutoSize = true;
            this.CheckBoxDublicateSCID.Location = new System.Drawing.Point(6, 67);
            this.CheckBoxDublicateSCID.Name = "CheckBoxDublicateSCID";
            this.CheckBoxDublicateSCID.Size = new System.Drawing.Size(180, 17);
            this.CheckBoxDublicateSCID.TabIndex = 26;
            this.CheckBoxDublicateSCID.Text = "Проверка уникальности SC ID";
            this.CheckBoxDublicateSCID.UseVisualStyleBackColor = true;
            // 
            // TB_LabelIDCount
            // 
            this.TB_LabelIDCount.Location = new System.Drawing.Point(160, 40);
            this.TB_LabelIDCount.Name = "TB_LabelIDCount";
            this.TB_LabelIDCount.Size = new System.Drawing.Size(33, 20);
            this.TB_LabelIDCount.TabIndex = 22;
            this.TB_LabelIDCount.Text = "2";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(160, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(33, 20);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "2";
            // 
            // DG_UplSt
            // 
            this.DG_UplSt.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.DG_UplSt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_UplSt.Location = new System.Drawing.Point(6, 514);
            this.DG_UplSt.Name = "DG_UplSt";
            this.DG_UplSt.Size = new System.Drawing.Size(910, 78);
            this.DG_UplSt.TabIndex = 40;
            this.DG_UplSt.Visible = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1419, 642);
            this.Controls.Add(this.DG_UplSt);
            this.Controls.Add(this.UploadStationGB);
            this.Controls.Add(this.BT_OpenWorkForm);
            this.Controls.Add(this.DG_LOTList);
            this.Controls.Add(this.Fas_Start);
            this.Controls.Add(this.LineSettings);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.Fas_Start.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_LOTList)).EndInit();
            this.UploadStationGB.ResumeLayout(false);
            this.UploadStationGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DG_UplSt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView GridInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.GroupBox LineSettings;
        private System.Windows.Forms.GroupBox Fas_Start;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.DateTimePicker DateTimePicker;
        internal System.Windows.Forms.RadioButton RB_Server_Time;
        internal System.Windows.Forms.RadioButton RB_Local_DateTime;
        private System.Windows.Forms.DataGridView DG_LOTList;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.CheckBox CheckBoxSN;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.TextBox TB_LabelSNCount;
        internal System.Windows.Forms.Button BT_OpenWorkForm;
        private System.Windows.Forms.GroupBox UploadStationGB;
        internal System.Windows.Forms.CheckBox CheckBoxID;
        internal System.Windows.Forms.CheckBox checkBox1;
        internal System.Windows.Forms.CheckBox CheckBoxDublicateSCID;
        internal System.Windows.Forms.TextBox TB_LabelIDCount;
        internal System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView DG_UplSt;
    }
}