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
    class Desassembly_STB : BaseClass
    {

        public Desassembly_STB()
        {
            ListHeader = new List<string>() { "№", "CH приемника", "Код ошибки", "ScanDate" };
            IDApp = 5;
        }
        public override void LoadWorkForm()
        {
            Label Label_ShiftCounter = control.Controls.Find("Label_ShiftCounter", true).OfType<Label>().FirstOrDefault();
            Label LB_LOTCounter = control.Controls.Find("LB_LOTCounter", true).OfType<Label>().FirstOrDefault();
            var FUG = control.Controls.Find("Desassembly_STBGroup", true).FirstOrDefault();
            var CB = (ComboBox)control.Controls.Find("CB_ErrorCode", true).FirstOrDefault();
            var CBDEsc = (ComboBox)control.Controls.Find("CB_INFO", true).FirstOrDefault();
            FUG.Visible = true;
            FUG.Location = new Point(194, 12);
            FUG.Size = new Size(538,199);
            GetErrorCode(CB);
            GetErrorCodeDescription(CBDEsc);
            ShiftCounterStart();
            Label_ShiftCounter.Text = ShiftCounter.ToString();
            LB_LOTCounter.Text = LotCounter.ToString();
        }
        public override void KeyDownMethod()
        {        
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();
            var BT = control.Controls.Find("BT_Disassebly", true).FirstOrDefault();
            var CBer = control.Controls.Find("CB_ErrorCode", true).FirstOrDefault();
            if (TB.TextLength == 23)
            {
                int ShortSN;
                if (!int.TryParse(TB.Text.Substring(15), out ShortSN))
                { LabelStatus(Controllabel, $"{TB.Text} \n Неверный формат номера!", Color.Red); TB.Clear(); TB.Select(); return; }

                if (CheckPacking(ShortSN)) //Проверяем таблицу упаковки 
                { // pac.SerialNumber,   liter =,  pac.PalletNum,  pac.BoxNum, pac.UnitNum, pac.PackingDate,  us.UserName
                    var listpac = GetInfoPacing(ShortSN);
                    LabelStatus(Controllabel, $" {TB.Text} Приемник был упакован \n Литтер {listpac[1]}, Паллет {listpac[2]}, Коробка {listpac[3]} \n номер {listpac[4]} дата упаковки {listpac[5]}, упаковщик  {listpac[6]}", Color.Red); TB.Clear(); TB.Select(); return;
                }

                //var ShortSN = int.Parse(TB.Text.Substring(15));
                var ArraysSN = GetSerialNum(ShortSN);
                if (ArraysSN.Count == 0)
                {
                    BT.Enabled = false;CBer.Enabled = false;  LabelStatus(Controllabel, $"{TB.Text} \n Не найден номер на FAS_Start", Color.Red); TB.Clear();
                    TB.Select(); return;
                }
                PCBID = int.Parse(ArraysSN[7].ToString());
                if (ArrayCheck(ArraysSN, Controllabel, TB.Text)) //Если возвращает True, то проверку не проходит
                {     BT.Enabled = false;   CBer.Enabled = false; TB.Text = ""; TB.Select();  return;   }

                TB.Enabled = false;
                BT.Enabled = true;
                CBer.Enabled = true;
                CBer.Select();
            }
            else
            {
                BT.Enabled = false; 
                CBer.Enabled = false;
                TB.Clear();
                LabelStatus(Controllabel, $"{TB.Text} \n Неверный формат номера!", Color.Red); 
                TB.Select();
            }
        }

        bool ArrayCheck(ArrayList list,Label lb,string sn)
        {
            // S.LOTID[0], S.IsUsed[1], S.IsActive[2], S.IsUploaded[3], S.IsWeighted[4], S.IsPacked[5], S.InRepair[6], F.PCBID[7]

            if (list[0].ToString() != LOTID.ToString()) //Если не совпадает лот, ошибка
            { LabelStatus(lb, $"Серийный номер {sn} \n  из другого ЛОТа! ", Color.Red); return true; }

            //Если совпадает лот, номер used 1, active 1, packed 0, repair 0, то проверка пройдена успешно 
            if (list[0].ToString() == LOTID.ToString() && list[1].ToString() == "True" & list[2].ToString() == "True" && list[5].ToString() == "False" & list[6].ToString() == "False")
                return false;

            //Если  совпадает лот, active 0,packed 0, repair 0,то ошибка
            if (list[0].ToString() == LOTID.ToString()  & list[2].ToString() == "false" && list[5].ToString() == "false" & list[6].ToString() == "false")
            { LabelStatus(lb, $"Серийный номер {sn} \n  удалён из базы! ", Color.Red); return true; }


            if (list[0].ToString() == LOTID.ToString() & list[1].ToString() == "true" & list[2].ToString() == "false" && list[5].ToString() == "false" & list[6].ToString() == "true")
            {
                var Array = GetInfoDis(int.Parse(sn.Substring(15)));
                LabelStatus(lb, $"Этот номер {sn} \n уже был откреплен с кодом ошибки {Array[1].ToString()}\n на линии {Array[2].ToString()} \n Время: {Array[3].ToString()} \n Пользователем {Array[4].ToString()}", Color.Red);
                return true;
            }

            if (list[0].ToString() == LOTID.ToString() & list[1].ToString() == "true" & list[2].ToString() == "true" && list[5].ToString() == "true")
            {
                var Array = GetInfoPac(int.Parse(sn.Substring(16)));
                LabelStatus(lb, $"Серийный номер {sn} уже упакован \n  Литер {Array[0].ToString()} Паллет {Array[1].ToString()} \n Группоавая {Array[2].ToString()} Приемник № {Array[3].ToString()} Дата упаковки  {Array[4].ToString()} \n Упакован {Array[5].ToString()}  ", Color.Red);
                return true;             
            }
            else
            {
                 LabelStatus(lb, $"{sn} - Проверка номера не пройдена, сообщите об ошибке технологу", Color.Red); return true; 
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
                           select new {Liter = lit.LiterName + pac.LiterIndex, pac.PalletNum,pac.BoxNum, pac.UnitNum, pac.PackingDate,Use.UserName }).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }

        ArrayList GetInfoDis(int serial)
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = (from d in Fas.FAS_Disassembly
                           join er in Fas.FAS_ErrorCode on d.ErrorCodeID equals er.ErrorCodeID
                           join l in Fas.FAS_Lines on d.DisAssemblyLineID equals l.LineID
                           join u in Fas.FAS_Users on d.DisassemblyByID equals u.UserID
                           where d.SerialNumber == serial
                           orderby d.ID descending
                           select new { d.FullSTBSN, Errorococe = er.Category + er.Code, l.LineName, d.DisassemblyDate, u.UserName }).First();

                var report = list.GetType().GetProperties().Select(c => c.GetValue(list));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }
        ArrayList GetSerialNum(int shortSN)
        {
            using (var Fas = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = (from S in Fas.FAS_SerialNumbers
                            join F in Fas.FAS_Start on S.SerialNumber equals F.SerialNumber
                            where S.SerialNumber == shortSN
                            select new { S.LOTID, S.IsUsed, S.IsActive, S.IsUploaded, S.IsWeighted, S.IsPacked, S.InRepair, F.PCBID });

                if (list.Count() == 0)
                    return ArrayList;

                var report = list.First().GetType().GetProperties().Select(c => c.GetValue(list.First()));
                foreach (var value in report)
                    ArrayList.Add(value);
                return ArrayList;

            }
        }


        public override void GetComponentClass()
        {
            
            var Grid = (DataGridView)control.Controls.Find("DG_LOTList", true).FirstOrDefault();
           
            GetLot(Grid);
            
        }

      

       public int GetErrorCodeID(string err )
        {
            using (var FAS = new FASEntities())
            {
                return  (from er in FAS.FAS_ErrorCode
                            where er.ErrGroup == 1 && er.Category + er.Code == err
                            select er.ErrorCodeID).FirstOrDefault();
               
            }
        }

        void GetErrorCode(ComboBox CB)
        {
            using (var FAS = new FASEntities())
            {
                var list = from er in FAS.FAS_ErrorCode
                           where er.ErrGroup == 1
                           select new { ErrorCode = er.Category + er.Code };
                CB.DataSource = list.ToList();
                CB.DisplayMember = "ErrorCode";                
                CB.Text = "";
            }
        }

        void GetErrorCodeDescription(ComboBox CB)
        {
            using (var FAS = new FASEntities())
            {
                var list = from er in FAS.FAS_ErrorCode
                           where er.ErrGroup == 1
                           select new { ErrorCode = er.Category + er.Code + "-" + er.Description};
                CB.DataSource = list.ToList();
                CB.DisplayMember = "ErrorCode";
                CB.Text = "";
            }
        }

        void GetLot(DataGridView Grid)
        {
            using (FASEntities FAS = new FASEntities())
            {
                var list = from Lot in FAS.FAS_GS_LOTs
                           join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
                           join Label in FAS.FAS_LabelScenario on Lot.LabelScenarioID equals Label.ID
                           where Lot.IsActive == true
                           orderby Lot.LOTID descending
                           select new
                           {
                               Lot = Lot.LOTCode,
                               Full_Lot = Lot.FULL_LOT_Code,
                               Model = model.ModelName,
                               InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true  &  s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               User = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Lot.LOTID                             
                           };

                Grid.DataSource = list.ToList();

            }
        }
    }
}
