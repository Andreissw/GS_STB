using GS_STB.Forms_Modules;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class FASStart : BaseClass
    {
        //public string PCBID { get; set; }
        public string FullSNPCB { get; set; }

        public string FullSTBSN { get; set; }

        public string SerNumber { get; set; }

        DateTime DateText;
        public FASStart()
        {
            ListHeader = new List<string>() { "№", "CH приемника", "CH платы", "Печать", "ScanDate" };
            IDApp = 4;

        }

        public override void LoadWorkForm()
        {
             ShiftCounterStart(control);  //Возможно этот метод надо будет засунуть в базовый класс     
             GetLineForPrint();
        }

        public override void KeyDownMethod()
        {
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
            Label ShiftCounterl = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            DataGridView DG_UpLog = control.Controls.Find("DG_UpLog", true).OfType<DataGridView>().FirstOrDefault();
            GetDate();
            if (TB.TextLength == 21)
            {
                if (CheckLazerPCBID(TB.Text)) //Проверка номера в базе LazerBase //Добавить THT Start
                {
                    LabelStatus(Controllabel, $"{TB.Text} не зарегистрирован в базе LazerBase", Color.Red); return;
                }

                var FullSN = CheckAssemlyPCBID();
                if (!string.IsNullOrEmpty(FullSN))
                {
                    if (!PrintBool)
                     { LabelStatus(Controllabel, $"{TB.Text} уже присвоен {FullSN}", Color.Red); return; }

                    var Mes = new msg($"{TB.Text} уже присвоен \n SN номер{FullSN} \n Хотите перепечатать SN номер?");
                    var Result = Mes.ShowDialog();
                    if (Result == DialogResult.No)
                    { LabelStatus(Controllabel, $"{TB.Text} уже присвоен SN {FullSN} \n печать отменена!", Color.Red); return; }

                    var printCodeSN = FullSN.Substring(0, 22) + ">6" + FullSN.Substring(22);
                    var PrintTextSN = FullSN.Substring(0, 2) + " " + FullSN.Substring(2, 4) + " " + FullSN.Substring(6, 2) + " " + FullSN.Substring(8, 2) +
                                      " " + FullSN.Substring(10, 2) + " " + FullSN.Substring(12, 3) + " " + FullSN.Substring(15, 8);
                    print(LabelSN(PrintTextSN, printCodeSN));

                    { LabelStatus(Controllabel, $"{TB.Text} уже присвоен SN {FullSN} \n Печать готова!", Color.Green); return; }

                }


                if (WriteDB(0, Controllabel)) { return; }

                if (FullSTBSN.Length == 23)
                {
                    if (PrintBool) //Принт, печатать этикетку или нет
                    {
                        var printCodeSN = FullSTBSN.Substring(0, 22) + ">6" + FullSTBSN.Substring(22);
                        var PrintTextSN = FullSTBSN.Substring(0, 2) + " " + FullSTBSN.Substring(2, 4) + " " + FullSTBSN.Substring(6, 2) + " " + FullSTBSN.Substring(8, 2) +
                                          " " + FullSTBSN.Substring(10, 2) + " " + FullSTBSN.Substring(12, 3) + " " + FullSTBSN.Substring(15, 8);
                        print(LabelSN(PrintTextSN, printCodeSN)); //LabelStatus(Controllabel, $"Номер платы {TB.Text} /n успешно соединен с номером приемника", Color.Green);
                    }
                   
                    AddToOperLogFasStart(); //Добавляем в лог данные
                    ShiftCounter = ShiftCounter + 1; //+ к выпуску
                    ShiftCounterl.Text = ShiftCounter.ToString(); //Обновляем лейбл
                    ShiftCounterUpdate(); //обновляем ShiftCounter
                    DG_UpLog.Rows.Add(ShiftCounter,FullSTBSN,TB.Text, PrintBool,DateTime.UtcNow.AddHours(2));//Добавляем в грид строку
                    DG_UpLog.Sort(DG_UpLog.Columns[0], System.ComponentModel.ListSortDirection.Descending); //Сортировка
                }
                else if (FullSNPCB.Length != 23)
                {
                    LabelStatus(Controllabel, $"Серийный номeр не сформирован!", Color.Red);
                    update();
                }
            }
            else
            {                
              LabelStatus(Controllabel, $"{TB.Text} не верный формат номера", Color.Red); return;               
            }
        }

        void GetDate()
        {
            if (this.DateFas_Start)
                DateText = DateTime.UtcNow.AddHours(2);
            else
                DateText = DateTime.Parse(DateFas_ST_Text + " " + DateTime.Now.ToString("HH:mm:ss"));
        }


        void update()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                int _serNumber = int.Parse(SerNumber);
                var F = FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == _serNumber);
                F.FirstOrDefault().IsActive = false;
                FAS.SaveChanges();
            }
        }
        //void ShiftCounterUpdate()
        //{
        //    using (FASEntities1 FAS = new FASEntities1())
        //    {
        //        var F = FAS.FAS_ShiftsCounter.Where(c => c.ID == ShiftCounterID);
        //        F.FirstOrDefault().ShiftCounter = ShiftCounter;
        //        FAS.SaveChanges();
        //    }

        //}

        
        void print(string content)
        {
            RawPrinterHelper.SendStringToPrinter(printName, content); //Нужно получать ответ от принтера Для Кости 
        }

        bool WriteDB(int itter,Label label)
        {
            SerNumber = "";
            var _serialNumer = GetSerialNumber();
            updateSerNum(_serialNumer);
            Thread.Sleep(003);
            if (CheckSerialNumer(_serialNumer) == 0)
            {
                itter += 1;
                if (itter == 4)
                {
                    if (TempSN())
                    { LabelStatus(label, "В БАЗЕ ЗАКОНЧИЛИСЬ СЕРИЙНЫЕ НОМЕРА!", Color.Red); return true; }
                    LabelStatus(label, "Запись в базу не удалась, поробуйте еще раз!", Color.Red); return true;                 
                }
                else
                { WriteDB(itter, label); return false; }
            }
            else
            {
                InsertFasStart(_serialNumer);
                FullSTBSN = GenerateFullSTBSN(_serialNumer);
                if (string.IsNullOrEmpty(FullSTBSN) || FullSTBSN.Length != 23 )
                {
                    LabelStatus(label, $"FullSTBSN не с генерировался {FullSTBSN} ", Color.Red); return true;
                }
                UpdateSerialNumber(FullSTBSN, _serialNumer);
                return false;
            }

          
        }

        void UpdateSerialNumber(string FullSTBSN,int serialNumer)
        {
            using (var FAS = new FASEntities1())
            {
                int _serNumber = serialNumer;
                var _fas = FAS.FAS_Start.Where(c => c.SerialNumber == _serNumber);
                _fas.FirstOrDefault().FullSTBSN = FullSTBSN;
                _fas.FirstOrDefault().AssemblyByID = (short)UserID;
                FAS.SaveChanges();
               
            }
        }


        string GenerateFullSTBSN(int serialnumber)
        {
            using (FASEntities1 f = new FASEntities1())
            {
                string FullSTBSN;
                int _serNumber = serialnumber;
                var ProdDate = f.FAS_Start.Where(c => c.SerialNumber == _serNumber).Select(c => c.ManufDate).FirstOrDefault().ToString("ddMMyyyy");
                var FullSTBSN_Arr = "0" + ProdDate + LineForPrint + LotCode + _serNumber; 

                var D =  StringToIntArray(FullSTBSN_Arr);

                var result = (D[0] * 1 + D[1] * 2 + D[2] * 3 + D[3] * 4 + D[4] * 5 + D[5] * 6 + D[6] * 7 + D[7] * 8 + D[8] * 9 + D[9] * 10 +
                D[10] * 1 + D[11] * 2 + D[12] * 3 + D[13] * 4 + D[14] * 5 + D[15] * 6 + D[16] * 7 + D[17] * 8 + D[18] * 9 + D[19] * 10 +
                D[20] * 1 + D[21] * 2);

                var result2 = (D[0] * 3 + D[1] * 4 + D[2] * 5 + D[3] * 6 + D[4] * 7 + D[5] * 8 + D[6] * 9 + D[7] * 10 + D[8] * 1 + D[9] * 2 +
                D[10] * 3 + D[11] * 4 + D[12] * 5 + D[13] * 6 + D[14] * 7 + D[15] * 8 + D[16] * 9 + D[17] * 10 + D[18] * 1 + D[19] * 2 +
                D[20] * 3 + D[21] * 4);

                var r1 = result % 11;
                var r2 = result2 % 11;

                if (r1 == 10)
                    if (r2 == 10)
                        FullSTBSN = "0" + FullSTBSN_Arr;
                    else
                        FullSTBSN = r2.ToString() + FullSTBSN_Arr;
                else
                    FullSTBSN = r1.ToString() + FullSTBSN_Arr;

                return FullSTBSN;
            }
            
            List<int> StringToIntArray(string raw)
            {
                var d = new List<int>();

                for (int i = 0; i < raw.Length; i++)
                    d.Add(int.Parse(raw.Substring(i,1)));

                return d;
            }
            
        }

        

        bool TempSN()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                var  _serialNumber = FAS.FAS_SerialNumbers.Where(c => c.LOTID == LOTID & c.IsUsed == false).Select(c=>c.SerialNumber).FirstOrDefault();
                if (_serialNumber == 0)
                    return true;
                return false;
            }
        }

        void InsertFasStart(int serialnumber)
        {           
            using (FASEntities1 FAS = new FASEntities1())
            {
                var fas_start = new FAS_Start()
                {
                    SerialNumber = serialnumber,
                    PCBID =  PCBID,
                    LineID = (byte)LineID,
                    ManufDate = DateText,
                    AssemblyDate = DateTime.UtcNow.AddHours(2),
                    PrintStationID = (short)StationID   
                };
                FAS.FAS_Start.Add(fas_start);
                FAS.SaveChanges();
            }
        }

        int GetSerialNumber()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                return FAS.FAS_SerialNumbers.Where(c => c.LOTID == (short)LOTID && c.IsUsed == false).Select(c => c.SerialNumber).Take(1).FirstOrDefault();
            }
        }

        void updateSerNum(int serialNumber) 
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                var FAS_S = FAS.FAS_SerialNumbers.Where(c=>c.SerialNumber == serialNumber);
                FAS_S.FirstOrDefault().IsUsed = true;
                FAS_S.FirstOrDefault().PrintStationID = (short)StationID;
                FAS.SaveChanges();
            }
        }

        int CheckSerialNumer(int serialnumber)
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                return FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == serialnumber && c.PrintStationID == StationID).Select(c => c.SerialNumber).FirstOrDefault();

            }
        }


        string CheckAssemlyPCBID()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {                
                return  FAS.FAS_Start.Where(v => v.PCBID == PCBID).Select(c => c.FullSTBSN).FirstOrDefault();               
            }
        }

        bool CheckLazerPCBID(string barcode) //Проверка LazerBase и THT Start
        {
            using (SMDCOMPONETSEntities SMD = new SMDCOMPONETSEntities())
            {
                PCBID = SMD.LazerBase.Where(c => c.Content == barcode).Select(c => c.IDLaser).FirstOrDefault(); //LazerBase
                if (PCBID == 0)
                  return true;                                

                var check = SMD.THTStart.Where(c => c.PCBserial == barcode).Select(c => c.PCBserial == c.PCBserial).FirstOrDefault(); //THTStart

                if (!check)
                    return true;

                return false;

                //Добавить THT Start

            }
        }

        public override void GetComponentClass()
        {
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Visible = true;
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Location = new Point(LocX, LocY);
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Size = new Size(249, 260);
            var Grid = (DataGridView)control.Controls.Find("DG_LOTList", true).FirstOrDefault();
            GetLot(Grid);
        }

        void GetLot(DataGridView Grid)
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                var list = from Lot in FAS.FAS_GS_LOTs
                           join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
                           join Label in FAS.FAS_LabelScenario on Lot.LabelScenarioID equals Label.ID
                           where Lot.IsActive == true orderby Lot.LOTCode descending
                           select new
                           {
                               Lot = Lot.LOTCode,
                               Full_Lot = Lot.FULL_LOT_Code,
                               Model = model.ModelName,
                               InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               User = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Lot.LOTID,
                               Label.Scenario
                           };                 

                Grid.DataSource = list.ToList();

            }
        }

        string LabelSN(string printTextSN, string printCodeSN,int X = 20,int Y = 30)
        {         
            string L = $@"
^ XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH20,30^JMA^PR2,2^MD10^JUS^LRN^CI0^XZ
^XA
^MMT
^PW685
^LL0354
^LS0
положение и контент сервисная служба
^FO{44 + X},{185 + Y}^GFA,04352,04352,00068,:Z64:
eJzt1D9v1DAUAPBnMpgpb2UINt8AxlS4DR+Fj5CKAZ8uOlOdBBv9AsDnYEDgKBIdb2VA4KhCbJUjFg/hzHOuFCRaKIIBpHtjfPrd8/tjgG1sYxvb+C+i+XOCjxef4fpyBv4FQ4afnF3SED85w8sRoP6CUV54kpGRRc661hkwFkbuZl56nWfO6cpWDioWFVdKlRYdOPBl4eod50uo/ALyfeuAi8nIuraPB9GytfTzUAUtuPfaOOPAZLFIxpzokgytvCZDgxki5EMyim9GF222lsGM1aj3MDgdXXQQl1FgodT9sQqqBt0oH8ioIZKBycgVjnzFORld11n+WinTKKUX8prXbvCODctVMrQZb+ni1NADGX3XnholGYccqR6sW1oOZdHsqUIvFDgNg3OZzVaYC6WbPSjzkoyyJiPU4DoLeEBGoWUy5JmhCy0oj7Wim58ahygkFUGQoaAcy9lYD8GzPhnMelAegYzrXSSjswhmJCPMyIgB/FcDo0sGL0Ct9extvR8c9G2AnFEek4F8lwxDNd0YlZ+9S4b0kQz6i4Ing24tQJ3UyRgdDO2Y8qC71JOx08U+2jNjnE+G8WZjvJiMKqQ8Tuqdt27+hBpmF5s8hJaAyPfJ6CdDQ6qHeZYMf2bgLtVDpXpcX9W779z8qYX4QG7qQX2BHPnQtcdte/zVEEakvtAkJQOz59QXLXTqi1z56rG7/dQyC3yTR5oxgbxPRpqPKxpoPJJxlQDvyTjELKTeFlrRfMgjJw9dJe1ARo6sn2b9hrjJWzKmGXuori4IWQhRpjycY/0rZIHmlBquaU7xERm2QjvEjGPOPpGRy7WJR2T0H5Oxrmggd5tGyOD1QPPBIhmehGpsQukn49HG6N5LZNEDo3eMjINpX5bTvqSda8S0L2RAfLkxZKjewB0yrORW4gMfj9cyh3g3vR/JSLv/+Whp2Yhh2v3N3t6jihlFhlYCnXySfr60OBnDh2FEhObu9IbUVOLvQv/4yrhzXp7U1W/xS4P9K4b94RN9jN/LvzSy8wzal/O+/p5hLia2sY1tbOP/ii8gSQz/:A196
положение и контент general satellite
^FO{224 + X},{217 + Y}^GFA,02688,02688,00028,:Z64:
eJzt1M1KxDAQB/CEHPYYwatsHmFfoDT4RF4rVLOwh30LH0UKHjzuIxjpxYNgZBEjhIwzST+268dREDpdevkxZPJPs4zNNdc/qLP0PsGHfl1xwPI8MBWMd6UOYBe2zCbInAhMo3k04waTyWRgBi2QedmbJrMKjdUsliW7CtJ1ZsgaHVjorKpVb5DMBObRINnSl+OYsEZzzDy93OpntGpigFYY/35Ls5SdpS0A7y2SFXW2xYHVbB9LZlxdHFgUnb2SWRMPLCzQfGeXrjc5GOXicL0PB/gezMvernWonFfHRhNazLP6sHKVTKWoFZry2LdEu3JSj2Y1Gl8fn/doX0vlqH+29W8GPxv73mh/kZPpJhgw+6YQO3gY9h5PyUxn9cSCIINv+zBqtLfzaKAWuW9YzydzPAqLVoimUsP5YdRkIls9MYwaLYidsKbNfWk9kb5qsphsn/tGW5FdbHebaR/PUdP53W830z6Wo0aT97CZzkl3hTywxQ7upnPiHYsTA70fDD8VngwexB2uR7H160nwItuNaMn0aByazpa8PerDu5zNl/yxMS3odrTx72Suueb60/oEc4oKnw==:460E
модель
^FT{20 + X},{32 + Y}^A0N,33,33^FH\^FD{ArrayList[5]}^FS
дата
^FT{320 + X},{32 + Y}^A0N,33,33^FH\^FD{DateText.ToString("dd.MM.yyyy")}^FS
время
^FT{490 + X},{32 + Y}^A0N,33,33^FH\^FD{DateText.ToString("HH:mm:ss")}^FS
сн_текст 
^FT{86 + X},{68 + Y}^A0N,33,36^FH\^FD{printTextSN}^FS
штрихкод
^BY3,3,125^FT{45 + X},{197 + Y}^BCN,,N,N
^FD>;{printCodeSN}^FS
made_QC
^FT{20 + X},{282 + Y}^A0N,33,31^FH\^FDMade in Russia^FS
^FT{448 + X},{282 + Y}^A0N,33,33^FH\^FDQC Passed^FS
количество этикеток
^PQ{labelCount},0,1,Y^XZ";

            return L;
        }


    }
}
