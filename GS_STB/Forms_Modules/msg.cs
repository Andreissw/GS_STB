﻿using System;
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

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                return;
        }
    }
}