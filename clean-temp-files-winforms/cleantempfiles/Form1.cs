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

        List<string> files_combinedall = new List<string>();
        List<string> dir_combinedall = new List<string>();

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
            label_dir2.Text = label_dir2.Text.Replace("USERNAME", Environment.UserName);
        }

        enum labelstatus
        {
            FILE = 0,
            DIR = 1
        }
        private void UpdateLabelStatus(string path, labelstatus TYPE, string prefix = "") {
            string toRename = prefix + (TYPE == labelstatus.FILE ?
                "file: " + path : "dir: " + path);
            label2.Text = toRename.Length > 65 ? toRename.Substring(0, 65) + "..." : toRename;
        }

        // async to prevent application unexpected freezing
        private async void bunifuButton2_Click(object sender, EventArgs e)
        {
            if (isInProcess) return;
            isInProcess = true;

            string[] directories =
            {
                label_dir1.Text,
                label_dir2.Text,
                label_dir3_opt.Text,
                label_dir4.Text
            };
            Control[] directories_labelReference =
            {
                label1,
                label3,
                label4,
                label5
            };
            List<string> countReference = new List<string>();
            int countReferenceInt = 0;

            foreach (string directory in directories)
            {
                UpdateLabelStatus(directory, labelstatus.DIR, "x: ");

                if (Directory.Exists(directory))
                {
                    List<string> directory_files = Directory.GetFiles(directory).ToList<string>();
                    List<string> directory_subdirectories = Directory.GetDirectories(directory, "*.*", 
                        SearchOption.AllDirectories).ToList<string>();

                    // add optional items to global ref lists
                    files_combinedall.AddRange(directory_files);
                    dir_combinedall.AddRange(directory_subdirectories);

                    foreach (string subdir in directory_subdirectories)
                    {
                        List<string> subdirectory_files = Directory.GetFiles(subdir).ToList<string>();
                        files_combinedall.AddRange(subdirectory_files);

                        // prevent app freezing
                        UpdateLabelStatus(subdir, labelstatus.DIR);
                        await Task.Delay(2);
                    }

                    // set for-reference values
                    countReference.Add(directory_subdirectories.Count + "D" +
                            directory_files.Count + "F");
                }

                directories_labelReference[countReferenceInt].Text = "found " + countReference[countReferenceInt];

                await Task.Delay(1000);
                countReferenceInt++;
            }

            // deletion countdown
            for (int i = 5; i > 0; i--)
            {
                label2.Text = "scanning complete. starting deletion in " + i + " second(s)..";
                await Task.Delay(1000);
            }

            // re(set) values
            bunifuProgressBar1.MaximumValue = files_combinedall.Count + dir_combinedall.Count;
            bunifuProgressBar1.Value = 0;
            timer1.Enabled = true;

            foreach (string file in files_combinedall)
            {
                try
                {
                    File.Delete(file);
                    UpdateLabelStatus(file, labelstatus.FILE, "removed ");
                } catch {
                    UpdateLabelStatus(file, labelstatus.FILE, "error ");
                }
                refIntProgress++;
                await Task.Delay(20);
            }
            foreach (string dir in dir_combinedall)
            {
                try
                {
                    Directory.Delete(dir, true);
                    UpdateLabelStatus(dir, labelstatus.DIR, "removed ");
                }
                catch
                {
                    UpdateLabelStatus(dir, labelstatus.DIR, "error ");
                }
                refIntProgress++;
                await Task.Delay(20);
            }

            // remove files in recycle bin
            SHEmptyRecycleBin(Handle, null, RecycleFlags.SHERB_NOCONFIRMATION | RecycleFlags.SHERB_NOPROGRESSUI);

            timer1.Enabled = false;
            await Task.Delay(1000);

            label2.Text = "COMPLETED! Deleted " + files_combinedall.Count + "F and " + 
                dir_combinedall.Count + "D";

            files_combinedall.Clear();
            dir_combinedall.Clear();
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
