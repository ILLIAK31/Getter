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
using System.Management;

namespace Getter
{
    public partial class Form1 : Form
    {
        static List<string> list = new List<string>
        {
                "Architecture",
                "AssetTag",
                "Availability",
                "Caption",
                "Characteristics",
                "ConfigManagerErrorCode",
                "ConfigManagerUserConfig",
                "CpuStatus",
                "CreationClassName",
                "CurrentClockSpeed",
                "CurrentVoltage",
                "DataWidth",
                "Description",
                "DeviceID",
                "ErrorCleared",
                "ErrorDescription",
                "ExtClock",
                "Family",
                "InstallDate",
                "L2CacheSize",
                "L2CacheSpeed",
                "L3CacheSize",
                "L3CacheSpeed",
                "LastErrorCode",
                "Level",
                "LoadPercentage",
                "Manufacturer",
                "MaxClockSpeed",
                "Name",
                "NumberOfCores",
                "NumberOfEnabledCore",
                "NumberOfLogicalProcessors",
                "OtherFamilyDescription",
                "PartNumber",
                "PNPDeviceID",
                "PowerManagementCapabilities",
                "PowerManagementSupported",
                "ProcessorId",
                "ProcessorType",
                "Revision",
                "Role",
                "SecondLevelAddressTranslationExtensions",
                "SerialNumber",
                "SocketDesignation",
                "Status",
                "StatusInfo",
                "Stepping",
                "SystemCreationClassName",
                "SystemName",
                "ThreadCount",
                "UniqueId",
                "UpgradeMethod",
                "Version",
                "VirtualizationFirmwareEnabled"
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("CPU");
        }
        void CPU_INFO()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("CategoryColumn", "AddressWidth");
            ManagementObjectSearcher obj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj2 in obj.Get())
            {
                dataGridView1.Columns.Add("CategoryColumn", Convert.ToString(obj2["AddressWidth"]));
            }
            foreach (ManagementObject obj2 in obj.Get())
            {
                foreach (string item in list)
                {
                    dataGridView1.Rows.Add(item, Convert.ToString(obj2[item]));
                }
            }
            try
            {
                OpenHardwareMonitor.Hardware.Computer computer = new OpenHardwareMonitor.Hardware.Computer();
                computer.CPUEnabled = true;
                computer.Open();
                foreach (var hardware in computer.Hardware)
                {
                    if (hardware.HardwareType == HardwareType.CPU)
                    {
                        foreach (var sensor in hardware.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature)
                            {
                                dataGridView1.Rows.Add("CPU Temperature", $"{sensor.Value} °C");
                            }
                        }
                    }
                }
                computer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            string selectedProcessor = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor.Equals("CPU", StringComparison.OrdinalIgnoreCase))
            {
                comboBox2.Items.Add("AddressWidth");
                foreach (string item in list)
                {
                    comboBox2.Items.Add(item);
                }
                CPU_INFO();
            }
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            string selectedProcessor = comboBox2.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedProcessor))
            {
                ManagementObjectSearcher obj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject obj2 in obj.Get())
                {
                    MessageBox.Show(selectedProcessor + " : " + Convert.ToString(obj2[selectedProcessor]));
                }
            }
        }
    }
}
