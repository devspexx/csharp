using QuickFix.UI.Forms;
using System.Windows.Forms;

namespace BorderlessForm
{
    public partial class MainForm : FormBase
    {
        private FormWindowState previousWindowState;
        public MainForm() {
            InitializeComponent();
            //MinimizeLabel.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) WindowState = FormWindowState.Minimized; };
            //MaximizeLabel.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) ToggleMaximize(); };
            //previousWindowState = MinMaxState;
            //CloseLabel.MouseClick += (s, e) => Close(e);
        }
        
        void Close(MouseEventArgs e)  {
            if (e.Button == MouseButtons.Left) Close();
        }

        private FormWindowState ToggleMaximize() {
            return WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }
    }
}
