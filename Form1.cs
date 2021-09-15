using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win10LockReplacement
{
    public partial class Form1 : Form
    {
        public static string Password;
        static bool autoKill;
        public Form1()
        {
            InitializeComponent();
            autoKill = false; //Assign this a value because thats a pretty good idea
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
            textBox1.PasswordChar = '\0';
            }
            else
            {
            textBox1.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 Secondform = new Form2();
            Secondform.Show();
            autoKill = true;
            Password = textBox1.Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (autoKill == true)
            {
                System.Diagnostics.Process cmd = new System.Diagnostics.Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.Arguments = "/C taskkill /F /IM explorer.exe && taskkill /F /IM taskmgr.exe && taskkill /F /IM discord.exe && taskkill /F /IM python.exe && taskkill /F /IM chrome.exe && taskkill /F /IM brave.exe && taskkill /F /IM firefox.exe && taskkill /F /IM resmon.exe && taskkill /F /IM regedit.exe"/* Add a few apps that should be unavailable during lockdown mode */;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.Start();
            }
            else
            {
                this.Show();
            }
        }

        public static async void doEndTimer(string sc)
        {
            if (sc == "CPG")
            {
                autoKill = false;
                await Task.Delay(250); /* Allow for Form2 visual updates */
                MessageBox.Show("You will now be logged out, please log in again to start the filesystem.");
                System.Diagnostics.Process cmd = new System.Diagnostics.Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.Arguments = "/C shutdown -l";
                cmd.Start();
            }
            else
            {
                //this is really bad, failed attempt to unlock, likely malicious
                System.Diagnostics.Process cmd = new System.Diagnostics.Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.Arguments = "/C shutdown -s -t 0";
                cmd.Start();
            }
        }
    }
}