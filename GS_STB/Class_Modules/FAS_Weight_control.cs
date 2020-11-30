using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class FAS_Weight_control : BaseClass

    {
        SerialPort SerialPort { get; set; }

        int ShortSN;
        string _SN;
        public FAS_Weight_control()
        {
            ListHeader = new List<string>() { "№", "Serial", "ScanDate" };
            IDApp = 20;
        }

        public override void LoadWorkForm()
        {
            ShiftCounterStart();
        }
        public override void KeyDownMethod()
        {
            TextBox TB = control.Controls.Find("SerialTextBox", true).OfType<TextBox>().FirstOrDefault();
            Label Controllabel = control.Controls.Find("Controllabel", true).OfType<Label>().FirstOrDefault();

            if (TB.TextLength == 23)
            {

                if (string.IsNullOrEmpty(COMPORT))
                    GetPortName();
                if (string.IsNullOrEmpty(COMPORT))
                { LabelStatus(Controllabel, $"COMPORT не подключен, проверьте COMPORT", Color.Red); return; }

                _SN = TB.Text;

                int k = 0;
                if (!int.TryParse(_SN.Substring(15), out k))
                { LabelStatus(Controllabel, $"Неверный формат номера {_SN}", Color.Red); return; }
                ShortSN = int.Parse(_SN.Substring(15)); //Если удачно, то преобразуем ShortSN

                SerialPort = new SerialPort(); //Инициализация ComPorta
                SerialPort.BaudRate = 9600;
                SerialPort.DataBits = 8;
                SerialPort.PortName = COMPORT;
                SerialPort.Parity = Parity.None;
                SerialPort.StopBits = StopBits.One;
                SerialPort.Handshake = Handshake.None;
                SerialPort.Encoding = System.Text.Encoding.Default;

                //F.PCBID, S.IsUsed, S.IsActive, S.IsUploaded, S.IsWeighted, S.IsPacked, S.InRepair 
                var SerialInfoList = GetSerialNum(ShortSN);
                if (CheckPoints(SerialInfoList, Controllabel))
                    return;




            }
            else
            {
                { LabelStatus(Controllabel, $"Не верный формат номера {TB.Text}", Color.Red); return; }
            }


        }


        public override void GetComponentClass()
        {
            control.Controls.Find("FAS_Weight", true).FirstOrDefault().Visible = true;
            control.Controls.Find("FAS_Weight", true).FirstOrDefault().Location = new Point(LocX, LocY);
            control.Controls.Find("FAS_Weight", true).FirstOrDefault().Size = new Size(609, 177);
        }

        //F.PCBID[0], S.IsUsed[1], S.IsActive[2], S.IsUploaded[3], S.IsWeighted[4], S.IsPacked[5], S.InRepair[6]
        bool CheckPoints(ArrayList L, Label Controllabel)
        {
            var O = L.Cast<string>().ToList();
            if (O[1] == "True" & O[2] == "True" & O[3] == "True" & O[4] == "False" & O[5] == "False" & O[6] == "False")
                return false;

            else if (O[1] == "True" & O[2] == "False" & O[5] == "False" & O[6] == "True")
            { LabelStatus(Controllabel, $"{_SN} находится в ремонте!", Color.Red); return true; }

            else if (O[1] == "True" & O[2] == "True" & O[3] == "False" & O[5] == "False" & O[6] == "False")
            { LabelStatus(Controllabel, $"{_SN} не прошит в приемник", Color.Red); return true; }

            else if (O[1] == "True" & O[2] == "True" & O[3] == "True" & O[4] == "True" & O[5] == "False" & O[6] == "False")
            { LabelStatus(Controllabel, $"{_SN} Уже прошел весовой контроль", Color.Red); return true; }

            else if (O[1] == "True" & O[2] == "True" & O[3] == "True" & O[4] == "True" & O[5] == "True" & O[6] == "False")
            {
                var P = GetPac();
                // P.UnitNum, S.FullSTBSN, Liter = L.LiterName + P.LiterIndex, P.PalletNum, P.BoxNum, P.PackingDate
                LabelStatus(Controllabel, $"Номер {P[0]} Уже упакован, Литер {P[1]}, Паллет {P[2]}, Групповая {P[3]}\n Дата упаковки {P[4]}", Color.Red); return true; 
            }
            else            
                { LabelStatus(Controllabel, $"{_SN} Не найден в базе", Color.Red); return true; }
           




        }

        ArrayList GetPac() //отсутствует баркод 
        {
            using (var FAS = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var list = from P in FAS.FAS_PackingGS
                           join S in FAS.FAS_Start on P.SerialNumber equals S.SerialNumber
                           join L in FAS.FAS_Liter on P.LiterID equals L.ID
                           where P.SerialNumber == ShortSN
                           select new { P.UnitNum, S.FullSTBSN, Liter = L.LiterName + P.LiterIndex, P.PalletNum, P.BoxNum, P.PackingDate };

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
    }
}
