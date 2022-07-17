using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUE.Classes
{
    class pcinfo_MONITOR
    {
        public List<string> _loadMonitorInfo()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.screen?redirectedfrom=MSDN&view=netcore-3.1
            List<string> screensall = new List<string>();
            Screen[] allScreens = Screen.AllScreens;
            foreach (var screen in allScreens) {
                string screenString = string.Empty;
                screenString = 
                    screen.DeviceName.Replace("\\", "").Replace(".", "") + ";" +
                    screen.Bounds.Width + "|" + screen.Bounds.Height + ";" +
                    MonitorHelper.DeviceFriendlyName(screen) + ";" +
                    screen.Primary.ToString();
                screensall.Add(screenString);
                screenString = string.Empty;
            }
            //for (int i = 0; i < allScreens.Length; i++)
            //{
            //    MessageBox.Show(screensall.ToArray()[i]);
            //}
            return screensall;
        }
    }
}
