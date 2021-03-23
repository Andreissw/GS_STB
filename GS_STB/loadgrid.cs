using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_STB
{
    class loadgrid
    {
        static public void Loadgrid(DataGridView grid, string cmd) //Достает с базы PSIGMA FLAT и преобразует в грид, таблицу
        {
            try
            {
                //SqlConnection sqlcon = new SqlConnection(@"Data Source=WSG150170\SQLEXPRESS; Initial Catalog= FAS; integrated security=True;");
                SqlConnection sqlcon = new SqlConnection(@"Data Source=traceability\flat; Initial Catalog= FAS; user id=volodin;password=volodin;");
                SqlCommand c = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                sqlcon.Open();
                c = sqlcon.CreateCommand();
                c.CommandText = cmd;
                da.SelectCommand = c;
                da.Fill(ds, "Table1");

                grid.DataSource = ds;
                grid.DataMember = "Table1";
                sqlcon.Close();
            }


            catch (Exception)
            {


            }

        }
    }
}
