﻿using GS_STB.Class_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Forms_Modules
{
    public partial class FixedRange : Form
    {

        int LotID;
        BaseClass BC;
        int index;
        public int STSer { get; set; }
        public int EndSer { get; set; }

        bool AbortSn = false;

        public FixedRange(int LotID)
        {
            InitializeComponent();
            LBText.Text = "В лоте присутствует разбивка серийных номеров на \n диапозоны, выберите нужный вам диапозон";
            this.LotID = LotID;            
            button2.Enabled = true;
            AbortSn = true;
        }

        public FixedRange(int LotID, BaseClass BC)
        {
            InitializeComponent();
            LBText.Text = "В лоте присутствует разбивка серийных номеров на \n диапозоны, выберите нужный вам диапозон";
            this.LotID = LotID;
            this.BC = BC;
            button2.Enabled = true;

        }

        public FixedRange(int LotID, BaseClass BC,string TEXT,int index)
        {
            InitializeComponent();
            this.index = index;
            LBText.Text = TEXT;
            this.LotID = LotID;
            this.BC = BC;
            button2.Enabled = false;


        }

        private void FixedRange_Load(object sender, EventArgs e)
        {
            GetRangeInfo();
            GridRange.ClearSelection();            
        }

        void GetInfoFasStart()
        {
            using (var fas = new FASEntities())
            {
                var list = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID).ToList().Select(b => new
                {
                    СтартДиапозон = b.RGStart,
                    КонецДиапозон = b.RGEnd,
                    b.LotID,
                    ЛитерИндекс = b.LitIndex,
                    ДатаЭтикетки = b.LabDate,
                    Номеров_в_диапозоне = fas.FAS_SerialNumbers.Where(c => c.LOTID == LotID && c.SerialNumber >= b.RGStart && c.SerialNumber <= b.RGEnd).Count(),
                    Не_использованных = fas.FAS_SerialNumbers.Where(c => c.LOTID == LotID && c.SerialNumber >= b.RGStart && c.SerialNumber <= b.RGEnd && c.IsUsed == false).Count()
                });

                //var list = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID).ToList();
                GridRange.DataSource = list.ToList();
            }
        }

        void GetInfoFasEnd()
        {
            using (var fas = new FASEntities())
            {
                var list = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID).ToList().Select(b => new
                {
                    СтартДиапозон = b.RGStart,
                    КонецДиапозон = b.RGEnd,
                    b.LotID,
                    ЛитерИндекс = b.LitIndex,
                    ДатаЭтикетки = b.LabDate,
                    Номеров_в_диапозоне = fas.FAS_SerialNumbers.Where(c => c.LOTID == LotID && c.SerialNumber >= b.RGStart && c.SerialNumber <= b.RGEnd).Count(),
                    Для_упаковки = fas.FAS_SerialNumbers.Where(c => c.LOTID == LotID && c.SerialNumber >= b.RGStart && c.SerialNumber <= b.RGEnd
                    && c.IsUsed == true && c.IsActive == true && c.IsPacked == false && c.IsUploaded == true && c.IsWeighted == true).Count(),
                    Не_использованных = fas.FAS_SerialNumbers.Where(c => c.LOTID == LotID && c.SerialNumber >= b.RGStart && c.SerialNumber <= b.RGEnd && c.IsUsed == false).Count(),                   
                });
                //var list = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID).ToList();
                GridRange.DataSource = list.ToList();
            }
        }

        void GetRangeInfo()
        {
            if (BC == null)           
                GetInfoFasStart();            
            else if (BC.GetType() == typeof(FAS_END))          
                GetInfoFasEnd();          
            else
                GetInfoFasStart();

                if (index != 0) //Условие на следующий диапозон
                {
                    int Result = 0;
                    for (int i = 0; i < GridRange.RowCount; i++) //отмечаем строки, которые можно выбрать
                    {
                        //Красим строки диаозона серым цветом, которые нельзя выбрать оператору
                        if (GridRange[3, i].Value.ToString() != (index + 1).ToString()) 
                        { GridRange.Rows[i].DefaultCellStyle.BackColor = Color.DarkGray; continue;}

                        if (GridRange[6,i].Value.ToString() == "0") //Если номеров нет
                        {continue;}

                        GridRange.Rows[i].DefaultCellStyle.BackColor = Color.Green; Result += 1;  
                    }
                    if (Result == 0) //Если номера в диапозонах закончились
                    { this.DialogResult = DialogResult.Abort;this.Close(); }

                    
                }
            }
        //}

        private void button1_Click(object sender, EventArgs e)
        {

            int I = GridRange.CurrentRow.Index;
            if (I == -1 || GridRange.RowCount == 0)
            { MessageBox.Show("Диапозон не выбран"); return; }

            if (AbortSn) // Если запускаем форму Abort
            {
                if (GridRange[6,I].Value.ToString() != "0") //Если неиспользованных не 0
                {MessageBox.Show("В дипозоне еще остались не использованные номера"); return;}

                STSer = int.Parse(GridRange[0, I].Value.ToString());
                EndSer = int.Parse(GridRange[1, I].Value.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            if (index != 0)//Если закончился диапозон, и нужно включить следующий
                if (GridRange[3,I].Value.ToString() != (index + 1).ToString()) 
                { MessageBox.Show($"Вы можете выбрать диапозон только с ЛитерИндекс - {index + 1}"); return; }

            if (GridRange[6, I].Value.ToString() == "0") 
            { MessageBox.Show("Текущий диапозон израсходован"); return; }

            BC.LitIndex = short.Parse(GridRange[3, I].Value.ToString());

            using (var fas = new FASEntities())
            {
                //Старт диапозона в литере
                BC.StartRange = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID && c.LitIndex == BC.LitIndex).Select(c => c.RGStart).Min();
                //Конец диапозона в литере
                BC.EndRange = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID && c.LitIndex == BC.LitIndex).Select(c => c.RGEnd).Max();
                //Старт диапозона в лоте
                BC.StartRangeLot = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID).Select(c => c.RGStart).Min();
                //Конец диапозона в лоте
                BC.EndRangeLot = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID).Select(c => c.RGEnd).Max();
                var l = fas.FAS_Fixed_RG.Where(c => c.LotID == LotID && c.LitIndex == BC.LitIndex).Select(c => new { c.RGStart, c.RGEnd, c.LabDate });

                BC.GridRange.RowCount = l.Count();
                BC.GridRange.ColumnCount = 3;

                int index = 0;
                foreach (var item in l)
                {                           
                    BC.GridRange[0, index].Value = item.RGStart; BC.GridRange[1, index].Value = item.RGEnd; BC.GridRange[2, index].Value = item.LabDate;                   
                    index += 1;
                }

            }

            this.DialogResult = DialogResult.OK;            
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            this.Close();
        }
    }
}