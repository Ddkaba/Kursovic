﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovic
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form6 = new Form6();
            form6.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form8 = new Form8();
            form8.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form9 = new Form9();
            form9.Show();
            this.Hide();
        }
    }
}
