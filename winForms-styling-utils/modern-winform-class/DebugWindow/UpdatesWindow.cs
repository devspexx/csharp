using Sp3eex;
using Sp3eex.Frontend.Forms;
using Spexx.Backend;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Spexx.Frontend.UserControls
{
    public partial class UpdatesWindow : UserControl
    {
        public UpdatesWindow()
        {
            InitializeComponent();

            if (DesignMode) return;
            label26.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            label27.Text = Win32Helper.Files.getExeLastCompiledDateTimeString();
            label3.Text += Program.PASSED_PARAMS.DEBUG_MODE ? " CTRL+D" : "";
        }

        private void label11_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Process.Start(label11.Text);
        }
        private void label9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Process.Start(label9.Text);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            App.app.panel_contentrender.Controls.Remove(this);
        }

        private void UpdatesWindow_Resize(object sender, EventArgs e)
        {
            ControlsUtils.CenterControlInPanel(panel3, panel4.Size);
        }

        private void label13_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Process.Start(label13.Text);
        }

        private void label17_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Process.Start(label17.Text);
        }
    }
}
