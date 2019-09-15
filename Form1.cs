using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickFix
{
    public partial class Form1 : Form
    {
        public static Form1 form1;
        public Form1()
        {
            InitializeComponent();
            form1 = this;
        }

        // Main form instance, it can be accessed from other classes
        public static Form1 Instance() {
            return form1;
        }

        bool closing = false;
        readonly int free_space = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;

        private async void Form1_Load(object sender, EventArgs e) {
            FormBorderStyle = FormBorderStyle.Sizable;
            FormBorderStyle = FormBorderStyle.None;

            // Set the form maximum size which equals screen size
            MaximumSize = new Size(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y);

            await Task.Delay(300);
            bunifuTransition1.ShowSync(loadingScreen1);
            await Task.Delay(200);

            await loadingScreen1.StartAnimation();

            await Task.Delay(100);
            bunifuTransition1.HideSync(loadingScreen1);
            bunifuTransition1.ShowSync(accountSwitch1);
        }

        private void Form1_Activated(object sender, EventArgs e) {
            BackColor = Color.FromArgb(25, 25, 26);
        }

        private void Form1_Deactivate(object sender, EventArgs e) {
            BackColor = Color.FromArgb(31, 31, 31);
        }

        private async void BunifuImageButton3_Click(object sender, EventArgs e) {
            await Task.Delay(120);
            WindowState = FormWindowState.Minimized;
        }

        private async void BunifuImageButton2_Click(object sender, EventArgs e) {
            await CloseApp();
        }

        // Close the app
        public async Task CloseApp() {
            if (!closing) {
                closing = true;

                // Animation effects
                bunifuTransition1.HideSync(panel2);
                bunifuTransition1.HideSync(loadingScreen1);
                bunifuTransition1.HideSync(accountSwitch1);
                await Task.Delay(500);

                // Hiding the form, preventing the user from interacting with it, focusing it
                Visible = false;
                ShowInTaskbar = false;
                // Saving data (uploading unsaved and remaining data to the database)

                Environment.Exit(0);
            } else {
                // Force shutdown
                return;
            }
        }

        private void Form1_Move(object sender, EventArgs e) {
            // Preventing the user from moving the form under the taskbar
            if (Cursor.Position.Y > Screen.PrimaryScreen.WorkingArea.Height) {
                Cursor.Position = new Point(Cursor.Position.X, Screen.PrimaryScreen.Bounds.Height - free_space);
            }
        }

        private void Form1_Resize(object sender, EventArgs e) {
            accountSwitch1.Left = (panel1.Width - accountSwitch1.Width) / 2;
            accountSwitch1.Location = new Point(accountSwitch1.Location.X, panel1.Height / 4);
            if (loadingScreen1.Visible) {
                loadingScreen1.Left = (panel1.Width - loadingScreen1.Width) / 2;
                loadingScreen1.Location = new Point(loadingScreen1.Location.X, panel1.Height / 4);
            }
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.TaskManagerClosing) {
                e.Cancel = true;
                await CloseApp();
            } else if (e.CloseReason == CloseReason.WindowsShutDown) {
                e.Cancel = false;
                await CloseApp();
            }
        }
    }
}
