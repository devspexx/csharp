using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUE.Classes
{
    class pcinfo_BASIC
    {
        DateTime basic_localtime = new DateTime();
        string basic_computer_name = "";
        string basic_computer_username = "";
        string basic_computer_is64bit = "";
        string basic_computer_windows_version = ""; 
        string basic_computer_windows_build = "";

        public void _loadBasicInfo()
        {
            basic_computer_name = Environment.MachineName;
            basic_computer_username = Environment.UserName;
            basic_computer_is64bit = Environment.Is64BitOperatingSystem == true ? "64bit" : "32bit";
            basic_computer_windows_version = Environment.OSVersion.VersionString;
            basic_computer_windows_build = Environment.OSVersion.Version.Build.ToString();
            basic_localtime = new Microsoft.VisualBasic.Devices.Clock().LocalTime;
        }
    }
}
