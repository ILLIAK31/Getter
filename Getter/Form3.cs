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
using OpenHardwareMonitor.Hardware;

namespace Getter
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        private OpenHardwareMonitor.Hardware.Computer computer;
        public Form3()
        {
            InitializeComponent();
            computer = new OpenHardwareMonitor.Hardware.Computer { CPUEnabled = true , GPUEnabled = true };
            computer.Open();
            System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
            timer.Interval = 100; // 1000 milliseconds = 1 second
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            double temp = GetGpuTemperature();
            label1.Text = $"{temp} °C";
            chart1.Series["GPU"].Points.AddY(temp);
        }
        private float GetGpuTemperature()
        {
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.GpuNvidia || hardware.HardwareType == HardwareType.GpuAti)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature && sensor.Name.Contains("GPU Core"))
                        {
                            return sensor.Value.GetValueOrDefault();
                        }
                    }
                }
            }
            return 0f;
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close OpenHardwareMonitor when the form is closed
            computer.Close();
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
    }
}
