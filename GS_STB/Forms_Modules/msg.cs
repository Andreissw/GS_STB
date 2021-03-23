using System;
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
    public partial class msg : Form

        
    {

        public msg(string Text)
        {
            InitializeComponent();
            this.Select();
            label1.Text = Text;
        }

        public msg(string Text, string Night,string Day)
        {
            InitializeComponent();
            this.Select();
            label1.Text = Text;
            button1.Text = Day;
            button2.Text = Night;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (e.GetHashCode() == 815388)
                return;

            this.Close();
        }

        private void msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();             
            }

            if (e.KeyCode == Keys.N)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

       
    }
}
