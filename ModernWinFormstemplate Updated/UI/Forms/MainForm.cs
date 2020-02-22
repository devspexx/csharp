using ShadowDemo;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BorderlessForm
{
    public partial class MainForm : Form
    {
        public void DecorationMouseDown(HitTestValues hit, Point p) {
            NativeMethods.ReleaseCapture();
            var pt = new POINTS {
                X = (short)p.X,
                Y = (short)p.Y
            };

            NativeMethods.SendMessage(Handle, (int) WindowMessages.WM_NCLBUTTONDOWN, (int) hit, pt);
        }

        public void DecorationMouseDown(HitTestValues hit) {
            DecorationMouseDown(hit, MousePosition);
        }

        protected static int MakeLong(short lowPart, short highPart) {
            return (int) (((ushort) lowPart) | (uint) (highPart << 16));
        }

        protected void ShowSystemMenu(Point pos) {
            NativeMethods.SendMessage(Handle, (int) WindowMessages.WM_SYSMENU, 0, MakeLong((short) pos.X, (short) pos.Y));
        }

        protected override void WndProc(ref Message m) {
            if (DesignMode) {
                base.WndProc(ref m);
                return;
            }
            switch (m.Msg) {
                case (int)WindowMessages.WM_NCCALCSIZE: {
                    WmNCCalcSize(ref m);
                    break;
                }
                case (int)WindowMessages.WM_NCPAINT: {
                    WmNCPaint(ref m);
                    break;
                }
                case (int)WindowMessages.WM_NCACTIVATE: {
                    WmNCActivate(ref m);
                    break;
                }
                case (int)WindowMessages.WM_SETTEXT: {
                    WmSetText(ref m);
                    break;
                }
                case (int)WindowMessages.WM_WINDOWPOSCHANGED: {
                    WmWindowPosChanged(ref m);
                    break;
                }
                case 174: {
                    break;
                }
                default: {
                    base.WndProc(ref m);
                    break;
                }
            }
        }

        private void SetWindowRegion(IntPtr hwnd, int left, int top, int right, int bottom) {
            var rgn = NativeMethods.CreateRectRgn(0, 0, 0, 0);
            var hrg = new HandleRef((object) this, rgn);
            _ = NativeMethods.GetWindowRgn(hwnd, hrg.Handle);
            NativeMethods.GetRgnBox(hrg.Handle, out RECT box);
            if (box.left != left || box.top != top || box.right != right || box.bottom != bottom) {
                var hr = new HandleRef((object) this, NativeMethods.CreateRectRgn(left, top, right, bottom));
                NativeMethods.SetWindowRgn(hwnd, hr.Handle, NativeMethods.IsWindowVisible(hwnd));
            }
            NativeMethods.DeleteObject(rgn);
        }

        public FormWindowState MinMaxState {
            get {
                var s = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_STYLE);
                var max = (s & (int) WindowStyle.WS_MAXIMIZE) > 0;
                if (max) {  
                    return FormWindowState.Maximized;
                }
                var min = (s & (int) WindowStyle.WS_MINIMIZE) > 0;
                if (min) {
                    return FormWindowState.Minimized;
                }
                return FormWindowState.Normal;
            }
        }

        private void WmWindowPosChanged(ref Message m) {
            DefWndProc(ref m);
            UpdateBounds();
            var pos = (WINDOWPOS) Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));
            SetWindowRegion(m.HWnd, 0, 0, pos.cx, pos.cy);
            m.Result = NativeConstants.TRUE;
        }

        private void WmNCCalcSize(ref Message m) {
            var r = (RECT) Marshal.PtrToStructure(m.LParam, typeof(RECT));
            var max = MinMaxState == FormWindowState.Maximized;
            if (max) {
                var x = NativeMethods.GetSystemMetrics(NativeConstants.SM_CXSIZEFRAME);
                var y = NativeMethods.GetSystemMetrics(NativeConstants.SM_CYSIZEFRAME);
                var p = NativeMethods.GetSystemMetrics(NativeConstants.SM_CXPADDEDBORDER + 2);
                var w = x + p;
                var h = y + p;

                r.left += w;
                r.top += h + 3;
                r.right -= w;
                r.bottom -= h - 7;

                bunifuImageButton1.Size = new Size(32, 25);
                APPBARDATA appBarData = new APPBARDATA
                {
                    cbSize = Marshal.SizeOf(typeof(APPBARDATA))
                };
                var autohide = (NativeMethods.SHAppBarMessage(NativeConstants.ABM_GETSTATE, ref appBarData) &
                    NativeConstants.ABS_AUTOHIDE) != 0;
                if (autohide) { r.bottom -= 1; }
                Marshal.StructureToPtr(r, m.LParam, true);
            } else {
                bunifuImageButton1.Size = new Size(25, 25);
            }
            m.Result = IntPtr.Zero;
        }



        private void WmNCPaint(ref Message msg) {
            msg.Result = NativeConstants.TRUE;
        }

        private void WmSetText(ref Message msg) {
            DefWndProc(ref msg);
        }
        
        private void WmNCActivate(ref Message msg) {
            _ = msg.WParam == NativeConstants.TRUE;
            if (MinMaxState == FormWindowState.Minimized) {
                DefWndProc(ref msg);
            } else {
                msg.Result = NativeConstants.TRUE;
            }
        }

        private DateTime titleClickTime = DateTime.MinValue;
        private Point titleClickPosition = Point.Empty;
        private FormWindowState previousWindowState;
        public bool loaded = false;
        public bool formclosing = false;
        private Dropshadow shadow;
        public static MainForm mainform;

        public MainForm() {
            InitializeComponent();
            panel17.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOPLEFT);
            panel16.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOPRIGHT);
            panel15.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOMLEFT);
            panel12.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOMRIGHT);
            TopBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTTOP);
            LeftBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTLEFT);
            RightBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTRIGHT);
            BottomBorderPanel.MouseDown += (s, e) => DecorationMouseDown(e, HitTestValues.HTBOTTOM);
            
            panel1.MouseDown += TitleLabel_MouseDown;
            panel3.MouseDown += TitleLabel_MouseDown;

            previousWindowState = MinMaxState;
            SizeChanged += MainForm_SizeChanged;

            mainform = this;
            loaded = true;
        }

        public static MainForm Get() {
            return mainform;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            MaximumSize = Screen.PrimaryScreen.Bounds.Size;
            shadow = new Dropshadow(this) {
                ShadowBlur = 9,
                ShadowSpread = -5,
                ShadowColor = Color.FromArgb(51, 51, 51)
            };
            shadow.RefreshShadow();
        }

        public void Close(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Close();
            }
        }

        private void DecorationMouseDown(MouseEventArgs e, HitTestValues h) {
            if (e.Button == MouseButtons.Left) {
                DecorationMouseDown(h);
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e) {
            if (previousWindowState != MinMaxState) {
                previousWindowState = MinMaxState;
            }
        }

        private FormWindowState ToggleMaximize() {
            //bool maximized = WindowState == FormWindowState.Maximized ? true : false;
            //if (maximized) {
            //    shadow.ShadowSpread = 0;
            //    shadow.ShadowColor = Color.Transparent;
            //    shadow.RefreshShadow();
            //}
            return WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void TitleLabel_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                var clickTime = (DateTime.Now - titleClickTime).TotalMilliseconds;
                if (clickTime < SystemInformation.DoubleClickTime && e.Location == titleClickPosition) {
                    ToggleMaximize();
                } else {
                    titleClickTime = DateTime.Now;
                    titleClickPosition = e.Location;
                    DecorationMouseDown(HitTestValues.HTCAPTION);
                }
            } else if (e.Button == MouseButtons.Right) {
                ShowSystemMenu(new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void BunifuImageButton1_MouseClick(object sender, MouseEventArgs e) {
            if (bunifuImageButton1.ClientRectangle.Contains(e.Location)) {
                Close(e);
            }
        }

        private void BunifuImageButton1_MouseMove(object sender, MouseEventArgs e) { 
            bunifuImageButton1.BackColor = Color.FromArgb(45, 45, 48); 
        }

        private void BunifuImageButton1_MouseLeave(object sender, EventArgs e) {
            bunifuImageButton1.BackColor = Color.FromArgb(28, 28, 28);
        }

        private void BunifuImageButton2_MouseClick(object sender, MouseEventArgs e) {
            if (bunifuImageButton2.ClientRectangle.Contains(e.Location)) {
                ToggleMaximize();
            }
        }

        private void BunifuImageButton2_MouseMove(object sender, MouseEventArgs e) {
            bunifuImageButton2.BackColor = Color.FromArgb(45, 45, 48);
        }

        private void BunifuImageButton2_MouseLeave(object sender, EventArgs e) {
            bunifuImageButton2.BackColor = Color.FromArgb(28, 28, 28);
        }

        private void BunifuImageButton3_MouseClick(object sender, MouseEventArgs e) {
            if (bunifuImageButton3.ClientRectangle.Contains(e.Location)) {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void BunifuImageButton3_MouseMove(object sender, MouseEventArgs e) {
            bunifuImageButton3.BackColor = Color.FromArgb(45, 45, 48);
        }

        private void BunifuImageButton3_MouseLeave(object sender, EventArgs e) {
            bunifuImageButton3.BackColor = Color.FromArgb(28, 28, 28);
        }


        private void MainForm_Resize(object sender, EventArgs e) {
            if (!loaded) {
                return;
            }
            shadow.RefreshShadow();
        }

        private void MainForm_Activated(object sender, EventArgs e) {
            if (!formclosing) {
                shadow.ShadowSpread = -4;
                shadow.RefreshShadow();
            }
        }

        private void MainForm_Deactivate(object sender, EventArgs e) {
            if (!formclosing) {
                shadow.ShadowSpread = -5;
                shadow.RefreshShadow();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            formclosing = true;
        }

        private void MainForm_LocationChanged(object sender, EventArgs e) {
            if (!loaded)
            {
                return;
            }
            shadow.RefreshShadow();
        }
    }
}