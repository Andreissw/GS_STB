using GS_STB.Class_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB
{
    public partial class WorkForm : Form
    {
        private BaseClass BaseC;     
        string UserName { get; set; }
        List<object> ListLoggin = new List<object>();

        readonly List<string> ListInfoGrid = new List<string>() { "Название приложения", "Название станции","Линия","Номер Лота","Полное имя лота","Модель" };

        public WorkForm(BaseClass BC)
        {
            InitializeComponent();
            BaseC = BC;
            this.Text = BaseC.GetType().Name;
            GetLoggin();//Настройка форма ввода пароля

            TB_RFIDIn.KeyDown += (a, e) => //Событие при вводе логина
            {
                if (e.KeyCode == Keys.Enter)
                    if (GetLoginData()) //Метод, которы проверяет логин и добавляет в ArrayList данные о пользователе    
                    {
                        TB_RFIDIn.Clear(); TB_RFIDIn.Select();
                    }
            };

        }

        public WorkForm(BaseClass BC,int LOTID)
        {
            InitializeComponent();
            
            BaseC = BC; //Приведение к типу
            BaseC.LOTID = LOTID;
            this.Text = BaseC.GetType().Name; //Заголовок формы называется именем модулем
            GetLoggin();//Настройка форма ввода пароля
            Times.Enabled = true; LBPrintSN.Text = DateTime.Now.ToString("dd.MM.yyyy"); //Настройка времени
            Controllabel.Text = "";

            //Реализация загрузки определенного класса

            ClearBT.Click += (a, e) =>
            {
                SerialTextBox.Enabled = true;
                SerialTextBox.Clear();
                SerialTextBox.Select();
            };

            TB_RFIDIn.KeyDown += (a, e) => //Событие при вводе логина
            {
                if (e.KeyCode == Keys.Enter)
                    if (GetLoginData()) //Метод, которы проверяет логин и добавляет в ArrayList данные о пользователе    
                    {
                        TB_RFIDIn.Clear(); TB_RFIDIn.Select();
                    }           
            };

            SerialTextBox.KeyDown += (a, e) => //Событие скнирование номера
            {
                if (e.KeyCode == Keys.Enter)
                { 
                    BaseC.KeyDownMethod();
                    BaseC.control = this;

                    if (BaseC.GetType() == typeof(UploadStation))
                        return;

                    SerialTextBox.Clear(); SerialTextBox.Select();
                }
            };

            //Сохранение координат
            BT_SevePrintSettings.Click += (a, e) => 
            {
                //var list = new List<string>() { IDX.Value.ToString(), SNX.Value.ToString(), IDY.Value.ToString(), SNY.Value.ToString()};

                //foreach (var item in pathlist)
                //{
                //    var line = item.Substring(0, 22) + list[pathlist.IndexOf(item)] + ".txt";
                //    File.Move(item,line) ;
                //}

                if (BaseC.CheckPathPrinterSettings())
                    BaseC.CreatePathPrinter();

                if (SNPRINT.Visible == true)
                    SaveSettingPrint(SNPRINT);
                if (IDPrint.Visible == true)
                    SaveSettingPrint(IDPrint);




            };

            BT_PrinterSettings.Click += (a, e) => { GB_PrinterSettings.Visible = true; };
            BT_ClosePrintSet.Click += (a, e) => { GB_PrinterSettings.Visible = false; SaveSettingBT.Text = ""; };

            CB_ErrorCode.TextChanged += (a, e) => 
            {
                CB_ErrorCode.MaxLength = 2;
                if (CB_ErrorCode.Text.Length == 2)
                    BT_Disassebly.Select();
            };

            BT_Disassebly.Click += (a, e) => 
            {
                if (string.IsNullOrEmpty(CB_ErrorCode.Text))
                { MessageBox.Show("Укажите код ошибки"); CB_ErrorCode.Select();  return; }

                if (CheckErrocode(CB_ErrorCode.Text))
                { MessageBox.Show("Не корректный код ошибки"); CB_ErrorCode.Select(); return; }

                var Dis = new Desassembly_STB();
                var ErrorCodeID =  Dis.GetErrorCodeID(CB_ErrorCode.Text);
                var serial = int.Parse(SerialTextBox.Text.Substring(16));

                BaseC.WriteToDBDesis(serial, SerialTextBox.Text,ErrorCodeID);
                BaseC.UpdateToDBDesis(serial);
                BaseC.DeleteToDBDesis(serial);
                BaseC.LabelStatus(Controllabel,$@"Серийный номер { SerialTextBox.Text }/n ОТКРЕПЛЁН УСПЕШНО",Color.Green);
                BaseC.AddLogDesis(serial, Dis.IDApp);
                Label_ShiftCounter.Text = (int.Parse(Label_ShiftCounter.Text) + 1).ToString();
                BaseC.ShiftCounterUpdate();
                DG_UpLog.Rows.Add(int.Parse(Label_ShiftCounter.Text), SerialTextBox.Text, CB_ErrorCode.Text, DateTime.UtcNow.AddHours(2));
                DG_UpLog.Sort(DG_UpLog.Columns[0], System.ComponentModel.ListSortDirection.Descending);
                SerialTextBox.Clear();
                CB_ErrorCode.Text = "";
                BT_Disassebly.Enabled = false;
                SerialTextBox.Select();
            };
        }

        void SaveSettingPrint(GroupBox GB)
        {
            foreach (var n in GB.Controls.OfType<NumericUpDown>())
            {
                var line = Directory.GetFiles(@"C:\PrinterSettings").Where(c => c.Contains(n.Name)).FirstOrDefault();
                if (string.IsNullOrEmpty(line))
                    return;
                var line2 = line.Substring(0, 23) + n.Value.ToString() + ".txt";

                File.Move(line, line2);
                SaveSettingBT.Text = "Сохранено!";
            }
        }

        bool CheckErrocode(string Err)
        {
            foreach (var item in CB_ErrorCode.Items)
            {
                if (item.ToString() == Err)               
                    return false;
                return true;
            }
            return true;

        }
        void GetWorkForm()// Инициализация компонентов рабочей формы
        {
            GB_Work.Visible = true;
            GB_Work.Location = new Point(12, 12);
            GB_Work.Size = new Size(1400, 800);
            GetGridSetting();
            BaseC.control = this;
            BaseC.LoadWorkForm();
            SetInfoGrid();            
            SerialTextBox.Select();
        }

        bool GetLoginData() //Проверка логина и получение данных о пользователе
        {
            if (GetUser())   //Проверка правильности пароля         
                return true;
            
            GetWorkForm();
            return false;
        }

        bool GetUser()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                UserName = FAS.FAS_Users.Where(c => c.RFID == TB_RFIDIn.Text && c.IsActiv == true).Select(c => c.UserName).FirstOrDefault();
                if (string.IsNullOrEmpty(UserName))
                    return true;
                BaseC.UserID = FAS.FAS_Users.Where(c => c.RFID == TB_RFIDIn.Text && c.IsActiv == true).Select(c => c.UserID).FirstOrDefault();
                //BaseC.ArrayList.Add(Name);
                return false;
            }
        }
        void GetLoggin() //Настройка форма ввода пароля
        {
            GB_UserData.Visible = true;
            GB_UserData.Location = new Point(12,12);
            GB_UserData.Size = new Size(429, 177);
            TB_RFIDIn.Select();
        }

    

        void SetInfoGrid()
        {
            if (BaseC.GetType() == typeof(FAS_END))
            { ListInfoGrid.AddRange(new List<string>() {"Литера","Кол-во в групповой","Кол-во в паллете" }); }

            if (BaseC.GetType() == typeof(FAS_Weight_control))
            {
                ListInfoGrid.RemoveRange(3, ListInfoGrid.Count - 3);                
                ListInfoGrid.Add("Допустимые пределы");
                BaseC.ArrayList.Add();
            }

            GridInfo.RowCount = ListInfoGrid.Count + 1;
            for (int i = 0; i < GridInfo.RowCount; i++)
            {
                if (GridInfo.RowCount - i == 1)
                {
                    GridInfo[0, i].Value = "Имя пользователя";
                    GridInfo[1, i].Value = UserName;
                    break;
                }
                GridInfo[0, i].Value = ListInfoGrid[i];
                GridInfo[1, i].Value = BaseC.ArrayList[i];
            }
        }

        void GetGridSetting()
        {
            DG_UpLog.ColumnCount = BaseC.ListHeader.Count;
            for (int i = 0; i < BaseC.ListHeader.Count; i++)            
                DG_UpLog.Columns[i].HeaderText = BaseC.ListHeader[i];
           
        }

        private void Times_Tick(object sender, EventArgs e)
                {
            CurrrentTimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }
       
    }
}
