using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowLab
{
    public partial class Form1 : Form
    {
        Shadow shadow;
        public Form1()
        {
            InitializeComponent();
            shadow = new Shadow();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shadow.AddSegment(new Segment((double)numericUpDown1.Value, (double)numericUpDown2.Value));
            textBox1.Text = shadow.GetLength().ToString();
        }
    }
}
