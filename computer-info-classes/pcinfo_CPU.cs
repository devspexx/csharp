using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUE.Classes
{
    class pcinfo_CPU
    {
        public string cpu_Name = "";
        public string cpu_DeviceID = "";
        public string cpu_Manufacturer = "";
        public string cpu_CurrentClockSpeed = "";
        public string cpu_MaxClockSpeed = "";
        public string cpu_Caption = "";
        public string cpu_NumberOfCores = "";
        public string cpu_NumberOfEnabledCore = "";
        public string cpu_NumberOfLogicalProcessors = "";
        public string cpu_Architecture = "";
        public string cpu_Family = "";
        public string cpu_ProcessorType = "";
        public string cpu_Characteristics = "";
        public string cpu_AddressWidth = "";
        public string cpu_SerialNumber = "";
        public string cpu_ThreadCount = "";
        public string cpu_LoadPercentage = "";
        public string cpu_CurrentVoltage = "";

        public void _loadCpuInfo()
        {
            // https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get()) {
                cpu_Name = obj["Name"].ToString();
                cpu_DeviceID = obj["DeviceID"].ToString();
                cpu_Manufacturer = obj["Manufacturer"].ToString();
                cpu_CurrentClockSpeed = obj["CurrentClockSpeed"].ToString();
                cpu_MaxClockSpeed = obj["MaxClockSpeed"].ToString();
                cpu_Caption = obj["Caption"].ToString();
                cpu_NumberOfCores = obj["NumberOfCores"].ToString();
                cpu_NumberOfEnabledCore = obj["NumberOfEnabledCore"].ToString();
                cpu_NumberOfLogicalProcessors = obj["NumberOfLogicalProcessors"].ToString();
                cpu_Architecture = obj["Architecture"].ToString();
                cpu_Family = obj["Family"].ToString();
                cpu_ProcessorType = obj["ProcessorType"].ToString();
                cpu_Characteristics = obj["Characteristics"].ToString();
                cpu_AddressWidth = obj["AddressWidth"].ToString();
                cpu_SerialNumber = obj["SerialNumber"].ToString();
                cpu_ThreadCount = obj["ThreadCount"].ToString(); 
                cpu_LoadPercentage = obj["LoadPercentage"].ToString();
                cpu_CurrentVoltage = obj["CurrentVoltage"].ToString(); // volts

                /**
                    Name  -  Intel(R) Core(TM) i5-4590 CPU @ 3.30GHz
                    DeviceID  -  CPU0
                    Manufacturer  -  GenuineIntel
                    CurrentClockSpeed  -  3300
                    Caption  -  Intel64 Family 6 Model 60 Stepping 3
                    NumberOfCores  -  4
                    NumberOfEnabledCore  -  4
                    NumberOfLogicalProcessors  -  4
                    Architecture  -  9 https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.architecture?view=netcore-3.1
                    Family  -  205
                    ProcessorType  -  3 https://docs.microsoft.com/en-us/dotnet/api/microsoft.powershell.commands.processortype?view=powershellsdk-1.1.0
                    Characteristics  -  4
                    AddressWidth  -  64
                */
            }

            //MessageBox.Show(
            //    "cpu_Name: " + cpu_Name + "\n" +
            //    "cpu_DeviceID: " + cpu_DeviceID + "\n" +
            //    "cpu_Manufacturer: " + cpu_Manufacturer + "\n" +
            //    "cpu_CurrentClockSpeed: " + cpu_CurrentClockSpeed + "\n" +
            //    "cpu_MaxClockSpeed: " + cpu_MaxClockSpeed + "\n" +
            //    "cpu_Caption: " + cpu_Caption + "\n" +
            //    "cpu_NumberOfCores: " + cpu_NumberOfCores + "\n" +
            //    "cpu_NumberOfEnabledCore: " + cpu_NumberOfEnabledCore + "\n" +
            //    "cpu_NumberOfLogicalProcessors: " + cpu_NumberOfLogicalProcessors + "\n" +
            //    "cpu_Architecture: " + cpu_Architecture + "\n" +
            //    "cpu_Family: " + cpu_Family + "\n" +
            //    "cpu_ProcessorType: " + cpu_ProcessorType + "\n" +
            //    "cpu_Characteristics: " + cpu_Characteristics + "\n" +
            //    "cpu_AddressWidth: " + cpu_AddressWidth + "\n" +
            //    "cpu_SerialNumber: " + cpu_SerialNumber + "\n" +
            //    "cpu_ThreadCount: " + cpu_ThreadCount + "\n" +
            //    "cpu_LoadPercentage: " + cpu_LoadPercentage + "\n" +
            //    "cpu_CurrentVoltage: " + cpu_CurrentVoltage
            //);
        }
    }
}
