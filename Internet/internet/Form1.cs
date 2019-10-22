using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace internet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            WebClient webClient = new WebClient();
            byte[] bytes = webClient.DownloadData("http://google.com/");
            stopwatch.Stop();
            double seconds = stopwatch.Elapsed.TotalSeconds;
            double speed = bytes.Count() / seconds;
            label1.Text = "Speed: " + speed / 100 + "kb/s (" + seconds + ")";
            //label1.Location = new Point(this.Height / 2, this.Width / 2);
        }
    }
}
