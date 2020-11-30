using GS_STB.Class_Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Forms_Modules
{
    public partial class SettingsForm : Form
    {
        private BaseClass BaseC;
        readonly List<string> ListInfoGrid = new List<string>() {"Название приложения","Название станции","Линия"};
        public int LineID { get; set; }

        public SettingsForm(BaseClass BC) //Конструктор, при инициализации Формы, инииализируются компоненты и методы
        {
            InitializeComponent();
            this.BaseC = BC;

            var list = new List<string>();
            foreach (var item in PrinterSettings.InstalledPrinters)           
                list.Add(item.ToString());

            BaseC.printName = list.Where(c => c.Contains("ZDesigner")).FirstOrDefault();


            SetInfoArray(); //Добавление в ArrayList информацию о модуле и ПК

            if (GetStationID())   //Если Имя компьютера отсутствует в базе, то добавляем в базу      
                BaseC.StationID = RegisterStation(); //Регистрация компютера и просвение ID    
            RB_Local_DateTime.CheckedChanged += (a, e) => { if (RB_Local_DateTime.Checked) { DateTimePicker.Enabled = true; return; } DateTimePicker.Enabled = false;      };
            BT_SaveLine.Click += (a, e) =>  //Событие Кнопка настройки линии (Добавление/Исправление)
            {
                if (string.IsNullOrEmpty(CB_Line.Text))    //Проверяем была ли выбрана линия           
                    return;

                if (CheckApp()) //Проверяем настроена ли была линия ранее, если да то update, если нет то добалвяет 
                    AddLine();  
                else
                    updateLine();

                if (BaseC.ArrayList.Count == 2)
                { BaseC.ArrayList.Add(CB_Line.Text) ; GetComponent(); return; }

                BaseC.ArrayList[2] = CB_Line.Text;
                GetComponent();
            };

            CB_Line.TextChanged += (a, e) => //Событие Выбор линии, поиск LineID по событию
            {
                GetLineID();
            };

            BTBack.Click += (a, e) => //Кнопка вернутся 
            {
                GetComponent();
            };

            DG_LOTList.CellClick += (a, e) => 
            {
                
            };

            if (GetLineName()) //Достаем имя линии, если имя линии нет, то настраиваем линию
            { GetSettingLine(); BTBack.Visible = false; label2.Visible = true; return; }

            //===============================================================================================
            //===============================================================================================

            GetComponent();

        }

        private void GetComponent()
        {
            SetInfoGrid();//Добавление в грид информацию с ArrayList 
            DG_LOTList.Visible = true;
            BT_OpenWorkForm.Visible = true;
            GBInfo.Visible = true;
            SettingLot.Visible = true;
            BaseC.control = this;
            BaseC.GetComponentClass(); //Переопределенный метод получение компонентов под определнный модуль          

            BT_OpenWorkForm.Click += (a, e) => //Кнопка открытие WorkForm
            {
                if (BaseC.GetType() == typeof(FAS_Weight_control))
                    { OpenForm(BaseC); return; }
               
                if (DG_LOTList.CurrentRow.Index == -1|| DG_LOTList.Rows.Count == 0) { MessageBox.Show("Лот не выбран!"); return; }  OpenForm(BaseC); 
            };

            BT_OpenSettings.Click += (a, e) => //Кнопка открытие настройки линии
            { OffComponents(LineSettings); BTBack.Visible = true; label2.Visible = false; GetSettingLine(); };
            
        }

        void updateLine()
        {
            using (FASEntities Fas = new FASEntities())
            {
                var App = Fas.FAS_App_ListForPC.Where(c => c.StationID == BaseC.StationID & c.App_ID == BaseC.IDApp);
                App.FirstOrDefault().LineID = (byte)this.LineID;
                App.FirstOrDefault().CreateDate = DateTime.UtcNow.AddHours(2);
                Fas.SaveChanges();
            }
        }

        void AddLine()
        {
            using (FASEntities FAS = new FASEntities())
            {
                var App = new FAS_App_ListForPC()
                {
                    App_ID = (short)BaseC.IDApp,
                    LineID = (byte)this.LineID,
                    StationID = (short)BaseC.StationID,
                    CreateDate = DateTime.UtcNow.AddHours(2)
                };
                FAS.FAS_App_ListForPC.Add(App);
                FAS.SaveChanges();
            }
        }

        void GetLineID()
        {
            using (FASEntities FAS = new FASEntities())
            {
                LineID = FAS.FAS_Lines.Where(c => c.LineName == CB_Line.Text).Select(c => c.LineID).FirstOrDefault();
            }
        }     

        bool CheckApp()
        {
            using (FASEntities Fas = new FASEntities())
            {
                var app = Fas.FAS_App_ListForPC.Where(c => c.StationID == BaseC.StationID && c.App_ID == BaseC.IDApp).Select(c=>c.ID).FirstOrDefault();
                if (app == 0)
                    return true;
                return false;
            }
        }
      
        void OpenForm(BaseClass BC)
        {
            //Передаем свойства всех элементов формы (Control)  в базовый класс
            BaseC.control = this;

            if (BaseC.GetType() == typeof(FAS_Weight_control))
            {
                WorkForm workForms = new WorkForm(BC);              
                BaseC.GetPortName();               
                workForms.ShowDialog();                
                return;
            }

                //очистка данных после 3 индекса в ArrayList
                if (BaseC.ArrayList.Count > 3)
                BaseC.ArrayList.RemoveRange(3, BaseC.ArrayList.Count - 3);

            //Добавление LOTID из грида по выделенной строке 
            int LOTID = int.Parse(DG_LOTList[6, DG_LOTList.CurrentRow.Index].Value.ToString());

            //Добавление LOTCode из грида по выделенной строке 
            BaseC.LotCode = int.Parse(DG_LOTList[0, DG_LOTList.CurrentRow.Index].Value.ToString());

            //Условие, если базовый класс приведен к типу FASStart
            if (BaseC.GetType() == typeof(FASStart))
            {
                BaseC.labelCount = int.Parse(TB_LabelSNCount.Text); //Указываем сколько этикеток печатать
                BaseC.UpPrintSN = CheckBoxSN.Checked; //Указываем надо ли печатать этикетку
                                                      //Условие выбора сетевого времени или локального указанного вручную
                if (RB_Server_Time.Checked)
                    BaseC.DateFas_Start = true; //Сетевое
                else
                { BaseC.DateFas_Start = false; BaseC.DateFas_ST_Text = DateTimePicker.Value.ToString("dd.MM.yyyy"); } //Локальное

            }

            //Условие, если базовый класс приведен к типу UploadStation
            if (BaseC.GetType() == typeof(UploadStation))
            {               
                BaseC.UpPrintID = CHID.Checked; //Указываем надо ли печатать этикетку ID номер 
                BaseC.UpPrintSN = CHSN.Checked; //Указываем надо ли печатать этикетку SN номер 
                BaseC.UpPrintCountID = int.Parse(TB_LabelIDCount.Text); //Количество ID этикеток
                BaseC.UpPrintCountSN = int.Parse(TBCHSN.Text); //Количество SN этикеток
                BaseC.CheckBoxDublicateSCID = CheckBoxDublicateSCID.Checked; //Проверка на дубликат в таблице Fas_Upload
                BaseC.GetPortName();
            }

            //Добавление данных в ArrayList из выделенного Лота
            for (int i = 0; i < 3; i++)          
                BaseC.ArrayList.Add(DG_LOTList[i,DG_LOTList.CurrentRow.Index].Value.ToString());


            //Условие, если базовый класс приведен к типу FAS_END
            if (BaseC.GetType() == typeof(FAS_END))
            {   //Опредление данных с Лота по упаковке

                //BaseC.literindex = int.Parse(DG_LOTList[3, DG_LOTList.CurrentRow.Index].Value.ToString());
                //BaseC.BoxCapacity = int.Parse(DG_LOTList[4, DG_LOTList.CurrentRow.Index].Value.ToString());
                //BaseC.PalletCapacity = int.Parse(DG_LOTList[5, DG_LOTList.CurrentRow.Index].Value.ToString());
                //Данные PalletCapacity, LiteraIndex, BoxCapacity
                for (int i = 3; i < 6; i++)
                    BaseC.ArrayList.Add(DG_LOTList[i, DG_LOTList.CurrentRow.Index].Value.ToString());
            }

         
            //Открытие формы
            WorkForm workForm = new WorkForm(BC, LOTID);           
             workForm.ShowDialog();
          
        }

        void DateVoid()
        { 
            
        }

        void GetSettingLine()
        {
            CB_Line.Items.Clear();
            LineSettings.Visible = true;
            LineSettings.Location = new Point(12, 18);
            LineSettings.Size = new Size(406, 138);

            using (FASEntities Fas = new FASEntities())
            {
                CB_Line.Items.AddRange(Fas.FAS_Lines.Where(c => c.LineName.StartsWith("FAS L")).Select(c=>c.LineName).ToArray());
            }
        }

        bool GetLineName() //Получение линии
        {
            using (FASEntities Fas = new FASEntities())
            {
                var Name =  (from app in Fas.FAS_App_ListForPC
                                join line in Fas.FAS_Lines on app.LineID equals line.LineID
                                where app.StationID == BaseC.StationID & app.App_ID == BaseC.IDApp
                                select line.LineName).FirstOrDefault();
                if (string.IsNullOrEmpty(Name))
                    return true;

                BaseC.ArrayList.Add(Name);
                BaseC.LineID = Fas.FAS_App_ListForPC.Where(c => c.StationID == BaseC.StationID & c.App_ID == BaseC.IDApp).Select(c => c.LineID).FirstOrDefault();
                return false;
            }
            
        }

        int RegisterStation()//Регистрация компютера 
        {
            
            using (FASEntities Fas = new FASEntities())
            {
                var _nameStation = BaseC.ArrayList[1].ToString();
                var Station = new FAS_Stations()
                {
                    StationName = _nameStation,
                    CreateDate = DateTime.UtcNow.AddHours(2)
                };

                Fas.FAS_Stations.Add(Station);
                Fas.SaveChanges();

                return Fas.FAS_Stations.Where(c => c.StationName == _nameStation).Select(c => c.StationID).FirstOrDefault();
            }
            
        }

        bool GetStationID()//Берем ID компьютера с базы
        {
            using (FASEntities Fas = new FASEntities())
            {
                string NameMachine = BaseC.ArrayList[1].ToString();
                BaseC.StationID = Fas.FAS_Stations.Where(c => c.StationName == NameMachine).Select(c => c.StationID).FirstOrDefault();
                if (BaseC.StationID == 0)
                    return true;
                return false;
            }
            
        }

        void SetInfoGrid() //Добавление в грид информацию с ArrayList
        {
            GridInfo.RowCount = 3;
            for (int i = 0; i < 3; i++)
            {
                GridInfo[0, i].Value = ListInfoGrid[i]; GridInfo[1, i].Value = BaseC.ArrayList[i];
            }
        }

        void SetInfoArray() //Добавление в ArrayList информацию о модуле и ПК
        { 
            BaseC.ArrayList = new ArrayList();
            BaseC.ArrayList.Add(BaseC.GetType().Name);
            BaseC.ArrayList.Add(Environment.MachineName);
        }

        void OffComponents(GroupBox GB)
        {
            foreach (var item in Controls.OfType<GroupBox>())
            {
                if (item.Name == GB.Name)
                    continue;
                item.Visible = false;
            }
        }

        private void CHSN_CheckedChanged(object sender, EventArgs e)
        {
            if (CHSN.Checked)
                TBCHSN.Text = "3";
            if (!CHSN.Checked)
                TBCHSN.Text = "0";
        }

        private void CHID_CheckedChanged(object sender, EventArgs e)
        {
            if (CHID.Checked)
                TB_LabelIDCount.Text = "2";
            if (!CHID.Checked)
                TB_LabelIDCount.Text = "0";
        }

        private void CheckBoxSN_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxSN.Checked)
                TB_LabelSNCount.Text = "2";
            if (!CheckBoxSN.Checked)
                TB_LabelSNCount.Text = "0";
        }
    }
}
