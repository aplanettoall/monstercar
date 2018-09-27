using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Timers;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int i = 8, j = 8, k = 8, l = 8;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        TcpClient client = new TcpClient();

        int a = 0;
        int b = 0;
        int FR_uyarı = 1;
        int FL_uyarı = 1;
        int RR_uyarı = 1;
        int RL_uyarı = 1;
        int diagnose_starter = 0;
        string FR = "";
        string FL = "";
        string RR = "";
        string RL = "";
        // int SensorCounter=0;
        string kontrol = "0000";

        private void start_Click(object sender, EventArgs e)
        {
            client.Connect("192.168.47.101", 80);

            if (client.Connected)
            {
                start.BackColor = Color.Green;
                start.Text = "CONNECTED";
                checkBox5.Checked = true;
            }
        }

        private void com_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            com.Enabled = false;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                communication();
                b++;
            }
        }

        private void communication()
        {
            string mesafe = " ";

            Stream Stream = client.GetStream();
            string s = kontrol;
            byte[] message = Encoding.ASCII.GetBytes(s);
            Stream.Write(message, 0, message.Length);

            byte[] bb = new byte[1000000000];
            int k = Stream.Read(bb, 0, 1000000000);

            for (int i = 0; i < k; i++)
            {
                mesafe = mesafe + Convert.ToChar(bb[i]);
            }

            RR = mesafe.Substring(1, 3);
            RL = mesafe.Substring(4, 3);
            FR = mesafe.Substring(7, 3);
            FL = mesafe.Substring(10, 3);

            //SAĞ ÖN SENSÖR
            if (Convert.ToInt64(FR) < 30 && Convert.ToInt64(FR) >= 20)
            {
                radar1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.arka4;
            }
            else if (Convert.ToInt64(FR) < 20 && Convert.ToInt64(FR) >= 10)
            {
                radar1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.arka3;
            }
            else if (Convert.ToInt64(FR) < 10 && Convert.ToInt64(FR) >= 5)
            {
                radar1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.arka2;
            }
            else if (Convert.ToInt64(FR) < 5 && Convert.ToInt64(FR) >= 0)
            {
                radar1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.arka1;
            }
            else
            {
                radar1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.arka4;
            }
            //SOL ÖN SENSÖR
            if (Convert.ToInt64(FL) < 30 && Convert.ToInt64(FL) >= 20)
            {
                radar2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.on4;
            }
            else if (Convert.ToInt64(FL) < 20 && Convert.ToInt64(FL) >= 10)
            {
                radar2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.on3;
            }
            else if (Convert.ToInt64(FL) < 10 && Convert.ToInt64(FL) >= 5)
            {
                radar2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.on2;
            }
            else if (Convert.ToInt64(FL) < 5 && Convert.ToInt64(FL) >= 0)
            {
                radar2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.on1;
            }
            else
            {
                radar2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.on4;
            }
            //SAĞ ARKA SENSÖR

            if (Convert.ToInt64(RR) < 30 && Convert.ToInt64(RR) >= 20)
            {
                radar4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.solsen4;
            }
            else if (Convert.ToInt64(RR) < 20 && Convert.ToInt64(RR) >= 10)
            {
                radar4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.solsen3;
            }
            else if (Convert.ToInt64(RR) < 10 && Convert.ToInt64(RR) >= 5)
            {
                radar4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.solsen2;
            }
            else if (Convert.ToInt64(RR) < 5 && Convert.ToInt64(RR) >= 0)
            {
                radar4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.solsen1;
            }
            else
            {
                radar4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.solsen4;
            }

            //SOL ARKA SENSÖR

            if (Convert.ToInt64(RL) < 30 && Convert.ToInt64(RL) >= 20)
            {
                radar3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sagsen4;
            }
            else if (Convert.ToInt64(RL) < 20 && Convert.ToInt64(RL) >= 10)
            {
                radar3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sagsen3;
            }
            else if (Convert.ToInt64(RL) < 10 && Convert.ToInt64(RL) >= 5)
            {
                radar3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sagsen2;
            }
            else if (Convert.ToInt64(RL) < 5 && Convert.ToInt64(RL) >= 0)
            {
                radar3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sagsen1;
            }
            else
            {
                radar3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sagsen4;
            }
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            /////////////////--KEY ATAMA--//////////////////////
            switch (e.KeyData)
            {
                case Keys.W:
                    illeri.PerformClick();
                    break;
                case Keys.S:
                    geri.PerformClick();
                    break;
                case Keys.D:
                    button_rig.PerformClick();
                    break;
                case Keys.A:
                    button_lef.PerformClick();
                    break;
            }
        }

        private void donme_Click(object sender, EventArgs e)
        {
            button_rig.Enabled = true;
            button_lef.Enabled = true;
            kontrol = kontrol.Remove(2, 2).Insert(2, "00");
        }

        private void dur_Click(object sender, EventArgs e)
        {
            illeri.Enabled = true;
            geri.Enabled = true;
            kontrol = kontrol.Remove(0, 2).Insert(0, "00");
        }

        private void illeri_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sag;
            panel2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sol;
            panel3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.onaktif;
            panel4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.onaktif;
            panel5.BackgroundImage = WindowsFormsApplication1.Properties.Resources.gerideak;
            panel7.BackgroundImage = WindowsFormsApplication1.Properties.Resources.gerideak;
            panel8.Visible = true;      //teker def.
            panel6.Visible = true;      //teker def.
            panel12.Visible = false;    //sağa dönüş label sağ - 12
            panel11.Visible = false;    //sağa dönüş label sol - 11 
            panel13.Visible = false;    //sola dünüş label sol - 13
            panel14.Visible = false;    //sola dönüş label sağ - 14
            i++;
            j--;
            kontrol = kontrol.Remove(0, 2).Insert(0, "01");
        }

        private void geri_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sag;
            panel2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sol;
            panel3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.ondeak;
            panel4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.ondeak;
            panel5.BackgroundImage = WindowsFormsApplication1.Properties.Resources.geriaktif;
            panel7.BackgroundImage = WindowsFormsApplication1.Properties.Resources.geriaktif;
            panel8.Visible = true;      //teker def.
            panel6.Visible = true;      //teker def.
            panel12.Visible = false;    //sağa dönüş label sağ - 12
            panel11.Visible = false;    //sağa dönüş label sol - 11 
            panel13.Visible = false;    //sola dünüş label sol - 13
            panel14.Visible = false;    //sola dönüş label sağ - 14
            i--;
            j++;
            kontrol = kontrol.Remove(0, 2).Insert(0, "10");
        }

        ///////////////--SAĞ BUTON--////////////////////////////////////
        private void button_rig_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sag2;
            panel2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sol;
            panel5.BackgroundImage = WindowsFormsApplication1.Properties.Resources.gerideak;
            panel7.BackgroundImage = WindowsFormsApplication1.Properties.Resources.gerideak;
            panel3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.ondeak;
            panel4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.ondeak;
            panel8.Visible = false;      //teker def.
            panel6.Visible = false;      //teker def.
            panel12.Visible = true;    //sağa dönüş label sağ - 12
            panel11.Visible = true;    //sağa dönüş label sol - 11 
            panel13.Visible = false;   //sola dünüş label sol - 13
            panel14.Visible = false;    //sola dönüş label sağ - 14
            k--;
            l++;
            button_rig.Enabled = false;
            button_lef.Enabled = true;
            kontrol = kontrol.Remove(2, 2).Insert(2, "10");
        }
        ///////////////--SOL BUTON--///////////////////////////////////////
        private void button_lef_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sag;
            panel2.BackgroundImage = WindowsFormsApplication1.Properties.Resources.sol2;
            panel5.BackgroundImage = WindowsFormsApplication1.Properties.Resources.gerideak;
            panel7.BackgroundImage = WindowsFormsApplication1.Properties.Resources.gerideak;
            panel3.BackgroundImage = WindowsFormsApplication1.Properties.Resources.ondeak;
            panel4.BackgroundImage = WindowsFormsApplication1.Properties.Resources.ondeak;
            panel8.Visible = false;      //teker def.
            panel6.Visible = false;      //teker def.
            panel12.Visible = false;    //sağa dönüş label sağ - 12
            panel11.Visible = false;    //sağa dönüş label sol - 11 
            panel13.Visible = true;    //sola dünüş label sol - 13
            panel14.Visible = true;    //sola dönüş label sağ - 14
            k++;
            l--;
            button_lef.Enabled = false;
            button_rig.Enabled = true;
            kontrol = kontrol.Remove(2, 2).Insert(2, "01");
        }      
    }
}