using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tangram.AppWindows
{
    public partial class SizeSettingWindow : Form
    {
        public double scale;
        public SizeSettingWindow(double scale)
        {
            InitializeComponent();
            this.scale = scale;
            textBox1.Text = scale.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            scale = int.Parse(textBox1.Text);
            Close();
        }

    }
}
