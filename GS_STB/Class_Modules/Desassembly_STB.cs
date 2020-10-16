using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    class Desassembly_STB : BaseClass
    {
        public Desassembly_STB()
        {
            ListHeader = new List<string>() { "№", "CH приемника", "Код ошибки", "ScanDate" };
        }
        public override void GetLotList(DataGridView Grid)
        {
           
        }

        public override void GetComponentClass(Control control)
        {
            
        }
    }
}
