using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Threading.Tasks;

namespace CPUE.Classes
{
    public class ComputerAllInfo
    {
        public static string computer_name = string.Empty;
        public static string computer_username = string.Empty;
        public static bool computer_is64bit = false;
        public static string computer_windows_version = string.Empty;
        public static int computer_windows_build = 0;
        public static ulong computer_ram_max_megabytes = 0;
        public static ulong computer_ram_rightnow_megabytes = 0;
        public static ulong computer_ram_virtual_max_megabytes = 0;
        public static long computer_disc_max_megabytes = 0;
        public static long computer_disc_rightnow_megabytes = 0;

        public static int computer_gpu_temparature = 0;
        public static int computer_cpu_temparature = 0;

        public static int computer_cpu_usage_percent = 0;

        public static string computer_gpu_name = string.Empty;
        public static string computer_gpu_deviceID = string.Empty;
        public static string computer_gpu_adapterRam = string.Empty;
        public static string computer_gpu_VideoArchitecture = string.Empty;

        public static Size computer_screen_size = new Size();
        public static DateTime computer_time_local = new DateTime();

        public static void LoadVariables() {
            computer_screen_size = new Microsoft.VisualBasic.Devices.Computer().Screen.Bounds.Size;
            computer_time_local = new Microsoft.VisualBasic.Devices.Clock().LocalTime;

            // COMPUTER BASIC
            computer_name = Environment.MachineName;
            computer_username = Environment.UserName;
            computer_is64bit = Environment.Is64BitOperatingSystem;
            computer_windows_version = Environment.OSVersion.VersionString;
            computer_windows_build = Environment.OSVersion.Version.Build;
            
            // RAM
            computer_ram_max_megabytes = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1000000;
            computer_ram_rightnow_megabytes = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1000000;
            computer_ram_virtual_max_megabytes = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailableVirtualMemory / 1000000;

            // DISC
            DriveInfo driveInfo = new DriveInfo("C:\\Windows");
            computer_disc_rightnow_megabytes = driveInfo.AvailableFreeSpace / 1000000;
            computer_disc_max_megabytes = driveInfo.TotalSize / 1000000;

            // GPU
            ManagementObjectSearcher objvide = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in objvide.Get()) {
                computer_gpu_name = obj["Name"].ToString();
                computer_gpu_deviceID = obj["DeviceID"].ToString();
                computer_gpu_adapterRam = obj["AdapterRAM"].ToString();
                computer_gpu_VideoArchitecture = obj["VideoArchitecture"].ToString();
            }

            // CPU (https://docs.microsoft.com/sl-si/windows/win32/cimwin32prov/win32-processor?redirectedfrom=MSDN)
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            computer_cpu_usage_percent = Convert.ToInt32(cpuCounter.NextValue());
        }
    }
}
