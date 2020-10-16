using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class FAS_END: BaseClass
    {
        public FAS_END()
        {
            ListHeader = new List<string>() { "№", "Serial", "Litera","GroupBox","Pallet","ScanDate" };
        }
        public override void GetLotList(DataGridView Grid)
        {
            
        }

        public override void GetComponentClass(Control control)
        {
           
        }
    }
}
