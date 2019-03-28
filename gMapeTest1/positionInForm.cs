using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gMapeTest1
{

    public delegate void MyDelegate(object sender, EventArgs e);
    public partial class positionInForm : Form
    {
        public event MyDelegate myEvent;
        public GMap.NET.WindowsForms.GMapControl gMapControl1;
        public positionInForm()
        {
            InitializeComponent();
        }
        public double latPosition ;
        public double lngPosition;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //private 

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.latInputBox.Text.Equals("") || this.lngInputBox.Text.Equals("")) {
                System.Windows.Forms.MessageBox.Show("经度或者纬度输入为空，请重新输入！");
                return;
            }
            this.latPosition = Convert.ToDouble(this.latInputBox.Text);
            this.lngPosition = Convert.ToDouble(this.lngInputBox.Text);
            this.gMapControl1.Position = new PointLatLng(this.latPosition, this.lngPosition);

        }
    }
}
