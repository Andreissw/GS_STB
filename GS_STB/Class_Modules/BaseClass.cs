using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB.Class_Modules
{
    public abstract class  BaseClass
    {
        public ArrayList ArrayList { get; set; }

        public List<string> ListHeader { get; set; }
        public int IDApp { get; set; }

        public int StationID { get; set; }

        public abstract void GetLotList(DataGridView Grid);

        public abstract void GetComponentClass(Control control);
        

    }
}
