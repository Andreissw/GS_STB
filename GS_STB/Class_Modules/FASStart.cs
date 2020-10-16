using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class FASStart: BaseClass
    {     

        public FASStart()
        {
            ListHeader = new List<string>() { "№", "CH приемника", "CH платы", "Печать", "ScanDate" };
        }
        public override void GetLotList(DataGridView Grid)
        {
            Grid.Visible = true;
            Grid.Location = new Point(12, 18);
            Grid.Size = new Size(910, 487);
        }

        public override void GetComponentClass(Control control)
        {
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Visible = true;
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Location = new Point(928, 235);
            control.Controls.Find("Fas_Start", true).FirstOrDefault().Size = new Size(249, 260);
        }


    }
}
