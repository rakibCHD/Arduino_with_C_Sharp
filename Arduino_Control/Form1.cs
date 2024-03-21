using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Arduino_Control
{
    public partial class Form1 : Form
    {
        SerialPort sp;
        string portName="COM1";
        int flag = 0;
        bool alreadyConnected = false;
        bool alreadyDisconnected = false;

        public Form1()
        {
            InitializeComponent();

            string[] port = SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(port);

            string[] pin = new string[12];
            for (int i = 2; i <= 13; i++)
            {
                pin[i - 2] = i.ToString();
            }
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(pin);

            label5.Text = "";
            label6.Text = "";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = 1;
            portName = comboBox1.Text;
            sp = new SerialPort(portName, 9600);
            sp.Open();

            label5.Text = "Port is : " + portName; 
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (sp.IsOpen)
                    sp.Write("p" + comboBox2.Text.ToString());
            }
            else
                MessageBox.Show("Select a port first!!");
            label6.Text = "Output Pin is : " + comboBox2.Text;
        }

        private void Refresh_form()
        {
            this.ActiveControl = label1;
            label1.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Refresh_form();
        }

        private void connect_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (!alreadyConnected)
                {
                    sp.Write("c");

                    string response = sp.ReadLine();

                    if (response.Trim() == "y")
                    {
                        MessageBox.Show("Connected Successfully.");
                        connect.Text = "Connected";
             
                        connect.BackColor = Color.Green;
                        connect.ForeColor = Color.White;
                        connect.TextAlign = ContentAlignment.MiddleCenter;
                        alreadyConnected = true;
                        alreadyDisconnected = false;

                        Disconnect.Text = "Disconnect";
                        Disconnect.BackColor = Color.LemonChiffon;
                        Disconnect.ForeColor = Color.Black;
                    }
                    else
                        MessageBox.Show("Connection Failed.");
                }
                else
                    MessageBox.Show("Already Connected With Port: "+portName);
            }
            else
              MessageBox.Show("Select a port first!!");
        }

        private void On_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (sp.IsOpen)
                    sp.Write("1");
            }
            else
                MessageBox.Show("Select a port first!!");
        }

        private void Off_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (sp.IsOpen)
                    sp.Write("0");
            }
            else
                MessageBox.Show("Select a port first!!");
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                if (alreadyConnected && !alreadyDisconnected)
                {
                    MessageBox.Show("Disconnected Successfuly");
                    Disconnect.Text = "Disconnected";

                    Disconnect.BackColor = Color.IndianRed;
                    Disconnect.ForeColor = Color.White;
                    Disconnect.TextAlign = ContentAlignment.MiddleCenter;
                    alreadyDisconnected = true;
                    alreadyConnected = false;

                    connect.Text = "Connect";
                    connect.BackColor = Color.LemonChiffon;
                    connect.ForeColor = Color.Black;
                }
                else if(alreadyDisconnected)
                    MessageBox.Show("Already Disconnected.");
                else
                    MessageBox.Show("Connect a port First.");
            }
            else
                MessageBox.Show("Connect a port First.");

        }
    }
}
