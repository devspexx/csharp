using Sp3eex;
using Sp3eex.Frontend.Forms;
using Spexx.Backend;
using Spexx.Frontend.Forms;
using System.Drawing;
using System.Windows.Forms;
using static Spexx.Backend.Win32Helper;

namespace Spexx.Frontend.UserControls
{
    public partial class DebugWindow : UserControl
    {
        public long app_rendered_frames;
        public int app_runtime = 0;

        public DebugWindow()
        {
            InitializeComponent();
            LOAD();
        }

        public void LOAD()
        {
            if (DesignMode) return;
            
            label6.Text = isEnabled(AppSettings.Functionality.SHOW_BORDERS);
            ConsoleLogging.Log(null, "Checking 'SHOW_BORDERS' constant - " + AppSettings.Functionality.SHOW_BORDERS);

            label23.Text = AppSettings.Themes.GLOBAL.BORDER_SIZE_PX + " PX";
            label24.Text = TextHelper.FromColorToHex(AppSettings.Themes.DARK.BORDER_COLOR_APP_ACTIVE);
            ConsoleLogging.Log(null, "Checking 'BORDER_COLOR_APP_ACTIVE' constant - " + AppSettings.Themes.DARK.BORDER_COLOR_APP_ACTIVE);

            label25.Text = TextHelper.FromColorToHex(AppSettings.Themes.DARK.BORDER_COLOR_APP_NOTACTIVE);
            ConsoleLogging.Log(null, "Checking 'BORDER_COLOR_APP_NOTACTIVE' constant - " + AppSettings.Themes.DARK.BORDER_COLOR_APP_NOTACTIVE);

            label29.Text = isEnabled(AppSettings.Functionality.BORDER_RESIZE_EANBLED);
            ConsoleLogging.Log(null, "Checking 'BORDER_RESIZE_EANBLED' constant - " + AppSettings.Functionality.BORDER_RESIZE_EANBLED);

            label26.Text = isEnabled(AppSettings.Functionality.SHOW_HEADER);
            ConsoleLogging.Log(null, "Checking 'SHOW_HEADER' constant - " + AppSettings.Functionality.SHOW_HEADER);

            label27.Text = TextHelper.FromColorToHex(AppSettings.Themes.DARK.HEADER_BACKGROUND_ACTIVE);
            ConsoleLogging.Log(null, "Checking 'HEADER_BACKGROUND_ACTIVE' constant - " + AppSettings.Themes.DARK.HEADER_BACKGROUND_ACTIVE);

            label28.Text = TextHelper.FromColorToHex(AppSettings.Themes.DARK.HEADER_BACKGROUND_NOTACTIVE);
            ConsoleLogging.Log(null, "Checking 'HEADER_BACKGROUND_NOTACTIVE' constant - " + AppSettings.Themes.DARK.HEADER_BACKGROUND_NOTACTIVE);

            label5.Text = TextHelper.FromColorToHex(AppSettings.Themes.DARK.HEADER_BUTTONS_BACKGROUND_ACTIVE);
            ConsoleLogging.Log(null, "Checking 'HEADER_BUTTONS_BACKGROUND_ACTIVE' constant - " + AppSettings.Themes.DARK.HEADER_BUTTONS_BACKGROUND_ACTIVE);

            label8.Text = TextHelper.FromColorToHex(AppSettings.Themes.DARK.HEADER_BUTTONS_BACKGROUND_NOTACTIVE);
            ConsoleLogging.Log(null, "Checking 'HEADER_BUTTONS_BACKGROUND_NOTACTIVE' constant - " + AppSettings.Themes.DARK.HEADER_BUTTONS_BACKGROUND_NOTACTIVE);

            label30.Text = isEnabled(AppSettings.Functionality.DISABLE_ALT_F4_PRESS);
            ConsoleLogging.Log(null, "Checking 'DISABLE_ALT_F4_PRESS' constant - " + AppSettings.Functionality.DISABLE_ALT_F4_PRESS);

            label21.Text = isEnabled(AppSettings.Functionality.ENABLE_DRAGGING);
            ConsoleLogging.Log(null, "Checking 'ENABLE_DRAGGING' constant - " + AppSettings.Functionality.ENABLE_DRAGGING);

            label19.Text = App.app.MaximumSize.Width + "x" + App.app.MaximumSize.Height;
            ConsoleLogging.Log(null, "Getting App Max Size; " + App.app.MaximumSize.Width + "x" + App.app.MaximumSize.Height);

            label32.Text = App.app.MinimumSize.Width + "x" + App.app.MinimumSize.Height;
            ConsoleLogging.Log(null, "Getting App Min Size; " + App.app.MinimumSize.Width + "x" + App.app.MinimumSize.Height);

            label45.Text = Program.EXCEPTIONS.count.ToString();
            ConsoleLogging.Log(null, "Setting Exceptions count to 0");

        }

        // helper for true/false
        private string isEnabled(bool enabled) => enabled ? "TRUE" : "FALSE";

        // helper for app runtime calc
        private string getAppRuntimeCalc()
        {
            string format = "{time} {time-stamp}";

            if (app_runtime < 60)
                return format.Replace("{time}", app_runtime.ToString()).Replace("{time-stamp}", "sec(s)");
            if (app_runtime < 3600)
                return format.Replace("{time}", (app_runtime / 60).ToString()).Replace("{time-stamp}", "min(s)");
            if (app_runtime < 86400)
                return format.Replace("{time}", (app_runtime / 3600).ToString()).Replace("{time-stamp}", "hour(s)");
            if (app_runtime >= 86400)
                return format.Replace("{time}", (app_runtime / 86400).ToString()).Replace("{time-stamp}", "day(s)");
            return "null";
        }

        // methods for live stats, external calls
        public void updateRenderedFrames()
        {
            app_rendered_frames += 1;
            label41.Text = app_rendered_frames.ToString();
        }
        public void updateCurrentSize(Size size)
        {
            label40.Text = size.Width + "x" + size.Height;
        }
        public void updateFocusState(string state)
        {
            label34.Text = state;
        }
        public void updateWindowState(FormWindowState formWindowState)
        {
            string state = string.Empty;
            switch (formWindowState)
            {
                case FormWindowState.Maximized:
                    state = "MAXIMIZED";
                    break;
                case FormWindowState.Normal:
                    state = "NORMAL";
                    break;
                case FormWindowState.Minimized:
                    state = "MINIMIZED";
                    break;
                default:
                    break;
            }
            label39.Text = state;
        }

        // events
        private void UISettings_Resize(object sender, System.EventArgs e)
        {
            ControlsUtils.CenterControlInPanel(panel3, panel4.Size);
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            App.app.panel_contentrender.Controls.Remove(this);
            App.app.IS_DEBUGGING = false;
        }

        private void runtime_timer_Tick(object sender, System.EventArgs e)
        {
            app_runtime++;
            label43.Text = getAppRuntimeCalc();
            label45.Text = Program.EXCEPTIONS.count.ToString();
        }
    }
}
