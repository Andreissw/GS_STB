using GS_STB.Forms_Modules;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    public abstract class BaseClass
    {
        List<string> PrintSetpath = new List<string>() { @"C:\PrinterSettings\XSN,0.txt", @"C:\PrinterSettings\YSN,0.txt", @"C:\PrinterSettings\YID,0.txt", @"C:\PrinterSettings\XID,0.txt" };
        public Control control { get; set; }
        public DataGridView GridRange = new DataGridView();
        public string printName { get; set; }
        //public bool PrintBool { get; set; }
        public int labelCount { get; set; }
        public int ShiftID { get; set; }
        public bool CheckBoxDublicateSCID { get; set; }
        public int LabelScenarioID { get; set; }
        //public int SelectedRow { get; set; }
        public int LocX { get; } = 827;
        public int LocY { get; } = 306;
        public ArrayList ArrayList { get; set; }
        public bool DateFas_Start { get; set; }
        public string DateFas_ST_Text { get; set; }
        public int LotCode { get; set; }
        public int PCBID { get; set; }
        public string LineForPrint { get; set; }
        public short LitIndex { get; set; }
        public int StartRangeLot { get; set; }
        public int EndRangeLot { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public int ShiftCounter { get; set; }
        public int LotCounter { get; set; }
        public int ShiftCounterID { get; set; }
        public int UserID { get; set; }
        public int LineID { get; set; }
        public List<string> ListHeader { get; set; }
        public int IDApp { get; set; }
        public int StationID { get; set; }
        public int LOTID { get; set; }
        public bool UpPrintSN { get; set; }
        public bool UpPrintID { get; set; }
        public int UpPrintCountSN { get; set; }
        public int UpPrintCountID { get; set; }
        public string COMPORT { get; set; }
        //public int Delay { get; set; } = 200;
        public bool CheckGetSN { get; set; } = false;
        public CancellationTokenSource cts { get; set; }
        public CancellationToken token { get; set; }
        public abstract void KeyDownMethod();
        public abstract void GetComponentClass();
        public abstract void LoadWorkForm();
        public void LabelStatus(Label label, string TEXT, Color color)
        {
            control.Invoke((Action)(() =>
            {
                label.Visible = true;
                label.Text = TEXT;
                label.ForeColor = color;
            }));
        }

        public bool CheckPathPrinterSettings()
        {
            if (!Directory.Exists(@"C:\PrinterSettings"))
                return true;

            var list = Directory.GetFiles(@"C:\PrinterSettings");
            if (list.Count() != 4)
                return true;

            return false;
        }

        public void CreatePathPrinter()
        {
            Directory.CreateDirectory(@"C:\PrinterSettings");
            foreach (var item in PrintSetpath)
            {
                var line = Directory.GetFiles(@"C:\PrinterSettings").Where(c => c.Contains(item.Substring(19, 3))).FirstOrDefault();

                if (!string.IsNullOrEmpty(line))
                    continue;

                var Fs = File.Create(item); Fs.Close();
            }

        }

      public ArrayList GetInfoPacing(int serialnumber)
        {
            using (var FAS = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = (from pac in FAS.FAS_PackingGS
                            join lit in FAS.FAS_Liter on pac.LiterID equals lit.ID
                            join us in FAS.FAS_Users on pac.PackingByID equals us.UserID
                            from st in FAS.FAS_Start.Where(c => c.SerialNumber == pac.SerialNumber).DefaultIfEmpty()
                            where pac.SerialNumber == serialnumber
                            select new
                            {
                                fullst = !st.FullSTBSN.Equals(null),
                                liter = lit.LiterName + pac.LiterIndex,
                                pac.PalletNum,
                                pac.BoxNum,
                                pac.UnitNum,
                                pac.PackingDate,
                                us.UserName

                            });

                if (list.Count() == 0)
                    return ArrayList;

                var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;
            }
        }

        public void GetLineForPrint()
        {
            using (FASEntities FAS = new FASEntities())
            {
                LineForPrint = FAS.FAS_Lines.Where(c => c.LineID == LineID).Select(c => c.Print_Line).FirstOrDefault();
            }
        }

        public string GetPrSet(List<string> list, string mask)
        {
            var X = list.Where(c => c.Contains(mask)).FirstOrDefault();
            X = X.Substring(X.IndexOf(',') + 1, X.IndexOf('.') - X.IndexOf(',') - 1);
            return X;
        }
        public void ShiftCounterUpdate()
        {
            using (FASEntities FAS = new FASEntities())
            {
                var date = DateTime.UtcNow.Day + DateTime.UtcNow.Month + DateTime.UtcNow.Year;
                var FF = FAS.FAS_ShiftsCounter.Where(c => c.StationID == StationID & c.ShiftID == ShiftID & c.ID_App == IDApp & c.CreateDate.Day + c.CreateDate.Month + c.CreateDate.Year == date).OrderByDescending(c=>c.CreateDate);
                //var FF = FAS.FAS_ShiftsCounter.Where(c => c.StationID == StationID & c.ShiftID == ShiftID & c.ID_App == IDApp & c.CreateDate == DateTime.UtcNow);

                FF.FirstOrDefault().ShiftCounter = ShiftCounter;
                FAS.SaveChanges();
            }

        }
        
        public void LotCounterUpdate()
        {
            using (FASEntities FAS = new FASEntities())
            {
                var date = DateTime.UtcNow.Day + DateTime.UtcNow.Month + DateTime.UtcNow.Year;

                var F = FAS.FAS_ShiftsCounter.Where(c => c.ID == ShiftCounterID);
                F.FirstOrDefault().LOT_Counter = LotCounter;
                FAS.SaveChanges();
            }

        }
        public void ShiftCounterStart()
        {           
            var datenow = DateTime.UtcNow.AddHours(2);
            //var datenow = DateTime.Parse("26.11.2020 19:32:00");
            var Hour = datenow.Hour;
            ShiftID = 0;

            var l = datenow; //Сейчас
            var r = Convert.ToDateTime("07:30:00");
            var r1 = Convert.ToDateTime("08:00:00");
            var x = Convert.ToDateTime("19:30:00");
            var x2 = Convert.ToDateTime("20:00:00");

            if (l >= r & l<= r1)          
                ShiftID = GetShiftID();   
            else if (l >= x & l <= x2)        
                ShiftID = GetShiftID(); 
            else if (Hour >= 21 || Hour < 8) //Ночь
                ShiftID = 4;
            else if (Hour >= 8 || Hour <= 21) //День
                ShiftID = 3;

            FindShiftID();

            void FindShiftID()
            {
                using (FASEntities FAS = new FASEntities())
                {
                    var date = DateTime.UtcNow.Day + DateTime.UtcNow.Month + DateTime.UtcNow.Year;

                    var lists = FAS.FAS_ShiftsCounter.OrderByDescending(c => c.CreateDate)
                        .Where(c => c.StationID == StationID & c.ID_App == IDApp & c.ShiftID == ShiftID & c.LOTID == LOTID & c.CreateDate.Day + c.CreateDate.Month + c.CreateDate.Year == date)
                        .Select(c => new { c.ID, c.ShiftCounter,c.LOT_Counter }).ToList();

                    if (lists.Count() == 0)
                    { AddShift(); FindShiftID(); return; }

                    foreach (var item in lists)
                    { ShiftCounterID = item.ID; ShiftCounter = item.ShiftCounter; LotCounter = (int)item.LOT_Counter; break; }
                }
            }

            void AddShift() //запись в шифткоунтер
            {
                using (FASEntities FAS = new FASEntities())
                {
                    var Shift = new FAS_ShiftsCounter()
                    {
                        StationID = (short)StationID,
                        ID_App = (short)IDApp,
                        ShiftID = (byte)ShiftID,
                        ShiftCounter = 0,
                        CreateDate = DateTime.UtcNow.AddHours(2),
                        LOTID = LOTID,
                        LOT_Counter = 0,
                        PassLOTRes = 0,
                        FAilLOTRes = 0                        
                    };

                    FAS.FAS_ShiftsCounter.Add(Shift);
                    FAS.SaveChanges();
                }

            }
        }

        int GetShiftID()
        {
            var mes = new msg("Выберите смену","Ночная смена","Дневная смена");
            var Result = mes.ShowDialog();            
            if (Result == DialogResult.OK)
                return 3;
            else
                return 4;
        }
        public void WriteToDBDesis(int serial,string FullSerial,int error)
        {
            using (var FAS = new FASEntities())
            {
                var des = new FAS_Disassembly()
                {
                    SerialNumber = serial,
                    FullSTBSN = FullSerial,
                    PCBID = PCBID,
                    LOTID = (short)LOTID,
                    ErrorCodeID = (short)error,
                    DisAssemblyLineID = (byte)LineID,
                    DisassemblyDate = DateTime.UtcNow.AddHours(2),
                    DisassemblyByID = (short)UserID
                };
                FAS.FAS_Disassembly.Add(des);
                FAS.SaveChanges();
            }
        }
        public void UpdateToDBDesis(int serial)
        {
            using (var FAS = new FASEntities())
            {
                var fas = FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == serial);
                fas.FirstOrDefault().IsUsed = false;
                fas.FirstOrDefault().IsUploaded = false;
                fas.FirstOrDefault().IsWeighted = false;                
                FAS.SaveChanges();

            }
        }
        public void DeleteToDBDesis(int serial)
        {
            using (var FAS = new FASEntities())
            {
                var fas = FAS.FAS_Start.Where(c => c.SerialNumber == serial).FirstOrDefault();
                FAS.FAS_Start.Remove(fas);
                FAS.SaveChanges();
            }
        }
        public void DeleteToDBWeight(int serial)
        {
            using (var FAS = new FASEntities())
            {
                var fas = FAS.FAS_WeightStation.Where(c => c.SerialNumber == serial).FirstOrDefault();
                if (fas == null)
                    return;

                FAS.FAS_WeightStation.Remove(fas);
                FAS.SaveChanges();
            }
        }
        public void DeleteToUpload(int serial)
        {
            using (var FAS = new FASEntities())
            {
                var fas = FAS.FAS_Upload.Where(c => c.SerialNumber == serial).FirstOrDefault();
                if (fas == null)
                    return;

                FAS.FAS_Upload.Remove(fas);
                FAS.SaveChanges();

            }
        }
        public void AddLogDesis(int serial,int appid,long smartcardID, string fullstbsn,string CASID)
        {
            using (var FAS = new FASEntities())
            {
                var op = new FAS_OperationLog()
                {
                    PCBID = PCBID,
                    ProductionAreaID = (byte)LineID,
                    StationID = (short)StationID,
                    ApplicationID = (short)appid,
                    StateCodeDate = DateTime.UtcNow.AddHours(2),
                    StateCodeByID = (short)UserID,
                    SerialNumber = serial,
                    SmartCardId = smartcardID,
                    FullSTBSN = fullstbsn,
                    CASID = CASID


                };
                FAS.FAS_OperationLog.Add(op);
                FAS.SaveChanges();
            }
        }
        public void AddToOperLogFasStart(string FullSTBSN)
        {
            using (FASEntities FAS = new FASEntities())
            {
                var FasOp = new FAS_OperationLog()
                {
                    PCBID = PCBID,
                    ProductionAreaID = (byte)LineID,
                    StationID = (short)StationID,
                    ApplicationID = (short)IDApp,
                    StateCodeDate = DateTime.UtcNow.AddHours(2),
                    StateCodeByID = (short)UserID,
                    SerialNumber = int.Parse(FullSTBSN.Substring(15)),
                    FullSTBSN = FullSTBSN
                };

                FAS.FAS_OperationLog.Add(FasOp);
                FAS.SaveChanges();
            }
        }

        public void AddToOperLogFASEND(string FullSTBSN, int pcbid)
        {
            using (FASEntities FAS = new FASEntities())
            {
                var FasOp = new FAS_OperationLog()
                {
                    PCBID = pcbid,
                    ProductionAreaID = (byte)LineID,
                    StationID = (short)StationID,
                    ApplicationID = (short)IDApp,
                    StateCodeDate = DateTime.UtcNow.AddHours(2),
                    StateCodeByID = (short)UserID,
                    SerialNumber = int.Parse(FullSTBSN.Substring(15)),
                    FullSTBSN = FullSTBSN
                };

                FAS.FAS_OperationLog.Add(FasOp);
                FAS.SaveChanges();
            }
        }
        public bool CheckPacking(int serialnumber)
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_PackingGS.Select(c => c.SerialNumber == serialnumber).FirstOrDefault();
            }
        }
        public void GetPortName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity");
            var R = searcher.Get();
            try
            {

          
            foreach (var item in R)
            {
                    var Result = item["Name"];
                    if (Result != null)                   
                        if (Result.ToString().Contains("COM"))
                        {
                            var Name = item["Name"].ToString();
                            if (Name.IndexOf("COM") == 17)
                            {
                                COMPORT = Name.Substring(17, 4);    return;
                            }
                           
                        }
            }

            }
            catch (Exception)
            {

                return;
            }
        }

       public ArrayList GetSerialNum(int shortSN)
        {
            using (var Fas = new FASEntities())
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
       public string CheckLazer(int pcbid)
        {
            using (var smd = new SMDCOMPONETSEntities())
            {
                return smd.LazerBase.Where(c => c.IDLaser == pcbid).Select(c => c.Content).FirstOrDefault();
            }
        }

        public DateTime GetManufDate(string SN)
        {
            using (var _fas = new FASEntities())
            {
                return _fas.FAS_Start.Where(c => c.FullSTBSN == SN).Select(c => c.ManufDate).FirstOrDefault();
            }
        }
    }
}
