using System;
using System.Windows.Forms;

namespace ModernFormDesign.UI.UserControls
{
    public partial class Account : UserControl
    {
        public Account()
        {
            InitializeComponent();
        }

        private void Account_Resize(object sender, EventArgs e) {
            label1.Left = (panel9.Width - label1.Width) / 2;
            label2.Left = (panel7.Width - label2.Width) / 2;
        }
    }
}
