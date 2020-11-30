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
            
            Times.Enabled = true; LBPrintSN.Text = DateTime.Now.ToString("dd.MM.yyyy"); //Настройка времени
            Controllabel.Text = "";

            CloseApp.Click += (a, e) => 
            {
                var Result = MessageBox.Show("Уверены, что хотите выйти?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                    Application.Exit();
            };

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
            TestGrid.Rows.Add(12);

            //Реализация загрузки определенного класса

            CloseApp.Click += (a, e) =>
            {
                var Result = MessageBox.Show("Уверены, что хотите выйти?","Предупреждение",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                    Application.Exit();

            };

            BackButton.Click += (a, e) => 
            {
                var Result = MessageBox.Show("Уверены, что хотите вернуться в меню настройки?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Result == DialogResult.Yes)
                    Close();

            };

            GETSNCH.Click += (a, e) =>
            {
                if (GETSNCH.Checked)               
                    BaseC.CheckGetSN = true;
                if (!GETSNCH.Checked)
                    BaseC.CheckGetSN = false;

            };

            BTPrint.Click += (a, e) =>
             {
                 if (ListPrinter.SelectedIndex == -1)
                    { MessageBox.Show("Принтер не выбран"); return; }

                 BaseC.printName = ListPrinter.SelectedItem.ToString();
                 PrintLBName.Text = "Текущий принтер \n" + BaseC.printName;
                 ListPrinter.ClearSelected();                 
             };


            ClearBT.Click += (a, e) =>
            {
                //BaseC.cts.Cancel();
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
                    TestGrid.Rows.Clear();
                    TestGrid.RowCount = 12;
                    BaseC.KeyDownMethod();

                    if (BaseC.GetType() == typeof(UploadStation))
                        return;

                    if (BaseC.GetType() == typeof(Desassembly_STB))
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

            BT_PrinterSettings.Click += (a, e) => 
            {
                GB_PrinterSettings.Visible = true; SettingDelay.Visible = true;
                foreach (Control item in SettingDelay.Controls)               
                    if (item.Name.Contains("Delay"))
                        item.Enabled = true;
                
               
            };
            BT_ClosePrintSet.Click += (a, e) => 
            { 
                GB_PrinterSettings.Visible = false; SettingDelay.Visible = false; SaveSettingBT.Text = ""; SerialTextBox.Select();
                foreach (Control item in SettingDelay.Controls)
                    if (item.Name.Contains("Delay"))
                        item.Enabled = false;
            };

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
                { MessageBox.Show("Не корректный код ошибки"); CB_ErrorCode.Select();  return; }

                var Dis = new Desassembly_STB();
                var ErrorCodeID =  Dis.GetErrorCodeID(CB_ErrorCode.Text);
                var serial = int.Parse(SerialTextBox.Text.Substring(15));
                var smID = GetSmartCardID(serial);
                var fullStbsn = GetFullSTBSN(serial);
                var casid = GetCASID(serial);
                BaseC.WriteToDBDesis(serial, SerialTextBox.Text,ErrorCodeID);
                BaseC.UpdateToDBDesis(serial);
                BaseC.DeleteToDBWeight(serial);
                BaseC.DeleteToDBDesis(serial);
                BaseC.DeleteToUpload(serial);                
                BaseC.AddLogDesis(serial, Dis.IDApp,smID,fullStbsn,casid);
                BaseC.LabelStatus(Controllabel, $"Серийный номер { SerialTextBox.Text } \n ОТКРЕПЛЁН УСПЕШНО", Color.Green);
                BaseC.ShiftCounter += 1;
                BaseC.LotCounter += 1;
                BaseC.ShiftCounterUpdate();
                BaseC.LotCounterUpdate();
                Label_ShiftCounter.Text = BaseC.ShiftCounter.ToString();
                LB_LOTCounter.Text = BaseC.LotCounter.ToString();
                SerialTextBox.Enabled = true;
                DG_UpLog.Rows.Add(int.Parse(Label_ShiftCounter.Text), SerialTextBox.Text, CB_ErrorCode.Text, DateTime.UtcNow.AddHours(2));
                DG_UpLog.Sort(DG_UpLog.Columns[0], System.ComponentModel.ListSortDirection.Descending);
                SerialTextBox.Clear();
                CB_ErrorCode.Text = "";
                BT_Disassebly.Enabled = false;
                SerialTextBox.Select();
            };
        }

        string GetCASID(int serial)
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_Upload.Where(c => c.SerialNumber == serial).Select(c => c.CASID).FirstOrDefault();
            }
            
        }

        long GetSmartCardID(int serial)
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_Upload.Where(c => c.SerialNumber == serial).Select(c => c.SmartCardID).FirstOrDefault();
            }

        }

        string GetFullSTBSN(int serial)
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_Start.Where(c => c.SerialNumber == serial).Select(c => c.FullSTBSN).FirstOrDefault();
            }

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
                if (item.ToString().Contains(Err))
                    return false;
            return true;

            //if (CB_ErrorCode.Items.Cast<List<string>>().Select(c => c.Contains(Err)).FirstOrDefault())
            //    return false;
            //return true;

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
            using (FASEntities FAS = new FASEntities())
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
                BaseC.ArrayList.Add("+/-15");
                ListInfoGrid.Add("Вес коробки");
                BaseC.ArrayList.Add(150);
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

        private void SaveDelay_Click(object sender, EventArgs e)
        {
            string line = "";
            foreach (Control item in SettingDelay.Controls)           
                if (item.Name.Contains("Delay"))
                {
                    var i = SettingDelay.Controls.IndexOf(item);
                    foreach (Control item2 in SettingDelay.Controls)
                        if (item2.TabIndex == i)
                        { 
                            line += item2.Text + ","; 
                            continue; 
                        }      
                }

            using (var fas = new FASEntities())
            {
                var F = (from lot in fas.FAS_GS_LOTs
                         join m in fas.FAS_Models on lot.ModelID equals m.ModelID
                         where lot.LOTID == BaseC.LOTID
                         select m);

                F.FirstOrDefault().DelaySetting = line;
                fas.SaveChanges();
            }

            SaveDelays.Text = "Сохранено";
        }

        private void WorkForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
