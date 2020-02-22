using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BorderlessForm;

namespace SPlayer.UI.UserControls
{
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        private void label2_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                //MainForm.get().bunifuTransition2.HideSync(MainForm.get().account1.register1);
                //MainForm.get().bunifuTransition2.ShowSync(MainForm.get().account1.login1);
            }
        }

        private void label2_MouseLeave(object sender, EventArgs e){
            //label2.Font = new Font(label2.Font, FontStyle.Regular);
        }

        private void label2_MouseMove(object sender, MouseEventArgs e) {
            //label2.Font = new Font(label2.Font, FontStyle.Underline);
        }
    }
}
