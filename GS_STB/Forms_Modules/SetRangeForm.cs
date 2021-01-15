using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace GS_STB.Forms_Modules
{
    public partial class SetRangeForm : Form
    {
        int lotid;
        int LotCode;
        string NotUsedLB = $"Лот еще не был использован, \n Укажите кол-во серийных номеров в диапозоне отгрузки и начальную дату";        
        int count;
        string UsedLB = $"В лоте уже используется номера, \nукажите кол-во серийный номеров для начальной отгрузки Литера 0 и дату";
        string UsedLB2 = $"В лоте уже используется номера, \n Укажите кол-во серийных номеров в диапозоне отгрузки и начальную дату";
        bool UsedLot = false;
        int SNCount;
        string lot;
        public SetRangeForm(int LotID, int LotCode)
        {           
            InitializeComponent();
            this.lotid = LotID;
            this.LotCode = LotCode;
        }

        private void SetRangeForm_Load(object sender, EventArgs e)
        {
            GetSNCount(); //Инфо о количество номеров в лоте
            CheckLot(); //Проверяет выпускался ли лот, ставит флажок на булвой перемнной UsedLot (true/false)
            label1.Text = $"Лот - {GetLotName()}. Размер лота - {SNCount}.";
            DateRange.Value = DateTime.Now;
            label1.Text = "Укажите кол-во серийных номеров в диапозоне отгрузки и начальную дату";
            if (!UsedLot) //Лот не использованный 
            {
                //label1.Text += "\n" + NotUsedLB;
                GRNotUsed.Visible = true; GRNotUsed.Location = new Point(17, 91);
            }
            else //Лот использованный 
            {
                //if (!Checkrange())
                    //    label1.Text += "\n" + UsedLB;
                    //else
                    //    label1.Text += "\n" + UsedLB2;

                    GRNotUsed.Visible = true; GRNotUsed.Location = new Point(17, 91); //Инициализация компонентов 
            }
        }      

        private void button1_Click(object sender, EventArgs e)
        {
            //Присутствует 2  условия, если лот вообще не использован и если лот уже выпускается
            if (!UsedLot) //Лот не использованный
            {
                int RangeStart = 0; // Создаю пустую переменную
                var EndRange = GetRangeEnd(); // 
                short literindex = 1; 
                DateTime d = DateRange.Value;

                //Есть 2 условия, если диапозн создавался ранее и если не создавался

                if (Checkrange()) //Создавался ранее
                //Берем последние данные по диапозону, прибавляем + 1
                { RangeStart = getrange() + 1; literindex = (short)(GetLiterIndex() + 1);  d = GetDatetime().AddDays(1); } 
                else // Не создавался ранее
                    RangeStart = GetRangeStart(); //Берем самый начальный номер в диапозоне

                AddRangeMethod(RangeStart, EndRange, d,literindex); //Метод Добавление диапозона 

               
            }
            else //Если лот уже выпускается
            {
                var RangeEnd = GetRangeEnd();
                if (!Checkrange())//Если ранее диапозоны не устанавливались
                {                   
                    var Start = GetRangeEndUsed() + 1;
                    AddRangeMethod(Start, RangeEnd, DateRange.Value, 1);
                }
                else
                {
                    var RangeStart = GetRangeTop(); //Определяем с какого номера создать диапозон
                    var literindex = (short)(GetLiterIndex() + 1);
                    var d = GetDatetime().AddDays(1);
                    AddRangeMethod(RangeStart, RangeEnd, d, literindex);
                } //Если диапозоны уже существуют

                #region СтарыйКод
                //var RangeEnd = GetRangeEnd();

                //if (!Checkrange()) //Если ранее диапозоны не устанавливались
                //{
                //    var RangeStart = GetRangeStart();
                //    DateTime d = DateRange.Value;
                //    var count = GetCountSNUsed();
                //    if (SNNum.Value > count)
                //    { MessageBox.Show($"Кол-во номеров в диапозоне которое вы выбрали '{SNNum.Value}' меньше текущего количества использованных номеров в лоту {count}"); return; }

                //    AddRangeMethod(RangeStart, RangeEnd, d, 0);
                //}
                //else
                //{
                //    var RangeStart = getrange() + 1;
                //    var literindex = (short)(GetLiterIndex() + 1);
                //    var d = GetDatetime().AddDays(1);
                //    AddRangeMethod(RangeStart, RangeEnd, d, literindex);
                //}
                #endregion
            }

            if (GridExcelReport.RowCount > 1) //Выгрузка Excel 
            {
                OpenExcel(); 
                MessageBox.Show("Диапозон добавлен успешно!");
                DialogResult = DialogResult.OK;
                this.Close();
            }


        }
            private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void OpenExcel()
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application(); //Создаем объект Excel
            ExcelApp.Application.Workbooks.Add(Type.Missing); //Добавляет в Excel Лист
            Range cells = ExcelApp.Columns["D:D"];
            cells.NumberFormat = "@";

            for (int i = 0; i < GridExcelReport.ColumnCount; i++)        //Добавляет заголовки     
                ExcelApp.Cells[1, i + 1] = GridExcelReport.Columns[i].HeaderText;

            for (int i = 0; i < GridExcelReport.RowCount; i++) //Добавляем таблицу           
                for (int k = 0; k < GridExcelReport.ColumnCount; k++)               
                    ExcelApp.Cells[i + 1, k + 1] = GridExcelReport[k, i].Value;      

            ExcelApp.Visible = true;
            GC.Collect();
            ExcelApp = null;
        }
        void GetSNCount()
        {
            using (var fas = new FASEntities())
            {
                SNCount = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Count();
            }
        }

        string GetLotName()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid).Select(c => c.FULL_LOT_Code).FirstOrDefault();
            }
        }

        void CheckLot()
        {
            using (var fas = new FASEntities())
            {
                var count = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.IsUsed == true).Count();
                if (count != 0)
                    UsedLot = true;
            }
        }

        void AddRangeMethod(int RangeStart,int RangeEnd, DateTime d, short literindex)
        {
            var Sum = (int)((RangeStart - 1) + SNNum.Value); // Берем стартовое значение и прибавляем к нему значение которое ввели в поле
            //Если результат больше конечного диапозона, ошибка
            if (Sum > RangeEnd)
            {
                MessageBox.Show($"Кол-во номеров в диапозоне которое вы выбрали '{SNNum.Value}'превышает диапозон в лоте, в диапозоне осталось '{RangeEnd - RangeStart + 1}'"); return;
            }

            //Проверка, есть ли диапозон с указанной датой
            if (CheckDateRange())
            {
                MessageBox.Show($"Уже существует лот '{lot}', который работал в диапозоне с такой датой '{DateRange.Value.ToString("dd.MM.yyyy")}'"); return;
            }

            //Существует 2 условия, Если значение которое мы указали равно или меньше 15000 шт. И если больше 15000 шт.

            if (SNNum.Value <= 15000) //Равно или меньше 15000 шт.
            {
                RangeActivate(RangeStart, RangeEnd); //Активируем диапозон в таблице GsLots   
                addFixedRange(literindex, RangeStart, (int)(RangeStart + SNNum.Value) - 1, d); //Добавляет диапозон в таблицу FAS_Fixed_Range
            }
            else //Если больше 15000 шт.
            {
                RangeActivate(RangeStart, RangeEnd); //Активируем диапозон в таблице GsLots  

                int C = (int)(SNNum.Value / 15000);
                for (int i = 0; i < C; i++) //Делим на диапозоны по датам
                {
                    addFixedRange(literindex, RangeStart, RangeStart + (15000 - 1), d);
                    RangeStart = getrange() + 1; d = d.AddDays(1);
                }

                //добавляем отстаток
                var r = (int)(SNNum.Value - (15000 * C));
                if (r > 0)
                {
                    addFixedRange(literindex, RangeStart, RangeStart + (r - 1), d);
                }
            }
        }

        DateTime GetDatetime()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.LabDate).FirstOrDefault();
            }
        }

        int GetCountSNUsed()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.IsUsed == true).Count();
            }
        }

        short GetLiterIndex()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.LitIndex).FirstOrDefault();
            }
        }

        int GetRangeEnd()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Max();
            }
        }

        int GetRangeStart()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Min();
            }
        }
       
        int GetRangeEndUsed()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid && c.IsUsed == true && c.FixedID == null).Select(c => c.SerialNumber).Max();
            }
        }

        int GetRangeTop()
        {
            using (var fas = new FASEntities())
            {
                // Находим в лоте в таблице Fas_Start самый большой серийный номер 
                var sers = (from st in fas.FAS_Start
                           join ser in fas.FAS_SerialNumbers on st.SerialNumber equals ser.SerialNumber
                           where ser.LOTID == lotid
                           orderby st.SerialNumber descending
                           select st.SerialNumber).FirstOrDefault();

                //Определяем есть ли fixedRange
                var R = fas.FAS_SerialNumbers.Where(c => c.SerialNumber == sers).Select(c => c.FixedID).FirstOrDefault();

                var Re = (from Fi in fas.FAS_Fixed_RG
                          where Fi.LotID == lotid
                          select Fi.RGEnd).Max();

                if (R != null)//Если номер в диапозоне                                  
                    return Re + 1;

                if (sers <= Re) //Если перед нашим максимально найденным номером, есть диапозон
                    return Re + 1;
                else //Если перед номером нет диапозонов
                    return sers + 1;

            }
        }



        bool Checkrange()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).Select(c => c.LotID == c.LotID).FirstOrDefault();
            }
        }
        int getrange()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.RGEnd).FirstOrDefault();
            }
        }

        //int getranged() //Фнукция используется если лот уже выпускается без дипозона
        //{
        //    using (var fas = new FASEntities())
        //    {
        //        return fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.RGEnd).FirstOrDefault();
        //    }
        //}

        void addFixedRange(short liter, int st, int end,DateTime d)
        {
            var Date = DateTime.Parse(d.ToString("dd.MM.yyyy"));
            using (var fas = new FASEntities())
            {
                var F = new FAS_Fixed_RG() //Добавляем диапозон в FixedRange
                {
                    LotID = lotid,
                    LitIndex = liter,
                    RGStart = st,
                    RGEnd = end,
                    LabDate = Date
                };
                
                fas.FAS_Fixed_RG.Add(F);
                fas.SaveChanges(); //Сохраняем

                //Берем ID записи, которую мы сохранили
                var FixedID = fas.FAS_Fixed_RG.Where(c => c.LotID == lotid).OrderByDescending(c => c.id).Select(c => c.id).FirstOrDefault();

                //Присваиваем ID  в FAS_serialNumbers всем номерам которые попадают в диапозон
                var Fas_ser = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid & c.SerialNumber >= st & c.SerialNumber <= end).AsEnumerable()
                    .Select(c => { c.FixedID = FixedID; return c; });

                foreach (FAS_SerialNumbers item in Fas_ser)
                    fas.Entry(item).State = (System.Data.Entity.EntityState)EntityState.Modified;
                fas.SaveChanges();



                ReportExcel(st,end,Date);
            }
        }

        void ReportExcel(int st, int end, DateTime d)
        {
            for (int i = st; i <= end; i++)
            {
                var date = d.ToString("ddMMyyyy");
                var FullSTBSN = GenerateFullSTBSN(i,date);
                GridExcelReport.Rows.Add(GridExcelReport.RowCount,i,d.ToString("dd.MM.yyyy"), FullSTBSN);
            }
        }

        void RangeActivate(int st, int end )
        {
            using (var fas = new FASEntities())
            {
                var Result = fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid).Select(c => c.FixedRG).FirstOrDefault();
                if (Result != null)
                    return;

                var gs = fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid);
                gs.FirstOrDefault().RangeStart = st;
                gs.FirstOrDefault().RangeEnd = end;
                gs.FirstOrDefault().FixedRG = true;
                gs.FirstOrDefault().StartDate = DateTime.Parse(DateRange.Value.ToString("dd.MM.yyyy"));
                fas.SaveChanges();
            }
        }
               

        bool CheckDateRange()
        {
            using (var fas = new FASEntities())
            {
                var D = DateTime.Parse(DateRange.Value.ToString("dd.MM.yyyy"));
                //lot = fas.FAS_Start.Where(c =>c.ManufDate == D).Select(c => fas.FAS_GS_LOTs.Where(b=>b.LOTID == c.LotID ).Select(b =>b.FULL_LOT_Code).FirstOrDefault()).FirstOrDefault();
                return fas.FAS_Start.Where(c => c.ManufDate == D).Select(c => c.ManufDate == c.ManufDate).FirstOrDefault();
            }
        }

        string GenerateFullSTBSN(int serialnumber,string ProdDate) //Генерация Полного Серийного номера
        {
        
                string FullSTBSN;
                int _serNumber = serialnumber;               
                var FullSTBSN_Arr = "0" + ProdDate + "01" + LotCode + _serNumber;
                var D = StringToIntArray(FullSTBSN_Arr);

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
            

            List<int> StringToIntArray(string raw)
            {
                var d = new List<int>();

                for (int i = 0; i < raw.Length; i++)
                    d.Add(int.Parse(raw.Substring(i, 1)));

                return d;
            }
        }
    }
}
