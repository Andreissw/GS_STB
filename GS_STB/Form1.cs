using GS_STB.Class_Modules;
using GS_STB.Forms_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetListApp();            
            GetModule();//Вывод списка в ЛистБокс            
            List<BaseClass> ListClasses = new List<BaseClass>() { new FAS_END(), new UploadStation(), new FASStart(), new Desassembly_STB(), new FAS_Weight_control(), };

            BT_OK.Click += (a, e) => // Событие нажатие кнопки
            {
                IndexOpen(ListClasses);
            };

            listBox1.DoubleClick += (a, e) => // Событие нажатие кнопки
            {
                IndexOpen(ListClasses);
            };         

        }

        void GetLot(DataGridView Grid)
        {
            using (FASEntities FAS = new FASEntities())
            {
                var list = from Lot in FAS.FAS_GS_LOTs
                           join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
                           where Lot.IsActive == true
                           orderby Lot.LOTID descending
                           select new
                           {
                               Lot = Lot.LOTCode,
                               Full_Lot = Lot.FULL_LOT_Code,
                               Model = model.ModelName,
                               InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               User = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
                               //Lot.LOTID,             
                               Стартдиапазон = Lot.RangeStart,
                               Конецдиапазон = Lot.RangeEnd,
                               Lot.FixedRG,
                               Lot.StartDate
                           };

                Grid.DataSource = list.ToList();
            }
        }

        private void IndexOpen(List<BaseClass> ListClasses)
        {
            if (CheckIndex())
            {
                if (listBox1.SelectedIndex == 5)
                { OpenModule(); return; }

                if (listBox1.SelectedIndex == 6)
                { OpenModuleAbortSN(); return; }

                OpenModule(ListClasses[listBox1.SelectedIndex]);//Открытие модулей GS_STB, которые работают на линии
            }
        }

        protected List<string> ListApp = new List<string>() {  };
        // Список модулей
        void GetModule() //Вывод списка в ЛистБокс
        {
            foreach (var item in ListApp)           
                listBox1.Items.Add(item);           
        }

        void GetListApp()
        {
            using (FASEntities Fas = new FASEntities()) //131,37
            {   short[] sh = new short[]{4,3,5,20,2,24, 37};
                ListApp.AddRange(Fas.FAS_Applications.Where(c => sh.Contains(c.App_ID)).Select(c => c.App_Name).AsEnumerable());
            }
        }

        bool CheckIndex() //Проверка номера индека
        {
            if (listBox1.SelectedIndex == -1)
            { MessageBox.Show("Выберите модуль");  return false; }
            //if (listBox1.SelectedIndex == 1 || listBox1.SelectedIndex == 2)
            //    return true;
            return true;

        }

        void OpenModule(BaseClass BC) //Открытие определенного класса
        {            
            SettingsForm settingsForm = new SettingsForm(BC);           
            var r = settingsForm.ShowDialog();           
        }

        void OpenModule() //Открытие определенного класса
        {
            FAS_LOT_Managment Managment = new FAS_LOT_Managment();
            Managment.ShowDialog();
        }

        void OpenModuleAbortSN() //Открытие определенного класса
        {
            AbortSNcs AbortForm = new AbortSNcs();
            AbortForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
