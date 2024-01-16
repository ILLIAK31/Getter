using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Getter
{
    public partial class Form4 : MetroFramework.Forms.MetroForm
    {
        public Form4()
        {
            InitializeComponent();
            Drivers_INFO();
            Drivers_INFO2();
        }
        void Drivers_INFO()
        {
            try
            {
                // Create a new process to run the driverquery command
                Process process = new Process();
                process.StartInfo.FileName = "driverquery";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // Start the process
                process.Start();

                // Read the output of the command
                string output = process.StandardOutput.ReadToEnd();

                // Wait for the process to finish
                process.WaitForExit();

                // Display the output in a TextBox (you can use any control of your choice)
                textBox1.Text = output;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void Drivers_INFO2()
        {
            try
            {
                // Connect to the WMI service
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPSignedDriver");

                // Retrieve the collection of drivers
                ManagementObjectCollection collection = searcher.Get();

                // Create a DataTable to store driver information
                DataTable driverTable = new DataTable();
                driverTable.Columns.Add("Driver Name");
                driverTable.Columns.Add("Description");
                driverTable.Columns.Add("Manufacturer");
                driverTable.Columns.Add("Driver Version");
                driverTable.Columns.Add("Signing Status");

                // Iterate through the collection and add rows to the DataTable
                foreach (ManagementObject obj in collection)
                {
                    driverTable.Rows.Add(
                        obj["DeviceName"],
                        obj["Description"],
                        obj["Manufacturer"],
                        obj["DriverVersion"],
                        obj["Signer"]
                    );
                }

                // Bind the DataTable to the DataGridView
                dataGridView2.DataSource = driverTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
