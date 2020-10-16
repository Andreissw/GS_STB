using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class UploadStation :BaseClass
    {
        public UploadStation()
        {
            ListHeader = new List<string>() { "№", "SN", "SC ID", "CAS ID", "HDCP","CERT","MAC","LDS","SW","SS GS1","StartTime","ScanDate"};
        }
        public override void GetLotList(DataGridView Grid)
        {
            
        }

        public override void GetComponentClass(Control control)
        {
            control.Controls.Find("UploadStationGB", true).FirstOrDefault().Visible = true;
            control.Controls.Find("UploadStationGB", true).FirstOrDefault().Location = new Point(928, 235);
            control.Controls.Find("UploadStationGB", true).FirstOrDefault().Size = new Size(214, 96);

            control.Controls.Find("DG_UplSt", true).FirstOrDefault().Visible = true;            
        }
    }
}
