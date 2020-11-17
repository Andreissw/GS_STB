using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    
    class FAS_END: BaseClass
    {
        public string Liter;
        public int unitNum;
        ArrayList Arraylpac = new ArrayList();        
        public FAS_END()
        {
            ListHeader = new List<string>() { "№", "Serial", "Litera","GroupBox","Pallet","ScanDate" };
            IDApp = 2;
        }
      
        public override void LoadWorkForm()
        {
            
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();
            var FUG = control.Controls.Find("FAS_EndGB", true).FirstOrDefault();
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label NextBoxNum = control.Controls.Find("NextBoxNum", true).OfType<Label>().FirstOrDefault();
            Label PalletNum = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();

            Liter = GetLiter();
            var literID = GetLiterID();
            FUG.Visible = true;
            FUG.Location = new Point(6, 0);
            FUG.Size = new Size(533, 236);

            if (!CheckCounter())
            { AddPacCounter(); BoxNum.Text = "1"; PalletNum.Text = "1"; NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString(); }
            else
            {
                Arraylpac = GetpackingCounter();                
                PalletNum.Text = Arraylpac[0].ToString();
                BoxNum.Text = Arraylpac[1].ToString();
                NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString();

                if (Arraylpac[0].ToString() != ArrayList[7].ToString())
                {                   
                    var grid = GetDatePac(short.Parse(PalletNum.Text), short.Parse(BoxNum.Text), byte.Parse(literID),short.Parse(ArrayList[6].ToString()));
                    Grid.RowCount = grid.Count();
                    for (int i = 0; i < Grid.RowCount; i++)
                    {
                        Grid[0, i].Value = grid[i].UnitNum;
                        Grid[1, i].Value = grid[i].SerialNumber;
                        Grid[2, i].Value = grid[i].Littera;
                        Grid[3, i].Value = grid[i].BoxNum;
                        Grid[4, i].Value = grid[i].PalletNum;
                        Grid[5, i].Value = grid[i].PackingDate;
                    }
                }
            }
            ShiftCounterStart(control);


        }
        public override void KeyDownMethod()
        {
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
            if (TB.TextLength != 23)
            { LabelStatus(Controllabel, "Не верный номер", Color.Red); Grid.BackgroundColor = Color.Red; return; }

            if (CheckCurrentDublicate())
            { LabelStatus(Controllabel, $"{TB.Text} уже был отсканирован в этой коробке", Color.Red); return; }

            if (CheckSN(Controllabel,int.Parse(TB.Text.Substring(16))))
            {  return; }

            if (Grid.RowCount < int.Parse(ArrayList[7].ToString()))
                UnitCounter(control,Grid,TB);
            else if (Grid.RowCount == int.Parse(ArrayList[7].ToString()))
                BoxAndPalletCounter(control,Grid,TB);

            WriteToDB();

            LabelStatus(Controllabel, $"{TB.Text} Номер успешно добавлен", Color.Green);

        }

        public override void GetComponentClass()
        {           

            var Grid = (DataGridView)control.Controls.Find("DG_LOTList", true).FirstOrDefault();
            GetLot(Grid);
        }

        void WriteToDB()
        { 
        
        }

        void BoxAndPalletCounter(Control control, DataGridView Grid, TextBox TB)
        {
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label NextBoxNum = control.Controls.Find("NextBoxNum", true).OfType<Label>().FirstOrDefault();
            Label PalletNum = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();

            BoxNum.Text = (int.Parse( BoxNum.Text) + 1).ToString();
            NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString();
            Grid.RowCount = 0;
            Grid.BackgroundColor = Color.Gold;
            Grid.Rows.Add(1, TB.Text, Liter, BoxNum.Text, PalletNum.Text, DateTime.UtcNow.AddHours(2));

            if(( int.Parse(BoxNum.Text) % int.Parse(ArrayList[8].ToString())) == 1)          
                PalletNum.Text = (int.Parse(PalletNum.Text) + 1).ToString();
         


        }
        void UnitCounter(Control control,DataGridView Grid, TextBox TB)
        {
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label PalletNum = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();
            Grid.Rows.Add(Grid.RowCount + 1,TB.Text,Liter, BoxNum.Text, PalletNum.Text, DateTime.UtcNow.AddHours(2));
            if (Grid.RowCount != 0)
                Grid.Sort(Grid.Columns[0], System.ComponentModel.ListSortDirection.Descending);

            if (Grid.RowCount == int.Parse(ArrayList[7].ToString()))
                Grid.BackgroundColor = Color.Green;
            else
                Grid.BackgroundColor = Color.Gold;

        }
        ArrayList GetSerialNum(int shortSN)
        {
            using (var Fas = new FASEntities1())
            {
                var ArrayList = new ArrayList();
                var list = (from S in Fas.FAS_SerialNumbers
                            join F in Fas.FAS_Start on S.SerialNumber equals F.SerialNumber
                            where LOTID == LOTID & S.SerialNumber == shortSN
                            select new { S.IsUsed, S.IsActive, S.IsUploaded, S.IsWeighted, S.IsPacked, S.InRepair, F.PCBID }).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }

        ArrayList GetInfoPac(int serial)
        {
            using (var Fas = new FASEntities1())
            {
                var ArrayList = new ArrayList();
                var list = (from pac in Fas.FAS_PackingGS
                            join lit in Fas.FAS_Liter on pac.LiterID equals lit.ID
                            join Use in Fas.FAS_Users on pac.PackingByID equals Use.UserID
                            where pac.SerialNumber == serial
                            select new { Liter = lit.LiterName + pac.LiterIndex, pac.PalletNum, pac.BoxNum, pac.UnitNum, pac.PackingDate, Use.UserName }).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }

        bool CheckSN(Label TB,int snshort)
        {
            var list = GetSerialNum(snshort);

            if (list[0].ToString() == "true" & list[1].ToString() == "true" & list[2].ToString() == "true" & list[3].ToString() == "true" & list[4].ToString() == "false" & list[5].ToString() == "false")
                return false;
            if (list[0].ToString() == "true" & list[1].ToString() == "false" & list[4].ToString() == "false" & list[5].ToString() == "true")
            { LabelStatus(TB,$"{snshort} находится в ремонте",Color.Red); return true; }
            if (list[0].ToString() == "true" & list[1].ToString() == "true" & list[2].ToString() == "false" & list[4].ToString() == "false" & list[5].ToString() == "false")
            { LabelStatus(TB, $"{snshort} не прошит в приемник", Color.Red); return true; }
            if (list[0].ToString() == "true" & list[1].ToString() == "true" & list[2].ToString() == "true" & list[3].ToString() == "false" & list[4].ToString() == "false" & list[5].ToString() == "false")
            { LabelStatus(TB, $"{snshort} не прошел весовой контроль", Color.Red); return true; }
            if (list[0].ToString() == "true" & list[1].ToString() == "true" & list[2].ToString() == "true" & list[3].ToString() == "true" & list[4].ToString() == "true" & list[5].ToString() == "false")
            { 
                var info = GetInfoPac(snshort);
                { LabelStatus(TB, $"номер {info[4].ToString()} уже упакован /n Литер {info[0].ToString()}, Паллет {info[1].ToString()} /n Групповая {info[2].ToString()} /n Дата упаковки {info[5].ToString()} Упакован: {info[6].ToString()}  ", Color.Red); return true; }

            }

            LabelStatus(TB, "Метод CheckSN не пройден - обратитесь к Технологу", Color.Red);
            return true;
            //ElseIf IsUsed = True And IsActiv = True And IsUploaded = True And IsWeighted = True And IsPacked = True And inRepair = False Then
            

        }
        bool CheckCurrentDublicate()
        {
            return true;
        }

        string GetLiter()
        {
            using (var Fas = new FASEntities1())
            {
                var linename = ArrayList[2].ToString();
                return Fas.FAS_Liter.Where(c => c.Description == linename).Select(c => c.LiterName).FirstOrDefault();
            }
        }

        string GetLiterID()
        {
            using (var Fas = new FASEntities1())
            {
                var linename = ArrayList[2].ToString();
                return Fas.FAS_Liter.Where(c => c.Description == linename).Select(c => c.ID).FirstOrDefault().ToString();
            }
        }
       List<GridInfo> GetDatePac(short palletNum, short BoxNum,byte literID,short litterIndex)
        {

            using (var Fas = new FASEntities1())
            {
                var ArrayList = new ArrayList();
                
                 var list = (from pac in Fas.FAS_PackingGS
                            join st in Fas.FAS_Start on pac.SerialNumber equals st.SerialNumber
                            join l in Fas.FAS_Liter on pac.LiterID equals l.ID
                            where pac.PalletNum == palletNum & pac.BoxNum == BoxNum & pac.LiterID == literID & pac.LiterIndex == litterIndex & pac.LOTID == LOTID orderby pac.UnitNum descending
                            select new GridInfo(){UnitNum =  pac.UnitNum, SerialNumber = st.FullSTBSN,Littera = l.LiterName + pac.LiterIndex,BoxNum = pac.BoxNum, PalletNum = pac.PalletNum, PackingDate = pac.PackingDate }).ToList();
                return list;
                
            }
        }

        ArrayList GetpackingCounter()
        {

            using (var Fas = new FASEntities1())
            {
                var ArrayList = new ArrayList();
                var list = (from pac in Fas.FAS_PackingCounter
                            where pac.LOTID == LOTID && pac.LineID == LineID
                            select new { pac.PalletCounter, pac.BoxCounter}).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }

        void AddPacCounter()
        {
            using (var FAS = new FASEntities1())
            {
                var Pac = new FAS_PackingCounter()
                {
                    PalletCounter = 1,
                    BoxCounter = 1,
                    UnitCounter = 1,
                    LineID = (byte)LineID,
                    LOTID = (short)LOTID
                };
                FAS.FAS_PackingCounter.Add(Pac);
                FAS.SaveChanges();
            }
        }

        bool CheckCounter()
        {
            using (var FAS = new FASEntities1())
            {
                return FAS.FAS_PackingCounter.Where(c => c.LOTID == LOTID & c.LineID == LineID).Select(c => c.ID == c.ID).FirstOrDefault();
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
                               Lot.LiterIndex,                              
                               Lot.BoxCapacity,
                               Lot.PalletCapacity,
                               Lot.LOTID,
                               InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               User = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                                                           
                           };

                Grid.DataSource = list.ToList();

            }
        }
    }
}
