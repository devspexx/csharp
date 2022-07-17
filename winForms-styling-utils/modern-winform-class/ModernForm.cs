using Sp3eex.Frontend.Forms;
using Spexx.Frontend.Forms;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Spexx.Backend.Win32Helper.Enums;

namespace Sp3eex.Referenced_Utils.CustomForm
{
    public partial class ModernForm : Form
    {
        #region GLOBAL VARIABLES

        public static bool max = false;
        public static ModernForm modernForm;
        private Size app_size;

        #endregion GLOBAL VARIABLES

        #region MAIN PROGRAM

        /* ---------------------
         * main program
         * --------------------- */

        public ModernForm()
        {
            modernForm = this;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.ResumeLayout(false);
            app_size = Size;
        }

        #endregion MAIN PROGRAM

        #region PROGRAM

        protected static int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }
        protected void ShowSystemMenu(MouseButtons buttons, Point pos)
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

            if (m.Msg == 0x0084)
            {
                m.Result = (IntPtr) 2;
                base.WndProc(ref m);
                return;
            }

            if (m.Msg == 0x1503)
            {
                m.Result = (IntPtr) 0;
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
        private void WmNCCalcSize(ref Message m)
        {
            var r = (RECT) Marshal.PtrToStructure(m.LParam, typeof(RECT));
            var max = MinMaxState == FormWindowState.Maximized;
            if (max)
            {
                r.left += 7;
                r.top += 7;
                r.right -= 7;
                r.bottom -= 5;

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
        private void SetWindowRegion(IntPtr hwnd, int left, int top, int right, int bottom)
        {
            // dont add rect if borders func isnt enabled
            if (!AppSettings.Functionality.SHOW_BORDERS) return;

            // handle rect
            var rgn = NativeMethods.CreateRectRgn(left, top, right, bottom);
            var hrg = new HandleRef((object) this, rgn);

            // get the current window region
            NativeMethods.GetWindowRgn(hwnd, hrg.Handle);
            NativeMethods.GetRgnBox(hrg.Handle, out RECT box);

            // set app allowed rendering region
            NativeMethods.SetWindowRgn(hwnd, this.Handle, NativeMethods.IsWindowVisible(hwnd));

            // delete the obj
            NativeMethods.DeleteObject(rgn);
        }
        public FormWindowState MinMaxState
        {
            get
            {
                var s = NativeMethods.GetWindowLong(Handle, NativeConstants.GWL_STYLE);
                var max = (s & (int)WindowStyle.WS_MAXIMIZE) > 0;
                if (max) {
                    return FormWindowState.Maximized;
                }
                var min = (s & (int)WindowStyle.WS_MINIMIZE) > 0;
                if (min) {
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

        #endregion PROGRAM
    }
}
