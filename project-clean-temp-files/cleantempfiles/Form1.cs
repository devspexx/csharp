using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cleantempfiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }
        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        private bool isProgressing = false;

        private async void button1_Click(object sender, EventArgs e)
        {
            if (isProgressing) return;
            isProgressing = true;

            string path = "C:/Users/" + Environment.UserName + "/AppData/Local/Temp";
            string path2 = "C:/Windows/Temp";
            string path3 = "C:/temp";

            if (!Directory.Exists(path))
            {
                MessageBox.Show("%TEMP% does not exist:: " + path);
                return;
            }

            List<string> dirs = new List<string>();
            List<string> files = new List<string>();

            foreach (string dir in Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories))
            {
                dirs.Add(dir);
                label1.Text = "found dir: " + dir;
                foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                {
                    label1.Text = "found file: " + file;
                    files.Add(file);
                }
                await Task.Delay(1);
            }
            foreach (string dir in Directory.GetDirectories(path2, "*.*", SearchOption.AllDirectories))
            {
                dirs.Add(dir);
                label1.Text = "found dir: " + dir;
                foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                {
                    label1.Text = "found file: " + file;
                    files.Add(file);
                }
                await Task.Delay(1);
            }
            if (Directory.Exists("C:/temp"))
            {
                foreach (string dir in Directory.GetDirectories(path3, "*.*", SearchOption.AllDirectories))
                {
                    dirs.Add(dir);
                    label1.Text = "found dir: " + dir;
                    foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                    {
                        label1.Text = "found file: " + file;
                        files.Add(file);
                    }
                    await Task.Delay(1);
                }
            }

            await Task.Delay(1000);
            label1.Text = "starting deletion in 3..";
            await Task.Delay(1000);
            label1.Text = "starting deletion in 2..";
            await Task.Delay(1000);
            label1.Text = "starting deletion in 1..";
            await Task.Delay(1000);
            label1.ForeColor = Color.IndianRed;
            progressBar1.Maximum = dirs.Count;
            foreach (string dir in dirs)
            {
                progressBar1.Value++;
                label1.Text = "deleting: " + dir;
                
                try
                {
                    Directory.Delete(dir, true);
                } catch (Exception) { }
                await Task.Delay(1);
                
            }

            // arg index 1 = If this value is an empty string or NULL, all Recycle Bins on all drives will be emptied.
            // arg index 2 = don't show progress reports/confirmation windows
            SHEmptyRecycleBin(Handle, null, RecycleFlags.SHERB_NOCONFIRMATION);

            label1.Text = "deleted dirs: " + dirs.Count + ", files: " + files.Count;

            Application.Restart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = "C:/Users/" + Environment.UserName + "/AppData/Local/Temp";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", label4.Text);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", label3.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", label2.Text);
        }
    }
}
