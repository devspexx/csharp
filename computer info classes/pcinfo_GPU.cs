using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CPUE.Classes
{
    class pcinfo_GPU
    {
        string gpu_Name = "";
        string gpu_Status = "";
        string gpu_DeviceID = "";
        string gpu_AdapterRAM = "";
        string gpu_AdapterDACType = "";
        string gpu_Monochrome = "";
        string gpu_InstalledDisplayDrivers = "";
        string gpu_DriverVersion = "";
        string gpu_VideoArchitecture = "";
        string gpu_VideoMemoryType = "";
        string gpu_MaxRefreshRate = "";
        string gpu_MinRefreshRate = "";
        string gpu_VideoModeDescription = "";

        public void _loadGpuInfo()
        {
            // https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-videocontroller
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get()) {
                gpu_Name = obj["Name"].ToString();
                gpu_Status = obj["Status"].ToString();
                gpu_DeviceID = obj["DeviceID"].ToString();
                gpu_AdapterRAM = SizeSuffix((long)Convert.ToDouble(obj["AdapterRAM"]));
                gpu_AdapterDACType = obj["AdapterDACType"].ToString();
                gpu_Monochrome = obj["Monochrome"].ToString();
                gpu_InstalledDisplayDrivers = obj["InstalledDisplayDrivers"].ToString();
                gpu_DriverVersion = obj["DriverVersion"].ToString();
                gpu_VideoArchitecture = obj["VideoArchitecture"].ToString();
                gpu_VideoMemoryType = obj["VideoMemoryType"].ToString();
                gpu_MaxRefreshRate = obj["MaxRefreshRate"].ToString();
                gpu_MinRefreshRate = obj["MinRefreshRate"].ToString();
                gpu_VideoModeDescription = obj["VideoModeDescription"].ToString();
            }

            /** 
                Name  -  NVIDIA GeForce GTX 760
                Status  -  OK
                Caption  -  NVIDIA GeForce GTX 760
                DeviceID  -  VideoController1
                AdapterRAM  -  2147483648
                AdapterDACType  -  Integrated RAMDAC
                Monochrome  -  False
                InstalledDisplayDrivers  -  nvd3dumx.dll,nvwgf2umx.dll,nvwgf2umx.dll,nvwgf2umx.dll,nvd3dum,nvwgf2um,nvwgf2um,nvwgf2um
                DriverVersion  -  21.21.13.7306
                VideoProcessor  -  GeForce GTX 760
                VideoArchitecture  -  5
                VideoMemoryType  -  2
            **/

            //MessageBox.Show(
            //    "gpu_Name: " + gpu_Name + "\n" +
            //    "gpu_Status: " + gpu_Status + "\n" +
            //    "gpu_DeviceID: " + gpu_DeviceID + "\n" +
            //    "gpu_AdapterRAM: " + gpu_AdapterRAM + "\n" +
            //    "gpu_AdapterDACType: " + gpu_AdapterDACType + "\n" +
            //    "gpu_Monochrome: " + gpu_Monochrome + "\n" +
            //    "gpu_InstalledDisplayDrivers: " + gpu_InstalledDisplayDrivers + "\n" +
            //    "gpu_DriverVersion: " + gpu_DriverVersion + "\n" +
            //    "gpu_VideoArchitecture: " + gpu_VideoArchitecture + "\n" +
            //    "gpu_VideoMemoryType: " + gpu_VideoMemoryType + "\n" +
            //    "gpu_MaxRefreshRate: " + gpu_MaxRefreshRate + "\n" +
            //    "gpu_MinRefreshRate: " + gpu_MinRefreshRate + "\n" +
            //    "gpu_VideoModeDescription: " + gpu_VideoModeDescription
            //);
        }

        readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        string SizeSuffix(Int64 value) {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
    }
}
