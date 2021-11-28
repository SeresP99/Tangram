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
        TangramProject.Tangram mainWindow;

        public SizeSettingWindow(double scale, TangramProject.Tangram mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            this.scale = scale;
            textBox1.Text = scale.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) >= 65 && int.Parse(textBox1.Text) <= 200)
            {
                scale = int.Parse(textBox1.Text);
                Close();
            }
            else
                MessageBox.Show("Please type in a number within the specified limit: 65% - 200%");
        }

    }
}
