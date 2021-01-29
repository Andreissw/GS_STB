namespace GS_STB.Forms_Modules
{
    partial class FAS_LOT_Managment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridLot = new System.Windows.Forms.DataGridView();
            this.LotsGR = new System.Windows.Forms.GroupBox();
            this.BT_LOT_Info = new System.Windows.Forms.Button();
            this.BT_AddLot = new System.Windows.Forms.Button();
            this.LotInfoGR = new System.Windows.Forms.GroupBox();
            this.GridRange = new System.Windows.Forms.DataGridView();
            this.UpdateBT = new System.Windows.Forms.Button();
            this.OptionBT = new System.Windows.Forms.Button();
            this.LBOption = new System.Windows.Forms.Label();
            this.GridInfo = new System.Windows.Forms.DataGridView();
            this.c = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.BackButtonInfoLot = new System.Windows.Forms.Button();
            this.AddLotGR = new System.Windows.Forms.GroupBox();
            this.GBDateRange = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DTPic = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.NumerSN = new System.Windows.Forms.NumericUpDown();
            this.BT_RegisterNewLOT = new System.Windows.Forms.Button();
            this.CertGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HDCPGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridAddLot = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.BackButtonAddLot = new System.Windows.Forms.Button();
            this.BTRange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridLot)).BeginInit();
            this.LotsGR.SuspendLayout();
            this.LotInfoGR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).BeginInit();
            this.AddLotGR.SuspendLayout();
            this.GBDateRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumerSN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CertGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HDCPGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridAddLot)).BeginInit();
            this.SuspendLayout();
            // 
            // GridLot
            // 
            this.GridLot.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridLot.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GridLot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridLot.Location = new System.Drawing.Point(6, 19);
            this.GridLot.Name = "GridLot";
            this.GridLot.ReadOnly = true;
            this.GridLot.RowHeadersVisible = false;
            this.GridLot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridLot.Size = new System.Drawing.Size(1012, 635);
            this.GridLot.TabIndex = 0;
            // 
            // LotsGR
            // 
            this.LotsGR.Controls.Add(this.GridLot);
            this.LotsGR.Controls.Add(this.BT_LOT_Info);
            this.LotsGR.Controls.Add(this.BT_AddLot);
            this.LotsGR.Location = new System.Drawing.Point(1295, 731);
            this.LotsGR.Name = "LotsGR";
            this.LotsGR.Size = new System.Drawing.Size(1102, 184);
            this.LotsGR.TabIndex = 4;
            this.LotsGR.TabStop = false;
            this.LotsGR.Text = "Lots";
            // 
            // BT_LOT_Info
            // 
            this.BT_LOT_Info.FlatAppearance.BorderSize = 0;
            this.BT_LOT_Info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_LOT_Info.Image = global::GS_STB.Properties.Resources.info;
            this.BT_LOT_Info.Location = new System.Drawing.Point(1024, 94);
            this.BT_LOT_Info.Name = "BT_LOT_Info";
            this.BT_LOT_Info.Size = new System.Drawing.Size(62, 61);
            this.BT_LOT_Info.TabIndex = 3;
            this.BT_LOT_Info.UseVisualStyleBackColor = true;
            this.BT_LOT_Info.Click += new System.EventHandler(this.BT_LOT_Info_Click);
            // 
            // BT_AddLot
            // 
            this.BT_AddLot.FlatAppearance.BorderSize = 0;
            this.BT_AddLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_AddLot.Image = global::GS_STB.Properties.Resources.document__1_;
            this.BT_AddLot.Location = new System.Drawing.Point(1024, 19);
            this.BT_AddLot.Name = "BT_AddLot";
            this.BT_AddLot.Size = new System.Drawing.Size(59, 69);
            this.BT_AddLot.TabIndex = 2;
            this.BT_AddLot.UseVisualStyleBackColor = true;
            this.BT_AddLot.Click += new System.EventHandler(this.BT_AddLot_Click);
            // 
            // LotInfoGR
            // 
            this.LotInfoGR.Controls.Add(this.BTRange);
            this.LotInfoGR.Controls.Add(this.GridRange);
            this.LotInfoGR.Controls.Add(this.UpdateBT);
            this.LotInfoGR.Controls.Add(this.OptionBT);
            this.LotInfoGR.Controls.Add(this.LBOption);
            this.LotInfoGR.Controls.Add(this.GridInfo);
            this.LotInfoGR.Controls.Add(this.label1);
            this.LotInfoGR.Controls.Add(this.BackButtonInfoLot);
            this.LotInfoGR.Location = new System.Drawing.Point(12, 24);
            this.LotInfoGR.Name = "LotInfoGR";
            this.LotInfoGR.Size = new System.Drawing.Size(1394, 683);
            this.LotInfoGR.TabIndex = 5;
            this.LotInfoGR.TabStop = false;
            this.LotInfoGR.Text = "LotInfo";
            this.LotInfoGR.Visible = false;
            // 
            // GridRange
            // 
            this.GridRange.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridRange.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridRange.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.GridRange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridRange.DefaultCellStyle = dataGridViewCellStyle12;
            this.GridRange.Location = new System.Drawing.Point(606, 19);
            this.GridRange.Name = "GridRange";
            this.GridRange.RowHeadersVisible = false;
            this.GridRange.Size = new System.Drawing.Size(581, 329);
            this.GridRange.TabIndex = 146;
            this.GridRange.Visible = false;
            this.GridRange.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridRange_CellBeginEdit);
            this.GridRange.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRange_CellEndEdit);
            // 
            // UpdateBT
            // 
            this.UpdateBT.FlatAppearance.BorderSize = 0;
            this.UpdateBT.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.UpdateBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.AppWorkspace;
            this.UpdateBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBT.Image = global::GS_STB.Properties.Resources.Save_as_37111;
            this.UpdateBT.Location = new System.Drawing.Point(1193, 521);
            this.UpdateBT.Name = "UpdateBT";
            this.UpdateBT.Size = new System.Drawing.Size(85, 74);
            this.UpdateBT.TabIndex = 145;
            this.UpdateBT.TabStop = false;
            this.UpdateBT.UseVisualStyleBackColor = true;
            this.UpdateBT.Visible = false;
            this.UpdateBT.Click += new System.EventHandler(this.UpdateBT_Click);
            // 
            // OptionBT
            // 
            this.OptionBT.FlatAppearance.BorderSize = 0;
            this.OptionBT.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.OptionBT.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ButtonFace;
            this.OptionBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OptionBT.Image = global::GS_STB.Properties.Resources.OFF2;
            this.OptionBT.Location = new System.Drawing.Point(1193, 56);
            this.OptionBT.Name = "OptionBT";
            this.OptionBT.Size = new System.Drawing.Size(146, 87);
            this.OptionBT.TabIndex = 144;
            this.OptionBT.TabStop = false;
            this.OptionBT.UseVisualStyleBackColor = true;
            this.OptionBT.Click += new System.EventHandler(this.OptionBT_Click);
            // 
            // LBOption
            // 
            this.LBOption.AutoSize = true;
            this.LBOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LBOption.Location = new System.Drawing.Point(1193, 19);
            this.LBOption.Name = "LBOption";
            this.LBOption.Size = new System.Drawing.Size(197, 20);
            this.LBOption.TabIndex = 143;
            this.LBOption.Text = "Редактирование данных";
            // 
            // GridInfo
            // 
            this.GridInfo.AllowUserToAddRows = false;
            this.GridInfo.AllowUserToResizeColumns = false;
            this.GridInfo.AllowUserToResizeRows = false;
            this.GridInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GridInfo.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.GridInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridInfo.ColumnHeadersVisible = false;
            this.GridInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c,
            this.Column1});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridInfo.DefaultCellStyle = dataGridViewCellStyle14;
            this.GridInfo.Location = new System.Drawing.Point(10, 19);
            this.GridInfo.Name = "GridInfo";
            this.GridInfo.ReadOnly = true;
            this.GridInfo.RowHeadersVisible = false;
            this.GridInfo.Size = new System.Drawing.Size(590, 576);
            this.GridInfo.TabIndex = 141;
            this.GridInfo.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridInfo_CellBeginEdit);
            this.GridInfo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridInfo_CellEndEdit);
            this.GridInfo.SelectionChanged += new System.EventHandler(this.GridInfo_SelectionChanged);
            // 
            // c
            // 
            this.c.HeaderText = "";
            this.c.Name = "c";
            this.c.ReadOnly = true;
            this.c.Width = 5;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(14, 601);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 24);
            this.label1.TabIndex = 140;
            this.label1.Text = "Назад";
            // 
            // BackButtonInfoLot
            // 
            this.BackButtonInfoLot.FlatAppearance.BorderSize = 0;
            this.BackButtonInfoLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButtonInfoLot.Image = global::GS_STB.Properties.Resources.arrow_full_left_32;
            this.BackButtonInfoLot.Location = new System.Drawing.Point(6, 625);
            this.BackButtonInfoLot.Name = "BackButtonInfoLot";
            this.BackButtonInfoLot.Size = new System.Drawing.Size(73, 60);
            this.BackButtonInfoLot.TabIndex = 139;
            this.BackButtonInfoLot.TabStop = false;
            this.BackButtonInfoLot.UseVisualStyleBackColor = true;
            this.BackButtonInfoLot.Click += new System.EventHandler(this.BackButtonInfoLot_Click);
            // 
            // AddLotGR
            // 
            this.AddLotGR.Controls.Add(this.GBDateRange);
            this.AddLotGR.Controls.Add(this.label3);
            this.AddLotGR.Controls.Add(this.NumerSN);
            this.AddLotGR.Controls.Add(this.BT_RegisterNewLOT);
            this.AddLotGR.Controls.Add(this.CertGrid);
            this.AddLotGR.Controls.Add(this.HDCPGrid);
            this.AddLotGR.Controls.Add(this.GridAddLot);
            this.AddLotGR.Controls.Add(this.label2);
            this.AddLotGR.Controls.Add(this.BackButtonAddLot);
            this.AddLotGR.Location = new System.Drawing.Point(1426, 59);
            this.AddLotGR.Name = "AddLotGR";
            this.AddLotGR.Size = new System.Drawing.Size(98, 51);
            this.AddLotGR.TabIndex = 6;
            this.AddLotGR.TabStop = false;
            this.AddLotGR.Text = "AddLot";
            this.AddLotGR.Visible = false;
            // 
            // GBDateRange
            // 
            this.GBDateRange.Controls.Add(this.label4);
            this.GBDateRange.Controls.Add(this.DTPic);
            this.GBDateRange.Location = new System.Drawing.Point(614, 70);
            this.GBDateRange.Name = "GBDateRange";
            this.GBDateRange.Size = new System.Drawing.Size(235, 81);
            this.GBDateRange.TabIndex = 150;
            this.GBDateRange.TabStop = false;
            this.GBDateRange.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 20);
            this.label4.TabIndex = 149;
            this.label4.Text = "Начало даты в диапазоне";
            // 
            // DTPic
            // 
            this.DTPic.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DTPic.Location = new System.Drawing.Point(10, 39);
            this.DTPic.Name = "DTPic";
            this.DTPic.Size = new System.Drawing.Size(178, 26);
            this.DTPic.TabIndex = 148;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(610, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 20);
            this.label3.TabIndex = 147;
            this.label3.Text = "SNIN - количество в лоте";
            // 
            // NumerSN
            // 
            this.NumerSN.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NumerSN.Location = new System.Drawing.Point(614, 41);
            this.NumerSN.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.NumerSN.Name = "NumerSN";
            this.NumerSN.Size = new System.Drawing.Size(219, 27);
            this.NumerSN.TabIndex = 146;
            // 
            // BT_RegisterNewLOT
            // 
            this.BT_RegisterNewLOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BT_RegisterNewLOT.Location = new System.Drawing.Point(1005, 531);
            this.BT_RegisterNewLOT.Name = "BT_RegisterNewLOT";
            this.BT_RegisterNewLOT.Size = new System.Drawing.Size(226, 73);
            this.BT_RegisterNewLOT.TabIndex = 145;
            this.BT_RegisterNewLOT.Text = "Зарегистрировать ЛОТ";
            this.BT_RegisterNewLOT.UseVisualStyleBackColor = true;
            this.BT_RegisterNewLOT.Click += new System.EventHandler(this.BT_RegisterNewLOT_Click);
            // 
            // CertGrid
            // 
            this.CertGrid.AllowUserToAddRows = false;
            this.CertGrid.AllowUserToResizeColumns = false;
            this.CertGrid.AllowUserToResizeRows = false;
            this.CertGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CertGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CertGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.CertGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CertGrid.ColumnHeadersVisible = false;
            this.CertGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CertGrid.DefaultCellStyle = dataGridViewCellStyle16;
            this.CertGrid.Location = new System.Drawing.Point(897, 247);
            this.CertGrid.Name = "CertGrid";
            this.CertGrid.RowHeadersVisible = false;
            this.CertGrid.Size = new System.Drawing.Size(334, 222);
            this.CertGrid.TabIndex = 144;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // HDCPGrid
            // 
            this.HDCPGrid.AllowUserToAddRows = false;
            this.HDCPGrid.AllowUserToResizeColumns = false;
            this.HDCPGrid.AllowUserToResizeRows = false;
            this.HDCPGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.HDCPGrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HDCPGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.HDCPGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HDCPGrid.ColumnHeadersVisible = false;
            this.HDCPGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.HDCPGrid.DefaultCellStyle = dataGridViewCellStyle18;
            this.HDCPGrid.Location = new System.Drawing.Point(897, 19);
            this.HDCPGrid.Name = "HDCPGrid";
            this.HDCPGrid.RowHeadersVisible = false;
            this.HDCPGrid.Size = new System.Drawing.Size(334, 222);
            this.HDCPGrid.TabIndex = 143;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // GridAddLot
            // 
            this.GridAddLot.AllowUserToAddRows = false;
            this.GridAddLot.AllowUserToResizeColumns = false;
            this.GridAddLot.AllowUserToResizeRows = false;
            this.GridAddLot.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridAddLot.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GridAddLot.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.GridAddLot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridAddLot.ColumnHeadersVisible = false;
            this.GridAddLot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GridAddLot.DefaultCellStyle = dataGridViewCellStyle20;
            this.GridAddLot.Location = new System.Drawing.Point(10, 19);
            this.GridAddLot.Name = "GridAddLot";
            this.GridAddLot.RowHeadersVisible = false;
            this.GridAddLot.Size = new System.Drawing.Size(598, 588);
            this.GridAddLot.TabIndex = 142;
            this.GridAddLot.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridAddLot_CellContentClick);
            this.GridAddLot.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridAddLot_CellEndEdit);
            this.GridAddLot.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridAddLot_CellValueChanged);
            this.GridAddLot.SelectionChanged += new System.EventHandler(this.GridAddLot_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(6, 612);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 20);
            this.label2.TabIndex = 139;
            this.label2.Text = "Назад";
            // 
            // BackButtonAddLot
            // 
            this.BackButtonAddLot.FlatAppearance.BorderSize = 0;
            this.BackButtonAddLot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackButtonAddLot.Image = global::GS_STB.Properties.Resources.arrow_full_left_32;
            this.BackButtonAddLot.Location = new System.Drawing.Point(6, 625);
            this.BackButtonAddLot.Name = "BackButtonAddLot";
            this.BackButtonAddLot.Size = new System.Drawing.Size(56, 58);
            this.BackButtonAddLot.TabIndex = 138;
            this.BackButtonAddLot.TabStop = false;
            this.BackButtonAddLot.UseVisualStyleBackColor = true;
            this.BackButtonAddLot.Click += new System.EventHandler(this.BackButtonAddLot_Click);
            // 
            // BTRange
            // 
            this.BTRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BTRange.Location = new System.Drawing.Point(379, 600);
            this.BTRange.Name = "BTRange";
            this.BTRange.Size = new System.Drawing.Size(221, 48);
            this.BTRange.TabIndex = 148;
            this.BTRange.Text = "диапазон";
            this.BTRange.UseVisualStyleBackColor = true;
            this.BTRange.Click += new System.EventHandler(this.button1_Click);
            // 
            // FAS_LOT_Managment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1855, 861);
            this.Controls.Add(this.AddLotGR);
            this.Controls.Add(this.LotInfoGR);
            this.Controls.Add(this.LotsGR);
            this.Name = "FAS_LOT_Managment";
            this.Text = "FAS_LOT_Managment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FAS_LOT_Managment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridLot)).EndInit();
            this.LotsGR.ResumeLayout(false);
            this.LotInfoGR.ResumeLayout(false);
            this.LotInfoGR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridInfo)).EndInit();
            this.AddLotGR.ResumeLayout(false);
            this.AddLotGR.PerformLayout();
            this.GBDateRange.ResumeLayout(false);
            this.GBDateRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumerSN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CertGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HDCPGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridAddLot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GridLot;
        internal System.Windows.Forms.Button BT_AddLot;
        internal System.Windows.Forms.Button BT_LOT_Info;
        private System.Windows.Forms.GroupBox LotsGR;
        private System.Windows.Forms.GroupBox LotInfoGR;
        private System.Windows.Forms.GroupBox AddLotGR;
        private System.Windows.Forms.Button BackButtonAddLot;
        private System.Windows.Forms.Button BackButtonInfoLot;
        private System.Windows.Forms.DataGridView GridInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn c;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label LBOption;
        private System.Windows.Forms.Button OptionBT;
        private System.Windows.Forms.Button UpdateBT;
        private System.Windows.Forms.DataGridView GridRange;
        private System.Windows.Forms.DataGridView GridAddLot;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView HDCPGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView CertGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        internal System.Windows.Forms.Button BT_RegisterNewLOT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown NumerSN;
        private System.Windows.Forms.GroupBox GBDateRange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DTPic;
        private System.Windows.Forms.Button BTRange;
    }
}