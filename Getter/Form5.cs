using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Getter
{
    public partial class Form5 : MetroFramework.Forms.MetroForm
    {
        public Form5()
        {
            InitializeComponent();
            SystemINFO();
        }
        void SystemINFO()
        {
            // Run the systeminfo command and capture the output
            string systemInfoOutput = RunSystemInfoCommand();

            // Display the output in a TextBox (you can use any control of your choice)
            textBox1.Text = systemInfoOutput;
        }
        private string RunSystemInfoCommand()
        {
            // Create a new process to run the systeminfo command
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c systeminfo";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Start the process and read the output
            process.Start();
            string output = process.StandardOutput.ReadToEnd();

            // Wait for the process to exit
            process.WaitForExit();

            return output;
        }
    }
}
