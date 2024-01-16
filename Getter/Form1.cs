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
        static List<string> list2 = new List<string>
        {
            "AdapterCompatibility", "AdapterDACType", "AdapterRAM", 
            "Availability", "CapabilityDescriptions", "Caption", 
            "ColorTableEntries", "ConfigManagerErrorCode", "ConfigManagerUserConfig", 
            "CreationClassName", "CurrentBitsPerPixel", "CurrentHorizontalResolution", 
            "CurrentNumberOfColors", "CurrentNumberOfColumns", "CurrentNumberOfRows", 
            "CurrentRefreshRate", "CurrentScanMode", "CurrentVerticalResolution", 
            "Description", "DeviceID", "DeviceSpecificPens", "DitherType", 
            "DriverDate", "DriverVersion", "ErrorCleared", "ErrorDescription", 
            "ICMIntent", "ICMMethod", "InfFilename", "InfSection", "InstallDate", 
            "InstalledDisplayDrivers", "LastErrorCode", "MaxMemorySupported", 
            "MaxNumberControlled", "MaxRefreshRate", "MinRefreshRate", "Monochrome",
            "Name", "NumberOfColorPlanes", "NumberOfVideoPages", "PNPDeviceID", 
            "PowerManagementCapabilities", "PowerManagementSupported", "ProtocolSupported", 
            "ReservedSystemPaletteEntries", "SpecificationVersion", "Status", "StatusInfo", 
            "SystemCreationClassName", "SystemName", "SystemPaletteEntries", "TimeOfLastReset", 
            "VideoArchitecture", "VideoMemoryType", "VideoMode", "VideoModeDescription", 
            "VideoProcessor"
        };
        static List<string> list3 = new List<string>
        {
            "BankLabel", "Capacity", "Caption", "ConfiguredClockSpeed", 
            "ConfiguredVoltage", "CreationClassName", "DataWidth", "Description", 
            "DeviceLocator", "FormFactor", "HotSwappable", "InstallDate", 
            "InterleaveDataDepth", "InterleavePosition", "Manufacturer", "MaxVoltage",
            "MemoryType", "MinVoltage", "Model", "Name", "OtherIdentifyingInfo", 
            "PartNumber", "PositionInRow", "PoweredOn", "Removable", "Replaceable", 
            "SerialNumber", "SKU", "SMBIOSMemoryType", "Speed", "Status", "Tag", 
            "TotalWidth", "TypeDetail", "Version"
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("CPU");
            comboBox1.Items.Add("GPU");
            comboBox1.Items.Add("RAM");
            comboBox1.Items.Add("Drivers");
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
        }
        void GPU_INFO()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("CategoryColumn", "GPU");
            dataGridView1.Columns.Add("ValueColumn", "Value");
            ManagementObjectSearcher obj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");

            foreach (ManagementObject obj2 in obj.Get())
            {
                foreach (PropertyData property in obj2.Properties)
                {
                    dataGridView1.Rows.Add(property.Name, Convert.ToString(property.Value));
                }
            }
        }
        void RAM_INFO()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add("CategoryColumn", "RAM");
            dataGridView1.Columns.Add("ValueColumn", "Value");

            ManagementObjectSearcher obj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");

            foreach (ManagementObject obj2 in obj.Get())
            {
                foreach (PropertyData property in obj2.Properties)
                {
                    dataGridView1.Rows.Add(property.Name, Convert.ToString(property.Value));
                }
            }

        }
        private void button_start_Click(object sender, EventArgs e)
        {
            string selectedProcessor = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor.Equals("CPU", StringComparison.OrdinalIgnoreCase))
            {
                Form2 form2 = new Form2();
                form2.Show();
                comboBox2.Items.Add("AddressWidth");
                foreach (string item in list)
                {
                    comboBox2.Items.Add(item);
                }
                CPU_INFO();
            }
            else if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor.Equals("GPU", StringComparison.OrdinalIgnoreCase))
            {
                Form3 form3 = new Form3();
                form3.Show();
                comboBox2.Items.Add("AcceleratorCapabilities");
                foreach (string item in list2)
                {
                    comboBox2.Items.Add(item);
                }
                GPU_INFO();
            }
            else if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor.Equals("RAM", StringComparison.OrdinalIgnoreCase))
            {
                comboBox2.Items.Add("Attributes");
                foreach (string item in list3)
                {
                    comboBox2.Items.Add(item);
                }
                RAM_INFO();
            }
            else if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor.Equals("Drivers", StringComparison.OrdinalIgnoreCase))
            {
                Form4 form4 = new Form4();
                form4.Show();
            }
        }

        private void button_end_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            string selectedProcessor = comboBox2.SelectedItem?.ToString();
            string selectedProcessor2 = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor2.Equals("CPU", StringComparison.OrdinalIgnoreCase))
            {
                ManagementObjectSearcher obj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject obj2 in obj.Get())
                {
                    MessageBox.Show(selectedProcessor + " : " + Convert.ToString(obj2[selectedProcessor]));
                }
            }
            else if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor2.Equals("GPU", StringComparison.OrdinalIgnoreCase))
            {
                ManagementObjectSearcher obj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject obj2 in obj.Get())
                {
                    MessageBox.Show(selectedProcessor + " : " + Convert.ToString(obj2[selectedProcessor]));
                }
            }
            else if (!string.IsNullOrEmpty(selectedProcessor) && selectedProcessor2.Equals("RAM", StringComparison.OrdinalIgnoreCase))
            {
                ManagementObjectSearcher obj = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
                foreach (ManagementObject obj2 in obj.Get())
                {
                    MessageBox.Show(selectedProcessor + " : " + Convert.ToString(obj2[selectedProcessor]));
                }
            }
        }

        private void button_system_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}
