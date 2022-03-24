using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;




namespace CansatGCS
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
       
        }
        private void COMPortlariListele()
        {
            string[] myPort;

            myPort = System.IO.Ports.SerialPort.GetPortNames();
            cmbBoxPort.Items.AddRange(myPort);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cmbBoxPort.SelectedIndex = 0;
            serialPort1.Encoding = UTF8Encoding.UTF8;
             COMPortlariListele();
        }

       


        private void btnSerialConnect_Click(object sender, EventArgs e)
        {
            if (cmbBoxPort.SelectedIndex < 0)
            {
                MessageBox.Show("COM port not found");
                return;
            }

            if (cmbBoxPort.SelectedIndex < 0)
            {
                MessageBox.Show("select the BaudRate");
                return;
            }

            try
            {
                if (serialPort1.IsOpen == false)
                {
                    serialPort1.PortName = cmbBoxPort.SelectedItem.ToString();
                    serialPort1.BaudRate = 9600;
                    serialPort1.Open();
                    btnSerialConnect.Text = "Disconnect";
                    btnSerialConnect.BackColor = Color.Green;
                    grbBoxSerialConnect.BackColor = Color.Green;
                    lblHata.Text = "İstasyonla bağlantı kuruldu !";
                    lblHata.BackColor = System.Drawing.Color.LightGreen;

                }
                else
                {
                    serialPort1.Close();
                    btnSerialConnect.Text = "Connect";
                    lblHata.Text = "Bağlantı kurulu degil !";
                    btnSerialConnect.BackColor = Color.Red;
                    grbBoxSerialConnect.BackColor = Color.Red;
                    lblHata.BackColor = System.Drawing.Color.IndianRed;
                
                }
            }
            catch
            {
                lblHata.Text = "Not connected to serial";
            }

        }
        delegate void GelenVerileriGuncelleCallback(string veri);
        int sayac;
        private void GelenVerileriGuncelle(string veri)
        {
            if (this.lblGelenVeri.InvokeRequired)
            {
                GelenVerileriGuncelleCallback d = new GelenVerileriGuncelleCallback(GelenVerileriGuncelle);
                this.Invoke(d, new object[] { veri });
            }
            else
            {
                lblGelenVeri.Text = veri;

                if (veri.Contains("1084"))
                {
                    richTxtBoxConTelemetri.Text += lblGelenVeri.Text;
                }
                else if (veri.Contains("6084"))
                {
                    richTxtBoxTPayloadTelemetri.Text += lblGelenVeri.Text;
                }
                else
                {
                    lblHata.Text = "Ayrıstırma !!";
                }
              
                string[] gelenVerilerimiz;
                gelenVerilerimiz = veri.Split(',');
                sayac = gelenVerilerimiz.Length;
                if (sayac == 16 && veri.Contains("1084"))
                {

                    //16 adet veri   container kısmı   x1
                    lblCTeamid.Text = gelenVerilerimiz[0];
                    lblCMissionTime.Text = gelenVerilerimiz[1];
                    lblCPacketCount.Text = gelenVerilerimiz[2];
                    lblCPacketType.Text = gelenVerilerimiz[3];
                    lblCMode.Text = gelenVerilerimiz[4];
                    lblCTPReleased.Text = gelenVerilerimiz[5];
                    lblCAltitude.Text = gelenVerilerimiz[6];
                    lblCTemp.Text = gelenVerilerimiz[7];
                    lblCVoltage.Text = gelenVerilerimiz[8];
                    lblCGpsTime.Text = gelenVerilerimiz[9];
                    lblCGpsLatitude.Text = gelenVerilerimiz[10];
                    lblCGpsLongtitude.Text = gelenVerilerimiz[11];
                    lblCGpsAltitude.Text = gelenVerilerimiz[12];
                    lblCGpsStats.Text = gelenVerilerimiz[13];
                    lblCSoftwareState.Text = gelenVerilerimiz[14];
                    lblCCmdEcho.Text = gelenVerilerimiz[15];


                }


                if (veri.Contains("6084") && sayac == 18)
                {
                    //18 adet veri   T.Payload kısmı    x4
                    lblPTeamid.Text = gelenVerilerimiz[0];
                    lblPMissionTime.Text = gelenVerilerimiz[1];
                    lblPPacketCount.Text = gelenVerilerimiz[2];
                    lblPPacketType.Text = gelenVerilerimiz[3];
                    lblPTPAltitude.Text = gelenVerilerimiz[4];
                    lblPTPTemperature.Text = gelenVerilerimiz[5];
                    lblPTPVoltage.Text = gelenVerilerimiz[6];
                    lblPGyroR.Text = gelenVerilerimiz[7];
                    lblPGyroP.Text = gelenVerilerimiz[8];
                    lblPGyroY.Text = gelenVerilerimiz[9];
                    lblPAccelR.Text = gelenVerilerimiz[10];
                    lblPAccelP.Text = gelenVerilerimiz[11];
                    lblPAccelY.Text = gelenVerilerimiz[12];
                    lblPMagR.Text = gelenVerilerimiz[13];
                    lblPMagP.Text = gelenVerilerimiz[14];
                    lblPMagY.Text = gelenVerilerimiz[15];
                    lblPPointingErr.Text = gelenVerilerimiz[16];
                    lblPTPSoftState.Text = gelenVerilerimiz[17];
                  
                }

            }
        }
        void grafikGoster()
        {
            this.chartC_Alt.Series["C_Alt"].Points.AddXY(lblLongtime.Text, lblCAltitude.Text);
            this.chartC_Temp.Series["C_Temp"].Points.AddXY(lblLongtime.Text, lblCTemp.Text);
            this.chartC_Volt.Series["C_Volt"].Points.AddXY(lblLongtime.Text, lblCVoltage.Text);
            this.chartTP_Alt.Series["TP_Alt"].Points.AddXY(lblLongtime.Text, lblPTPAltitude.Text);

            this.chartTP_Volt.Series["TP_Volt"].Points.AddXY(lblLongtime.Text, lblPTPVoltage.Text);
            this.chartTP_Gyro_Y.Series["TP_Gyro_Y"].Points.AddXY(lblLongtime.Text, lblPGyroY.Text);
            this.chartTP_Gyro_P.Series["TP_Gyro_P"].Points.AddXY(lblLongtime.Text, lblPGyroP.Text);
            this.chartTP_Gyro_R.Series["TP_Gyro_R"].Points.AddXY(lblLongtime.Text, lblPGyroR.Text);


            this.chartTP_Mag_Y.Series["TP_Mag_Y"].Points.AddXY(lblLongtime.Text, lblPMagY.Text);
            this.chartTP_Mag_P.Series["TP_Mag_P"].Points.AddXY(lblLongtime.Text, lblPMagP.Text);
            this.chartTP_Mag_R.Series["TP_Mag_R"].Points.AddXY(lblLongtime.Text, lblPMagR.Text);
            this.chartTP_Point_Err.Series["TP_Point_Err"].Points.AddXY(lblLongtime.Text, lblPPointingErr.Text);


            this.chartTP_Accel_Y.Series["TP_Accel_Y"].Points.AddXY(lblLongtime.Text, lblPAccelY.Text);
            this.chartTP_Accel_P.Series["TP_Accel_P"].Points.AddXY(lblLongtime.Text, lblPAccelP.Text);
            this.chartTP_Accel_R.Series["TP_Accel_R"].Points.AddXY(lblLongtime.Text, lblPAccelR.Text);
            this.chartTP_Temp.Series["TP_Temp"].Points.AddXY(lblLongtime.Text, lblPTPTemperature.Text);

        }

        private void timerSerialPort_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)   {  btnSerialConnect.Text = "Disconnected"; }
            else  { btnSerialConnect.Text = "Connnect"; }
            try
            {
                if (serialPort1.BytesToRead > 0)
                {

                    GelenVerileriGuncelle(serialPort1.ReadExisting());
                  
                  
                    serialPort1.Encoding = UTF8Encoding.UTF8;

                   
                }


            }
            catch (Exception)
            {
                lblGelenVeri.Text = "gelenVeriGuncelle  fonk !!";
               
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Form kapatılınca olması gerekenler yazıyoruz.
        }

        private void timerVeriYazdir_Tick(object sender, EventArgs e)
        {
            this.Text = "KTU UZAY GCS :   " + lblGelenVeri.Text;
            richTxtBoxGelenVeri.Text += lblGelenVeri.Text;
            lblLongtime.Text = DateTime.Now.ToLongTimeString();
            lblShortTime.Text = DateTime.Now.ToShortDateString();
            if (serialPort1.IsOpen)
            {

                grafikGoster();
            }



        }

        private void btnCxOn_Click(object sender, EventArgs e)
        {
            timerSerialPort.Enabled = true;
            timerVeriYazdir.Start();
        }

        private void btnCxOff_Click(object sender, EventArgs e)
        {
            timerSerialPort.Enabled = false;
            timerVeriYazdir.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
// Application.Restart();   butona koyunca istasyonu yeniden baslatabiliyoruz.