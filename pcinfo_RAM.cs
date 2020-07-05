using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CPUE.Classes
{
    class pcinfo_RAM
    {
        string ram_NameID = "";
        string ram_Capacity = "";
        string ram_ConfiguredVoltage = "";
        string ram_MaxVoltage = "";
        string ram_MemoryType = "";
        string ram_MinVoltage = "";
        string ram_SerialNumber = "";
        string ram_SMBIOSMemoryType = "";
        string ram_Speed = "";
        int ram_MaxMB = 0;
        int ram_UsedMB = 0;
        int int_virtualmaxmb = 0;

        public void _loadRamInfo()
        {
            ManagementObjectSearcher myRamObject = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (ManagementObject obj in myRamObject.Get()) {
                ram_NameID = obj["Name"].ToString();
                ram_Capacity = obj["Capacity"].ToString();
                ram_ConfiguredVoltage = obj["ConfiguredVoltage"].ToString();
                ram_MaxVoltage = obj["MaxVoltage"].ToString();
                ram_MemoryType = obj["MemoryType"].ToString();
                ram_MinVoltage = obj["MinVoltage"].ToString();
                ram_SerialNumber = obj["SerialNumber"].ToString();
                ram_SMBIOSMemoryType = obj["SMBIOSMemoryType"].ToString();
                ram_Speed = obj["Speed"].ToString();
            }

            ulong ram_max = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1000000;
            ulong ram_used = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1000000;
            ulong ram_virtual = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailableVirtualMemory / 1000000;
            ram_MaxMB = (int) ram_max;
            ram_UsedMB = (int) ram_max - (int) ram_used;
            int_virtualmaxmb = (int) ram_virtual;

            //MessageBox.Show(
            //    "ram_NameID: " + ram_NameID + "\n" +
            //    "ram_Capacity: " + ram_Capacity + "\n" +
            //    "ram_ConfiguredVoltage: " + ram_ConfiguredVoltage + "\n" +
            //    "ram_MaxVoltage: " + ram_MaxVoltage + "\n" +
            //    "ram_MemoryType: " + ram_MemoryType + "\n" +
            //    "ram_MinVoltage: " + ram_MinVoltage + "\n" +
            //    "ram_SerialNumber: " + ram_SerialNumber + "\n" +
            //    "ram_SMBIOSMemoryType: " + ram_SMBIOSMemoryType + "\n" +
            //    "ram_Speed: " + ram_Speed + "\n" +
            //    "ram_MaxMB: " + ram_MaxMB + "\n" +
            //    "ram_UsedMB: " + ram_UsedMB
            //);
        }
    }
}
