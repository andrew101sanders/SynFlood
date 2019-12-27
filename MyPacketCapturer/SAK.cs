using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPacketCapturer
{
    public partial class SAK : Form
    {
        frmCapture fcapture;
        SYNFlooder sflood;
        public SAK()
        {
            InitializeComponent();
        }

        private void PacketCapturebtn_Click(object sender, EventArgs e)
        {
            if (frmCapture.instantiations == 0)
            {
                fcapture = new frmCapture(); // creates a new frmSend
                fcapture.Show();

            }
        }

        private void synfloodbtn_Click(object sender, EventArgs e)
        {
            if (SYNFlooder.instantiations == 0)
            {
                sflood = new SYNFlooder(); // creates a new frmSend
                sflood.Show();

            }
        }
    }
}
