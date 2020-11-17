using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    public abstract class  BaseClass
    {
        List<string> PrintSetpath = new List<string>() { @"C:\PrinterSettings\XSN,0.txt", @"C:\PrinterSettings\YSN,0.txt", @"C:\PrinterSettings\YID,0.txt", @"C:\PrinterSettings\XID,0.txt" };
        public Control control { get; set; }
        public string printName { get; set; }
        public bool PrintBool { get; set; }
        public int labelCount { get; set; }
        public bool CheckBoxDublicateSCID{ get; set; }
        public int  LabelScenarioID { get; set; }
        public int SelectedRow { get; set; }
        public int LocX { get; } = 1042;
        public int LocY { get; } = 291;
        public ArrayList ArrayList { get; set; }
        public bool DateFas_Start { get; set; }
        public string DateFas_ST_Text { get; set; }
        public int LotCode { get; set; }
        public int PCBID { get; set; }
        public string LineForPrint { get; set; }
        //public int literindex { get; set; }
        //public int BoxCapacity { get; set; }
        //public int PalletCapacity { get; set; }
        public int ShiftCounter { get; set; }
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


        public abstract void KeyDownMethod();

        public abstract void GetComponentClass();

        public abstract void LoadWorkForm();

        public void LabelStatus(Label label, string TEXT, Color color)
        {
            control.Invoke((Action)(() =>
            {
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

        public void GetLineForPrint()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
               LineForPrint = FAS.FAS_Lines.Where(c => c.LineID == LineID).Select(c => c.Print_Line).FirstOrDefault();
            }
        }

        public void ShiftCounterUpdate()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                var date = DateTime.UtcNow.Day + DateTime.UtcNow.Month + DateTime.UtcNow.Year;

                var FF = FAS.FAS_ShiftsCounter.Where(c => c.StationID == StationID && c.ID_App == IDApp & c.CreateDate.Day + c.CreateDate.Month + c.CreateDate.Year == date);
                FF.FirstOrDefault().ShiftCounter = ShiftCounter;

                var F = FAS.FAS_ShiftsCounter.Where(c => c.ID == ShiftCounterID);
                F.FirstOrDefault().LOT_Counter = 1;
                FAS.SaveChanges();
            }

        }

        public void ShiftCounterStart(Control control)
        {
            Label ShiftCounterl = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            var datenow = DateTime.UtcNow.AddHours(2);
            var Hour = datenow.Hour;
            int ShiftID = 0;

            if (Hour >= 8 & Hour <= 20)
                ShiftID = 3;
            else if (Hour >= 20 & Hour <= 8)
                ShiftID = 4;

            FindShiftID();

            void FindShiftID()
            {
                using (FASEntities1 FAS = new FASEntities1())
                {
                    var date = DateTime.UtcNow.Day + DateTime.UtcNow.Month + DateTime.UtcNow.Year;

                    var lists = FAS.FAS_ShiftsCounter
                        .Where(c => c.StationID == StationID & c.ID_App == IDApp & c.ShiftID == ShiftID & c.LOTID == LOTID & c.CreateDate.Day + c.CreateDate.Month + c.CreateDate.Year == date)
                        .Select(c => new { c.ID, c.ShiftCounter }).ToList();

                    if (lists.Count() == 0)
                    { AddShift(); FindShiftID(); return; }

                    foreach (var item in lists)
                    { ShiftCounterID = item.ID; ShiftCounterl.Text = item.ShiftCounter.ToString(); break; }

                }
            }

            void AddShift() //запись в шифткоунтер
            {
                using (FASEntities1 FAS = new FASEntities1())
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

        public void WriteToDBDesis(int serial,string FullSerial,int error)
        {
            using (var FAS = new FASEntities1())
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
            using (var FAS = new FASEntities1())
            {
                var fas = FAS.FAS_SerialNumbers.Where(c => c.SerialNumber == serial);
                fas.FirstOrDefault().IsActive = false;
                fas.FirstOrDefault().InRepair = true;               

            }
        }

        public void DeleteToDBDesis(int serial)
        {
            using (var FAS = new FASEntities1())
            {
                var fas = FAS.FAS_Start.Where(c => c.SerialNumber == serial).FirstOrDefault();
                FAS.FAS_Start.Remove(fas);
                FAS.SaveChanges();

            }
        }

        public void AddLogDesis(int serial,int appid)
        {
            using (var FAS = new FASEntities1())
            {
                var op = new FAS_OperationLog()
                {
                    PCBID = PCBID,
                    ProductionAreaID = (byte)LineID,
                    StationID = (short)StationID,
                    ApplicationID = (short)appid,
                    StateCodeDate = DateTime.UtcNow.AddHours(2),
                    StateCodeByID = (short)UserID,
                    SerialNumber = serial
                };
                FAS.FAS_OperationLog.Add(op);
                FAS.SaveChanges();
            }

        }

     

        public void AddToOperLogFasStart()
        {
            using (FASEntities1 FAS = new FASEntities1())
            {
                var FasOp = new FAS_OperationLog()
                {
                    PCBID = PCBID,
                    ProductionAreaID = (byte)LineID,
                    StationID = (short)StationID,
                    ApplicationID = (short)IDApp,
                    StateCodeDate = DateTime.UtcNow.AddHours(2),
                    StateCodeByID = (short)UserID
                };

                FAS.FAS_OperationLog.Add(FasOp);
                FAS.SaveChanges();
            }
        }


    }
}
