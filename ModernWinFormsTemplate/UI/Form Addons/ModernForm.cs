using BorderlessForm;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FormEffects
{
    public class ModernForm : Form
    {
        public void DecorationMouseDown(HitTestValues hit, Point p)
        {
            NativeMethods.ReleaseCapture();
            var pt = new POINTS { X = (short)p.X, Y = (short)p.Y };
            NativeMethods.SendMessage(Handle, (int)WindowMessages.WM_NCLBUTTONDOWN, (int)hit, pt);
        }

        public void DecorationMouseDown(HitTestValues hit)
        {
            DecorationMouseDown(hit, MousePosition);
        }

        public void DecorationMouseUp(HitTestValues hit, Point p)
        {
            NativeMethods.ReleaseCapture();
            var pt = new POINTS { X = (short)p.X, Y = (short)p.Y };
            NativeMethods.SendMessage(Handle, (int)WindowMessages.WM_NCLBUTTONUP, (int)hit, pt);
        }

        protected static int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }

        protected void ShowSystemMenu(Point pos)
        {
            NativeMethods.SendMessage(Handle, (int)WindowMessages.WM_SYSMENU, 0, MakeLong((short)pos.X, (short)pos.Y));
        }

        protected override void WndProc(ref Message m)
        {
            if (DesignMode)
            {
                base.WndProc(ref m);
                return;
            }

            switch (m.Msg)
            {
                case (int)WindowMessages.WM_NCCALCSIZE:
                    {
                        WmNCCalcSize(ref m);
                        break;
                    }
                case (int)WindowMessages.WM_NCPAINT:
                    {
                        WmNCPaint(ref m);
                        break;
                    }
                case (int)WindowMessages.WM_NCACTIVATE:
                    {
                        WmNCActivate(ref m);
                        break;
                    }
                case (int)WindowMessages.WM_SETTEXT:
                    {
                        WmSetText(ref m);
                        break;
                    }
                case (int)WindowMessages.WM_WINDOWPOSCHANGED:
                    {
                        WmWindowPosChanged(ref m);
                        break;
                    }
                case 174:
                    {
                        break;
                    }
                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }

        private void SetWindowRegion(IntPtr hwnd, int left, int top, int right, int bottom)
        {
            var rgn = NativeMethods.CreateRectRgn(0, 0, 0, 0);
            var hrg = new HandleRef((object)this, rgn);
            _ = NativeMethods.GetWindowRgn(hwnd, hrg.Handle);
            NativeMethods.GetRgnBox(hrg.Handle, out RECT box);
            if (box.left != Owner.Left - 12 || box.top != Owner.Top - 12 || box.right != right || box.bottom != bottom)
            {
                var hr = new HandleRef((object)this, NativeMethods.CreateRectRgn(left, top, right, bottom));
                NativeMethods.SetWindowRgn(hwnd, hr.Handle, NativeMethods.IsWindowVisible(hwnd));
            }
            NativeMethods.DeleteObject(rgn);
        }

        public FormWindowState MinMaxState
        {
            get
            {
                var s = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_STYLE);

                var max = (s & (int)WindowStyle.WS_MAXIMIZE) > 0;
                if (max)
                {
                    return FormWindowState.Maximized;
                }

                var min = (s & (int)WindowStyle.WS_MINIMIZE) > 0;
                if (min)
                {
                    return FormWindowState.Minimized;
                }

                return FormWindowState.Normal;
            }
        }

        private void WmWindowPosChanged(ref Message m)
        {
            DefWndProc(ref m);
            UpdateBounds();
            var pos = (WINDOWPOS)Marshal.PtrToStructure(m.LParam, typeof(WINDOWPOS));
            SetWindowRegion(m.HWnd, 0, 0, pos.cx, pos.cy);
            m.Result = NativeConstants.TRUE;
        }

        private void WmNCCalcSize(ref Message m)
        {
            var r = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));
            var max = MinMaxState == FormWindowState.Maximized;
            if (max)
            {
                var x = NativeMethods.GetSystemMetrics(NativeConstants.SM_CXSIZEFRAME);
                var y = NativeMethods.GetSystemMetrics(NativeConstants.SM_CYSIZEFRAME);
                var p = NativeMethods.GetSystemMetrics(NativeConstants.SM_CXPADDEDBORDER);
                var w = (x + p);
                var h = (y + p);

                r.left += w;
                r.top += h;
                r.right -= w;
                r.bottom -= h;

                APPBARDATA appBarData = new APPBARDATA
                {
                    cbSize = Marshal.SizeOf(typeof(APPBARDATA))
                };
                var autohide = (NativeMethods.SHAppBarMessage(NativeConstants.ABM_GETSTATE, ref appBarData) &
                    NativeConstants.ABS_AUTOHIDE) != 0;
                if (autohide) { r.bottom -= 7; }
                Marshal.StructureToPtr(r, m.LParam, true);
            }
            m.Result = IntPtr.Zero;
        }

        private void WmNCPaint(ref Message msg)
        {
            msg.Result = NativeConstants.TRUE;
        }

        private void WmSetText(ref Message msg)
        {
            DefWndProc(ref msg);
        }

        private void WmNCActivate(ref Message msg)
        {
            _ = msg.WParam == NativeConstants.TRUE;
            if (MinMaxState == FormWindowState.Minimized)
            {
                DefWndProc(ref msg);
            }
            else
            {
                msg.Result = NativeConstants.TRUE;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ModernForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.Name = "ModernForm";
            this.ResumeLayout(false);

        }
    }
}
