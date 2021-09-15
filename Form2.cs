using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win10LockReplacement
{
    public partial class Form2 : Form
    {
        static int attemptsFailed;
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == Form1.Password)
            {
                Form1.doEndTimer("CPG");
                this.Hide();   
            }
            else
            {
                textBox1.Text = "";
                attemptsFailed++;
                if (attemptsFailed >= 3)
                {
                    textBox1.ReadOnly = true; /* Disallow more attempts. Leaving this form writeable would allow for the 10 second timer to be reset every time a false entry is attempted */
                    button1.Enabled = false;
                    MessageBox.Show("Too many failed attempts, shutting down in 10 seconds...");
                    await Task.Delay(10000);
                    System.Diagnostics.Process cmd = new System.Diagnostics.Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.Arguments = "/C shutdown -s -t 0";
                    cmd.Start();
                }
                else
                {
                    MessageBox.Show("Incorrect entry, " + (3 - attemptsFailed) + " attempts remaining.");
                }
            }
        }
    }
}
