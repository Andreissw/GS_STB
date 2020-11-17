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
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
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

        List<string> listdata;
        //int LabelScenarioID { get; set; }

        //public string Model, DUID, CASID, SW_v, SWGS1_v, HDCPUpload, CertUpload, MACUpload, ReadedSN;
        

         
         List<Byte> InfoBytes = new List<Byte>();
        public UploadStation()
        {
            ListHeader = new List<string>() { "№", "SN", "SC ID", "CAS ID", "HDCP","CERT","MAC","LDS","SW","SS GS1","Date"};
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
           
            var FUG = control.Controls.Find("FAS_Print", true).FirstOrDefault();
            var SNPrint = control.Controls.Find("SNPRINT", true).FirstOrDefault();
            var IDPrint = control.Controls.Find("IDPrint", true).FirstOrDefault();

            var CHPrintSN = control.Controls.Find("CHPrintSN", true).OfType<CheckBox>().FirstOrDefault();
            var CHPrintID = control.Controls.Find("CHPrintID", true).OfType<CheckBox>().FirstOrDefault();
            var CountLBSN = control.Controls.Find("CountLBSN", true).OfType<Label>().FirstOrDefault();
            var CountLBID = control.Controls.Find("CountLBID", true).OfType<Label>().FirstOrDefault();
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
            FUG.Location = new Point(272, 19);
            FUG.Size= new Size(440, 186);
            ArListGSLot = GetLotHDCP();

            if (CheckPathPrinterSettings())
                CreatePathPrinter();
           
            var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
            XSN.Value = int.Parse(GetPrSet(list, "XSN"));
            YSN.Value = int.Parse(GetPrSet(list, "YSN"));
            XID.Value = int.Parse(GetPrSet(list, "XID"));
            YID.Value = int.Parse(GetPrSet(list, "YID"));


            if (UpPrintID)
                IDPrint.Visible = true;
            if (UpPrintSN)
                SNPrint.Visible = true;

            ShiftCounterStart(control);
        }

        public override void KeyDownMethod()
        {
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Parallel();
        }

        string _SN;
        string keyDown( )
        {
            //Инициализация  контроллеров
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();           
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            var ClearBT = control.Controls.Find("ClearBT", true).OfType<Button>().FirstOrDefault();
          
            if (string.IsNullOrEmpty(COMPORT))            
                COMPORT = SerialPort.GetPortNames().FirstOrDefault();
            if (string.IsNullOrEmpty(COMPORT))
            { LabelStatus(Controllabel, $"COMPORT не подключен, проверьте COMPORT", Color.Red); return "true"; }

           
            //Присовение в переменную _SN значение текстобкса
            control.Invoke((Action)(() =>  {_SN = TB.Text;}));
            if (_SN.Length == 23)
            {
                //Инициализация Label, который выводит результат сканирования
                
                control.Invoke((Action)(() =>
                {
                    Controllabel.Text = "Загрузка"; 
                    Controllabel.ForeColor = Color.DodgerBlue;
                    TB.Enabled = false; //Блокировем Текстбокс во время ассинхронного метода
                    ClearBT.Enabled = false;
                    SerialPort = new SerialPort(); //Инициализация ComPorta
                    SerialPort.BaudRate = 115200;
                    SerialPort.DataBits = 8;
                    SerialPort.PortName = COMPORT;
                    SerialPort.Parity = Parity.None;
                    SerialPort.StopBits = StopBits.One;
                    SerialPort.Handshake = Handshake.None;
                    SerialPort.Encoding = System.Text.Encoding.Default;
                }));

                //Инициализация двух перменных типа string для печати SN
                var TextCode = _SN.Substring(0, 22) + ">6" + _SN.Substring(22);
                var TextSN = _SN.Substring(0, 2) + " " + _SN.Substring(2, 4) + " " + _SN.Substring(6, 2) + " " + _SN.Substring(8, 2) +
                                  " " + _SN.Substring(10, 2) + " " + _SN.Substring(12, 3) + " " + _SN.Substring(15, 8);

                //Перед преобразованием проверяем может ли ShortSN быть типом int
                int k = 0;
                if (!int.TryParse(_SN.Substring(15), out k))
                { LabelStatus(Controllabel, $"Неверный формат номера {_SN}", Color.Red); return "true"; }
                ShortSN = int.Parse(_SN.Substring(15)); //Если удачно, то преобразуем ShortSN


                ArListSNnumer = GetSerialNum(ShortSN); //Получаем список данных по короткому серийному номеру с таблицы FAS_SerialNumber  

                //Проверка данных в ArrayList 
                if (ArListSNnumer.Count == 0)
                { LabelStatus(Controllabel, $"Серийный номер {_SN}  Не найден в этом ЛОТе!", Color.Red); return "true"; }

                //Проверка булевых на активность номера, упаковку, весовой контроль и т.д
                if (CheckboolArListSNnumer(Controllabel))
                    return "true";

                //Проверяем, можем ли мы преобразовать PCBID в тип int
                if (!int.TryParse(ArListSNnumer[0].ToString(), out k))
                { LabelStatus(Controllabel, $"Не удалось приобразовать в число PCBID - {ArListSNnumer[0]}", Color.Red); return "true"; }
                PCBID = int.Parse(ArListSNnumer[0].ToString()); //Если можно, то преобразовываем

                var DUID = GetDUID(); //Считываем данные с приемника, читаем DUID

                //Если метод вернул "COM" то значит возникли проблемы с ComPort
                if (DUID == "COM")
                { LabelStatus(Controllabel, $"Ошибка связанная с COMPORT \n Проверьте COMPORT", Color.Red); return "true"; }

                //Если считали пустое значение, значит проблема с прошивой приемника
                if (string.IsNullOrEmpty(DUID))
                { LabelStatus(Controllabel, $"Ошибка при прошивке приемника, DUID не найден", Color.Red); return "true"; }

                //Получаем CASID = PTID + DUID
                CASID = ArListGSLot[7].ToString() + DUID;
                //Достаем с базы серийный номер по CASID
                var CASIDShortSerial = CheckCASID(CASID);

                //Если в базе ShortSN по CASID не совпадет с введенным ShortSN
                if (CASIDShortSerial != ShortSN & CASIDShortSerial != 0)
                {
                    //S.SerialNumber, Liter = L.LiterName + S.LiterIndex, S.PalletNum, S.BoxNum, S.UnitNum, S.PackingDate, U.UserName
                    //Проверяем был ли упакован задвоенный серийный номер по такому CASID 
                    var list = CheckSNPack(CASIDShortSerial);
                    //Если нет, то задвоение
                    if (list.Count == 0)
                    { LabelStatus(Controllabel, $@"{_SN } Задвоение CASID, заблокировать данный приемник", Color.Red); return "true"; }

                    //Если был упакован, то выводим информацию
                    LabelStatus(Controllabel, $"{list[0]} Серийный номер с таким CASID {CASID} \n был упакован. Liter {list[1]}, Pallet {list[2]}, BoxNum {list[3]} \n UnitNum {list[4]} Дата {list[5]}, Упаковал {list[6]} ", Color.Red); return "true";
                }              

                //ShortSN с базы равен с введенным ShortSN запрашиваем разрешение на печать номера
                else if (CASIDShortSerial == ShortSN)
                {
                    //Проверка настройка печати
                    if (!UpPrintID & !UpPrintSN )
                    { LabelStatus(Controllabel, $@"{_SN } Задвоение CASID, заблокировать данный приемник", Color.Red); return "true"; }

                    var mes = new msg("Номер был прошит ранее \n желаете ли перепечатать этикетку ?");
                    mes.ShowDialog(); //разрешение на печать
                    if (mes.DialogResult == DialogResult.No)
                        return "NoPrint";

                    using (var _fas = new FASEntities1())
                    {
                        var SmartID = _fas.FAS_Upload.Where(c => c.SerialNumber == ShortSN).Select(c => c.SmartCardID).FirstOrDefault();
                        Prints(TextCode, TextSN, SmartID.ToString());
                        return "Print";
                    }                   
                }

                var ArraySCID = CheckSCID(CASID);
                var Barcode = CheckLazer(PCBID);
                if (string.IsNullOrEmpty(Barcode))
                { LabelStatus(Controllabel, $@"{Barcode } Barcode не найден в базе Lazer", Color.Red); return "true"; }
                ArraySCID.Add(Barcode); //Поиск баркода в таблице Lazer, аргумент возможно нужен другой        

                if (ArraySCID.Count != 1)
                {
                    var listbool = ChecksIf(ArraySCID, Controllabel, CASID);// Метод, которые соврешает несколько проверок, выводим в listbool : CheckSNResult[0];  Replace[1]; UseReapload[2]
                    if (!listbool[0]) //CheckSNResult если false метод заканчивает работу
                        return "true";
                }

                ArrayLoadSnData = LoadSnData(ShortSN);
                //Проверка на содерждимое arrayLista 
                if (ArrayLoadSnData.Count == 0)
                { LabelStatus(Controllabel, $@"ArrayLoadSnData пустой", Color.Red); return "true"; }

                if (StartUpload(Controllabel))
                { LabelStatus(Controllabel, $@"{_SN} Произошла ошибка при прошивке", Color.Red); return "true"; }

                //Определение SmartID
                var SmartCardID = listdata[1].ToString();

                WriteDB(SmartCardID);

                Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
                DataGridView DG_UpLog = control.Controls.Find("DG_UpLog", true).OfType<DataGridView>().FirstOrDefault();
                control.Invoke((Action)(() =>
                {
                    Label_ShiftCounter.Text = ShiftCounter.ToString();
                    DG_UpLog.Rows.Add(ShiftCounter, _SN, SmartCardID, CASID, ArrayLoadSnData[2], ArrayLoadSnData[3], ArrayLoadSnData[1], "OK", listdata[2], listdata[3], DateTime.UtcNow.AddHours(2));
                    DG_UpLog.Sort(DG_UpLog.Columns[0], ListSortDirection.Descending);
                }));

                //Добавление в OperationLog строки 
                AddOpLog();

                Prints(TextCode, TextSN, listdata[1]);

                return "false";
            }
            else
                return "er";
        }

        ArrayList CheckSNPack(int SN)
        {
            using (var Fas = new FASEntities1())
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

        bool CheckboolArListSNnumer(Label label)
        {
            //S.IsUsed, S.IsActive, S.IsUploaded, S.IsWeighted, S.IsPacked, S.InRepair
            string text = "";
            if (ArListSNnumer[1].ToString() == "False")
                text += "Номер не используется. ";
            if (ArListSNnumer[2].ToString() == "False")
                text += "Номер не активный. ";
            if (ArListSNnumer[4].ToString() == "True")
                text += "Приемник был уже взвешен. ";
            if (ArListSNnumer[5].ToString() == "True")
                text += "Приемник уже упакован. ";
            if (ArListSNnumer[5].ToString() == "True")
                text += "Приемник в ремонте. ";

            if (string.IsNullOrEmpty(text))
                return false;

            LabelStatus(label, text, Color.Red);
            return true;

        }

            private void Prints(string TextCode, string TextSN, string SmartID)// Печать
        {
            if (CheckPathPrinterSettings())
                CreatePathPrinter();

            if (UpPrintSN)
            {
                var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
                var X = GetPrSet(list, "XSN");
                var Y = GetPrSet(list, "YSN");
                print(Print.PrintSN(ArrayList[5].ToString(), TextSN, TextCode, UpPrintCountSN, int.Parse(X), int.Parse(Y)));
            }

            if (UpPrintID)
            {
                var list = Directory.GetFiles(@"C:\PrinterSettings").ToList();
                var X = GetPrSet(list, "XID");
                var Y = GetPrSet(list, "YID");
                print(Print.PrintID(SmartID, UpPrintCountID, int.Parse(X), int.Parse(Y)));
            }
        }

        string GetPrSet(List<string> list,string mask)
        {
            var X = list.Where(c => c.Contains(mask)).FirstOrDefault();
            X = X.Substring(X.IndexOf(',') + 1, X.IndexOf('.') - X.IndexOf(',') - 1);
            return X;
        }

        void print(string content)
        {
            RawPrinterHelper.SendStringToPrinter(printName, content); //Нужно получать ответ от принтера Для Кости 
        }

        void AddOpLog()
        {
            using (var FAS = new FASEntities1())
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
                    SmartCardId = long.Parse(listdata[1].ToString())
                };

                FAS.FAS_OperationLog.Add(Fas);
                FAS.SaveChanges();
            }
        }
        int CheckCASID(string CASID)
        {
            using (var FAS = new FASEntities1())
            {
                return FAS.FAS_Upload.Where(c => c.CASID == CASID).Select(c => c.SerialNumber).FirstOrDefault();
            }
        }
        void WriteDB(string SmartID)
        {
            AddFas_Upload(Convert.ToInt64(SmartID));
            UpdateSerialNumber();
            ShiftCounter += 1;
            ShiftCounterUpdate();
        }

        void AddFas_Upload(long SmartCardID)
        {
            using (var FAS = new FASEntities1())
            {
                var Upload = new FAS_Upload()
                {
                    SerialNumber = ShortSN,
                    MAC = ArrayLoadSnData[1].ToString(),
                    LineID = (byte)LineID,
                    SmartCardID = SmartCardID,
                    CASID = CASID,
                    SWversion = listdata[2].ToString(),
                    SWGS1version = listdata[3].ToString(),
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
            using (var FAS = new FASEntities1())
            {
                var FAS_ = FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == ShortSN);
                FAS_.FirstOrDefault().IsUploaded = true;
                FAS.SaveChanges();
            }
        }

        

        List<bool> ChecksIf(ArrayList ArraySCID, Label Controllabel,string CASID)
        {
            var listbool = new List<bool>();

            // Если находится в ремонте приемник с такой номер
            if (ArListSNnumer[1].ToString() == "True" & ArListSNnumer[2].ToString() == "False" & ArListSNnumer[6].ToString() == "True")
            { LabelStatus(Controllabel, $@"{_SN} находится в ремонте", Color.Red); listbool.Add(false); }

            //Если номер неактивный 
            else if (ArListSNnumer[2].ToString() == "False" & ArListSNnumer[6].ToString() == "False")
            { LabelStatus(Controllabel, $@"{_SN} Удален из базы", Color.Red); listbool.Add(false); }
            
            //Если номер активный, используется , не загружен и не упакован
            else if (ArListSNnumer[1].ToString() == "True" & ArListSNnumer[2].ToString() == "True" & ArListSNnumer[3].ToString() == "False" & ArListSNnumer[5].ToString() == "False")
            {
                //St.FullSTBSN 0,   up.SmartCardID 1,    line.LineName 2,   up.UploadDate 3,  us1.UserName 4,   SN.IsPacked 5,     lit.LiterName 6,    pac.LiterIndex 7 ,
                //  pac.PalletNum 8,    pac.BoxNum 9,  pac.UnitNum 10,    pac.PackingDate 11,     pacus = us2.UserName 12
                if (CheckBoxDublicateSCID)
                {
                    LabelStatus(Controllabel, $"Приемник имеет ID номер {ArraySCID[1]} , Который пренадлежит СН {ArraySCID[1]} \n  PCB {ArraySCID[13] } Линия {ArraySCID[2]} Дата {ArraySCID[3]} Пользователь {ArraySCID[4]} ", Color.Red);
                    listbool.Add(false);
                }

                if (!CheckBoxDublicateSCID)
                    if (ArraySCID[5].ToString() == "")
                    {
                        Remove_Fas_Upload(long.Parse(ArraySCID[1].ToString()));
                        listbool.Add(true); listbool.Add(true); listbool.Add(false);
                    }

                if (ArraySCID[5].ToString() == "True")
                {
                    LabelStatus(Controllabel, $"ID номер {ArraySCID[1]} уже упакован с СН {ArraySCID[0]} \n  Литер {ArraySCID[6].ToString() + ArraySCID[7].ToString() }  Паллет {ArraySCID[8]} Групповая {ArraySCID[9]}, приемник №  {ArraySCID[10]} Дата упаковки {ArraySCID[11]} Пользователем {ArraySCID[12]}", Color.Red);
                    listbool.Add(false);
                }
                                
            }

            //Если номер активный, используется, загружен и есть проверка на дубликат  
            else if (ArListSNnumer[1].ToString() == "True" & ArListSNnumer[2].ToString() == "True" & ArListSNnumer[3].ToString() == "True" & CheckDublicateSCID)
            {   //Проверка FullSTBSN и smartID на равенство введенных данных
                if (ArraySCID[0].ToString() == _SN & ArraySCID[3].ToString() == CASID)
                {
                    listbool.Add(true); listbool.Add(false); listbool.Add(true);
                }
                else
                {
                    LabelStatus(Controllabel, $"Приемник имеет ID номер {ArraySCID[1]} , Который пренадлежит СН {ArraySCID[1]} \n  PCB {ArraySCID[13] } Линия {ArraySCID[2]} Дата {ArraySCID[3]} Пользователь {ArraySCID[4]} ", Color.Red);
                    listbool.Add(false);
                }

            }
            //Если номер активный, используется, загружен и отсутстувет проверка на дубликат 
            else if (ArListSNnumer[1].ToString() == "True" & ArListSNnumer[2].ToString() == "True" & ArListSNnumer[3].ToString() == "True" & !CheckDublicateSCID)
            {
                if (ArraySCID[5].ToString() == "")
                {
                    Remove_Fas_Upload(long.Parse(ArraySCID[1].ToString()));
                    listbool.Add(true); listbool.Add(true); listbool.Add(false);
                }
                else
                {
                    LabelStatus(Controllabel, $"ID номер {ArraySCID[1]} уже упакован с СН {ArraySCID[0]} \n  Литер {ArraySCID[6].ToString() + ArraySCID[7].ToString() }  Паллет {ArraySCID[8]} Групповая {ArraySCID[9]}, приемник №  {ArraySCID[10]} Дата упаковки {ArraySCID[11]} Пользователем {ArraySCID[12]}", Color.Red);
                    listbool.Add(false);
                }
            }
            else
            {
                LabelStatus(Controllabel, $"Что то пошло не так! \n Попробуйте еще раз со второй попытки операция не выполнена", Color.Red);
                listbool.Add(false);
            }

            return listbool;
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

            if (result == "er")
            {
                LabelStatus(Controllabel, $@"Неверный формат номера", Color.Red);
                TB.Clear(); TB.Enabled = true; TB.Select();
            }

            if (result == "false")
            {
                LabelStatus(Controllabel, $@"Прошло успешо!", Color.Green);  
                TB.Clear(); TB.Enabled = true; TB.Select(); ClearBT.Enabled = true;
            }

            if (result == "Print")
            {
                LabelStatus(Controllabel, $@"Приемник ранее был добавлен в базу, печать прошла успешно", Color.Green);
                TB.Clear(); TB.Enabled = true; TB.Select();
            }

            if (result == "NoPrint")
            {
                LabelStatus(Controllabel, $@"Приемник ранее был добавлен в базу, печать номера отменена!", Color.Gray);
                TB.Clear(); TB.Enabled = true; TB.Select();
            }

          


        }

        delegate string Methods(Label lb);
        bool StartUpload(Label Controllabel) //Прошивка приемника
        {            
            var progressbar = control.Controls.Find("PB", true).OfType<ProgressBar>().FirstOrDefault();
            var Text = control.Controls.Find("ProgressbarText", true).OfType<Label>().FirstOrDefault();

            //Инициализация ПрогресБара
            control.Invoke((Action)(() => 
            {
                Text.BackColor = Color.White;
                progressbar.Value = 0;
            }));
            
           

            //Создаем с помощью делегата список методов, которые соврешают проверки
            List<Methods> ListMethods = new List<Methods>() { GetModel, GetSmartID, GetSWVersion, GetSWGS1Version, GetHDCP, Getcert, GetMAC, GetSN };
            listdata = new List<string>();

            foreach (var item in ListMethods)
            {
                control.Invoke((Action)(() =>
                {
                    progressbar.Value += 1;
                    var procent = (Int32)Convert.ToInt32(progressbar.Value) / (progressbar.Maximum /100M);
                    Text.Text = $"{procent}% - Идёт процесс - " + item.GetMethodInfo().Name;
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

                listdata.Add(data);     
            }
            control.Invoke((Action)(() =>
            {                
                Text.Text = "Готово!";
            }));
            return false;
        }

            //F.PCBID 0, S.IsUsed 1, S.IsActive 2, S.IsUploaded 3, S.IsWeighted 4, S.IsPacked 5, S.InRepair 6      
            string GetSN(Label lb)
        {
            var ReadSN = SetSN(ArrayLoadSnData[4].ToString()); //00080920200253940592181
            if (ReadSN != _SN)                
                { LabelStatus(lb, $"Считанный номер {ReadSN} не соответсвует прошитому {_SN}", Color.Red); return ""; }

                return ReadSN;
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

            var SWGS1_v = GetDatafromSTB("AD","2D00");
            if (SWGS1_v == null)                
                { LabelStatus(lb, "Метод GetSWGS1Version  не прошел, приемник не вернул данные", Color.Red); return ""; }

                return SWGS1_v;
        }

        string GetSWVersion(Label lb)
        {
            if (ArListGSLot[4].ToString() == "False")
                 return "true"; 

            var SW_v = GetDatafromSTB("AC","2C00");
            if (SW_v == null)               
                { LabelStatus(lb, "Метод GetDuid не прошел ,  приемник не вернул данные", Color.Red); return ""; }

                return SW_v;
        }
        string GetDUID()
        {
            return GetDatafromSTB("A7","270008");
            //var CASID = ArListGSLot[7].ToString() + DUID;
            //return CASID;
        }

        string GetSmartID(Label lb)
        {
            if (ArListGSLot[8].ToString() == "6")
                return "true";

            var SmartID = GetDatafromSTB("AB", "2B000E");
            if (string.IsNullOrEmpty(SmartID))                
                { LabelStatus(lb, "SmartID не найден, приемник не дал ответ", Color.Red); return ""; }

                return SmartID;

        }
        string GetModel(Label lb)
        {
            if (ArListGSLot[3].ToString() == "False")
                return "true";

            var Model = GetDatafromSTB("AE","2E00");
            if (Model != ArrayList[5].ToString())               
                { LabelStatus(lb, "Модель не соответствует", Color.Red); return ""; }
                return Model;
        }



        ArrayList LoadSnData(int snShort)
        {
            using (var FAS = new FASEntities1())
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

        string SetSN(string SN)
        {
            int delay = 0;
            int timeout = 0;
            string Res = "";
            for (int i = 1; i < 10; i++)
            {
                delay += 200;
                var SNData = "8A" + StrToHex(SN);
                timeout = 100 + delay;
                SendToCOM(DataGenerationOneByte(SNData, "0DF201180091"),timeout);
                var WriteSN = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");
                Thread.Sleep(300 + delay);
                if (SendToCOM(DataGenerationOneByte("82", "0DF20101000C"), timeout) == "COM")
                    return "COM";

                var R_SN = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-", "");
                if (R_SN.IndexOf("0200") == 12)
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
            int delay = 0;
            int timeout = 0;
            for (int i = 1; i < 6; i++)
            {
                delay += 300;
                var MACData = "A4" + "04" + "65746830" + "11" + StrToHex(MAC);
                timeout = 200 + delay;
                if (SendToCOM(DataGenerationOneByte(MACData, "0DF201180091"), timeout) == "COM")
                    return "COM";

                var MacAnswer = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");

                if (MacAnswer.IndexOf("2400") == 12)
                    return "true";
            }

            return "false";
        }
       string WriteCert()
        {
            int delay = 0;
            int timeout = 0;
            var CERTKeyByte = GetCERTKeyByte();
            var CertKey = BitConverter.ToString(CERTKeyByte, 0, CERTKeyByte.Length).Replace("-", "");
            
            for (int i = 1; i < 3; i++)
            {
                delay += 200;
                var KeyLength = "0" + (CertKey.Length / 2).ToString("X");
                KeyLength = KeyLength[2].ToString() + KeyLength[3].ToString() + KeyLength[0].ToString() + KeyLength[1].ToString();
                var CertData = "A5" + "0B" + "6B6579735061636B616765" + KeyLength + CertKey;
                var datalength = "0" + (CertData.Length / 2).ToString("X");
                datalength = datalength[2].ToString() + datalength[3].ToString() + datalength[0].ToString() + datalength[1].ToString();
                timeout = 800 + delay;
                if (SendToCOM(DataGenerationOneOther(CertData, datalength), timeout) == "COM")
                    return "COM";
                
                var CertAnswer = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");
                if (CertAnswer.IndexOf("2500") == 12)
                { return "true"; }
            }
            return "false";
        }
        string WriteHDCP()
        {
            var HDCpKeyByte = GetHDCPContent();
            var HDCPKey = BitConverter.ToString(HDCpKeyByte,0, HDCpKeyByte.Length).Replace("-","");
            int delay = 0;
            int timeout = 0;
            for (int i = 1; i < 3; i++)
            {                
                delay += 200;
                var HDCPData = "8B" + "00000000" + HDCPKey;
                var DataLength = "0" + (HDCPData.Length / 2).ToString("X");
                DataLength = DataLength[2].ToString() + DataLength[3].ToString() + DataLength[0].ToString() + DataLength[1].ToString();
                timeout = 800 + delay;
                if (SendToCOM(DataGenerationOneOther(HDCPData, DataLength), timeout) == "COM")
                    return "COM"; 
                var HDCPAnswer = BitConverter.ToString(arrBuffer, 0, intSize).Replace("-","");
                if (HDCPAnswer.IndexOf("0B00") == 12)
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
            using (var FAS = new FASEntities1())
            {
                return FAS.FAS_CERT.Where(c => c.SerialNumber == ShortSN).Select(c => c.CertContent).FirstOrDefault();
            }

        }
        byte[] GetHDCPContent()
        {
            using (var FAS = new FASEntities1())
            {
                return FAS.FAS_HDCP.Where(c => c.SerialNumber == ShortSN).Select(c => c.HDCPContent).FirstOrDefault();
            }
        
        }
        bool CheckInfos(ArrayList array, Label lb)
        {
            //St.FullSTBSN 0,   up.SmartCardID 1,    line.LineName 2,   up.UploadDate 3,  us1.UserName 4,   SN.IsPacked 5,     lit.LiterName 6,    pac.LiterIndex 7 ,
            //  pac.PalletNum 8,    pac.BoxNum 9,  pac.UnitNum 10,    pac.PackingDate 11,     pacus = us2.UserName 12
            if (CheckBoxDublicateSCID)
            {
                LabelStatus(lb, $"Приемник имеет ID номер {array[1]} , Который пренадлежит СН {array[1]} \n  PCB {array[13] } Линия {array[2]} Дата {array[3]} Пользователь {array[4]} ", Color.Red);
                return true;
            }

            if (!CheckBoxDublicateSCID)
                if (array[5].ToString() == "")
                {
                    Remove_Fas_Upload(long.Parse(array[1].ToString()));
                    return true;
                }             

            if (array[5].ToString() == "True")
            {
                LabelStatus(lb, $"ID номер {array[1]} уже упакован с СН {array[0]} \n  Литер {array[6].ToString() + array[7].ToString() }  Паллет {array[8]} Групповая {array[9]}, приемник №  {array[10]} Дата упаковки {array[11]} Пользователем {array[12]}", Color.Red);
                return true;
            }
            return false;
        }
        void Remove_Fas_Upload(long smartID)
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                var line = FAS.FAS_Upload.Where(c => c.SmartCardID == smartID).FirstOrDefault();
                FAS.FAS_Upload.Remove(line);
                FAS.SaveChanges();
            }
        }
        string CheckLazer(int pcbid)
        {
            using (var smd = new SMDCOMPONETSEntities())
            {
                return smd.LazerBase.Where(c => c.IDLaser == pcbid).Select(c => c.Content).FirstOrDefault();
            }
        }
        ArrayList CheckSCID(string CASID) //отсутствует баркод 
        {
            using (var FAS = new FASEntities1())
            {
                var ArrayList = new ArrayList();
                var list = (from up in FAS.FAS_Upload
                            join SN in FAS.FAS_SerialNumbers on up.SerialNumber equals SN.SerialNumber
                            join line in FAS.FAS_Lines on up.LineID equals line.LineID
                            join St in FAS.FAS_Start on SN.SerialNumber equals St.SerialNumber
                            join pac in FAS.FAS_PackingGS on up.SerialNumber equals pac.SerialNumber
                            join us1 in FAS.FAS_Users on up.UploadByID equals us1.UserID
                            join us2 in FAS.FAS_Users on pac.PackingByID equals us2.UserID
                            join lit in FAS.FAS_Liter on pac.LiterID equals lit.ID
                            where up.CASID == CASID
                            select new
                            {
                                St.FullSTBSN,
                                up.SmartCardID,
                                line.LineName,
                                up.UploadDate, 
                                us1.UserName,
                                SN.IsPacked,
                                lit.LiterName,
                                pac.LiterIndex,
                                pac.PalletNum,
                                pac.BoxNum,
                                pac.UnitNum,
                                pac.PackingDate,
                                pacus = us2.UserName,
                                up.CASID
                            });

                if (list.Count() == 0)
                    return ArrayList;

                var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;
            }
        }               
        string GetDatafromSTB(string ByteRequest,string ByteAnswer)
        {
            int Delay = 0;
            int TimeOut = 0;
            string ResText = "";
            for (int i = 0; i < 3; i++)
            {
                Delay += 200;
                TimeOut = 300 + Delay;
                if (SendToCOM(DataGenerationOneByte(ByteRequest, "0DF20101000C"), TimeOut) == "COM")
                    return "COM";
                
                var ResHex = BitConverter.ToString(arrBuffer,0,intSize);
                ResHex = ResHex.Replace("-", ""); //0D-F2-03-11-00-11-2B-00-0E-34-35-30-34-30-39-30-35-33-33-33-35-36-37-CE-E9            
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
                { intSize = SerialPort.Read(arrBuffer, 0, 1024); }
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
        ArrayList GetSerialNum(int shortSN)
        {
            using (var Fas = new FASEntities1())
            {
                var ArrayList = new ArrayList();
                var list = (from S in Fas.FAS_SerialNumbers
                            join F in Fas.FAS_Start on S.SerialNumber equals F.SerialNumber
                            where S.LOTID == LOTID & S.SerialNumber == shortSN
                            select new { F.PCBID, S.IsUsed, S.IsActive, S.IsUploaded, S.IsWeighted, S.IsPacked, S.InRepair });

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
       
        void GetLot(DataGridView Grid)
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                var list = from Lot in FAS.FAS_GS_LOTs
                           join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID                           
                           where Lot.IsActive == true                    
                           select new
                           {
                               Lot = Lot.LOTCode,
                               Full_Lot = Lot.FULL_LOT_Code,
                               Model = model.ModelName,
                               InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Ready = (from s in FAS.FAS_SerialNumbers where s.IsUploaded == false & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               User = (from s in FAS.FAS_SerialNumbers where s.IsUploaded == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Lot.LOTID                          
                           };
                Grid.DataSource = list.ToList();
            }
        }          
    
     
        

        ArrayList GetLotHDCP()
        {
            using (FASEntities1 FAS = new FASEntities1())
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
    }
}
