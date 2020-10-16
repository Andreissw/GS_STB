using GS_STB.Class_Modules;
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
    public partial class WorkForm : Form
    {
        private BaseClass BaseC;
        List<object> ListLoggin = new List<object>();
        readonly List<string> ListInfoGrid = new List<string>() { "Название приложения", "Название станции","Линия","Номер Лота","Полное имя лота","Модель","Имя пользователя" };
        public WorkForm(BaseClass BC)
        {
            InitializeComponent();
            BaseC = BC; //Приведение к типу
            this.Text = BaseC.GetType().Name; //Заголовок формы называется именем модулем
            GetLoggin();//Настройка форма ввода пароля
            Times.Enabled = true; CurrrentDateLabel.Text = DateTime.Now.ToString("dd.MM.yyyy"); //Настройка времени

            TB_RFIDIn.KeyDown += (a, e) => //Событие при вводе логина
            {
                if (e.KeyCode == Keys.Enter)
                    if (GetLoginData()) //Метод, которы проверяет логин и добавляет в ArrayList данные о пользователе    
                    {
                        TB_RFIDIn.Clear(); TB_RFIDIn.Select();
                    }           
            };
            
        }

      
        void GetWorkForm()// Инициализация компонентов рабочей формы
        {
            GB_Work.Visible = true;
            GB_Work.Location = new Point(12, 12);
            GB_Work.Size = new Size(1258, 629);
            SetInfoGrid();
            GetGridSetting();
            SerialTextBox.Select();
        }

        bool GetLoginData() //Проверка логина и получение данных о пользователе
        {
            if (false)   //Проверка правильности пароля         
                return true;
            
            GetWorkForm();
            return false;
        }
        void GetLoggin() //Настройка форма ввода пароля
        {
            GB_UserData.Visible = true;
            GB_UserData.Location = new Point(12,12);
            GB_UserData.Size = new Size(429, 177);
            TB_RFIDIn.Select();
        }

    

        void SetInfoGrid()
        {
            GridInfo.RowCount = ListInfoGrid.Count;
            for (int i = 0; i < ListInfoGrid.Count; i++)
            {
                GridInfo[0, i].Value = ListInfoGrid[i];
                //GridInfo[1, i].Value = BaseC.ArrayList[i];
            }
        }

        void GetGridSetting()
        {
            DG_UpLog.ColumnCount = BaseC.ListHeader.Count;
            for (int i = 0; i < BaseC.ListHeader.Count; i++)            
                DG_UpLog.Columns[i].HeaderText = BaseC.ListHeader[i];
           
        }

        private void Times_Tick(object sender, EventArgs e)
        {
            CurrrentTimeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
