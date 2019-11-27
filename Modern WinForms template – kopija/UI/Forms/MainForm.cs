using FormEffects;
using ShadowDemo;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BorderlessForm
{
    public partial class MainForm : ModernForm
    // Temp. change "ModernForm" to "Form" to load the designer.
    {
        private FormWindowState previousWindowState;
        public bool loaded = false;
        public bool formclosing = false;
        private Dropshadow shadow;

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
            bunifuImageButton2.MouseDown += TitleLabel_MouseDown;
            panel3.MouseDown += TitleLabel_MouseDown;
            panel2.MouseDown += TitleLabel_MouseDown;
            this.Shown += new EventHandler(MainForm_Shown);
            this.Resize += new EventHandler(MainForm_Resize);
            this.LocationChanged += new EventHandler(MainForm_Resize);

            previousWindowState = MinMaxState;
            SizeChanged += MainForm_SizeChanged;
            loaded = true;
        }

        private DateTime titleClickTime = DateTime.MinValue;
        private Point titleClickPosition = Point.Empty;

        private void MainForm_Load(object sender, EventArgs e) {
            if (!DesignMode) {
                shadow = new Dropshadow(this) {
                    ShadowBlur = 10,
                    ShadowSpread = -4,
                    ShadowColor = Color.FromArgb(30, 30, 30)
                };
                shadow.RefreshShadow();
            }
        }

        void Close(MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                Close();
            }
        }

        void DecorationMouseDown(MouseEventArgs e, HitTestValues h) {
            if (e.Button == MouseButtons.Left) {
                DecorationMouseDown(h);
            }
        }

        void MainForm_SizeChanged(object sender, EventArgs e) {
            if (previousWindowState != MinMaxState) {
                previousWindowState = MinMaxState;
            }
        }

        private FormWindowState ToggleMaximize() {
            return WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        void TitleLabel_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                var clickTime = (DateTime.Now - titleClickTime).TotalMilliseconds;
                if (clickTime < SystemInformation.DoubleClickTime && e.Location == titleClickPosition) {
                    ToggleMaximize();
                } else {
                    titleClickTime = DateTime.Now;
                    titleClickPosition = e.Location;
                    DecorationMouseDown(HitTestValues.HTCAPTION);
                }
            }
        }

        private void bunifuImageButton1_MouseClick(object sender, MouseEventArgs e) {
            if (bunifuImageButton1.ClientRectangle.Contains(e.Location)) {
                Close(e);
            }
        }

        private void bunifuImageButton1_MouseLeave(object sender, EventArgs e) {
            bunifuImageButton1.BackColor = Color.FromArgb(28, 28, 28);
        }

        private void bunifuImageButton2_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                ShowSystemMenu(MouseButtons, new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                ShowSystemMenu(MouseButtons, new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                ShowSystemMenu(MouseButtons, new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                ShowSystemMenu(MouseButtons, new Point(Cursor.Position.X, Cursor.Position.Y));
            }
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            if (loaded) {
                shadow.RefreshShadow();
            }
        }

        private void bunifuImageButton1_MouseMove(object sender, MouseEventArgs e) {
            bunifuImageButton1.BackColor = Color.FromArgb(240, 71, 71);
        }

        private void MainForm_Shown(object sender, EventArgs e) { }

        private void MainForm_Activated(object sender, EventArgs e) {
            if (!formclosing) {
                shadow.ShadowSpread = -3;
                shadow.RefreshShadow();
            }
        }

        private void MainForm_Deactivate(object sender, EventArgs e) {
            if (!formclosing) {
                shadow.ShadowSpread = -4;
                shadow.RefreshShadow();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            formclosing = true;
        }
    }
}