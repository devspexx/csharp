using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CPUE.Classes
{
    class pcinfo_DISC
    {
        public List<string> _loadDiscInfo() 
        {
            List<string> drives = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives) {
                string DriveString = string.Empty;
                DriveString = d.Name + ";";
                if (d.IsReady == true) {
                    DriveString +=
                        d.DriveFormat + ";" +
                        d.AvailableFreeSpace + ";" +
                        d.TotalSize;
                    drives.Add(DriveString);
                    DriveString = string.Empty;
                }
            }

            for (int i = 0; i < allDrives.Length; i++) {
                MessageBox.Show(drives.ToArray()[i]);
            }
            return drives;
        }
    }

}
