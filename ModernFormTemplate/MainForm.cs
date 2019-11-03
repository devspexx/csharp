using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static DropshadowBorderlessWinForm.FormShadow;

namespace BorderlessForm
{
    public partial class MainForm : FormBase
    {
        private FormWindowState previousWindowState;

        public MainForm()
        {
            InitializeComponent();
            //Activated += MainForm_Activated;
            Deactivate += MainForm_Deactivate;

            //foreach (var control in new[] { SystemLabel, MinimizeLabel, MaximizeLabel, CloseLabel })
            //{
            //    control.MouseEnter += (s, e) => SetLabelColors((Control)s, MouseState.Hover);
            //    control.MouseLeave += (s, e) => SetLabelColors((Control)s, MouseState.Normal);
            //    control.MouseDown += (s, e) => SetLabelColors((Control)s, MouseState.Down);
            //}

            panel17.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOPLEFT);
            panel16.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOPRIGHT);
            panel15.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOMLEFT);
            panel12.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOMRIGHT);

            TopBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOP);
            LeftBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTLEFT);
            RightBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTRIGHT);
            BottomBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOM);

            //TopBorderPanel.BackColor = Color.FromArgb(32, 32, 33, 100);
            //LeftBorderPanel.BackColor = Color.FromArgb(32, 32, 33, 100);
            //RightBorderPanel.BackColor = Color.FromArgb(32, 32, 33, 100);
            //BottomBorderPanel.BackColor = Color.FromArgb(32, 32, 33, 100);


            //SystemLabel.MouseDown += SystemLabel_MouseDown;
            //SystemLabel.MouseUp += SystemLabel_MouseUp;

            panel1.MouseDown += TitleLabel_MouseDown;
            bunifuImageButton2.MouseDown += TitleLabel_MouseDown;
            panel3.MouseDown += TitleLabel_MouseDown;
            panel2.MouseDown += TitleLabel_MouseDown;

            //panel1.MouseUp += (s, e) => { ShowSystemMenu(MouseButtons); };
            //TextChanged += (s, e) => panel1.Text = Text;

            //MinimizeLabel.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) WindowState = FormWindowState.Minimized; };
            //MaximizeLabel.MouseClick += (s, e) => { if (e.Button == MouseButtons.Left) ToggleMaximize(); };
            previousWindowState = MinMaxState;
            SizeChanged += MainForm_SizeChanged;
            //CloseLabel.MouseClick += (s, e) => Close(e);
            panel11.BackColor = Color.FromArgb(32, 34, 37); 
            panel14.BackColor = Color.FromArgb(32, 34, 37);
            panel13.BackColor = Color.FromArgb(32, 34, 37);
        }

        private DateTime systemClickTime = DateTime.MinValue;
        private DateTime systemMenuCloseTime = DateTime.MinValue;


        void Close(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Close();
        }

        void DecorationMouseDown(MouseEventArgs e, HitTestValues h)
        {
            if (e.Button == MouseButtons.Left) DecorationMouseDown(h);
        }

        void MainForm_Deactivate(object sender, EventArgs e) {

        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //var maximized = MinMaxState == FormWindowState.Maximized;
            //MaximizeLabel.Text = maximized ? "2" : "1";

            //var panels = new[] { /*TopLeftCornerPanel, TopRightCornerPanel, BottomLeftCornerPanel, BottomRightCornerPanel*/
            //    TopBorderPanel, LeftBorderPanel, RightBorderPanel, BottomBorderPanel };

            //foreach (var panel in panels)
            //{
            //    panel.Visible = !maximized;
            //}

            if (previousWindowState != MinMaxState)
            {
                //if (maximized)
                //{
                //    //SystemLabel.Left = 0;
                //    //SystemLabel.Top = 0;
                //    CloseLabel.Left += RightBorderPanel.Width;
                //    CloseLabel.Top = 0;
                //    MaximizeLabel.Left += RightBorderPanel.Width;
                //    MaximizeLabel.Top = 0;
                //    MinimizeLabel.Left += RightBorderPanel.Width;
                //    MinimizeLabel.Top = 0;
                //}
                //else if (previousWindowState == FormWindowState.Maximized)
                //{
                //    SystemLabel.Left = LeftBorderPanel.Width;
                //    SystemLabel.Top = TopBorderPanel.Height;
                //    CloseLabel.Left -= RightBorderPanel.Width;
                //    CloseLabel.Top = TopBorderPanel.Height;
                //    MaximizeLabel.Left -= RightBorderPanel.Width;
                //    MaximizeLabel.Top = TopBorderPanel.Height;
                //    MinimizeLabel.Left -= RightBorderPanel.Width;
                //    MinimizeLabel.Top = TopBorderPanel.Height;
                //}

                previousWindowState = MinMaxState;
            }
        }

        private FormWindowState ToggleMaximize()
        {
            return WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private DateTime titleClickTime = DateTime.MinValue;
        private Point titleClickPosition = Point.Empty;

        void TitleLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var clickTime = (DateTime.Now - titleClickTime).TotalMilliseconds;
                if (clickTime < SystemInformation.DoubleClickTime && e.Location == titleClickPosition)
                    ToggleMaximize();
                else
                {
                    titleClickTime = DateTime.Now;
                    titleClickPosition = e.Location;
                    DecorationMouseDown(HitTestValues.HTCAPTION);
                }
            }
        }

        private void bunifuImageButton1_MouseHover(object sender, EventArgs e)
        {
            bunifuImageButton1.BackColor = Color.FromArgb(240, 71, 71);

        }

        private void bunifuImageButton1_MouseClick(object sender, MouseEventArgs e) {
            if (bunifuImageButton1.ClientRectangle.Contains(e.Location)) {
                Close(e);
            }
        }

        private void bunifuImageButton1_MouseLeave(object sender, EventArgs e) {
            bunifuImageButton1.BackColor = Color.FromArgb(32, 34, 37); // default
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e) {
            
        }

        private void bunifuImageButton2_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
            {
                var clickTime = (DateTime.Now - systemClickTime).TotalMilliseconds;
                if (clickTime < SystemInformation.DoubleClickTime)
                    Close();
                else
                {
                    systemClickTime = DateTime.Now;
                    if ((systemClickTime - systemMenuCloseTime).TotalMilliseconds > 200)
                    {
                        //SetLabelColors(SystemLabel, MouseState.Normal);
                        ShowSystemMenu(MouseButtons, PointToScreen(new Point(8, 32)));
                        systemMenuCloseTime = DateTime.Now;
                    }
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            
        }
    }
}
