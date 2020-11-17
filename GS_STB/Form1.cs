using GS_STB.Class_Modules;
using GS_STB.Forms_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
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
            List<BaseClass> ListClasses = new List<BaseClass>() { new FAS_END(), new UploadStation(), new FASStart(), new Desassembly_STB(), new FAS_Weight_control(),  };
             
            BT_OK.Click += (a, e) => // Событие нажатие кнопки
            { 
                if (CheckIndex()) OpenModule(ListClasses[listBox1.SelectedIndex]);  
            };           

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
            using (FASEntities1 Fas = new FASEntities1())
            {   short[] sh = new short[]{4,3,5,20,2};
                ListApp.AddRange(Fas.FAS_Applications.Where(c => sh.Contains(c.App_ID)).Select(c => c.App_Name).AsEnumerable());
            }
        }

        bool CheckIndex() //Проверка номера индека
        {
            if (listBox1.SelectedIndex == -1)
            { MessageBox.Show("Выберите модуль");  return false; }
            return true;
        }

        void OpenModule(BaseClass BC) //Открытие определенного класса
        {
            SettingsForm settingsForm = new SettingsForm(BC);
            settingsForm.ShowDialog();
        }

    }
}
