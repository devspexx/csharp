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
        bool isInProcess = false;
        int refIntProgress = 0;

        List<string> dir_combinedall = new List<string>();
        List<string> files1 = new List<string>();
        List<string> files2 = new List<string>();
        List<string> files3 = new List<string>();
        List<string> files4 = new List<string>();
        List<string> dir1 = new List<string>();
        List<string> dir2 = new List<string>();
        List<string> dir3 = new List<string>();
        List<string> dir4 = new List<string>();

        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_dir2.Text = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp";
        }

        // async to prevent application unexpected freezing
        private async void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (isInProcess) return;
            isInProcess = true;

            foreach (string dir in Directory.GetDirectories(label_dir1.Text))
            {
                dir1.Add(dir);
                foreach (string file in Directory.GetFiles(dir))
                {
                    files1.Add(file);
                    label2.Text = file;
                }
                await Task.Delay(1);
                label2.Text = dir;
                dir_combinedall.Add(dir);
            }
            label1.Text = dir1.Count + "D" + files1.Count + "F";
            
            foreach (string dir in Directory.GetDirectories(label_dir2.Text))
            {
                dir2.Add(dir);
                foreach (string file in Directory.GetFiles(dir))
                {
                    files2.Add(file);
                    label2.Text = file;
                }
                await Task.Delay(1);
                label2.Text = dir;
                dir_combinedall.Add(dir);
            }
            label3.Text = dir2.Count + "D" + files2.Count + "F";
            
            if (Directory.Exists(label_dir3_opt.Text))
            {
                foreach (string dir in Directory.GetDirectories(label_dir3_opt.Text))
                {
                    dir3.Add(dir);
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        files3.Add(file);
                        label2.Text = file;
                    }
                    await Task.Delay(1);
                    label2.Text = dir;
                    dir_combinedall.Add(dir);
                }
                label4.Text = dir3.Count + "D" + files3.Count + "F";
            }
                
            foreach (string dir in Directory.GetDirectories(label_dir4.Text))
            {
                dir4.Add(dir);
                foreach (string file in Directory.GetFiles(dir))
                {
                    files4.Add(file);
                    label2.Text = file;
                }
                await Task.Delay(1);
                label2.Text = dir;
                dir_combinedall.Add(dir);
            }
            label5.Text = dir4.Count + "D" + files4.Count + "F";

            for (int i = 5; i > 0; i--)
            {
                label2.Text = "scanning complete. starting deletion in " + i + " second(s)..";
                await Task.Delay(1000);
            }

            bunifuProgressBar1.MaximumValue = dir_combinedall.Count;
            bunifuProgressBar1.Value = 0;
            timer1.Enabled = true;

            foreach (string dir in dir_combinedall)
            {
                try
                {
                    Directory.Delete(dir, true);
                    label2.Text = "removed: " + dir;
                } catch {
                    label2.Text = "error: " + dir;
                }
                refIntProgress++;
                await Task.Delay(1);
            }

            // remove files in recycle bin
            SHEmptyRecycleBin(Handle, null, RecycleFlags.SHERB_NOCONFIRMATION);

            timer1.Enabled = false;
            await Task.Delay(1000);

            bunifuButton2.Enabled = true;
            bunifuProgressBar1.ValueByTransition = 0;
            label2.Text = "COMPLETED! Deleted " + dir_combinedall.Count + " directories.";
            dir_combinedall.Clear();
            dir1.Clear();
            dir2.Clear();
            dir3.Clear();
            dir4.Clear();
            files1.Clear();
            files2.Clear();
            files3.Clear();
            files4.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bunifuProgressBar1.Value != bunifuProgressBar1.MaximumValue)
                bunifuProgressBar1.ValueByTransition += refIntProgress - bunifuProgressBar1.Value; 
        }
        private void label_recyclebin_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "shell:RecycleBinFolder");
        }
        private void label_dir4_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", label_dir4.Text);
        }
        private void label_dir3_opt_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(label_dir3_opt.Text))
                Process.Start("explorer.exe", label_dir3_opt.Text);
        }
        private void label_dir2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", label_dir2.Text);
        }
        private void label_dir1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", label_dir1.Text);
        }
    }
}
