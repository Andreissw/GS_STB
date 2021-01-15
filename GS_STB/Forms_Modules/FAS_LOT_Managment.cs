using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Forms_Modules
{
   
    public partial class FAS_LOT_Managment : Form
    {
        public FAS_LOT_Managment()
        {
            InitializeComponent();
            ShowGR(LotsGR, true);
        }

        byte labelscenarioId;
        byte WorkScenarioId;
        short modelID;
        bool Option = false;
        string CellData;
        string _cell;
        //LotCode  FullLotCode 	Model Spec DTVS TRICOLOR Market  Ptid BOX Pallet HDCP  Cert Mac ModelCH SW SW1 Weight	Label Date	User st  end range stDate      
        List<string> ListName = new List<string>()         
        {"LotCode", "FullLotCode","LOTID", "Model", "Spec", "Manufacturer", "Operator","Market","PTID","BoxCapacity","PalletCapacity","HDCP","Cert","Mac"
         ,"ModelCheck","SWRead","SWGS1Read","WeightCheck","LabelScenario","WorkingScenario","Дата создания","Создатель Лота","Начальный диапозон","Конечный диапозон"
            ,"Работа по диапозону","Стартовая дата диапозона"};

        List<string> ListNameAdd = new List<string>()
        {"LotCode", "FullLotCode","Model", "Spec", "Manufacturer", "Operator","Market","PTID","BoxCapacity","PalletCapacity","HDCP","Cert","Mac"
         ,"ModelCheck","SWRead","SWGS1Read","WeightCheck","WorkingScenario","LabelScenario"};

        private void FAS_LOT_Managment_Load(object sender, EventArgs e)
        {          
           GetLot(GridLot);            
        }

        string GetPath() //Выбор пути для загрузки данных
        {
            var Folder = new FolderBrowserDialog();
            Folder.ShowDialog();

            return Folder.SelectedPath;
            //return @"C:\Users\a.volodin\Desktop\B527_HDCP_100 шт";

        }
        private void GridAddLot_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GridAddLot.CommitEdit(DataGridViewDataErrorContexts.Commit); // Для того чтобы работало событие на фчек бокс в гриде
        }

        private void GridAddLot_CellValueChanged(object sender, DataGridViewCellEventArgs e) //Событие на  чек бокс в гриде добавление лота
        {
            if (e.ColumnIndex == 0)
                return;

            if (e.RowIndex == 10) //Открытие HDCP
                GetData(HDCPGrid);
            else if (e.RowIndex == 11) //Открытие Сертификатов
                GetData(CertGrid);          

            void GetData(DataGridView grid)
            {
                if (GridAddLot.CurrentCell.Value.ToString() == "True")
                {
                    var path = GetPath(); //Выбираем путь
                    if (string.IsNullOrEmpty(path))
                    { var r = (DataGridViewCheckBoxCell)GridAddLot.CurrentCell; r.Value = false; return; }

                    var Dir = new DirectoryInfo(path);
                    foreach (var item in Dir.GetFiles())
                        grid.Rows.Add(item.Name, item.FullName);
                }
                else if (GridAddLot.CurrentCell.Value.ToString() == "False")                
                    grid.RowCount = 0;                    
                
            }
        }
        private void BT_AddLot_Click(object sender, EventArgs e) //Открытие формы Добавить Лот
        {
            // [0]LotCode", [1]"FullLotCode",[2]"Model", [3]"Spec",
            //[4]"Manufacturer", [5]"Operator",[6]"Market",[7]"PTID",[8]"BoxCapacity"
            //,[9]"PalletCapacity",[10]"HDCP",[11]"Cert",[12]"Mac"
            //,[13]"ModelCheck",[14]"SWRead",[15]"SWGS1Read",[16]"LabelScenario"};

            if (GridLot.CurrentRow.Index == -1)
                return;
            ShowGR(LotsGR, false);
            ShowGR(AddLotGR, true);
            OpenAdd();
            
        }

        private void OpenAdd() //Обновление грида на добавление лота
        {   
            CellData = "";
            GridAddLot.RowCount = ListNameAdd.Count;
            for (int i = 0; i < GridAddLot.RowCount; i++)
                GridAddLot[0, i].Value = ListNameAdd[i];

            var CBCEll = new DataGridViewComboBoxCell();
            CBCEll.DataSource = ModelList();
            GridAddLot[1, 2] = CBCEll;

            for (int i = 10; i <= 17; i++)
            {
                var BoolCell = new DataGridViewCheckBoxCell();
                BoolCell.Value = false;
                GridAddLot[1, i] = BoolCell;
            }
            var LbWork = new DataGridViewComboBoxCell();
            LbWork.DataSource = WorkScenario();
            GridAddLot[1, 17] = LbWork;

            var LbCEll = new DataGridViewComboBoxCell();
            LbCEll.DataSource = LabelScenario();
            GridAddLot[1, 18] = LbCEll;
            GridAddLot[1, 0].Value = null;
            GridAddLot[1, 1].Value = null;
            GridAddLot[1, 3].Value = null;
            GridAddLot[1, 4].Value = "DTVS";
            GridAddLot[1, 5].Value = "TRICOLOR";
            GridAddLot[1, 6].Value = null;
            GridAddLot[1, 7].Value = null;
            GridAddLot[1, 8].Value = null;
            GridAddLot[1, 9].Value = null;
        }

        private void OptionBT_Click(object sender, EventArgs e) //Редактирования
        {
            if (!Option)
            {
                Option = true; this.OptionBT.Image = global::GS_STB.Properties.Resources.ON;
                LBOption.Text = "Редактирование данных \n ON"; GridInfo.ReadOnly = false; UpdateBT.Visible = true; BTRange.Visible = true;
                //-------------------------------------------- Превращение ячейки модели в кмбобокс для редактирования Model
                string _cell = GridInfo[1, 3].Value.ToString();
                GetModelId(_cell);                
                var CBCEll = new DataGridViewComboBoxCell();
                CBCEll.DataSource = ModelList();                
                CBCEll.Value = _cell;
                GridInfo[1, 3] = CBCEll;
                //-------------------------------------------

                //------------------------------------------- Преварщение ячейки в булевые значение hdcp cert и т.д
                for (int i = 11; i <= 17; i++)
                {
                    var BoolCell = new DataGridViewCheckBoxCell();
                    //BoolCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;                    
                    bool value = bool.Parse(GridInfo[1, i].Value.ToString());
                    BoolCell.Value = value;
                    GridInfo[1, i] = BoolCell;
                }
                //------------------------------------------- 

                string _cellLB = GridInfo[1, 18].Value.ToString(); // LabelScenario Combobox
                GetlabelscenarioID(_cellLB.Substring(0,1));
                var LbCEll = new DataGridViewComboBoxCell();
                LbCEll.DataSource = LabelScenario();
                LbCEll.Value = _cellLB;
                GridInfo[1, 18] = LbCEll;

                string _cellWork = GridInfo[1, 19].Value.ToString(); // WorkScenario Combobox
                GetWorkScenarioID(_cellWork.Substring(0, 1));
                var _cellWorkCB = new DataGridViewComboBoxCell();
                _cellWorkCB.DataSource = WorkScenario();
                _cellWorkCB.Value = _cellWork;
                GridInfo[1, 19] = _cellWorkCB;
            }
            else
            {
                OffInfo();               
            }
        }

        private void BT_RegisterNewLOT_Click(object sender, EventArgs e) //Сохранение нового лота
        {
            var list = new List<string>(); 
            if (NumerSN.Value == 0) //Проверка на кол-во номеров в лоте
            {
                MessageBox.Show("Количество в лоте не указано", "Ошибка с заполнением данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GridAddLot.ClearSelection();
                NumerSN.Select();
                return;
            }           

            for (int i = 0; i < GridAddLot.RowCount; i++) //Проверка на заполнение грида
            {
                if (i == 2)
                    continue;

                if (GridAddLot[1, i].Value == null)
                { MessageBox.Show("Не все поля заполнены!","Ошибка с заполнением данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    GridAddLot.ClearSelection(); GridAddLot[1, i].Selected = true; return; }

                list.Add(GridAddLot[1, i].Value.ToString());
            }

            if (bool.Parse(GridAddLot[1, 10].Value.ToString()) == true)
            {
                if (NumerSN.Value != HDCPGrid.RowCount)
                {
                    MessageBox.Show("Количество SN в лоте не соответсвтует Кол-ву HDCP", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == true)
            {
                if (NumerSN.Value != CertGrid.RowCount)
                {
                    MessageBox.Show("Количество SN в лоте не соответсвтует Кол-ву Cert", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            var LotID = SaveGSLot();
            if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == true & bool.Parse(GridAddLot[1, 10].Value.ToString()) == true)
                AddSerialNumberHDCPCert(LotID);
            else if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == true & bool.Parse(GridAddLot[1, 10].Value.ToString()) == false)
                AddSerialNumberCert(LotID);
            else if (bool.Parse(GridAddLot[1, 11].Value.ToString()) == false & bool.Parse(GridAddLot[1, 10].Value.ToString()) == true)
                AddSerialNumberHDCP(LotID);
            else
                AddSerialNumber(LotID);

            OpenAdd();
            MessageBox.Show("Лот успешно добавлен!");
            //if (bool.Parse(GridAddLot[1, 19].Value.ToString()) == true)
            //    RangeMethod((int)NumerSN.Value, 11); 

        }

        void RangeMethod(int snIn,int lotid)
        {
            if (snIn <= 15000) //Если в лоте меньше или равно 15000 номеров
            {
                using (var fas = new FASEntities())
                {
                    var st = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Min();
                    var end = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Max();

                    var range = new FAS_Fixed_RG()
                    {
                        LotID = lotid,
                        LitIndex = 1,
                        LabDate = DTPic.Value,
                        RGStart = st,
                        RGEnd = end,                        
                    };

                    var gslot = fas.FAS_GS_LOTs.Where(c => c.LOTID == lotid);
                    gslot.FirstOrDefault().RangeStart = st;
                    gslot.FirstOrDefault().RangeEnd = end;
                    gslot.FirstOrDefault().FixedRG = true;
                    gslot.FirstOrDefault().StartDate = DTPic.Value;

                    fas.FAS_Fixed_RG.Add(range);
                    fas.SaveChanges();

                }
            }
            else //Если в лоте больше номеров
            {
                using (var fas = new FASEntities())
                {
                    var st = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Min();
                    var end = fas.FAS_SerialNumbers.Where(c => c.LOTID == lotid).Select(c => c.SerialNumber).Max();

                    var count = snIn / 15000;
                    var k = st;
                    for (int i = 1; i <= count; i++)
                    {
                        st = st + 15000;

                        var _range = new FAS_Fixed_RG()
                        {
                            LotID = lotid,
                            LitIndex = (short)i,

                        };
                    }          
                }
            }
        }
   

        short SaveGSLot() // Сохранение в таблицу GSLots
        {
            // [0]LotCode", [1]"FullLotCode",[2]"Model", [3]"Spec",
            //[4]"Manufacturer", [5]"Operator",[6]"Market",[7]"PTID",[8]"BoxCapacity"
            //,[9]"PalletCapacity",[10]"HDCP",[11]"Cert",[12]"Mac"
            //,[13]"ModelCheck",[14]"SWRead",[15]"SWGS1Read",[16]"LabelScenario"};
            using (var fas = new FASEntities())
            {
                var _fas = new FAS_GS_LOTs()
                {
                    LOTCode = short.Parse(GridAddLot[1, 0].Value.ToString()),
                    FULL_LOT_Code = GridAddLot[1, 1].Value.ToString(),
                    ModelID = modelID,
                    Specification = GridAddLot[1, 3].Value.ToString(),
                    Manufacturer = GridAddLot[1, 4].Value.ToString(),
                    Operator = GridAddLot[1, 5].Value.ToString(),
                    MarketID = GridAddLot[1, 6].Value.ToString(),
                    PTID = GridAddLot[1, 7].Value.ToString(),
                    IsActive = true,
                    BoxCapacity = int.Parse(GridAddLot[1, 8].Value.ToString()),
                    PalletCapacity = int.Parse(GridAddLot[1, 9].Value.ToString()),
                    IsHDCPUpload = bool.Parse(GridAddLot[1, 10].Value.ToString()),
                    IsCertUpload = bool.Parse(GridAddLot[1, 11].Value.ToString()),
                    IsMACUpload = bool.Parse(GridAddLot[1, 12].Value.ToString()),
                    ModelCheck = bool.Parse(GridAddLot[1, 13].Value.ToString()),
                    SWRead = bool.Parse(GridAddLot[1, 14].Value.ToString()),
                    SWGS1Read = bool.Parse(GridAddLot[1, 15].Value.ToString()),
                    GetWeight = bool.Parse(GridAddLot[1, 16].Value.ToString()),
                    LabelScenarioID = labelscenarioId,
                    WorkingScenarioID = WorkScenarioId,
                    CreateDate = DateTime.UtcNow.AddHours(2),
                    CreateByID = 211,   
                };
                fas.FAS_GS_LOTs.Add(_fas);
                fas.SaveChanges();

                return fas.FAS_GS_LOTs.OrderByDescending(c => c.LOTID).Select(c => c.LOTID).FirstOrDefault();
            }            
        }

        void OffInfo() //Режим редактирование, выключение
        {
            Option = false; this.OptionBT.Image = global::GS_STB.Properties.Resources.OFF2;
            LBOption.Text = "Редактирование данных \n OFF"; GridInfo.ReadOnly = true; UpdateBT.Visible = false; BTRange.Visible = false;
            string _cell = GridInfo[1, 3].Value.ToString();
            GridInfo[1, 3] = new DataGridViewTextBoxCell();
            GridInfo[1, 3].Value = _cell;

            for (int i = 11; i <= 17; i++)
            {
                var TextCell = new DataGridViewTextBoxCell();
                TextCell.Value = GridInfo[1, i].Value.ToString();
                GridInfo[1, i] = TextCell;
            }

            string _cellLB = GridInfo[1, 18].Value.ToString();
            GridInfo[1, 18] = new DataGridViewTextBoxCell();
            GridInfo[1, 18].Value = _cellLB;

            string _cellLBWork = GridInfo[1, 19].Value.ToString();
            GridInfo[1, 19] = new DataGridViewTextBoxCell();
            GridInfo[1, 19].Value = _cellLBWork;
        }

        private void GridInfo_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0) //Защита от редактирования 1 столбца 
                e.Cancel = true;
        }

        private void UpdateBT_Click(object sender, EventArgs e) //Update данных
        {
            //LotCode  FullLotCode LOTID Model Spec DTVS TRICOLOR Market  Ptid BOX Pallet HDCP  Cert Mac ModelCH SW SW1 Weight
            //Label Date	User st  end range stDate      
            short lotid = short.Parse(GridInfo[1,2].Value.ToString());
            using (var FAS = new FASEntities())
            {
                var _fas = FAS.FAS_GS_LOTs.Where(c => c.LOTID == lotid);
                _fas.FirstOrDefault().LOTCode = short.Parse(GridInfo[1, 0].Value.ToString());//LotCode
                _fas.FirstOrDefault().FULL_LOT_Code = GridInfo[1, 1].Value.ToString(); //FullLotCode
                _fas.FirstOrDefault().ModelID = modelID;  //Model
                _fas.FirstOrDefault().Specification = GridInfo[1, 4].Value.ToString(); // Spec
                _fas.FirstOrDefault().Manufacturer = GridInfo[1, 5].Value.ToString(); //DTVS
                _fas.FirstOrDefault().Operator = GridInfo[1, 6].Value.ToString(); // TRICOLOR
                _fas.FirstOrDefault().MarketID = GridInfo[1, 7].Value.ToString(); // Market
                _fas.FirstOrDefault().PTID = GridInfo[1, 8].Value.ToString(); //Ptid
                _fas.FirstOrDefault().BoxCapacity = int.Parse(GridInfo[1, 9].Value.ToString()); //BOX
                _fas.FirstOrDefault().PalletCapacity = int.Parse(GridInfo[1, 10].Value.ToString()); //Pallet
                _fas.FirstOrDefault().IsHDCPUpload = bool.Parse(GridInfo[1, 11].Value.ToString()); //HDCP
                _fas.FirstOrDefault().IsCertUpload = bool.Parse(GridInfo[1, 12].Value.ToString()); //Cert
                _fas.FirstOrDefault().IsMACUpload = bool.Parse(GridInfo[1, 13].Value.ToString()); //Mac
                _fas.FirstOrDefault().ModelCheck = bool.Parse(GridInfo[1, 14].Value.ToString()); //ModelCH
                _fas.FirstOrDefault().SWRead = bool.Parse(GridInfo[1, 15].Value.ToString()); //SW
                _fas.FirstOrDefault().SWGS1Read = bool.Parse(GridInfo[1, 16].Value.ToString()); //SW1
                _fas.FirstOrDefault().GetWeight = bool.Parse(GridInfo[1, 17].Value.ToString()); //Weight
                _fas.FirstOrDefault().LabelScenarioID = labelscenarioId; // Label
                _fas.FirstOrDefault().WorkingScenarioID = WorkScenarioId;
                _fas.FirstOrDefault().CreateDate = DateTime.Parse(GridInfo[1, 20].Value.ToString()); //Date
                FAS.SaveChanges();

                MessageBox.Show("Сохранено!","Сообщение",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        private void GridInfo_SelectionChanged(object sender, EventArgs e)
        {
            if (!Option)
                return;

            DataGridView DG = sender as DataGridView;

            if (DG.CurrentCell.ColumnIndex == 1)

                if (DG.CurrentCell.RowIndex == 3 || DG.CurrentCell.RowIndex == 18 || DG.CurrentCell.RowIndex == 19)
                {
                    DG.CurrentCell = DG[1, DG.CurrentCell.RowIndex];
                    DG.BeginEdit(false);
                    var f = DG.EditingControl as DataGridViewComboBoxEditingControl;
                    f.DroppedDown = true;
                }
            if (DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value != null)
            {

                CellData = DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value.ToString(); return;
            }

            CellData = "";
        }

        private void GridAddLot_SelectionChanged(object sender, EventArgs e) //Открытие по щелчку списка у comboBox в элементе DataGrid
        {
            DataGridView DG = sender as DataGridView;
            
            if (DG.CurrentCell.ColumnIndex == 1)

                if (DG.CurrentCell.RowIndex == 2 || DG.CurrentCell.RowIndex == 17 || DG.CurrentCell.RowIndex == 18)
                {
                    DG.CurrentCell = DG[1, DG.CurrentCell.RowIndex];
                    DG.BeginEdit(false);
                    var f = DG.EditingControl as DataGridViewComboBoxEditingControl;
                    f.DroppedDown = true;
                }
                    if (DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value != null)
                    {   
                  
                    CellData = DG[DG.CurrentCell.ColumnIndex, DG.CurrentCell.RowIndex].Value.ToString(); return;
                    }

            CellData = "";
        }
        private void GridAddLot_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // [0]LotCode", [1]"FullLotCode",[2]"Model", [3]"Spec",
            //[4]"Manufacturer", [5]"Operator",[6]"Market",[7]"PTID",[8]"BoxCapacity"
            //,[9]"PalletCapacity",[10]"HDCP",[11]"Cert",[12]"Mac"
            //,[13]"ModelCheck",[14]"SWRead",[15]"SWGS1Read",[16]"LabelScenario"};

            DataGridView DG = sender as DataGridView;
            if (DG[e.ColumnIndex, e.RowIndex].Value == null)
                return;

            _cell = DG[e.ColumnIndex, e.RowIndex].Value.ToString(); // пишем в переменную _cell, то что мы ввели   

            if (e.RowIndex == 0) //Проверка LotCode, 1 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;

                CheckLength(e, 3, DG); return;
            }

            if (e.RowIndex == 1) //Проверка FullLotCode, 2 строка в гриде
            { CheckLength(e, 50, DG); return; }          

            if (e.RowIndex == 2) //Проверка Model, 4 строка в гриде            
            { GetModelId(_cell); return; }

            if (e.RowIndex == 3) //Проверка Spec, 5 строка в гриде
            { CheckLength(e, 50, DG); return; }

            if (e.RowIndex == 4) //Проверка DTVS, 6 строка в гриде
            { CheckLength(e, 5, DG); return; }

            if (e.RowIndex == 5) //Проверка TRICOLOR, 7 строка в гриде
            { CheckLength(e, 10, DG); return; }

            if (e.RowIndex == 6) //Проверка Market, 8 строка в гриде
            { CheckLength(e, 15, DG); return; }

            if (e.RowIndex == 7) //Проверка Ptid, 9 строка в гриде
            { CheckLength(e, 2, DG); return; }

            if (e.RowIndex == 8 || e.RowIndex == 9) //Проверка BOX, pallet 10,11 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;
                CheckLength(e, 3, DG); return;
            }

            if (e.RowIndex == 17) //Проверка LabelScenario, 19 строка в гриде
            { GetWorkScenarioID(DG[1, 17].Value.ToString().Substring(0, 1)); return; }

            if (e.RowIndex == 18) //Проверка LabelScenario, 19 строка в гриде
            { GetlabelscenarioID(DG[1, 18].Value.ToString().Substring(0, 1)); return; }
          
        }

        private void GridInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e) //Событие срабатывает, когда редактирование ячейки закончено
        {
            //LotCode[0]  FullLotCode[1] LOTID[2]	Model[3] Spec[4] DTVS[5] TRICOLOR[6] Market[7] Ptid[8]
            //BOX[9] Pallet[10] HDCP[11]  Cert[12] Mac[13] ModelCH[14] SW[15] SW1[16] Weight[17]
            //Label[18] Date[19]	User st  end range stDate    
            DataGridView DG = sender as DataGridView;
            if (DG[e.ColumnIndex, e.RowIndex].Value == null)
                return;

            _cell = DG[e.ColumnIndex, e.RowIndex].Value.ToString(); // пишем в переменную _cell, то что мы ввели   

            if (e.RowIndex == 0) //Проверка LotCode, 1 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;
                                
                CheckLength(e, 3, DG);  return;        
            }

            if (e.RowIndex == 1) //Проверка FullLotCode, 2 строка в гриде
            {  CheckLength(e, 50, DG); return; }

            if (e.RowIndex == 2 || e.RowIndex == 20 || e.RowIndex == 21 || e.RowIndex == 22 || e.RowIndex == 23 || e.RowIndex == 24)
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; return;}

            if (e.RowIndex == 3) //Проверка Model, 4 строка в гриде            
            { GetModelId(_cell); return; }
           
            if (e.RowIndex == 4) //Проверка Spec, 5 строка в гриде
            { CheckLength(e, 50, DG); return;}

            if (e.RowIndex == 5) //Проверка DTVS, 6 строка в гриде
            {  CheckLength(e, 5, DG); return; }

            if (e.RowIndex == 6) //Проверка TRICOLOR, 7 строка в гриде
            { CheckLength(e, 10, DG); return; }

            if (e.RowIndex == 7) //Проверка Market, 8 строка в гриде
            { CheckLength(e, 15, DG); return; }

            if (e.RowIndex == 8) //Проверка Ptid, 9 строка в гриде
            { CheckLength(e, 2, DG); return; }

            
            if (e.RowIndex == 9 || e.RowIndex == 10) //Проверка BOX, pallet 10,11 строка в гриде
            {
                if (!ChecInt(e, DG))
                    return;
                 CheckLength(e, 3, DG); return; }

            if (e.RowIndex == 18) //Проверка LabelScenario, 19 строка в гриде
            { GetlabelscenarioID(DG[1,18].Value.ToString().Substring(0,1)); return; }

            if (e.RowIndex == 19) //Проверка LabelScenario, 19 строка в гриде
            { GetWorkScenarioID(DG[1, 19].Value.ToString().Substring(0, 1)); return; }

            if (e.RowIndex == 20) //Проверка Date, 20 строка в гриде
            {
                if (DG.Name == "GridInfo")
                {
                    ChecDate(e, DG); return;
                }
                
            }
        }

        void ChecDate(DataGridViewCellEventArgs e, DataGridView DG)
        {
            DateTime k;
            if (!DateTime.TryParse(_cell, out k))
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; M("Не верный формат вводных данных, ввести разрешено только Дату"); return; }
        }

        bool ChecInt(DataGridViewCellEventArgs e, DataGridView DG)
        {
            Int16 k;
            if (!short.TryParse(_cell, out k))
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; M("Не верный формат вводных данных, ввести разрешено только число"); return false; }
            return true;
        }

        void CheckLength(DataGridViewCellEventArgs e, int L, DataGridView DG)
        {
            if (_cell.Length > L)
            { DG[e.ColumnIndex, e.RowIndex].Value = CellData; M($"Для вводных данных максимум {L} символов \nВы Ввели {_cell.Length} символа!"); return; }
            DG[e.ColumnIndex, e.RowIndex].Value = _cell;
        }

       

        private void BT_LOT_Info_Click(object sender, EventArgs e) //Информация о лоте
        {
            if (GridLot.CurrentRow.Index == -1)
                return;

            this.OptionBT.Image = global::GS_STB.Properties.Resources.OFF2;
            CellData = "";
            Option = false;
            LBOption.Text = "Редактирование данных \n OFF";
            UpdateBT.Visible = false;
            GridRange.Visible = false;
            ShowGR(LotsGR, false);
            ShowGR(LotInfoGR, true);
            var lotid = short.Parse(GridLot[6, GridLot.CurrentRow.Index].Value.ToString());
            var list = GetLotInfo(lotid);
            GetLotRange(GridRange,lotid);
            GridInfo.RowCount = ListName.Count;
            GridInfo[1, 3] = new DataGridViewTextBoxCell();

            for (int i = 0; i < GridInfo.RowCount-1; i++)
            {
                GridInfo[0, i].Value = ListName[i];
                GridInfo[1, i].Value = list[i];
            }
        }

        private void BackButtonInfoLot_Click(object sender, EventArgs e) //Возврат в лот
        {
            OffInfo();
            GetLot(GridLot);
            ShowGR(LotsGR, true);
            ShowGR(LotInfoGR, false);      
        }

        private void BackButtonAddLot_Click(object sender, EventArgs e)
        {
            ShowGR(LotsGR, true);
            ShowGR(AddLotGR, false);
        }

        void ShowGR(GroupBox GB, bool B)
        {
            GB.Visible = B;
            GB.Location = new Point(10,10);
            GB.Size = new Size(1400, 689);
        }

        ArrayList GetLotInfo( short LotID)
        {
            using (FASEntities FAS = new FASEntities())
            {
                var ArrayList = new ArrayList();
                var data = from lot in FAS.FAS_GS_LOTs
                           where lot.LOTID == LotID
                           join model in FAS.FAS_Models on lot.ModelID equals model.ModelID
                           join lb in FAS.FAS_LabelScenario on lot.LabelScenarioID equals lb.ID
                           join user in FAS.FAS_Users on lot.CreateByID equals user.UserID
                           join work in FAS.FAS_WorkingScenario on lot.WorkingScenarioID equals work.ID
                           select new
                           {
                               LotCode      = lot.LOTCode,
                               FullLotCode  = lot.FULL_LOT_Code,
                               LOTID        = lot.LOTID,
                               Model        = model.ModelName,
                               Spec         = lot.Specification,
                               DTVS         = lot.Manufacturer,
                               TRICOLOR     = lot.Operator,
                               Market       = lot.MarketID,
                               Ptid         = lot.PTID,
                               BOX          = lot.BoxCapacity,
                               Pallet       = lot.PalletCapacity,
                               HDCP         = lot.IsHDCPUpload,
                               Cert         = lot.IsCertUpload,
                               Mac          = lot.IsMACUpload,
                               ModelCH      = lot.ModelCheck,
                               SW           = lot.SWRead,
                               SW1          = lot.SWGS1Read,
                               Weight       = lot.GetWeight,
                               Label        = lb.Scenario + " | " + lb.Description,
                               WorkScenario = work.Scenario + " | " + work.Description,
                               Date         = lot.CreateDate,
                               User         = user.UserName,
                               st           = lot.RangeStart,
                               end          = lot.RangeEnd,
                               range        = lot.FixedRG,
                               stDate       = lot.StartDate
                           };
                try
                {
                    var report = data.First().GetType().GetProperties().Select(c => c.GetValue(data.First()));
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

        void GetLotRange(DataGridView Grid,int lotid)
        {
            using (FASEntities Fas = new FASEntities())
            {
                Grid.DataSource = (from fix in Fas.FAS_Fixed_RG
                                   where fix.LotID == lotid
                                  select new 
                                  { 
                                      ID = fix.id,LitIndex = fix.LitIndex, Мин_диапозон = fix.RGStart, Макс_диапозон = fix.RGEnd, ДатаЭтикетки = fix.LabDate,
                                      Кол_во_номеров = (fix.RGEnd - fix.RGStart) + 1

                                  }).ToList();
                if (Grid.RowCount != 0) 
                { 
                    Grid.Visible = true;
                    Grid.Columns[0].Visible = false;
                }
            }
        }

        void GetLot(DataGridView Grid)
        {
            loadgrid.Loadgrid(Grid, @"use FAS select 
LOTCode,FULL_LOT_Code,ModelName,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID) InLot,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 0 and s.IsActive = 1) Ready,
(select count(1) from FAS_SerialNumbers as s where LOTID = gs.LOTID and s.IsUsed = 1 ) Used,
LOTID, RangeStart,RangeEnd, FixedRG,StartDate
from FAS_GS_LOTs as gs
left join FAS_Models as m on gs.ModelID = m.ModelID
where IsActive = 1
order by LOTID desc ");
            //using (FASEntities FAS = new FASEntities())
            //{
            //    //Grid.DataSource = (from Lot in FAS.FAS_GS_LOTs
            //    //                   join model in FAS.FAS_Models on Lot.ModelID equals model.ModelID
            //    //                   where Lot.IsActive == true
            //    //                   orderby Lot.LOTID descending
            //    //                   select new
            //    //                   {
            //    //                       Lot = Lot.LOTCode,
            //    //                       Full_Lot = Lot.FULL_LOT_Code,
            //    //                       Model = model.ModelName,
            //    //                       InLot = (from s in FAS.FAS_SerialNumbers where s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //    //                       Ready = (from s in FAS.FAS_SerialNumbers where s.IsUsed == false & s.IsActive == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //    //                       Used = (from s in FAS.FAS_SerialNumbers where s.IsUsed == true & s.LOTID == Lot.LOTID select s.LOTID).Count(),
            //    //                       LotID = Lot.LOTID,
            //    //                       СтартДиапозон = Lot.RangeStart,
            //    //                       КонецДиапозон = Lot.RangeEnd,
            //    //                       FixedRG = Lot.FixedRG,
            //    //                       StartDate = Lot.StartDate
            //    //                   }).ToArray();
            //}
        }

        

        void M(string line)
        {
            MessageBox.Show(line,"Не правильный формат данных",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        void GetModelId(string model)
        {
            using (var fas = new FASEntities())
            {
                modelID = fas.FAS_Models.Where(c => c.ModelName == model).Select(c => c.ModelID).FirstOrDefault();
            }
        }

        void GetlabelscenarioID(string Scenario)
        {
            using (var fas = new FASEntities())
            {
                labelscenarioId = fas.FAS_LabelScenario.Where(c => c.Scenario == Scenario).Select(c => c.ID).FirstOrDefault();
            }
        }

        void GetWorkScenarioID(string Scenario)
        {
            using (var fas = new FASEntities())
            {
                WorkScenarioId = fas.FAS_WorkingScenario.Where(c => c.Scenario == Scenario).Select(c => c.ID).FirstOrDefault();
            }
        }

        object LabelScenario()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_LabelScenario.Select(c => c.Scenario + " | " + c.Description).ToList();
            }
        }

        object WorkScenario()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_WorkingScenario.Select(c => c.Scenario + " | " + c.Description).ToList();
            }
        }

        object ModelList()
        {
            using (var fas = new FASEntities())
            {
                return fas.FAS_Models.Where(c=>c.ModelTypeID == 1).Select(c => c.ModelName).Distinct().ToList();
            }
        }

        void AddSerialNumber(short LotID)
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,
                    };

                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();
                }
            }
        }
        void AddSerialNumberHDCP(short LotID)
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,

                        FAS_HDCP = new FAS_HDCP()
                        {
                            HDCPName = HDCPGrid[0, i].Value.ToString(),
                            HDCPContent = File.ReadAllBytes(HDCPGrid[1, i].Value.ToString()),
                        },
                    };
                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();

                }
            }
        }
        void AddSerialNumberCert(short LotID)
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,

                        FAS_CERT = new FAS_CERT()
                        {
                            CERTName = CertGrid[0, i].Value.ToString(),
                            CertContent = File.ReadAllBytes(CertGrid[1, i].Value.ToString()),
                        }

                    };
                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();

                }
            }
        }

        void AddSerialNumberHDCPCert(short LotID)
        {
            using (var fas = new FASEntities())
            {
                for (int i = 0; i < NumerSN.Value; i++)
                {
                    var _ser = new FAS_SerialNumbers()
                    {
                        LOTID = LotID,
                        IsUsed = false,
                        IsActive = true,
                        IsUploaded = false,
                        IsWeighted = false,
                        IsPacked = false,
                        InRepair = false,

                        FAS_HDCP = new FAS_HDCP()
                        {
                            HDCPName = HDCPGrid[0, i].Value.ToString(),
                            HDCPContent = File.ReadAllBytes(HDCPGrid[1, i].Value.ToString()),
                        },

                        FAS_CERT = new FAS_CERT()
                        {
                            CERTName = CertGrid[0, i].Value.ToString(),
                            CertContent = File.ReadAllBytes(CertGrid[1, i].Value.ToString()),
                        }

                    };
                    fas.FAS_SerialNumbers.Add(_ser);
                    fas.SaveChanges();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetRangeForm SetRange = new SetRangeForm(int.Parse(GridInfo[1, 2].Value.ToString()), int.Parse(GridInfo[1,0].Value.ToString()));
            var Result = SetRange.ShowDialog();
            if (Result == DialogResult.Cancel)
                return; 

            GridRange.Visible = true;
            GetLotRange(GridRange, int.Parse(GridInfo[1, 2].Value.ToString()));

        }

        private void GridRange_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 4)            
                e.Cancel = true;
            
        }

        private void GridRange_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DateTime d = DateTime.MinValue;
            var r = DateTime.TryParse(GridRange[e.ColumnIndex,e.RowIndex].Value.ToString(),out d);
            if (!r)
            {
                MessageBox.Show("Не верный формат даты");
                return;
            }
        }
    }
}
