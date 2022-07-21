// with Registry
// using Microsoft.Win32;
string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
{
    foreach (string skName in rk.GetSubKeyNames())
    {
        using (RegistryKey sk = rk.OpenSubKey(skName))
        {
            try
            {
                listBox1.Items.Add(sk.GetValue("DisplayName"));
            }
            catch (Exception ex) { 

            }
        }
    }
}

// with WMI 
// using System.Management;
ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
foreach (ManagementObject mo in mos.Get())
{
    Console.WriteLine(mo["Name"]);
}
