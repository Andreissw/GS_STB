using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class FAS_Weight_control : BaseClass
    {
        public override void LoadWorkForm()
        {

        }
        public override void KeyDownMethod()
        {

        }
        public FAS_Weight_control()
        {
            ListHeader = new List<string>() { "№", "Serial","ScanDate" };
            IDApp = 20;
        }       

        public override void GetComponentClass()
        {
            control.Controls.Find("FAS_Weight", true).FirstOrDefault().Visible = true;
            control.Controls.Find("FAS_Weight", true).FirstOrDefault().Location = new Point(LocX, LocY);
            control.Controls.Find("FAS_Weight", true).FirstOrDefault().Size = new Size(609, 177);
        }
    }
}
