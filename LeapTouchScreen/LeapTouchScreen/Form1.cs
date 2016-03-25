using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeapTouchScreen
{
    public partial class Form1 : Form
    {
        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Program.Calibrate_KeyPress);
        }

        private void Track_Click(object sender, EventArgs e)
        {
            Program.Init();
            Console.WriteLine("start program");
        }

        private void Calibrate_Click(object sender, EventArgs e)
        {
            Console.WriteLine("calibrate");
            Program.Init();
        }
    }
}
