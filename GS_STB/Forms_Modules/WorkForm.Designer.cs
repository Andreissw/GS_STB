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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GB_UserData = new System.Windows.Forms.GroupBox();
            this.BT_LogOut = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.TB_RFIDIn = new System.Windows.Forms.TextBox();
            this.GB_Work = new System.Windows.Forms.GroupBox();
            this.ProgressbarText = new System.Windows.Forms.Label();
            this.Desassembly_STBGroup = new System.Windows.Forms.GroupBox();
            this.BT_Disassebly = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CB_INFO = new System.Windows.Forms.ComboBox();
            this.CB_ErrorCode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PB = new System.Windows.Forms.ProgressBar();
            this.FAS_Print = new System.Windows.Forms.GroupBox();
            this.CountLBID = new System.Windows.Forms.Label();
            this.CountLBSN = new System.Windows.Forms.Label();
            this.CHPrintID = new System.Windows.Forms.CheckBox();
            this.CHPrintSN = new System.Windows.Forms.CheckBox();
            this.GB_PrinterSettings = new System.Windows.Forms.GroupBox();
            this.IDPrint = new System.Windows.Forms.GroupBox();
            this.XID = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.YID = new System.Windows.Forms.NumericUpDown();
            this.SNPRINT = new System.Windows.Forms.GroupBox();
            this.XSN = new System.Windows.Forms.NumericUpDown();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.YSN = new System.Windows.Forms.NumericUpDown();
            this.Label_ShiftCounter = new System.Windows.Forms.Label();
            this.Controllabel = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.SerialTextBox = new System.Windows.Forms.TextBox();
            this.DG_UpLog = new System.Windows.Forms.DataGridView();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.LBPrintSN = new System.Windows.Forms.Label();
            this.CurrrentTimeLabel = new System.Windows.Forms.Label();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FAS_EndGB = new System.Windows.Forms.GroupBox();
            this.GroupBox7 = new System.Windows.Forms.GroupBox();
            this.NextBoxNum = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.PalletNum = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BoxNum = new System.Windows.Forms.Label();
            this.Times = new System.Windows.Forms.Timer(this.components);
            this.SaveSettingBT = new System.Windows.Forms.Label();
            this.ClearBT = new System.Windows.Forms.Button();
            this.BT_PrinterSettings = new System.Windows.Forms.Button();
            this.BT_ClosePrintSet = new System.Windows.Forms.Button();
            this.BT_SevePrintSettings = new System.Windows.Forms.Button();
            this.BT_CloseApp = new System.Windows.Forms.Button();
            this.GB_UserData.SuspendLayout();
            this.GB_Work.SuspendLayout();
            this.Desassembly_STBGroup.SuspendLayout();
            this.FAS_Print.SuspendLayout();
            this.GB_PrinterSettings.SuspendLayout();
            this.IDPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YID)).BeginInit();
            this.SNPRINT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XSN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YSN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_UpLog)).BeginInit();
            this.GroupBox5.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.FAS_EndGB.SuspendLayout();
            this.GroupBox7.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB_UserData
            // 
            this.GB_UserData.BackColor = System.Drawing.Color.Tan;
            this.GB_UserData.Controls.Add(this.BT_LogOut);
            this.GB_UserData.Controls.Add(this.Label13);
            this.GB_UserData.Controls.Add(this.TB_RFIDIn);
            this.GB_UserData.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GB_UserData.Location = new System.Drawing.Point(1633, 700);
            this.GB_UserData.Name = "GB_UserData";
            this.GB_UserData.Size = new System.Drawing.Size(91, 30);
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
            this.GB_Work.Controls.Add(this.ClearBT);
            this.GB_Work.Controls.Add(this.ProgressbarText);
            this.GB_Work.Controls.Add(this.Desassembly_STBGroup);
            this.GB_Work.Controls.Add(this.PB);
            this.GB_Work.Controls.Add(this.FAS_Print);
            this.GB_Work.Controls.Add(this.Label_ShiftCounter);
            this.GB_Work.Controls.Add(this.Controllabel);
            this.GB_Work.Controls.Add(this.Label2);
            this.GB_Work.Controls.Add(this.SerialTextBox);
            this.GB_Work.Controls.Add(this.DG_UpLog);
            this.GB_Work.Controls.Add(this.GroupBox5);
            this.GB_Work.Controls.Add(this.GroupBox4);
            this.GB_Work.Controls.Add(this.FAS_EndGB);
            this.GB_Work.Location = new System.Drawing.Point(12, 12);
            this.GB_Work.Name = "GB_Work";
            this.GB_Work.Size = new System.Drawing.Size(1326, 737);
            this.GB_Work.TabIndex = 31;
            this.GB_Work.TabStop = false;
            this.GB_Work.Visible = false;
            // 
            // ProgressbarText
            // 
            this.ProgressbarText.AutoSize = true;
            this.ProgressbarText.BackColor = System.Drawing.Color.Transparent;
            this.ProgressbarText.Location = new System.Drawing.Point(8, 684);
            this.ProgressbarText.Name = "ProgressbarText";
            this.ProgressbarText.Size = new System.Drawing.Size(0, 13);
            this.ProgressbarText.TabIndex = 32;
            // 
            // Desassembly_STBGroup
            // 
            this.Desassembly_STBGroup.Controls.Add(this.BT_Disassebly);
            this.Desassembly_STBGroup.Controls.Add(this.label1);
            this.Desassembly_STBGroup.Controls.Add(this.CB_INFO);
            this.Desassembly_STBGroup.Controls.Add(this.CB_ErrorCode);
            this.Desassembly_STBGroup.Controls.Add(this.label3);
            this.Desassembly_STBGroup.Location = new System.Drawing.Point(1163, 615);
            this.Desassembly_STBGroup.Name = "Desassembly_STBGroup";
            this.Desassembly_STBGroup.Size = new System.Drawing.Size(114, 20);
            this.Desassembly_STBGroup.TabIndex = 132;
            this.Desassembly_STBGroup.TabStop = false;
            this.Desassembly_STBGroup.Text = "Desassembly_STB";
            this.Desassembly_STBGroup.Visible = false;
            // 
            // BT_Disassebly
            // 
            this.BT_Disassebly.Enabled = false;
            this.BT_Disassebly.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT_Disassebly.Location = new System.Drawing.Point(6, 148);
            this.BT_Disassebly.Name = "BT_Disassebly";
            this.BT_Disassebly.Size = new System.Drawing.Size(309, 46);
            this.BT_Disassebly.TabIndex = 34;
            this.BT_Disassebly.Text = "Открепить номер";
            this.BT_Disassebly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(6, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 25);
            this.label1.TabIndex = 36;
            this.label1.Text = "Расшифровка кодов отказа";
            // 
            // CB_INFO
            // 
            this.CB_INFO.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_INFO.FormattingEnabled = true;
            this.CB_INFO.Location = new System.Drawing.Point(6, 109);
            this.CB_INFO.Name = "CB_INFO";
            this.CB_INFO.Size = new System.Drawing.Size(384, 33);
            this.CB_INFO.TabIndex = 37;
            // 
            // CB_ErrorCode
            // 
            this.CB_ErrorCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.CB_ErrorCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CB_ErrorCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CB_ErrorCode.FormattingEnabled = true;
            this.CB_ErrorCode.ImeMode = System.Windows.Forms.ImeMode.On;
            this.CB_ErrorCode.Location = new System.Drawing.Point(10, 44);
            this.CB_ErrorCode.MaxDropDownItems = 25;
            this.CB_ErrorCode.MaxLength = 2;
            this.CB_ErrorCode.Name = "CB_ErrorCode";
            this.CB_ErrorCode.Size = new System.Drawing.Size(158, 33);
            this.CB_ErrorCode.Sorted = true;
            this.CB_ErrorCode.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 25);
            this.label3.TabIndex = 35;
            this.label3.Text = "Укажите код отказа";
            // 
            // PB
            // 
            this.PB.BackColor = System.Drawing.Color.Chartreuse;
            this.PB.ForeColor = System.Drawing.Color.Gray;
            this.PB.Location = new System.Drawing.Point(1, 679);
            this.PB.Maximum = 8;
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(988, 24);
            this.PB.Step = 1;
            this.PB.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PB.TabIndex = 32;
            this.PB.Visible = false;
            // 
            // FAS_Print
            // 
            this.FAS_Print.Controls.Add(this.CountLBID);
            this.FAS_Print.Controls.Add(this.CountLBSN);
            this.FAS_Print.Controls.Add(this.CHPrintID);
            this.FAS_Print.Controls.Add(this.CHPrintSN);
            this.FAS_Print.Controls.Add(this.BT_PrinterSettings);
            this.FAS_Print.Controls.Add(this.GB_PrinterSettings);
            this.FAS_Print.Location = new System.Drawing.Point(272, 19);
            this.FAS_Print.Name = "FAS_Print";
            this.FAS_Print.Size = new System.Drawing.Size(440, 186);
            this.FAS_Print.TabIndex = 131;
            this.FAS_Print.TabStop = false;
            this.FAS_Print.Text = "Печать";
            this.FAS_Print.Visible = false;
            // 
            // CountLBID
            // 
            this.CountLBID.AutoSize = true;
            this.CountLBID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CountLBID.Location = new System.Drawing.Point(283, 146);
            this.CountLBID.Name = "CountLBID";
            this.CountLBID.Size = new System.Drawing.Size(14, 15);
            this.CountLBID.TabIndex = 134;
            this.CountLBID.Text = "1";
            // 
            // CountLBSN
            // 
            this.CountLBSN.AutoSize = true;
            this.CountLBSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CountLBSN.Location = new System.Drawing.Point(283, 129);
            this.CountLBSN.Name = "CountLBSN";
            this.CountLBSN.Size = new System.Drawing.Size(14, 15);
            this.CountLBSN.TabIndex = 133;
            this.CountLBSN.Text = "1";
            // 
            // CHPrintID
            // 
            this.CHPrintID.AutoSize = true;
            this.CHPrintID.Enabled = false;
            this.CHPrintID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CHPrintID.Location = new System.Drawing.Point(286, 85);
            this.CHPrintID.Name = "CHPrintID";
            this.CHPrintID.Size = new System.Drawing.Size(73, 20);
            this.CHPrintID.TabIndex = 132;
            this.CHPrintID.Text = "PrintID";
            this.CHPrintID.UseVisualStyleBackColor = true;
            // 
            // CHPrintSN
            // 
            this.CHPrintSN.AutoSize = true;
            this.CHPrintSN.Enabled = false;
            this.CHPrintSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CHPrintSN.Location = new System.Drawing.Point(286, 104);
            this.CHPrintSN.Name = "CHPrintSN";
            this.CHPrintSN.Size = new System.Drawing.Size(79, 20);
            this.CHPrintSN.TabIndex = 131;
            this.CHPrintSN.Text = "PrintSN";
            this.CHPrintSN.UseVisualStyleBackColor = true;
            // 
            // GB_PrinterSettings
            // 
            this.GB_PrinterSettings.Controls.Add(this.SaveSettingBT);
            this.GB_PrinterSettings.Controls.Add(this.IDPrint);
            this.GB_PrinterSettings.Controls.Add(this.SNPRINT);
            this.GB_PrinterSettings.Controls.Add(this.BT_ClosePrintSet);
            this.GB_PrinterSettings.Controls.Add(this.BT_SevePrintSettings);
            this.GB_PrinterSettings.Location = new System.Drawing.Point(6, 22);
            this.GB_PrinterSettings.Name = "GB_PrinterSettings";
            this.GB_PrinterSettings.Size = new System.Drawing.Size(274, 155);
            this.GB_PrinterSettings.TabIndex = 130;
            this.GB_PrinterSettings.TabStop = false;
            this.GB_PrinterSettings.Text = "Printer Settings";
            this.GB_PrinterSettings.Visible = false;
            // 
            // IDPrint
            // 
            this.IDPrint.Controls.Add(this.XID);
            this.IDPrint.Controls.Add(this.label4);
            this.IDPrint.Controls.Add(this.label8);
            this.IDPrint.Controls.Add(this.YID);
            this.IDPrint.Location = new System.Drawing.Point(136, 19);
            this.IDPrint.Name = "IDPrint";
            this.IDPrint.Size = new System.Drawing.Size(124, 63);
            this.IDPrint.TabIndex = 16;
            this.IDPrint.TabStop = false;
            this.IDPrint.Text = "Координаты SmartID";
            this.IDPrint.Visible = false;
            // 
            // XID
            // 
            this.XID.Location = new System.Drawing.Point(6, 31);
            this.XID.Name = "XID";
            this.XID.Size = new System.Drawing.Size(46, 20);
            this.XID.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "X";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Y";
            // 
            // YID
            // 
            this.YID.Location = new System.Drawing.Point(58, 31);
            this.YID.Name = "YID";
            this.YID.Size = new System.Drawing.Size(46, 20);
            this.YID.TabIndex = 10;
            // 
            // SNPRINT
            // 
            this.SNPRINT.Controls.Add(this.XSN);
            this.SNPRINT.Controls.Add(this.Label19);
            this.SNPRINT.Controls.Add(this.Label20);
            this.SNPRINT.Controls.Add(this.YSN);
            this.SNPRINT.Location = new System.Drawing.Point(6, 19);
            this.SNPRINT.Name = "SNPRINT";
            this.SNPRINT.Size = new System.Drawing.Size(124, 63);
            this.SNPRINT.TabIndex = 15;
            this.SNPRINT.TabStop = false;
            this.SNPRINT.Text = "Координаты SN";
            this.SNPRINT.Visible = false;
            // 
            // XSN
            // 
            this.XSN.Location = new System.Drawing.Point(6, 31);
            this.XSN.Name = "XSN";
            this.XSN.Size = new System.Drawing.Size(46, 20);
            this.XSN.TabIndex = 9;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(6, 16);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(14, 13);
            this.Label19.TabIndex = 0;
            this.Label19.Text = "X";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(58, 16);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(14, 13);
            this.Label20.TabIndex = 0;
            this.Label20.Text = "Y";
            // 
            // YSN
            // 
            this.YSN.Location = new System.Drawing.Point(58, 31);
            this.YSN.Name = "YSN";
            this.YSN.Size = new System.Drawing.Size(46, 20);
            this.YSN.TabIndex = 10;
            // 
            // Label_ShiftCounter
            // 
            this.Label_ShiftCounter.AutoSize = true;
            this.Label_ShiftCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_ShiftCounter.Location = new System.Drawing.Point(19, 16);
            this.Label_ShiftCounter.Name = "Label_ShiftCounter";
            this.Label_ShiftCounter.Size = new System.Drawing.Size(71, 76);
            this.Label_ShiftCounter.TabIndex = 30;
            this.Label_ShiftCounter.Text = "0";
            // 
            // Controllabel
            // 
            this.Controllabel.AutoSize = true;
            this.Controllabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Controllabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Controllabel.Location = new System.Drawing.Point(1, 213);
            this.Controllabel.Name = "Controllabel";
            this.Controllabel.Size = new System.Drawing.Size(172, 24);
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
            this.SerialTextBox.Size = new System.Drawing.Size(727, 31);
            this.SerialTextBox.TabIndex = 27;
            // 
            // DG_UpLog
            // 
            this.DG_UpLog.AllowUserToAddRows = false;
            this.DG_UpLog.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DG_UpLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DG_UpLog.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DG_UpLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DG_UpLog.Location = new System.Drawing.Point(0, 383);
            this.DG_UpLog.Name = "DG_UpLog";
            this.DG_UpLog.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DG_UpLog.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DG_UpLog.Size = new System.Drawing.Size(989, 290);
            this.DG_UpLog.TabIndex = 26;
            // 
            // GroupBox5
            // 
            this.GroupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.GroupBox5.Controls.Add(this.LBPrintSN);
            this.GroupBox5.Controls.Add(this.CurrrentTimeLabel);
            this.GroupBox5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupBox5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GroupBox5.Location = new System.Drawing.Point(995, 383);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(316, 128);
            this.GroupBox5.TabIndex = 25;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "Дата и Время на этикетке СН";
            // 
            // LBPrintSN
            // 
            this.LBPrintSN.AutoSize = true;
            this.LBPrintSN.BackColor = System.Drawing.SystemColors.Control;
            this.LBPrintSN.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LBPrintSN.Location = new System.Drawing.Point(6, 28);
            this.LBPrintSN.Name = "LBPrintSN";
            this.LBPrintSN.Size = new System.Drawing.Size(160, 27);
            this.LBPrintSN.TabIndex = 6;
            this.LBPrintSN.Text = "Current DATE";
            // 
            // CurrrentTimeLabel
            // 
            this.CurrrentTimeLabel.AutoSize = true;
            this.CurrrentTimeLabel.BackColor = System.Drawing.SystemColors.Control;
            this.CurrrentTimeLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CurrrentTimeLabel.Location = new System.Drawing.Point(6, 77);
            this.CurrrentTimeLabel.Name = "CurrrentTimeLabel";
            this.CurrrentTimeLabel.Size = new System.Drawing.Size(155, 27);
            this.CurrrentTimeLabel.TabIndex = 6;
            this.CurrrentTimeLabel.Text = "Current TIME";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.GridInfo);
            this.GroupBox4.Controls.Add(this.BT_CloseApp);
            this.GroupBox4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupBox4.Location = new System.Drawing.Point(734, 19);
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
            this.GridInfo.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
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
            // FAS_EndGB
            // 
            this.FAS_EndGB.Controls.Add(this.GroupBox7);
            this.FAS_EndGB.Controls.Add(this.GroupBox2);
            this.FAS_EndGB.Controls.Add(this.groupBox3);
            this.FAS_EndGB.Location = new System.Drawing.Point(1152, 665);
            this.FAS_EndGB.Name = "FAS_EndGB";
            this.FAS_EndGB.Size = new System.Drawing.Size(65, 35);
            this.FAS_EndGB.TabIndex = 133;
            this.FAS_EndGB.TabStop = false;
            this.FAS_EndGB.Text = "FAS END";
            this.FAS_EndGB.Visible = false;
            // 
            // GroupBox7
            // 
            this.GroupBox7.BackColor = System.Drawing.Color.Green;
            this.GroupBox7.Controls.Add(this.NextBoxNum);
            this.GroupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupBox7.Location = new System.Drawing.Point(274, 120);
            this.GroupBox7.Name = "GroupBox7";
            this.GroupBox7.Size = new System.Drawing.Size(256, 97);
            this.GroupBox7.TabIndex = 8;
            this.GroupBox7.TabStop = false;
            this.GroupBox7.Text = "Следующая коробка";
            // 
            // NextBoxNum
            // 
            this.NextBoxNum.AutoSize = true;
            this.NextBoxNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NextBoxNum.Location = new System.Drawing.Point(3, 25);
            this.NextBoxNum.Name = "NextBoxNum";
            this.NextBoxNum.Size = new System.Drawing.Size(68, 73);
            this.NextBoxNum.TabIndex = 0;
            this.NextBoxNum.Text = "0";
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.Color.Green;
            this.GroupBox2.Controls.Add(this.PalletNum);
            this.GroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GroupBox2.Location = new System.Drawing.Point(274, 31);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(254, 82);
            this.GroupBox2.TabIndex = 7;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Pallet";
            // 
            // PalletNum
            // 
            this.PalletNum.AutoSize = true;
            this.PalletNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PalletNum.Location = new System.Drawing.Point(6, 24);
            this.PalletNum.Name = "PalletNum";
            this.PalletNum.Size = new System.Drawing.Size(51, 55);
            this.PalletNum.TabIndex = 0;
            this.PalletNum.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Green;
            this.groupBox3.Controls.Add(this.BoxNum);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(6, 119);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(256, 97);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Текущаяя корбка";
            // 
            // BoxNum
            // 
            this.BoxNum.AutoSize = true;
            this.BoxNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BoxNum.Location = new System.Drawing.Point(3, 25);
            this.BoxNum.Name = "BoxNum";
            this.BoxNum.Size = new System.Drawing.Size(68, 73);
            this.BoxNum.TabIndex = 0;
            this.BoxNum.Text = "0";
            // 
            // Times
            // 
            this.Times.Tick += new System.EventHandler(this.Times_Tick);
            // 
            // SaveSettingBT
            // 
            this.SaveSettingBT.AutoSize = true;
            this.SaveSettingBT.ForeColor = System.Drawing.Color.Green;
            this.SaveSettingBT.Location = new System.Drawing.Point(64, 130);
            this.SaveSettingBT.Name = "SaveSettingBT";
            this.SaveSettingBT.Size = new System.Drawing.Size(0, 13);
            this.SaveSettingBT.TabIndex = 17;
            // 
            // ClearBT
            // 
            this.ClearBT.FlatAppearance.BorderSize = 0;
            this.ClearBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearBT.Image = global::GS_STB.Properties.Resources.recycling_recyclebin_empty_delete_trash_1771;
            this.ClearBT.Location = new System.Drawing.Point(653, 262);
            this.ClearBT.Name = "ClearBT";
            this.ClearBT.Size = new System.Drawing.Size(75, 71);
            this.ClearBT.TabIndex = 135;
            this.ClearBT.UseVisualStyleBackColor = true;
            // 
            // BT_PrinterSettings
            // 
            this.BT_PrinterSettings.BackColor = System.Drawing.Color.Transparent;
            this.BT_PrinterSettings.FlatAppearance.BorderSize = 0;
            this.BT_PrinterSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_PrinterSettings.ForeColor = System.Drawing.Color.Transparent;
            this.BT_PrinterSettings.Image = global::GS_STB.Properties.Resources.gtkprintreport;
            this.BT_PrinterSettings.Location = new System.Drawing.Point(286, 30);
            this.BT_PrinterSettings.Name = "BT_PrinterSettings";
            this.BT_PrinterSettings.Size = new System.Drawing.Size(51, 49);
            this.BT_PrinterSettings.TabIndex = 29;
            this.BT_PrinterSettings.UseVisualStyleBackColor = false;
            // 
            // BT_ClosePrintSet
            // 
            this.BT_ClosePrintSet.FlatAppearance.BorderSize = 0;
            this.BT_ClosePrintSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_ClosePrintSet.Image = global::GS_STB.Properties.Resources.delete;
            this.BT_ClosePrintSet.Location = new System.Drawing.Point(210, 88);
            this.BT_ClosePrintSet.Name = "BT_ClosePrintSet";
            this.BT_ClosePrintSet.Size = new System.Drawing.Size(50, 55);
            this.BT_ClosePrintSet.TabIndex = 5;
            this.BT_ClosePrintSet.UseVisualStyleBackColor = true;
            // 
            // BT_SevePrintSettings
            // 
            this.BT_SevePrintSettings.FlatAppearance.BorderSize = 0;
            this.BT_SevePrintSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_SevePrintSettings.Image = global::GS_STB.Properties.Resources._04;
            this.BT_SevePrintSettings.Location = new System.Drawing.Point(9, 88);
            this.BT_SevePrintSettings.Name = "BT_SevePrintSettings";
            this.BT_SevePrintSettings.Size = new System.Drawing.Size(49, 57);
            this.BT_SevePrintSettings.TabIndex = 2;
            this.BT_SevePrintSettings.UseVisualStyleBackColor = true;
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
            // WorkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1712, 824);
            this.Controls.Add(this.GB_Work);
            this.Controls.Add(this.GB_UserData);
            this.Name = "WorkForm";
            this.Text = "WorkForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.GB_UserData.ResumeLayout(false);
            this.GB_UserData.PerformLayout();
            this.GB_Work.ResumeLayout(false);
            this.GB_Work.PerformLayout();
            this.Desassembly_STBGroup.ResumeLayout(false);
            this.Desassembly_STBGroup.PerformLayout();
            this.FAS_Print.ResumeLayout(false);
            this.FAS_Print.PerformLayout();
            this.GB_PrinterSettings.ResumeLayout(false);
            this.GB_PrinterSettings.PerformLayout();
            this.IDPrint.ResumeLayout(false);
            this.IDPrint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YID)).EndInit();
            this.SNPRINT.ResumeLayout(false);
            this.SNPRINT.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.XSN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YSN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DG_UpLog)).EndInit();
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.FAS_EndGB.ResumeLayout(false);
            this.GroupBox7.ResumeLayout(false);
            this.GroupBox7.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GB_UserData;
        internal System.Windows.Forms.Button BT_LogOut;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox TB_RFIDIn;
        private System.Windows.Forms.GroupBox GB_Work;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.Button BT_CloseApp;
        private System.Windows.Forms.DataGridView GridInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        internal System.Windows.Forms.Label Controllabel;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox SerialTextBox;
        internal System.Windows.Forms.DataGridView DG_UpLog;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.Label LBPrintSN;
        internal System.Windows.Forms.Label CurrrentTimeLabel;
        private System.Windows.Forms.Timer Times;
        internal System.Windows.Forms.Label Label_ShiftCounter;
        internal System.Windows.Forms.GroupBox GB_PrinterSettings;
        internal System.Windows.Forms.Button BT_ClosePrintSet;
        internal System.Windows.Forms.Button BT_SevePrintSettings;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Button BT_PrinterSettings;
        internal System.Windows.Forms.GroupBox FAS_Print;
        internal System.Windows.Forms.ProgressBar PB;
        internal System.Windows.Forms.GroupBox Desassembly_STBGroup;
        internal System.Windows.Forms.Button BT_Disassebly;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox CB_INFO;
        internal System.Windows.Forms.ComboBox CB_ErrorCode;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.GroupBox FAS_EndGB;
        internal System.Windows.Forms.GroupBox GroupBox7;
        internal System.Windows.Forms.Label NextBoxNum;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Label PalletNum;
        internal System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.Label BoxNum;
        private System.Windows.Forms.Label ProgressbarText;
        private System.Windows.Forms.Button ClearBT;
        private System.Windows.Forms.NumericUpDown XSN;
        private System.Windows.Forms.GroupBox IDPrint;
        private System.Windows.Forms.NumericUpDown XID;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown YID;
        private System.Windows.Forms.GroupBox SNPRINT;
        private System.Windows.Forms.NumericUpDown YSN;
        private System.Windows.Forms.CheckBox CHPrintID;
        private System.Windows.Forms.CheckBox CHPrintSN;
        private System.Windows.Forms.Label CountLBSN;
        private System.Windows.Forms.Label CountLBID;
        private System.Windows.Forms.Label SaveSettingBT;
    }
}