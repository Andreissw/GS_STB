using GS_STB.Class_Modules;
using GS_STB.Forms_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            GetModule();//Вывод списка в ЛистБокс
            List<BaseClass> ListClasses = new List<BaseClass>() {  new FASStart(), new UploadStation(), new Desassembly_STB(), new FAS_Weight_control(), new FAS_END() };
             
            BT_OK.Click += (a, e) => // Событие нажатие кнопки
            { 
                if (CheckIndex()) { OpenModule(ListClasses[listBox1.SelectedIndex]);  };
            };

        }
        readonly List<string> ListApp = new List<string>() {  "FASStart", "UploadStation", "Desassembly_STB", "FAS_Weight_control", "FAS_END" };
        // Список модулей
        void GetModule() //Вывод списка в ЛистБокс
        {
            foreach (var item in ListApp)           
                listBox1.Items.Add(item);           
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
