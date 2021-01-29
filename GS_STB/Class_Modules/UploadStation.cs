using GS_STB.Forms_Modules;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class UploadStation :BaseClass
    {
        byte[] arrBuffer = new byte[1024];
        int intSize;
        bool CheckDublicateSCID { get; set; }
        List<bool> ListUploadSt { get; set; }

        string CASID;
        int ShortSN;
        ArrayList ArListGSLot { get; set; }
        ArrayList ArrayLoadSnData = new ArrayList();
        SerialPort SerialPort { get; set; }
        ArrayList ArListSNnumer { get; set; }        
        string DUID { get; set; }
        List<string> listdata;
        //int LabelScenarioID { get; set; }

        //public string Model, DUID, CASID, SW_v, SWGS1_v, HDCPUpload, CertUpload, MACUpload, ReadedSN;     
         
         List<Byte> InfoBytes = new List<Byte>();
        public UploadStation()
        {
            ListHeader = new List<string>() { "№","Время прошивки", "SN", "SC ID", "CAS ID", "HDCP","CERT","MAC","LDS","SW","SS GS1","Date"};
            IDApp = 3;
            ArListGSLot = new ArrayList();

        }
        public override void GetComponentClass()
        {            
            var UploadStationGB = control.Controls.Find("UploadStationGB", true).FirstOrDefault();            
            UploadStationGB.Visible = true;
            UploadStationGB.Location = new Point(LocX, LocY);
            UploadStationGB.Size = new Size(214, 96);            
            var Grid = (DataGridView)control.Controls.Find("DG_LOTList", true).FirstOrDefault();
            GetLot(Grid);
        }

        public override void LoadWorkForm()
        {            
            Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
            Label COM = control.Controls.Find("COM", true).OfType<Label>().FirstOrDefault();
            var FUG = control.Controls.Find("FAS_Print", true).FirstOrDefault();
            var GETSNCH = control.Controls.Find("GETSNCH", true).FirstOrDefault().Visible = true;
            var SNPrint = control.Controls.Find("SNPRINT", true).FirstOrDefault();
            var IDPrint = control.Controls.Find("IDPrint", true).FirstOrDefault();

            var CHPrintSN = control.Controls.Find("CHPrintSN", true).OfType<CheckBox>().FirstOrDefault();
            var SettingDelay = control.Controls.Find("SettingDelay", true).OfType<GroupBox>().FirstOrDefault();
            var CHPrintID = control.Controls.Find("CHPrintID", true).OfType<CheckBox>().FirstOrDefault();
            var CountLBSN = control.Controls.Find("CountLBSN", true).OfType<Label>().FirstOrDefault();
            var CountLBID = control.Controls.Find("CountLBID", true).OfType<Label>().FirstOrDefault();
            var PrintLBName = control.Controls.Find("PrintLBName", true).OfType<Label>().FirstOrDefault();

            PrintLBName.Text = "Текущий принтер \n" + printName;

            var ListPrinter = control.Controls.Find("ListPrinter", true).OfType<ListBox>().FirstOrDefault();
            foreach (var item in PrinterSettings.InstalledPrinters)            
                if (item.ToString().Contains("ZDesigner"))
                    ListPrinter.Items.Add(item);

            CHPrintSN.Checked = UpPrintSN;
            CHPrintID.Checked = UpPrintID;
            CountLBSN.Text = "Кол-во этикеток SN = " + UpPrintCountSN.ToString();
            CountLBID.Text = "Кол-во этикеток ID = " + UpPrintCountID.ToString();
            if (CHPrintSN.Checked)           
                CHPrintSN.BackColor = Color.Green;
            else
                CHPrintSN.BackColor = Color.White;

            if (CHPrintID.Checked)
                CHPrintID.BackColor = Color.Green;
            else
                CHPrintID.BackColor = Color.White;

            var XSN = control.Controls.Find("XSN", true).OfType<NumericUpDown>().FirstOrDefault();
            var YSN = control.Controls.Find("YSN", true).OfType<NumericUpDown>().FirstOrDefault();
            var XID = control.Controls.Find("XID", true).OfType<NumericUpDown>().FirstOrDefault();
            var YID = control.Controls.Find("YID", true).OfType<NumericUpDown>().FirstOrDefault();
            control.Controls.Find("PB", true).FirstOrDefault().Visible = true;
            FUG.Visible = true;
            FUG.Location = new Point(264, 19);
            FUG.Size= new Size(585, 200);
            ArListGSLot = GetLotHDCP();

            if (CheckPathPrinterSettings())
                CreatePathPrinter();

            if (string.IsNullOrEmpty(COMPORT))
                COMPORT = "COM9";

            SerialPort = new SerialPort(); //Инициализация ComPorta
            SerialPort.BaudRate = 115200;
            SerialPort.DataBits = 8;
            SerialPort.PortName = COMPORT;
            SerialPort.Parity = Parity.None;
            SerialPort.StopBits = StopBits.One;
            SerialPort.Handshake = Handshake.None;
            SerialPort.Encoding = System.Text.Encoding.Default;

            try
            {
                SerialPort.Open();
                SerialPort.Close();
            }
            catch (Exception)
            {
                Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
                { LabelStatus(Controllabel, $"ComPort 9 не найден", Color.Red); }
            }

            COM.Text = COMPORT;

            var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
            XSN.Value = int.Parse(GetPrSet(list, "XSN"));
            YSN.Value = int.Parse(GetPrSet(list, "YSN"));
            XID.Value = int.Parse(GetPrSet(list, "XID"));
            YID.Value = int.Parse(GetPrSet(list, "YID"));


            if (UpPrintID)
                IDPrint.Visible = true;
            if (UpPrintSN)
                SNPrint.Visible = true;

            var Delay = GetDelaySettings();
            if (!string.IsNullOrEmpty(Delay))
            {
                var ListDelay = Delay.Split(',').ToList();
                foreach (Control item in SettingDelay.Controls)
                    if (item.Name.Contains("Delay"))
                    {
                        var i = SettingDelay.Controls.IndexOf(item);
                        foreach (Control item2 in SettingDelay.Controls)
                            if (item2.TabIndex == i)
                                item2.Text= ListDelay[i-1];
                    }
            }

            ShiftCounterStart();
            Label_ShiftCounter.Text = ShiftCounter.ToString();
            LB_LOTCounter.Text = LotCounter.ToString();
        }

        public override void KeyDownMethod()
        {
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            cts = new CancellationTokenSource();
            token = cts.Token;
            Parallel();
        }

        string _SN;
        string keyDown( )
        {
            //Инициализация  контроллеров
            var progressbar = control.Controls.Find("PB", true).OfType<ProgressBar>().FirstOrDefault();
            var Text = control.Controls.Find("ProgressbarText", true).OfType<Label>().FirstOrDefault();
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();           
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            var ClearBT = control.Controls.Find("ClearBT", true).OfType<Button>().FirstOrDefault();
            var Start = DateTime.UtcNow.AddHours(2);

            //Доработать для англ винды
            //if (string.IsNullOrEmpty(COMPORT))
            //    GetPortName();
            //if (string.IsNullOrEmpty(COMPORT))
            //{ LabelStatus(Controllabel, $"COMPORT не подключен, проверьте COMPORT", Color.Red); return "true";  }

            if (string.IsNullOrEmpty(COMPORT))
                COMPORT = "COM9";     

            //Присовение в переменную _SN значение текстобкса
            control.Invoke((Action)(() =>  {_SN = TB.Text;}));
            if (_SN.Length == 23)
            {
                //Инициализация Label, который выводит результат сканирования                
                control.Invoke((Action)(() =>
                {
                    progressbar.Value = 0;
                    progressbar.Value += 1;
                    Text.BackColor = Color.White;
                    Text.Text = "Проверка DUID приемника";
                    Controllabel.Text = "Загрузка"; 
                    Controllabel.ForeColor = Color.DodgerBlue;
                    TB.Enabled = false; //Блокировем Текстбокс во время ассинхронного метода
                    //ClearBT.Enabled = false;                    
                    SerialPort.PortName = COMPORT;
                    
                }));

                if (UpPrintID || UpPrintSN)
                    if (string.IsNullOrEmpty(printName))
                    { LabelStatus(Controllabel, $"Принтер не идентифицирован!", Color.Red); return "true"; }

                //Перед преобразованием проверяем может ли ShortSN быть типом int                
                if (!int.TryParse(_SN.Substring(15), out int k))
                { LabelStatus(Controllabel, $"Неверный формат номера {_SN}", Color.Red); return "true"; }
                ShortSN = int.Parse(_SN.Substring(15)); //Если удачно, то преобразуем ShortSN

                if (CheckPacking(ShortSN)) //Проверяем таблицу упаковки 
                { // pac.SerialNumber,   liter =,  pac.PalletNum,  pac.BoxNum, pac.UnitNum, pac.PackingDate,  us.UserName
                    var listpac = GetInfoPacing(ShortSN);
                    LabelStatus(Controllabel, $" {_SN} Приемник был упакован \n Литтер {listpac[1]}, Паллет {listpac[2]}, Коробка {listpac[3]} \n номер {listpac[4]} дата упаковки {listpac[5]}, упаковщик  {listpac[6]}", Color.Red); return "true";
                }

                //Инициализация двух перменных типа string для печати SN
                var TextCode = _SN.Substring(0, 22) + ">6" + _SN.Substring(22);
                var TextSN = _SN.Substring(0, 2) + " " + _SN.Substring(2, 4) + " " + _SN.Substring(6, 2) + " " + _SN.Substring(8, 2) +
                                  " " + _SN.Substring(10, 2) + " " + _SN.Substring(12, 3) + " " + _SN.Substring(15, 8);

                ArListSNnumer = GetSerialNum(ShortSN); //Получаем список данных по короткому серийному номеру с таблицы FAS_SerialNumber  

                //Проверка данных в ArrayList 
                if (ArListSNnumer.Count == 0)
                { LabelStatus(Controllabel, $"Серийный номер {_SN}  Не найден в этом ЛОТе!", Color.Red); return "true"; }

                //Проверка булевых на активность номера, упаковку, весовой контроль и т.д
                //if (CheckboolArListSNnumer(Controllabel))
                //    return "true";

                //Проверяем, можем ли мы преобразовать PCBID в тип int
                if (!int.TryParse(ArListSNnumer[0].ToString(), out k))
                { LabelStatus(Controllabel, $"Не удалось приобразовать в число PCBID - {ArListSNnumer[0]}", Color.Red); return "true"; }
                PCBID = int.Parse(ArListSNnumer[0].ToString()); //Если можно, то преобразовываем

                DUID = GetDUID(); //Считываем данные с приемника, читаем DUID

                //Если метод вернул "COM" то значит возникли проблемы с ComPort
                if (DUID == "COM")
                { LabelStatus(Controllabel, $"Ошибка связанная с COMPORT \n Проверьте COMPORT", Color.Red); return "true"; }

                //Если считали пустое значение, значит проблема с прошивой приемника
                if (string.IsNullOrEmpty(DUID))
                { LabelStatus(Controllabel, $"Ошибка при прошивке приемника, DUID не найден", Color.Red); return "true"; }

                //Получаем CASID = PTID + DUID
                CASID = ArListGSLot[7].ToString() + DUID;

                //Достаем с таблицы upload серийный номер по CASID
                var CASIDShortSerial = CheckCASID(CASID);
                
                //Если в базе ShortSN по CASID не совпадет с введенным ShortSN
                if (CASIDShortSerial != ShortSN & CASIDShortSerial != 0)
                {
                    //S.SerialNumber, Liter = L.LiterName + S.LiterIndex, S.PalletNum, S.BoxNum, S.UnitNum, S.PackingDate, U.UserName
                    //Проверяем был ли упакован задвоенный серийный номер по такому CASID 
                    var list = CheckSNPack(CASIDShortSerial);
                    //Если нет, то задвоение
                    if (list.Count == 0)
                        { LabelStatus(Controllabel, $"{_SN } Задвоение CASID, заблокировать данный приемник \n Приемник был прошит ранее с таким номером {CASIDShortSerial}", Color.Red); return "true"; }

                    //Если был упакован, то выводим информацию
                    LabelStatus(Controllabel, $"{list[0]} Серийный номер с таким CASID {CASID} \n был упакован. Liter {list[1]}, Pallet {list[2]}, BoxNum {list[3]} \n UnitNum {list[4]} Дата {list[5]}, Упаковал {list[6]} ", Color.Red); return "true";
                }

                //Проверка флажков 
                var Result = CheckBoolSerialNumber();
                if (Result == "false")
                { LabelStatus(Controllabel, $"{_SN} - Номер не прошел проверку SerialNumbers, отложите приемник \n сообщите об ошибке технологу", Color.Red); return "true"; }

                if (Result == "UpOk") //Если приемник уже прошит с таким номером FullSTBSN, проверка
                    if (CASIDShortSerial == 0)                    
                        using (var fas = new FASEntities())
                        {
                            var SmID = fas.FAS_Upload.Where(c => c.SerialNumber == ShortSN).Select(c => c.SmartCardID).FirstOrDefault();                       
                            LabelStatus(Controllabel, $"{_SN} - Серийный номер уже присвоен другому приемнику  \n Его SmartID - {SmID} ", Color.Red); return "true";
                        }                

                if (CASIDShortSerial == ShortSN)   //ShortSN с базы равен с введенным ShortSN 
                {
                    if (CheckBoxDublicateSCID)
                    {
                        if (!UpPrintID & !UpPrintSN) //Проверка настройка печати
                        {
                            using (var fas = new FASEntities())
                            {
                                var SmID = fas.FAS_Upload.Where(c => c.SerialNumber == CASIDShortSerial).Select(c => c.SmartCardID).FirstOrDefault();                           
                                LabelStatus(Controllabel, $"{_SN } Задвоение CASID, заблокировать данный приемник \n Приемник с SN - {CASIDShortSerial} и SmartID - {SmID} уже был прошит ранее", Color.Red); return "true";
                            }
                        }

                        var mes = new msg("Номер был прошит ранее \n желаете ли перепечатать этикетку ?");
                        mes.ShowDialog(); //разрешение на печать
                        if (mes.DialogResult == DialogResult.No)
                            return "NoPrint";

                        if (string.IsNullOrEmpty(printName))
                        { LabelStatus(Controllabel, $"Принтер не идентифицирован!", Color.Red); return "true"; }

                        using (var _fas = new FASEntities())
                        {
                            var SmartID = _fas.FAS_Upload.Where(c => c.SerialNumber == ShortSN).Select(c => c.SmartCardID).FirstOrDefault();
                            var DateText = GetManufDate(_SN);
                            Prints(TextCode, TextSN, SmartID.ToString(),DateText);
                            return "Print";
                        }
                    }
                    else //Если проверки на дубликат нет        
                        //Удаляем с Upload      //ставим флажки в SerialNumbers
                        Remove_Fas_Upload(); SetBoolSerialNumbers(ShortSN);
                }

                ArrayLoadSnData = LoadSnData(ShortSN);
                //Проверка на содерждимое arrayLista 
                if (ArrayLoadSnData.Count == 0)
                { LabelStatus(Controllabel, $@"ArrayLoadSnData пустой", Color.Red); return "true"; }

                if (StartUpload(Controllabel))
                { LabelStatus(Controllabel, $@"{_SN} Произошла ошибка при прошивке", Color.Red); return "true"; }

                control.Invoke((Action)(() =>
                { progressbar.Value += 1; Text.Text = "Прошивка завершена успешно"; }));
                //Определение SmartID
                var SmartCardID = listdata[2].ToString();

                control.Invoke((Action)(() =>
                { Text.Text = "Прошивка завершена успешно - Идёт процесс передачи данных"; Text.ForeColor = Color.Black; }));
                WriteDB(SmartCardID);

                Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
                Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
                DataGridView DG_UpLog = control.Controls.Find("DG_UpLog", true).OfType<DataGridView>().FirstOrDefault();
                control.Invoke((Action)(() =>
                {
                    var EndDate = DateTime.UtcNow.AddHours(2);
                    var Res = (EndDate - Start).TotalSeconds.ToString("#.##");
                    Label_ShiftCounter.Text = ShiftCounter.ToString();                    
                    LB_LOTCounter.Text = LotCounter.ToString();
                    DG_UpLog.Rows.Add(ShiftCounter, Res, _SN, SmartCardID, CASID, ArrayLoadSnData[2], ArrayLoadSnData[3], ArrayLoadSnData[1], "OK", listdata[3], listdata[4], DateTime.UtcNow.AddHours(2));
                    DG_UpLog.Sort(DG_UpLog.Columns[0], ListSortDirection.Descending);
                }));
                //Добавление в OperationLog строки 
                AddOpLog(TB.Text);          
                var DateTexts = GetManufDate(_SN);
                Prints(TextCode, TextSN, listdata[2],DateTexts);
                control.Invoke((Action)(() =>
                { Text.Text = "Готово"; }));
                return "false";
            }
            else
                return "er";
        }

        ArrayList CheckSNPack(int SN)
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = (from S in Fas.FAS_PackingGS
                            join U in Fas.FAS_Users on S.PackingByID equals U.UserID
                            join L in Fas.FAS_Liter on S.LiterID equals L.ID
                            where S.SerialNumber == SN
                            select new { S.SerialNumber, Liter = L.LiterName + S.LiterIndex, S.PalletNum, S.BoxNum, S.UnitNum, S.PackingDate, U.UserName });
                try
                {
                    var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                    foreach (var value in report)
                        ArrayList.Add(value);
                    return ArrayList;
                }
                catch (Exception)
                {
                    return ArrayList;
                }
            }
        }

        private void Prints(string TextCode, string TextSN, string SmartID,DateTime DateText)// Печать
        {
            if (CheckPathPrinterSettings())
                CreatePathPrinter();

            if (UpPrintSN)
            {
                var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
                var X = GetPrSet(list, "XSN");
                var Y = GetPrSet(list, "YSN");                
                print(Print.PrintSN(ArrayList[5].ToString(), TextSN, TextCode, UpPrintCountSN, DateText, int.Parse(X), int.Parse(Y)));
            }

            if (UpPrintID)
            {
                var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
                var X = GetPrSet(list, "XID");
                var Y = GetPrSet(list, "YID");
                print(Print.PrintID(SmartID, UpPrintCountID, int.Parse(X), int.Parse(Y)));
            }
        }

        void print(string content)
        {
            RawPrinterHelper.SendStringToPrinter(printName, content); //Нужно получать ответ от принтера Для Кости 
        }

        void AddOpLog(string fullstbsn)
        {
            using (var FAS = new FASEntities())
            {
                var Fas = new FAS_OperationLog()
                {
                    PCBID = PCBID,
                    ProductionAreaID = (byte)LineID,
                    StationID = (short)StationID,
                    ApplicationID = (short)IDApp,
                    StateCodeDate = DateTime.UtcNow.AddHours(2),
                    StateCodeByID = (short)UserID,
                    SerialNumber = ShortSN,
                    SmartCardId = long.Parse(listdata[2].ToString()),
                    FullSTBSN = fullstbsn,
                    CASID = CASID
                };
                FAS.FAS_OperationLog.Add(Fas);
                FAS.SaveChanges();
            }
        }
        int CheckCASID(string CASID)
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_Upload.Where(c => c.CASID == CASID).Select(c => c.SerialNumber).FirstOrDefault();
            }
        }
        void WriteDB(string SmartID)
        {
            AddFas_Upload(Convert.ToInt64(SmartID));
            UpdateSerialNumber();
            ShiftCounter += 1;
            LotCounter += 1;
            ShiftCounterUpdate();
            LotCounterUpdate();
        }

        void AddFas_Upload(long SmartCardID)
        {
            using (var FAS = new FASEntities())
            {
                var Upload = new FAS_Upload()
                {
                    SerialNumber = ShortSN,
                    MAC = ArrayLoadSnData[1].ToString(),
                    LineID = (byte)LineID,
                    SmartCardID = SmartCardID,
                    CASID = CASID,
                    SWversion = listdata[3].ToString(),
                    SWGS1version = listdata[4].ToString(),
                    LDS = true,
                    UploadDate = DateTime.UtcNow.AddHours(2),
                    UploadByID = (short)UserID
                };
                FAS.FAS_Upload.Add(Upload);
                FAS.SaveChanges();
            }
        }

        void UpdateSerialNumber()
        {
            using (var FAS = new FASEntities())
            {
                var FAS_ = FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == ShortSN);
                FAS_.FirstOrDefault().IsUploaded = true;
                FAS.SaveChanges();
            }
        }

        async void Parallel()
        {
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
            var ClearBT = control.Controls.Find("ClearBT", true).OfType<Button>().FirstOrDefault();
            var result = "true";         

            await Task.Run(() =>
            {
                result = keyDown();

            });

            if (result == "true")
                { TB.Enabled = false; ClearBT.Enabled = true; }
            
            if (result == "er")
            {
                LabelStatus(Controllabel, $@"Неверный формат номера", Color.Red);
                TB.Clear(); TB.Enabled = true; TB.Select(); ClearBT.Enabled = true;
            }

            if (result == "false")
            {
                LabelStatus(Controllabel, $@"Прошло успешо!", Color.Green);  
                TB.Clear(); TB.Enabled = true; TB.Select(); ClearBT.Enabled = true;
            }

            if (result == "Print")
            {
                LabelStatus(Controllabel, $@"Приемник ранее был добавлен в базу, печать прошла успешно", Color.Green);
                TB.Clear(); TB.Enabled = true; TB.Select(); ClearBT.Enabled = true;
            }

            if (result == "NoPrint")
            {
                LabelStatus(Controllabel, $@"Приемник ранее был добавлен в базу, печать номера отменена!", Color.Gray);
                TB.Clear(); TB.Enabled = true; TB.Select(); ClearBT.Enabled = true;
            }
        }

        delegate string Methods(Label lb);
        bool StartUpload(Label Controllabel) //Прошивка приемника
        {            
            var progressbar = control.Controls.Find("PB", true).OfType<ProgressBar>().FirstOrDefault();
            var Text = control.Controls.Find("ProgressbarText", true).OfType<Label>().FirstOrDefault();

            //Инициализация ПрогресБара         
            //Создаем с помощью делегата список методов, которые соврешают проверки
            List<Methods> ListMethods = new List<Methods>() { SetSN, GetModel, GetSmartID, GetSWVersion, GetSWGS1Version, GetHDCP, Getcert, GetMAC, GetSN,GetLDS };
            listdata = new List<string>();
            foreach (var item in ListMethods)
            {
                control.Invoke((Action)(() =>
                {
                    var procent = ((Int32)Convert.ToInt32(progressbar.Value) / ((progressbar.Maximum - 2) / 100M)).ToString("#.#");
                    Text.Text = $"{procent}% - Идёт процесс прошивки - " + item.GetMethodInfo().Name;
                    Text.ForeColor = Color.Green;
                }));
                var data = item(Controllabel);
                if (data == "")
                {
                    control.Invoke((Action)(() =>
                    {
                        progressbar.Value = 0;
                        Text.BackColor = Color.Coral;                        
                        Text.Text = $"Процесс приостановлен на методе -" + item.GetMethodInfo().Name;
                        Text.Enabled = true;                        
                    }));                    
                    return true;
                }

                control.Invoke((Action)(() =>
                {
                    progressbar.Value += 1;                                        
                }));

                listdata.Add(data);     
            }
            control.Invoke((Action)(() =>
            {                
                Text.Text = "Готово!";
            }));
            return false;
        }

        //F.PCBID 0, S.IsUsed 1, S.IsActive 2, S.IsUploaded 3, S.IsWeighted 4, S.IsPacked 5, S.InRepair 6   
        string GetLDS (Label lb)
        {
            var LDS = SetLDS();
            if (LDS == "")
            { LabelStatus(lb, "Проблема прошивки \n Метод GetLDS не прошел", Color.Red); return ""; }

            if (LDS == "COM")
            { LabelStatus(lb, $@"Ошибка связанная с COMPORT \n Проверьте COMPORT", Color.Red); return ""; }

            return "true";
        }

        string GetSN(Label lb)
        {
            if (!CheckGetSN)
                return "true";

            var ReadSN = GetSN();
            if (ReadSN != _SN)
            { LabelStatus(lb, $"Считанный номер {ReadSN} не соответсвует прошитому {_SN}", Color.Red); return ""; }
            return ReadSN;
        }

        string SetSN(Label lb)
        {         
            var Result = SetSN(ArrayLoadSnData[4].ToString());
            if (Result == "")
            { 
                LabelStatus(lb, $@"Прошивка серийного номера не удалась", Color.Red); 
                SendToCOM(DataGenerationOneByte("8D", "0DF201180091"), 300);
                return "";                
            }
            return Result;
        }
        string GetMAC(Label lb)
        {
            if (ArListGSLot[2].ToString() == "False")
                return "true";

            var Result = WriteMac(ArrayLoadSnData[1].ToString());
            if (Result == "COM")                
                { LabelStatus(lb, $@"Ошибка связанная с COMPORT \n Проверьте COMPORT", Color.Red); return ""; }

            if (Result == "false")
            { LabelStatus(lb, "Проблема прошивки \n Метод WriteMac не прошел", Color.Red); return ""; }

            return "true";
        }
        string Getcert(Label lb)
        {
            if (ArListGSLot[1].ToString() == "False")
                return "true";

            var Result = WriteCert();
            if (Result  == "COM")                
                { LabelStatus(lb, $@"Ошибка связанная с COMPORT \n Проверьте COMPORT", Color.Red); return ""; }

            if (Result == "false")
            { LabelStatus(lb, "Проблема прошивки \n Метод Getcert не прошел", Color.Red); return ""; }

            return "true";
        }
        string GetHDCP(Label lb)
        {          
            if (ArListGSLot[0].ToString() == "False")
               return "true"; 

            var Result = WriteHDCP();
            if (Result == "COM")                
                { LabelStatus(lb, $@"Ошибка связанная с COMPORT \n Проверьте COMPORT", Color.Red); return ""; }

            if (Result == "true")
            { LabelStatus(lb, "Проблема прошивки \n Метод GetHDCP не прошел", Color.Red); return ""; }

            return "true";
        }

        string GetSWGS1Version(Label lb)
        {
            if (ArListGSLot[5].ToString() == "False")
                return "true"; 

            var SWGS1_v = GetDatafromSTB("AD","2D00", "GetSWGS1Version",5, "Delay6");
            if (SWGS1_v == null)                
                { LabelStatus(lb, "Метод GetSWGS1Version  не прошел, приемник не вернул данные", Color.Red); return ""; }
                return SWGS1_v;
        }

        string GetSWVersion(Label lb)
        {
            if (ArListGSLot[4].ToString() == "False")
                 return "true"; 

            var SW_v = GetDatafromSTB("AC","2C00", "GetSWVersion",4, "Delay5");
            if (SW_v == null)               
                { LabelStatus(lb, "Метод GetDuid не прошел ,  приемник не вернул данные", Color.Red); return ""; }

                return SW_v;
        }
        string GetDUID()
        {
            return GetDatafromSTB("A7","270008","GetDUID",0, "Delay1");         
        }

        string GetSmartID(Label lb)
        {
            if (ArListGSLot[8].ToString() == "6") //Для C592/ C593 Клиента
            {
                var Result = GetClientSmartID();
                if (Result.Length != 14)
                    return "";
                return Result;                
            }

            var SmartID = GetDatafromSTB("AB", "2B000E","SmartID",3, "Delay4");
            if (string.IsNullOrEmpty(SmartID))                
                { LabelStatus(lb, "SmartID не найден, приемник не дал ответ", Color.Red); return ""; }

                return SmartID;

        }
        string GetModel(Label lb)
        {
            if (ArListGSLot[3].ToString() == "False")
                return "true";

            var Model = GetDatafromSTB("AE","2E00","GetModel",2, "Delay3");
            if (Model != ArrayList[5].ToString())               
                { LabelStatus(lb, "Модель не соответствует", Color.Red); return ""; }
                return Model;
        }

        string GetClientSmartID()
        {
            var G = control.Controls.Find("TestGrid", true).OfType<DataGridView>().FirstOrDefault();//================================            

            var Sub_Result = (Convert.ToInt32(DUID, 16)).ToString();
            if (Sub_Result.Length != 8)
            {
                string nul = "";
                for (int i = 0; i < 8 - Sub_Result.Length; i++)
                    nul = nul + "0";

                Sub_Result = nul + Sub_Result;
            }

            var Result = "0" + Sub_Result;

            var ptid = (Convert.ToInt32(ArListGSLot[7].ToString(), 16)).ToString();
            if (ptid.Length == 2)
                ptid = "0" + ptid;

            control.Invoke((Action)(() => { G[0, 3].Value = "GetClientSmartID"; }));

            Result = ptid + Result;
            var sums = Int64.Parse(Result).ToString().Sum(c => c - '0');
            return sums + Result;
        }

        ArrayList LoadSnData(int snShort)
        {
            using (var FAS = new FASEntities())
            {
                var ArrayList = new ArrayList();
                ArrayList.Add(1);
                ArrayList.Add(GenMac(snShort));

                var list = (from start in FAS.FAS_Start
                            join hdcp in FAS.FAS_HDCP on start.SerialNumber equals hdcp.SerialNumber
                            join cert in FAS.FAS_CERT on start.SerialNumber equals cert.SerialNumber
                            where start.SerialNumber == snShort
                            select new { hdcp.HDCPName, cert.CERTName, start.FullSTBSN, start.ManufDate }).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }

        string GenMac(int SNshort) //40501557
        {
            var MacHex = int.Parse(SNshort.ToString().Substring(1,7)).ToString("X");
            //MacHex = MacHex.ToString("X");            
            if (MacHex.Length < 6)
                for (int i = 0; i < 5; i++)
                {
                    MacHex = "0" + MacHex;
                    if (MacHex.Length == 6)
                        break;
                }
            return "00:21:52:" + MacHex.Substring(0, 2) + ":" + MacHex.Substring(2, 2) + ":" + MacHex.Substring(4, 2);
        }

        string SetLDS()
        {
            var Delay = control.Controls.Find("Delay11", true).OfType<NumericUpDown>().FirstOrDefault();
            var G = control.Controls.Find("TestGrid", true).OfType<DataGridView>().FirstOrDefault();            
            var D = int.Parse(Delay.Text);
            int timeout = 0;            

            for (int i = 0; i < 3; i++)
            {
                D += 200;
                timeout = 100 + D;
                if (SendToCOM(DataGenerationOneByte("A9", "0DF20101000C"), timeout) == "COM")              
                    return "COM";

                control.Invoke((Action)(() => { G[0, 9].Value = "SetLDS"; G[1, 9].Value = timeout; G[2, 9].Value = i; }));

                var LDS = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-", "");
                if (LDS.Contains("290001"))
                    return "true";
            }
            return "";
        }
        string SetSN(string SN)
        {
            var G = control.Controls.Find("TestGrid", true).OfType<DataGridView>().FirstOrDefault();
            var Delay = control.Controls.Find("Delay2", true).OfType<NumericUpDown>().FirstOrDefault();
            var D = int.Parse(Delay.Text);

            var timeout = 300 + D;    
            var SNData = "8A" + StrToHex(SN);
            SendToCOM(DataGenerationOneByte(SNData, "0DF201180091"), timeout);
            control.Invoke((Action)(() => { G[0, 1].Value = "SetSN"; G[1, 1].Value = timeout; }));
            return "true";         
        }

        string GetSN()
        {            
            int timeout = 0;
            string Res = "";
            var Delay = control.Controls.Find("Delay9", true).OfType<NumericUpDown>().FirstOrDefault();
            var D = int.Parse(Delay.Text);
            for (int i = 1; i < 5; i++)
            {
                D += 200;
                //var SNData = "8A" + StrToHex(SN);
                timeout = 800 + D;
                //SendToCOM(DataGenerationOneByte(SNData, "0DF201180091"),timeout);
                //var WriteSN = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");
                //Thread.Sleep(300 + delay);
                if (SendToCOM(DataGenerationOneByte("82", "0DF20101000C"), timeout) == "COM")
                    return "COM";

                var R_SN = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-", "");
                if (R_SN.Contains("0200"))
                {
                    var R_SN_OUT = R_SN.Substring(16,46);
                    for (int x = 0; x < R_SN_OUT.Length; x += 2)                    
                        Res += R_SN_OUT.Substring(x + 1, 1);
                    return Res;
                }
            }
            return "";
        }
        string WriteMac(string MAC)
        {
            var G = control.Controls.Find("TestGrid", true).OfType<DataGridView>().FirstOrDefault();
            var Delay = control.Controls.Find("Delay9", true).OfType<NumericUpDown>().FirstOrDefault();
            var D = int.Parse(Delay.Text);
            int timeout = 0;
            
            for (int i = 1; i < 6; i++)
            {
                 D += 200;
                var MACData = "A4" + "04" + "65746830" + "11" + StrToHex(MAC);
                timeout = 200 + D;
                if (SendToCOM(DataGenerationOneByte(MACData, "0DF201180091"), timeout) == "COM")
                    return "COM";
                control.Invoke((Action)(() => { G[0, 8].Value = "WriteMac"; G[1, 8].Value = timeout; G[2, 8].Value = i; }));
                var MacAnswer = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");

                if (MacAnswer.Contains("2400"))
                    return "true";
            }

            return "false";
        }
       string WriteCert()
        {
            var G = control.Controls.Find("TestGrid", true).OfType<DataGridView>().FirstOrDefault();
            var Delay = control.Controls.Find("Delay8", true).OfType<NumericUpDown>().FirstOrDefault();
            var D = int.Parse(Delay.Text);

            int timeout = 0;
            var CERTKeyByte = GetCERTKeyByte();
            var CertKey = BitConverter.ToString(CERTKeyByte, 0, CERTKeyByte.Length).Replace("-", "");
           
            for (int i = 0; i < 3; i++)
            {
                D += 200;
                var KeyLength = "0" + (CertKey.Length / 2).ToString("X");
                KeyLength = KeyLength[2].ToString() + KeyLength[3].ToString() + KeyLength[0].ToString() + KeyLength[1].ToString();
                var CertData = "A5" + "0B" + "6B6579735061636B616765" + KeyLength + CertKey;
                var datalength = "0" + (CertData.Length / 2).ToString("X");
                datalength = datalength[2].ToString() + datalength[3].ToString() + datalength[0].ToString() + datalength[1].ToString();
                timeout = 800 + D;
                if (SendToCOM(DataGenerationOneOther(CertData, datalength), timeout) == "COM")
                    return "COM";

                control.Invoke((Action)(() => { G[0, 7].Value = "WriteCert"; G[1, 7].Value = timeout; G[2, 7].Value = i; }));

                var CertAnswer = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");
                if (CertAnswer.Contains("2500"))
                { return "true"; }
            }
            return "false";
        }
        string WriteHDCP()
        {
            var G = control.Controls.Find("TestGrid", true).OfType<DataGridView>().FirstOrDefault();           
            var Delay = control.Controls.Find("Delay7", true).OfType<NumericUpDown>().FirstOrDefault();
            var D = int.Parse(Delay.Text);

            var HDCpKeyByte = GetHDCPContent();
            var HDCPKey = BitConverter.ToString(HDCpKeyByte,0, HDCpKeyByte.Length).Replace("-","");
            
            int timeout = 0;
            for (int i = 0; i < 3; i++)
            {                
                D += 200;
                var HDCPData = "8B" + "00000000" + HDCPKey;
                var DataLength = "0" + (HDCPData.Length / 2).ToString("X");
                DataLength = DataLength[2].ToString() + DataLength[3].ToString() + DataLength[0].ToString() + DataLength[1].ToString();
                timeout = 800 + D;
                if (SendToCOM(DataGenerationOneOther(HDCPData, DataLength), timeout) == "COM")
                    return "COM";
               
                control.Invoke((Action)(() => { G[0, 6].Value = "WriteHDCP"; G[1, 6].Value = timeout; G[2, 6].Value = i; }));

                var HDCPAnswer = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");
                if (HDCPAnswer.Contains("0B00"))
                { return "false"; }  
            }

            return "true";
        }
        string StrToHex(string MAC)
        {
            string sHex = "";
            foreach (var item in Encoding.ASCII.GetBytes(MAC))            
                sHex += Convert.ToString(new StringBuilder().AppendFormat("{0:X}", item));   
            return sHex;
        }
        byte[] GetCERTKeyByte()
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_CERT.Where(c => c.SerialNumber == ShortSN).Select(c => c.CertContent).FirstOrDefault();
            }

        }
        byte[] GetHDCPContent()
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_HDCP.Where(c => c.SerialNumber == ShortSN).Select(c => c.HDCPContent).FirstOrDefault();
            }
        
        }      
        void Remove_Fas_Upload()
        {
            using (FASEntities FAS = new FASEntities())
            {
                var line = FAS.FAS_Upload.Where(c => c.CASID == CASID).FirstOrDefault();
                FAS.FAS_Upload.Remove(line);
                FAS.SaveChanges();
            }
        }

        string CheckBoolSerialNumber()
        {
            var Bools = ArListSNnumer;
            Bools.RemoveAt(0);
            var B = Bools.OfType<bool>().ToList();
            //S.IsUsed, S.IsActive, S.IsUploaded, S.IsWeighted, S.IsPacked, S.InRepair
            //Used 1, Active 1,Uploaded 1, Packed 0, Repair 0 = Проверка прошла
            if (B[0] & B[1] & B[2] & !B[4] & !B[5])            
                return "UpOk";

            if (B[0] & B[1] & !B[2] & !B[4] & !B[5])
                return "UpNok";

            return "false";//Если условие выше не пройдены, Проверка не пройдена
        }    
        string GetDatafromSTB(string ByteRequest,string ByteAnswer,string method,int row,string DelayName)
        {
            var G = control.Controls.Find("TestGrid", true).OfType<DataGridView>().FirstOrDefault();//================================
            var Delay = control.Controls.Find(DelayName, true).OfType<NumericUpDown>().FirstOrDefault();

            var D = int.Parse(Delay.Text);
            int TimeOut = 0;
            string ResText = "";
            for (int i = 0; i < 3; i++)
            {
                D += 200;
                TimeOut = 300 + D;
                if (SendToCOM(DataGenerationOneByte(ByteRequest, "0DF20101000C"), TimeOut) == "COM")
                    return "COM";
                
                var ResHex = BitConverter.ToString(arrBuffer,0,intSize);
                ResHex = ResHex.Replace("-", "");     
                
                control.Invoke((Action)(() => { G[0, row].Value = method; G[1, row].Value = TimeOut; G[2, row].Value = i; }));//===========================

                if (ResHex.IndexOf(ByteAnswer) == 12 & i < 4)                         
                {                     
                    var RexHexOut = ResHex.Substring(18,ResHex.Length - 22);   
                    while (RexHexOut.Length > 0)
                    {
                        ResText += Convert.ToChar(Convert.ToUInt32(RexHexOut.Substring(0, 2), 16)).ToString();
                        RexHexOut = RexHexOut.Substring(2, RexHexOut.Length - 2);
                    }
                    return ResText;                     
                }
            }
            return ResText;
        }
        string SendToCOM(string GeneratedRequest,int T) //0DF20101000CAEB049
        {
            var ArrayByte = StringToByteArray(GeneratedRequest); //count 8 12-242-1-1-0-12-171-219
            try
            {
                SerialPort.Open();
                SerialPort.Write(ArrayByte.ToArray(),0, ArrayByte.ToArray().Length);
                Thread.Sleep(T);

                
                while (SerialPort.BytesToRead > 0)
                 intSize = SerialPort.Read(arrBuffer, 0, 1024); 
                
                //var line = SerialPort.ReadLine();
                SerialPort.Close();
                return "false";
            }
            catch (Exception e)
            {               
                SerialPort.Close();
                MessageBox.Show($@"Проблемы с ComPort {SerialPort.PortName},Проверьте подключение" + "Системная ошибка -" + e.ToString());
                return "COM";
            }
        }      
        List<byte> StringToByteArray(string raw)
        {
            var d = new List<byte>();

            for (int i = 0; i < raw.Length / 2 ; i++)
                d.Add(Convert.ToByte(raw.Substring((i*2),2), 0x10));

            return d;
        }
        string DataGenerationOneByte(string ByteRequest,string Header)
        {                  
            byte[] ByteRequestHex = new byte[(ByteRequest.Length / 2)];

            for (int i = 0; i < ByteRequestHex.Length; i++)
                ByteRequestHex[i] = (Convert.ToByte(ByteRequest.Substring((i*2),2), 0x10));

            var crcmodel = new RocksoftCrcModel(32, 0x4c11db7, 4294967295, false, false, 0);
            //var ByteRequestCS = Convert.ToString(crcmodel.ComputeCrc(ref ByteRequestHex), 0x10);
            var ByteRequestCS =crcmodel.ComputeCrc(ref ByteRequestHex).ToString("X"); //"A160B9BF"
            var L = ByteRequestCS.Length - 1; //946C22DB
            ByteRequestCS =  ByteRequestCS[L - 1].ToString() + ByteRequestCS[L].ToString() + ByteRequestCS[L - 3].ToString() + ByteRequestCS[L - 2].ToString();
            //ByteRequestCS =  ByteRequestCS[L - 1].ToString();
            return Header + ByteRequest + ByteRequestCS; //0DF20101000CACDE72 
        }       
        string DataGenerationOneOther(string data, string dataLength)
        {
            string Header = "0DF201" + dataLength;

            byte[] HeaderHex = new byte[(Header.Length / 2)];

            for (int i = 0; i < HeaderHex.Length; i++)
                HeaderHex[i] = (Convert.ToByte(Header.Substring((i * 2), 2), 0x10));

            var crcmodel = new RocksoftCrcModel(32, 0x4c11db7, 4294967295, false, false, 0);
            //var ByteRequestCS = Convert.ToString(crcmodel.ComputeCrc(ref ByteRequestHex), 0x10);
            var HeaderCS = crcmodel.ComputeCrc(ref HeaderHex).ToString("X");
            HeaderCS = HeaderCS[6].ToString() + HeaderCS[7].ToString();


            byte[] DataHex = new byte[(data.Length / 2)];
            for (int i = 0; i < DataHex.Length; i++)          
                DataHex[i] = (Convert.ToByte(data.Substring((i * 2), 2), 0x10));

            var ByteRequestCS = crcmodel.ComputeCrc(ref DataHex).ToString("X");
            var L = ByteRequestCS.Length - 1;
            ByteRequestCS = ByteRequestCS[L - 1].ToString() + ByteRequestCS[L].ToString() + ByteRequestCS[L - 3].ToString() + ByteRequestCS[L - 2].ToString();
            return Header + HeaderCS + data + ByteRequestCS;     
        }  
        void GetLot(DataGridView Grid)
        {
            loadgrid.Loadgrid(Grid, @"use FAS select 
LOTCode,FULL_LOT_Code,ModelName,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID) InLot,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 0 and s.IsActive = 1) Ready,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 1 ) Used,
LOTID
from FAS_GS_LOTs as gs
left join FAS_Models as m on gs.ModelID = m.ModelID

where IsActive = 1
order by LOTID desc ");
            #region Старый код
            //using (FASEntities FAS = new FASEntities())
            //{
            //    var list = from Lot in FAS.FAS_GS_LOTs
            //               join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID                           
            //               where Lot.IsActive == true orderby Lot.LOTID descending                
            //               select new
            //               {
            //                   Lot = Lot.LOTCode,
            //                   Full_Lot = Lot.FULL_LOT_Code,
            //                   Model = model.ModelName,
            //                   InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   Ready = (from s in FAS.FAS_SerialNumbers where s.IsUploaded == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   Used = (from s in FAS.FAS_SerialNumbers where s.IsUploaded == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   Lot.LOTID                          
            //               };
            //    Grid.DataSource = list.ToList();
            //}
            #endregion
        }
        void SetBoolSerialNumbers(int serialnumber)
        {
            using (var FAS = new FASEntities())
            {
               var fas = FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == serialnumber);                
                fas.FirstOrDefault().IsUploaded = false;
                fas.FirstOrDefault().IsPacked = false;
                fas.FirstOrDefault().IsWeighted = false;
            }
        }
        ArrayList GetLotHDCP()
        {
            using (FASEntities FAS = new FASEntities())
            {
                var ArrayListHDCP = new ArrayList();

                var list = (from Lot in FAS.FAS_GS_LOTs
                           join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
                           where Lot.IsActive == true & Lot.LOTID == LOTID
                           select new
                           {                          
                               Lot.IsHDCPUpload,
                               Lot.IsCertUpload,
                               Lot.IsMACUpload,
                               Lot.ModelCheck,
                               Lot.SWRead,
                               Lot.SWGS1Read,
                               Lot.LabelScenarioID,
                               Lot.PTID,
                               Lot.WorkingScenarioID
                              
                           }).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)                
                    ArrayListHDCP.Add(value);
                return ArrayListHDCP;
            }
        }
        string GetDelaySettings()
        {
            using (var fas = new FASEntities())
            {

                var Result = (from lot in fas.FAS_GS_LOTs
                        join m in fas.FAS_Models on lot.ModelID equals m.ModelID
                        where lot.LOTID == LOTID
                        select m.DelaySetting).FirstOrDefault();

                if (string.IsNullOrEmpty(Result))               
                   return (from lot in fas.FAS_GS_LOTs
                     join m in fas.FAS_Models on lot.ModelID equals m.ModelID                     
                     select m.DelaySetting).FirstOrDefault();

                return Result;
               

                //var l = (from lot in fas.FAS_GS_LOTs
                //         join m in fas.FAS_Models on lot.ModelID equals m.ModelID where lot.LOTID == LOTID
                //         select m.DelaySetting).ToList();
                //return "";


            }
        }
    }
}
