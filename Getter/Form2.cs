using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Getter
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        private OpenHardwareMonitor.Hardware.Computer computer;
        static int index = 1;
        public Form2()
        {
            InitializeComponent();
            computer = new OpenHardwareMonitor.Hardware.Computer { CPUEnabled = true };
            computer.Open();

            // Set up a timer to update the temperature every second
            Timer timer = new Timer();
            timer.Interval = 1000; // 1000 milliseconds = 1 second
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Get CPU temperature and update the label
            float cpuTemperature = GetCpuTemperature();
            dataGridView1.Columns.Add($"CPU Core #{index++}", $" {cpuTemperature} °C");
            //dataGridView1.Rows.Add($" {cpuTemperature} °C");
        }

        private float GetCpuTemperature()
        {
            foreach (var hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            return sensor.Value ?? 0;
                        }
                    }
                }
            }
            return 0;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close OpenHardwareMonitor when the form is closed
            computer.Close();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            float fcpu = pCPU.NextValue();
            float fram = pRAM.NextValue();
            metroProgressBarCPU.Value = (int)fcpu;
            metroProgressBarRAM.Value = (int)fram;
            lblCPU.Text = string.Format("{0:0.00}%", fcpu);
            lblRAM.Text = string.Format("{0:0.00}%", fram);
            chart1.Series["CPU"].Points.AddY(fcpu);
            chart1.Series["RAM"].Points.AddY(fram);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
