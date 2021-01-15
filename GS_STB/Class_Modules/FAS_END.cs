using GS_STB.Forms_Modules;
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
        List<short> Arraylpac = new List<short>();
        DateTime DateText;
        int SerNumber;
        string FullSTBSN;
        int ShortSN;
        byte literID;
        Label Controllabel = new Label();
        Label ControllabelFASEND = new Label();
        public FAS_END()
        {
            ListHeader = new List<string>() { "№", "Serial", "Litera","GroupBox","Pallet","ScanDate" };
            IDApp = 2;
        }

        void GetDataPac()
        {
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label NextBoxNum = control.Controls.Find("NextBoxNum", true).OfType<Label>().FirstOrDefault();
            Label PalletNum = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();
            Grid.RowCount = 0;
            if (!CheckCounter()) //Проверка на первый запуск по лоту и линии
            { AddPacCounter(); Arraylpac = GetpackingCounter(); BoxNum.Text = "1"; PalletNum.Text = "1"; NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString(); }
            else
            {
                // pac.PalletCounter[0], pac.BoxCounter[1], pac.UnitCounter[2]
                Arraylpac = GetpackingCounter();
                PalletNum.Text = Arraylpac[0].ToString();
                BoxNum.Text = Arraylpac[1].ToString();
                NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString();

                if (Arraylpac[2].ToString() != ArrayList[7].ToString())
                {
                    var grid = GetDatePac(short.Parse(PalletNum.Text), short.Parse(BoxNum.Text), literID);
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
        }
      
        public override void LoadWorkForm()
        {
            
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();
            var FUG = control.Controls.Find("FAS_EndGB", true).FirstOrDefault();
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label NextBoxNum = control.Controls.Find("NextBoxNum", true).OfType<Label>().FirstOrDefault();
            Label PalletNum = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();
            Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();

            Liter = GetLiter();
            literID = GetLiterID();
            ArrayList[6] = Liter + " " + LitIndex;

            FUG.Visible = true;
            FUG.Location = new Point(179, 11);
            FUG.Size = new Size(536, 209);

            GetDataPac();

            #region
            //if (!CheckCounter()) //Проверка на первый запуск по лоту и линии
            //{ AddPacCounter(); Arraylpac = GetpackingCounter(); BoxNum.Text = "1"; PalletNum.Text = "1"; NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString(); }
            //else
            //{
            //    // pac.PalletCounter[0], pac.BoxCounter[1], pac.UnitCounter[2]
            //    Arraylpac = GetpackingCounter();  
            //    PalletNum.Text = Arraylpac[0].ToString();
            //    BoxNum.Text = Arraylpac[1].ToString();
            //    NextBoxNum.Text = (int.Parse(BoxNum.Text) + 1).ToString();

            //    if (Arraylpac[2].ToString() != ArrayList[7].ToString())
            //    {                   
            //        var grid = GetDatePac(short.Parse(PalletNum.Text), short.Parse(BoxNum.Text), literID);
            //        Grid.RowCount = grid.Count();
            //        for (int i = 0; i < Grid.RowCount; i++)
            //        {
            //            Grid[0, i].Value = grid[i].UnitNum;
            //            Grid[1, i].Value = grid[i].SerialNumber;
            //            Grid[2, i].Value = grid[i].Littera;
            //            Grid[3, i].Value = grid[i].BoxNum;
            //            Grid[4, i].Value = grid[i].PalletNum;
            //            Grid[5, i].Value = grid[i].PackingDate;
            //        }
            //    }
            //}
            #endregion

            ShiftCounterStart();
            Label_ShiftCounter.Text = ShiftCounter.ToString();
            LB_LOTCounter.Text = LotCounter.ToString();

        }
        public override void KeyDownMethod()
        {
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
            ControllabelFASEND = control.Controls.Find("FASENDLB", true).OfType<Label>().FirstOrDefault();

            Controllabel.Text = "";
            ControllabelFASEND.Text = "";

            if (TB.TextLength != 23)
            { LabelStatus(Controllabel, "Не верный номер", Color.Red); Grid.BackgroundColor = Color.Red; return; }
            FullSTBSN = TB.Text;

            int k = 0;
            if (!int.TryParse(TB.Text.Substring(15), out k))
            { LabelStatus(Controllabel, $"Неверный формат номера {TB.Text}", Color.Red); return; }
            ShortSN = int.Parse(TB.Text.Substring(15)); //Если удачно, то преобразуем ShortSN

            if (CheckMethods()) //Проверка Сер. Ном. в таблицах и проверка диапозона
            { Grid.BackgroundColor = Color.Red;  return; }

            //Проверерка флажков
            if (CheckSN())
            { Grid.BackgroundColor = Color.Red; return; }            

            WriteToDB();
            return;

            //if (Grid.RowCount < int.Parse(ArrayList[7].ToString()))
            //    UnitCounter(control,Grid,TB);
            //else if (Grid.RowCount == int.Parse(ArrayList[7].ToString()))
            //    BoxAndPalletCounter(control,Grid,TB);            

            //LabelStatus(Controllabel, $"{TB.Text} Номер успешно добавлен", Color.Green);
        }
        void SetSerialNumber()
        {
            using (var fas = new FASEntities())
            {
                var _fas = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == ShortSN);
                _fas.FirstOrDefault().IsPacked = true;
                fas.SaveChanges();
            }
        }
        void UpdateCounter()
        {
            using (var fas = new FASEntities())
            {
                var _fas = fas.FAS_PackingCounter.Where(c => c.LineID == LineID && c.LOTID == LOTID && c.LitIndex == LitIndex) ;
                _fas.FirstOrDefault().PalletCounter = Arraylpac[0];
                _fas.FirstOrDefault().BoxCounter = Arraylpac[1];
                _fas.FirstOrDefault().UnitCounter = Arraylpac[2];
                fas.SaveChanges();
            }
        }

        void AddPackingGS()
        {
            using (var fas = new FASEntities())
            {
                var _fas = new FAS_PackingGS
                {
                    SerialNumber = ShortSN,
                    LiterID = literID,
                    LiterIndex = LitIndex,
                    PalletNum = Arraylpac[0],
                    BoxNum = Arraylpac[1],
                    LOTID = (short)LOTID,
                    UnitNum = Arraylpac[2],
                    PackingDate = DateTime.UtcNow.AddHours(2),
                    PackingByID = (short)UserID
                };
                fas.FAS_PackingGS.Add(_fas);
                fas.SaveChanges();
            }
        }
        public override void GetComponentClass()
        {           
            var Grid = (DataGridView)control.Controls.Find("DG_LOTList", true).FirstOrDefault();
            GetLot(Grid);
        }

        void WriteToDB()
        {   // pac.PalletCounter[0], pac.BoxCounter[1], pac.UnitCounter[2]
            Label LBPN = control.Controls.Find("PalletNum", true).OfType<Label>().FirstOrDefault();
            Label BoxNum = control.Controls.Find("BoxNum", true).OfType<Label>().FirstOrDefault();
            Label NextBoxNum = control.Controls.Find("NextBoxNum", true).OfType<Label>().FirstOrDefault();
            Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
            var Grid = (DataGridView)control.Controls.Find("DG_UpLog", true).FirstOrDefault();

            //если юнит каунтер = емкости коробки, то очищаем грид коробки и увеличиваем счетчик на 1
            if (Arraylpac[2].ToString() == ArrayList[7].ToString()) 
            {
                Grid.RowCount = 0;                
                //Если текущая коробка кратна кол-во коробок в паллете, то паллет + 1
/*PalletCouner*/Arraylpac[0] = Arraylpac[1] % int.Parse(ArrayList[8].ToString()) == 0 ? (short)(Arraylpac[0] + 1) : Arraylpac[0];
                LBPN.Text = Arraylpac[0].ToString();

 /*BoxCounter*/ Arraylpac[1] += 1;
                BoxNum.Text = Arraylpac[1].ToString();

                NextBoxNum.Text = (Arraylpac[1] + 1).ToString();
            }
            Arraylpac[2] = (short)(Grid.RowCount + 1);
            // "№", "Serial", "Litera","GroupBox","Pallet","ScanDate"             
            AddPackingGS();
            UpdateCounter();
            SetSerialNumber();
            ShiftCounter += 1;
            LotCounter += 1;
            ShiftCounterUpdate();
            LotCounterUpdate();
            Label_ShiftCounter.Text = ShiftCounter.ToString();
            LB_LOTCounter.Text = LotCounter.ToString();

            Grid.Rows.Add(Arraylpac[2], FullSTBSN, ArrayList[6], Arraylpac[1], Arraylpac[0], DateTime.UtcNow.AddHours(2));
            Grid.Sort(Grid.Columns[0], System.ComponentModel.ListSortDirection.Descending);
            Grid.BackgroundColor = Color.Green;
            LabelStatus(Controllabel, "Приемник успешно добавлен!", Color.Green);
            ControllabelFASEND.Text = "";
        }

        //NoInLot = Не найден номер в лоте(Работа без диапозона)
        //NoInRange = Не найден свободный номер в лоте (Работа без диапозона) 
        bool CheckRange()
        {
            Link: var R = GetlabelDate();
            
            if (R == "NoInLot")
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Не найден в лоте {LotCode}", Color.Red); return false; }

            if (R == "NoInRange")
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Не найден свободный номер", Color.Red); return false; }

            //===============================Работа с диапозоном ниже
            if (R == "NotRangeLot")            
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Нет в диапозоне лота \n от {StartRangeLot} до {EndRangeLot}", Color.Red); return false; }

            if (R == "Abort")
            { LabelStatus(ControllabelFASEND, $"Закончились серийные номера готовые к упаковке в диапозоне Лота!", Color.Red); return false; }

            if (R == "NotRange")
            { LabelStatus(ControllabelFASEND, $"{FullSTBSN} Номер не соответсвует текущему индексу литеры \n{ArrayList[6]} от {StartRange} до {EndRange} ", Color.Red); return false; }

            if (R == "New") //Открывается новый диапозон, возвращаемся к 198 строке в метод GetlabelDate()     
            {
                GetDataPac();
                goto Link;            
            }

            return true;
        }

        bool CheckTables() //Проверка таблиц на содержание данных
        {
            var list = CheckTableData(ShortSN); //Выгружаем три переменные Bool в массив [0] - FAS_Start; [1] - Upload; [2] - Packing

            if (list[0] == false) //Проверка FASStart
            {
                LabelStatus(Controllabel, $"{FullSTBSN} - Не найден в таблице Fas_Start", Color.Red); return false;
            }

            if (list[1] == false) //Проверка Upload
            {
                LabelStatus(Controllabel, $"{FullSTBSN} - Не найден в таблице Upload", Color.Red); return false;
            }

            if (list[2] == true) //Проверка Packing
            {
                var listpac = GetInfoPacing(ShortSN);
                LabelStatus(Controllabel, $" {FullSTBSN} Приемник был упакован \n Литтер {listpac[1]}, Паллет {listpac[2]}, Коробка {listpac[3]} \n номер {listpac[4]} дата упаковки {listpac[5]}, упаковщик  {listpac[6]}", Color.Red); return false;
            }

            return true;
        }

        bool CheckMethods() //Делаем две проверки, если какая то проверка не прошла, вылет из программы
        {
            var resultL = new List<bool>() { CheckRange(), CheckTables()};
            foreach (var item in resultL)
            {
                if (item == false)
                    return true;
            }
            return false;
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
            using (var Fas = new FASEntities())
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
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = (from pac in Fas.FAS_PackingGS
                            join lit in Fas.FAS_Liter on pac.LiterID equals lit.ID
                            join Use in Fas.FAS_Users on pac.PackingByID equals Use.UserID
                            where pac.SerialNumber == serial
                            select new { Liter = lit.LiterName + pac.LiterIndex, pac.PalletNum, pac.BoxNum, pac.UnitNum, pac.PackingDate, Use.UserName });

                if (list.Count() == 0)
                    return ArrayList;

                var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }

        bool CheckSN()
        {
            var list = GetSerialNum(ShortSN);
            var L = list.GetRange(0, 5).OfType<bool>().ToList();
            // S.IsUsed[0], S.IsActive[1], S.IsUploaded[2], S.IsWeighted[3], S.IsPacked[4], S.InRepair[5], F.PCBID[6]

            //used 1, active 1, uploaded 1, weighted 1, packed 0
            if (L[0] == true & L[1] == true & L[2] == true & L[3] == true & L[4] == false ) //Успешно пройдены проверки 
                return false;

            //used 1, active 1, uploaded 0, weighted 0, packed 0
            if (L[0] == true & L[1] == true & L[2] == false & L[4] == false ) //Не прошит приемник
            { LabelStatus(Controllabel, $"{ShortSN} не прошит в приемник", Color.Red); return true; }

            //used 1, active 1, uploaded 1, weighted 0, packed 0
            if (L[0] == true & L[1] == true & L[2] == true & L[3] == false & L[4] == false ) //Не прошел весовой контроль
            { LabelStatus(Controllabel, $"{ShortSN} не прошел весовой контроль", Color.Red); return true; }

            //used 1, active 1, uploaded 1, weighted 1, packed 1 
            if (L[0] == true & L[1] == true & L[2] == true & L[3] == true & L[4] == true) //Приемник уже упакован
            {
                var info = GetInfoPac(ShortSN);
                if (info.Count == 0)
                    { LabelStatus(Controllabel, "Этот номер по таблице SerialNumber упакован \n Но в таблице упаковки номера нет \n Обратитесь к технологу", Color.Red); return true;}

                 LabelStatus(Controllabel, $"номер {info[4].ToString()} уже упакован /n Литер {info[0].ToString()}, Паллет {info[1].ToString()} /n Групповая {info[2].ToString()} /n Дата упаковки {info[5].ToString()} Упакован: {info[6].ToString()}  ", Color.Red); return true; 
            }

            LabelStatus(Controllabel, "Произошла ошибка с проверкой SerialNumbers - обратитесь к Технологу", Color.Red);
            return true;
        }
        List<bool> CheckTableData(int ShortSN) //Старая проверка, которую я убрал из логики
        {
            using (var FAS = new FASEntities())
            {
                var List = new List<bool>();
                var list = (from ST in FAS.FAS_Start                            
                            from UP in FAS.FAS_Upload.Where(c=>c.SerialNumber ==ST.SerialNumber).DefaultIfEmpty()
                            from Pac in FAS.FAS_PackingGS.Where(c=>c.SerialNumber ==ST.SerialNumber).DefaultIfEmpty()
                            
                            where ST.SerialNumber == ShortSN
                            select new
                            {
                               ST = !ST.SerialNumber.Equals(null),
                               UP = !UP.SerialNumber.Equals(null),
                               Pac = !Pac.SerialNumber.Equals(null),
                               //User = !FAS.FAS_Users.Where(c=>c.UserID == Pac.PackingByID).Select(c=>c.UserName).FirstOrDefault().Equals(null),
                               //Liter = !FAS.FAS_Liter.Where(c=>c.ID == Pac.LiterID).Select(c => c.LiterName).FirstOrDefault().Equals(null),
                               //Pallet = !Pac.PalletNum.Equals(null),
                               //BoxNum = !Pac.BoxNum.Equals(null),
                               //Unitnum = !Pac.UnitNum.Equals(null),
                               //Date = !Pac.PackingDate.Equals(null)                               
                            });

                //if (list.Count() == 0)
                //    return ArrayList;

                var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                foreach (var value in report.OfType<bool>())
                    List.Add(value);
                return List;
            }
        }

        string GetLiter()
        {
            using (var Fas = new FASEntities())
            {
                var linename = ArrayList[2].ToString();
                return Fas.FAS_Liter.Where(c => c.Description == linename).Select(c => c.LiterName).FirstOrDefault();
            }
        }

        byte GetLiterID()
        {
            using (var Fas = new FASEntities())
            {
                var linename = ArrayList[2].ToString();
                return (byte)Fas.FAS_Liter.Where(c => c.Description == linename).Select(c => c.ID).FirstOrDefault();
            }
        }
       List<GridInfo> GetDatePac(short palletNum, short BoxNum,byte literID)
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                
                 var list = (from pac in Fas.FAS_PackingGS
                            join st in Fas.FAS_Start on pac.SerialNumber equals st.SerialNumber
                            join l in Fas.FAS_Liter on pac.LiterID equals l.ID
                            where pac.PalletNum == palletNum & pac.BoxNum == BoxNum & pac.LiterID == literID  & pac.LOTID == LOTID  & pac.LiterIndex == LitIndex orderby pac.UnitNum descending
                            select new GridInfo(){UnitNum =  pac.UnitNum, SerialNumber = st.FullSTBSN,Littera = l.LiterName + " " + pac.LiterIndex,BoxNum = pac.BoxNum, PalletNum = pac.PalletNum, PackingDate = pac.PackingDate }).ToList();
                return list;                
            }
        }

        List<short> GetpackingCounter()
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new List<short>();
                var list = (from pac in Fas.FAS_PackingCounter
                            where pac.LOTID == LOTID && pac.LineID == LineID && pac.LitIndex == LitIndex
                            select new { pac.PalletCounter, pac.BoxCounter, pac.UnitCounter}).ToList();
                
                    foreach (var item in list)
                    {
                        ArrayList.Add(item.PalletCounter); ArrayList.Add(item.BoxCounter); ArrayList.Add(item.UnitCounter); break;
                    }   
                
                return ArrayList;

            }
        }

        void AddPacCounter()
        {
            using (var FAS = new FASEntities())
            {
                var Pac = new FAS_PackingCounter()
                {
                    PalletCounter = 1,
                    BoxCounter = 1,
                    UnitCounter = 1,
                    LineID = (byte)LineID,
                    LOTID = (short)LOTID,
                    LitIndex = LitIndex
                    
                };
                FAS.FAS_PackingCounter.Add(Pac);
                FAS.SaveChanges();
            }
        }

        bool CheckCounter()
        {
            using (var FAS = new FASEntities())
            {
                return FAS.FAS_PackingCounter.Where(c => c.LOTID == LOTID & c.LineID == LineID & c.LitIndex == LitIndex).Select(c => c.ID == c.ID).FirstOrDefault();
            }
        }
        void GetLot(DataGridView Grid)
        {
            loadgrid.Loadgrid(Grid, @"use FAS select 
LOTCode,FULL_LOT_Code,ModelName, gs.LiterIndex, BoxCapacity,PalletCapacity,LOTID,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID) InLot,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 0 and s.IsActive = 1) Ready,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 1 ) Used
,RangeStart,RangeEnd,FixedRG,StartDate
from FAS_GS_LOTs as gs
left join FAS_Models as m on gs.ModelID = m.ModelID
where IsActive = 1
order by LOTID desc ");
            //using (FASEntities FAS = new FASEntities())
            //{
            //    var list = from Lot in FAS.FAS_GS_LOTs
            //               join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID                        
            //               where Lot.IsActive == true orderby LOTID descending                     
            //               select new
            //               {
            //                   Lot = Lot.LOTCode,
            //                   Full_Lot = Lot.FULL_LOT_Code,
            //                   Model = model.ModelName,   
            //                   Lot.LiterIndex,
            //                   Lot.BoxCapacity,
            //                   Lot.PalletCapacity,
            //                   Lot.LOTID,
            //                   InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   User = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //                   СтартДиапозон = Lot.RangeStart,
            //                   КонецДиапозон = Lot.RangeEnd,
            //                   Lot.FixedRG,
            //                   Lot.StartDate

            //               };

            //    Grid.DataSource = list.ToList();

            //}
        }

        string GetlabelDate()
        {
            SerNumber = 0;
            if (DateFas_Start) // Работа по диапозону
            {
                //NotRangeLot = Не попал в общий диапозон лота
                //NotRange = Не попал в диапозон Литера
                //Abort = Закончились серийные номера во всех диапозонах
                //False = Прошло успешно!           
                //New = Открывается новый диапозон
                //NoInLot = Не найден номер в лоте(Работа без диапозона)
                //NoInRange = Не найден свободный номер в лоте (Работа без диапозона) 

                var Result = GetSerialNumberRange();

                if (Result == "NotRangeLot") //Не попал в общий диапозон лота
                    return "NotRangeLot";

                if (Result == "Abort")
                    return "Abort"; //Закончились серийные номера во всех диапозонах  

                if (Result == "NotRange") //Не попал в диапозон Литера
                    return "NotRange";         

               if (Result == "New") //Если открывается новый диапозон
                   return "New";

                return "False"; //Все успешно
            }
            else //Работа не по диапозону 
            {
                var Result = GetSerialNumberNotRange();

                if (Result == "NoInLot")
                    return "NoInLot";
                if (Result == "NoInRange")
                    return "NoInRange";

                return "False";
            }
        }

        string GetSerialNumberNotRange()
        {            
            using (FASEntities FAS = new FASEntities())
            {
                var R = FAS.FAS_SerialNumbers.Where(C => C.SerialNumber == ShortSN & C.LOTID == LOTID & C.IsUsed == true & C.IsActive == true).Select(C => C.SerialNumber == C.SerialNumber).FirstOrDefault();
                if (!R)                
                    return "NoInLot";

                var Re = FAS.FAS_SerialNumbers.Where(C => C.SerialNumber == ShortSN & C.LOTID == LOTID & C.FixedID == null & C.IsUsed == true & C.IsActive == true).Select(C => C.SerialNumber == C.SerialNumber).FirstOrDefault();
                if (!Re)
                    return "NoInRange";

                return "False";
                

            }
        }

        string GetSerialNumberRange()        
        {
            bool result = false;
            var GridInfo = (DataGridView)control.Controls.Find("GridInfo", true).FirstOrDefault();
            for (int i = StartRangeLot; i <= EndRangeLot; i++) //Проверка общего диапозона 
                if (i == ShortSN)
                { result = true; break; }

            if (!result) //Не попал в общий диапозон
                return "NotRangeLot";
            
            using (FASEntities FAS = new FASEntities())
            {
                     var R = FAS.FAS_SerialNumbers  //Проверяем сколько осталось неупакованных номеров в диапозоне Лота, которые готовы к упаковке
                    .Where(c => c.IsUsed == true && c.IsActive == true && c.IsUploaded == true && c.IsWeighted == true && c.IsPacked == false
                    && c.SerialNumber >= StartRangeLot && c.SerialNumber <= EndRangeLot).Count();

                if (R == 0) //Закончились номера в диапозоне лота для упаковки                
                    return "Abort";                
            };

            result = false;
            for (int i = StartRange; i <= EndRange; i++) //Проверка диапозона Литера
                if (i == ShortSN)
                { result = true; break; }

            using (FASEntities FAS = new FASEntities())
            {
                var R = FAS.FAS_SerialNumbers  //Проверяем сколько осталось неупакованных номеров в диапозоне литера, которые готовы к упаковке
                    .Where(c => c.IsUsed == true && c.IsActive == true && c.IsUploaded == true && c.IsWeighted == true && c.IsPacked == false
                    && c.SerialNumber >= StartRange && c.SerialNumber <= EndRange).Count();

                if (R == 0) //Закончились номера в диапозоне Литера
                {
                    FixedRange FR = new FixedRange(LOTID, this, "В базе для этого диапозона закончились серийные номера \n выберите следующий или вызовите технолога", LitIndex);
                    var Result = FR.ShowDialog();

                    if (Result == DialogResult.OK)
                    {
                        ArrayList[6] = GridInfo[1, 6].Value.ToString().First() + " " + LitIndex;
                        GridInfo[1, 6].Value = ArrayList[6];
                        return "New";
                    }
                }
            };

            if (!result) //Не попал в Литер диапозон
                return "NotRange";

            return "False";

            #region
            //using (FASEntities FAS = new FASEntities())
            //{                
            //    for (int i = 0; i < GridRange.RowCount; i++)
            //    {
            //        var st = int.Parse(GridRange[0, i].Value.ToString()); //Берем начало диапозона
            //        var end = int.Parse(GridRange[1, i].Value.ToString()); //Берем конец диапозона

            //        //Запрос по дипозону, лоту used 0, active 1
            //        var ser = FAS.FAS_SerialNumbers.Where
            //        (c => c.LOTID == (short)LOTID && c.IsUsed == true && c.IsActive == true && c.IsUploaded == true && c.IsWeighted == true
            //        && c.SerialNumber >= st && c.SerialNumber <= end && c.SerialNumber == ShortSN)
            //        .Select(c => c.SerialNumber).FirstOrDefault();

            //        if (ser == ShortSN) //Если номер в диапозоне найден, сохраняем данные, успешно выходим из метода          
            //            return "False";                   
            //    }
            //}

            //FixedRange FR = new FixedRange(LOTID, this, "В базе для этого диапозона закончились серийные номера \n выберите следующий или вызовите технолога", LitIndex);
            //var Result = FR.ShowDialog();

            //if (Result == DialogResult.Abort)
            //    return "Abort";

            //if (Result == DialogResult.OK)
            //{
            //    GridInfo[0,6].Value = GridInfo[0,6].Value.ToString().First() + " " + LitIndex; return "New";
            //}

            //return "True";
            ////BaseC.DateFas_Start = true;
            #endregion

        }
    }
}
