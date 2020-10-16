using GS_STB.Class_Modules;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Forms_Modules
{
    public partial class SettingsForm : Form
    {
        private BaseClass BaseC;
        readonly List<string> ListInfoGrid = new List<string>() {"Название приложения","Название станции"};
        public SettingsForm(BaseClass BC) //Конструктор, при инициализации Формы, инииализируются компоненты и методы
        {
            InitializeComponent();
            this.BaseC = BC;
            SetInfoArray(); //Добавление в ArrayList информацию о модуле и ПК
            SetInfoGrid();//Добавление в грид информацию с ArrayList    

            if (!GetStationID())   //Если Имя компьютера отсутствует в базе, то добавляем в базу      
                BaseC.StationID = RegisterStation(); //Регистрация компютера и просвение ID

            if (string.IsNullOrEmpty(GetLineName()))
                { GetSettingLine(); return;  }

            BaseC.GetComponentClass(this);
            BaseC.GetLotList(DG_LOTList);
            BT_OpenWorkForm.Click += (a, e) => { OpenForm(BaseC); };

        }

        void OpenForm(BaseClass BC)
        {
            WorkForm workForm = new WorkForm(BC);
            workForm.ShowDialog();     
        }

        void GetSettingLine()
        {
            LineSettings.Visible = true;
            LineSettings.Location = new Point(12, 18);
            LineSettings.Size = new Size(429, 284);
        }

        string GetLineName()
        {
            return "Fas Line";
        }

        int RegisterStation()//Регистрация компютера 
        {
            return 1;
        }

        bool GetStationID()//Берем ID компьютера с базы
        {
            return true;
        }

        void SetInfoGrid() //Добавление в грид информацию с ArrayList
        {
            GridInfo.RowCount = 2;
            for (int i = 0; i < BaseC.ArrayList.Count; i++)
            {
                GridInfo[0, i].Value = ListInfoGrid[i]; GridInfo[1, i].Value = BaseC.ArrayList[i];
            }
        }

        void SetInfoArray() //Добавление в ArrayList информацию о модуле и ПК
        { 
            BaseC.ArrayList = new ArrayList();
            BaseC.ArrayList.Add(BaseC.GetType().Name);
            BaseC.ArrayList.Add(Environment.MachineName);
        }

      
    }
}
