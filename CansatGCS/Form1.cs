using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
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
            listWievPayloadTelemetry.View = View.Details;
            listWievPayloadTelemetry.Columns.Add("Teamid");
            listWievPayloadTelemetry.Columns.Add("MissionTime");
            listWievPayloadTelemetry.Columns.Add("PacketCount");
            listWievPayloadTelemetry.Columns.Add("PacketType");
            listWievPayloadTelemetry.Columns.Add("TPAltitude");
            listWievPayloadTelemetry.Columns.Add("TPTemperature");
            listWievPayloadTelemetry.Columns.Add("TPVoltage");
            listWievPayloadTelemetry.Columns.Add("GyroR");
            listWievPayloadTelemetry.Columns.Add("GyroP");
            listWievPayloadTelemetry.Columns.Add("GyroY");
            listWievPayloadTelemetry.Columns.Add("AccelR");
            listWievPayloadTelemetry.Columns.Add("AccelP");
            listWievPayloadTelemetry.Columns.Add("AccelY");
            listWievPayloadTelemetry.Columns.Add("MagR");
            listWievPayloadTelemetry.Columns.Add("MagP");
            listWievPayloadTelemetry.Columns.Add("MagY");
            listWievPayloadTelemetry.Columns.Add("PointingErr");
            listWievPayloadTelemetry.Columns.Add("TPSoftState");

            listWievContainerTelemetry.View = View.Details;
            listWievContainerTelemetry.Columns.Add("Teamid");
            listWievContainerTelemetry.Columns.Add("MissionTime");
            listWievContainerTelemetry.Columns.Add("PacketCount");
            listWievContainerTelemetry.Columns.Add("PacketType");
            listWievContainerTelemetry.Columns.Add("Mode");
            listWievContainerTelemetry.Columns.Add("TPReleased");
            listWievContainerTelemetry.Columns.Add("Altitude");
            listWievContainerTelemetry.Columns.Add("Temp");
            listWievContainerTelemetry.Columns.Add("Voltage");
            listWievContainerTelemetry.Columns.Add("GpsTime");
            listWievContainerTelemetry.Columns.Add("GpsLatitude");
            listWievContainerTelemetry.Columns.Add("GpsLongtitude");
            listWievContainerTelemetry.Columns.Add("GpsAltitude");
            listWievContainerTelemetry.Columns.Add("GpsStats");
            listWievContainerTelemetry.Columns.Add("SoftwareState");
            listWievContainerTelemetry.Columns.Add("CmdEcho");
         



           

            
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
                    timerCsvSave.Stop();


                }
            }
            catch
            {
                lblHata.Text = "Not connected to serial";
            }

        }
        delegate void GelenVerileriGuncelleCallback(string veri);
        int sayac;
        string[] gelenVerilerimiz;
        private void GelenVerileriGuncelle(string veri)
        {
            if (this.lblGelenVeri.InvokeRequired)
            {
                GelenVerileriGuncelleCallback d = new GelenVerileriGuncelleCallback(GelenVerileriGuncelle);
                this.Invoke(d, new object[] { veri });
                serialPort1.Encoding = UTF8Encoding.UTF8;
            }
            else
            {
              

              
                serialPort1.Encoding = UTF8Encoding.UTF8;
                gelenVerilerimiz = veri.Split(',');
                sayac = gelenVerilerimiz.Length;
                if (veri.Contains("1084"))
                {
                    listWievContainerTelemetry.Items.Add(new ListViewItem(gelenVerilerimiz));
                }
                else if (veri.Contains("6084"))
                {
                    listWievPayloadTelemetry.Items.Add(new ListViewItem(gelenVerilerimiz));
                }
                else
                {
                    lblHata.Text = "Ayrıstırma !!";
                }
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
            graphLblC_Alt.Text =""+ lblCAltitude.Text+" (m)";
            graphLblC_Temp.Text = lblCTemp.Text+" (C)";
            graphLblC_Volt.Text = lblCVoltage.Text+" (V)";
            graphLblTP_Alt.Text = lblPTPAltitude.Text+" (m)";

            this.chartTP_Volt.Series["TP_Volt"].Points.AddXY(lblLongtime.Text, lblPTPVoltage.Text);
            this.chartTP_Gyro_Y.Series["TP_Gyro_Y"].Points.AddXY(lblLongtime.Text, lblPGyroY.Text);
            this.chartTP_Gyro_P.Series["TP_Gyro_P"].Points.AddXY(lblLongtime.Text, lblPGyroP.Text);
            this.chartTP_Gyro_R.Series["TP_Gyro_R"].Points.AddXY(lblLongtime.Text, lblPGyroR.Text);
            graphLblTP_Volt.Text = lblPTPVoltage.Text + " (V)";
            graphLblTP_Gyro_Y.Text = lblPGyroY.Text;
            graphLblTP_Gyro_P.Text = lblPGyroP.Text;
            graphLblTP_Gyro_R.Text = lblPGyroR.Text;

            this.chartTP_Mag_Y.Series["TP_Mag_Y"].Points.AddXY(lblLongtime.Text, lblPMagY.Text);
            this.chartTP_Mag_P.Series["TP_Mag_P"].Points.AddXY(lblLongtime.Text, lblPMagP.Text);
            this.chartTP_Mag_R.Series["TP_Mag_R"].Points.AddXY(lblLongtime.Text, lblPMagR.Text);
            this.chartTP_Point_Err.Series["TP_Point_Err"].Points.AddXY(lblLongtime.Text, lblPPointingErr.Text);
            graphLblTP_Mag_Y.Text = lblPMagY.Text;
            graphLblTP_Mag_P.Text = lblPMagP.Text;
            graphLblTP_Mag_R.Text = lblPMagR.Text;
            graphLblTP_Point_Err.Text = lblPPointingErr.Text;

            this.chartTP_Accel_Y.Series["TP_Accel_Y"].Points.AddXY(lblLongtime.Text, lblPAccelY.Text);
            this.chartTP_Accel_P.Series["TP_Accel_P"].Points.AddXY(lblLongtime.Text, lblPAccelP.Text);
            this.chartTP_Accel_R.Series["TP_Accel_R"].Points.AddXY(lblLongtime.Text, lblPAccelR.Text);
            this.chartTP_Temp.Series["TP_Temp"].Points.AddXY(lblLongtime.Text, lblPTPTemperature.Text);
            graphLblTP_Accel_Y.Text = lblPAccelY.Text;
            graphLblTP_Accel_P.Text = lblPAccelP.Text;
            graphLblTP_Accel_R.Text = lblPAccelR.Text;
            graphLblTP_Temp.Text = lblPTPTemperature.Text+" (C)";
        }

        private void timerSerialPort_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)   {  btnSerialConnect.Text = "Disconnected"; }
            else  { btnSerialConnect.Text = "Connnect"; }
            try
            {
                if (serialPort1.BytesToRead > 0)
                {

                    GelenVerileriGuncelle(serialPort1.ReadLine());
                  
                  
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

       

        private void btnSimModeActive_Click(object sender, EventArgs e)
        {
            // SIM MODE - ACTIVE

            if (serialPort1.IsOpen)
            {serialPort1.Write("CMD,1084,SIM,ACTIVATE"); }
                
           
            
        }

        private void btnSimModeEnable_Click(object sender, EventArgs e)
        {
            // SIM MODE - ENABLE

            if (serialPort1.IsOpen)
            {  serialPort1.Write("CMD,1084,SIM,ENABLE"); }
          
        }

        private void btnSimModeDisable_Click(object sender, EventArgs e)
        {
            // SIM MODE - DISABLE
            if (serialPort1.IsOpen)
            { serialPort1.Write("CMD,1084,SIM,DISABLE"); }
           
        }

        private void btnSimpModeEnter_Click(object sender, EventArgs e)
        {
            // SIMP

            if (serialPort1.IsOpen)
            { serialPort1.Write("CMD,1084,SIMP,10132");}
            
        }

        private void btnSglpSet_Click(object sender, EventArgs e)
        {
            // SGLP - Set Ground Level Pressure
            if (serialPort1.IsOpen)
            { serialPort1.Write("CMD,1084,SGLP,SET");}
            
        }

        private void btnSetTime_Click(object sender, EventArgs e)
        {
            // SET TIME
            if (serialPort1.IsOpen)
            {  serialPort1.Write("CMD,1084,ST,12:05:27");}
           
        }

        private void btniMuSet_Click(object sender, EventArgs e)
        {
            // CI - Calibrate IMU
            if (serialPort1.IsOpen)
            { serialPort1.Write("CMD,1084,CI,SET"); }
           
        }

        private void btnTPRelease_Click(object sender, EventArgs e)
        {
            // PMREL - ON
            if (serialPort1.IsOpen)
            {  serialPort1.Write("CMD,1084,PMREL,ON1");}
           
        }

        private void btnPmrelOff_Click(object sender, EventArgs e)
        {
            // PMREL - OFF

            if (serialPort1.IsOpen)
            {  serialPort1.Write("CMD,1084,PMREL,OFF");}
           
        }

        private void btnPDeployment_Click(object sender, EventArgs e)
        {
            // PMREL - Parachute Deployment

            if (serialPort1.IsOpen)
            { /* ???*/
        }

    }

        private void btnCsvSaveToStop_Click(object sender, EventArgs e)
        {
            timerCsvSave.Stop();
            grbBoxCsvSave.BackColor = Color.Red;
        }

        List<string> csv_datas = new List<string>();
        string C_csvPath, T_csvPath;
        private void timerCsvSave_Tick(object sender, EventArgs e)
        {
           // Console.WriteLine(txtBoxContainerCsv.Text);
            //grbBoxCsvSave.BackColor = Color.Green;
            // Save csv Files
            C_csvPath = Path.Combine(Environment.CurrentDirectory, txtBoxContainerCsv.Text);
            T_csvPath = Path.Combine(Environment.CurrentDirectory, txtBoxTetherPayloadCsv.Text);

            using (StreamWriter file = new StreamWriter(C_csvPath, true))
            {
                if (lblGelenVeri.Text.Contains("1084") && sayac == 16)
                {
                    file.Write(lblGelenVeri.Text);
                }
                
            }
            using (StreamWriter file = new StreamWriter(T_csvPath, true))
            {
                if (lblGelenVeri.Text.Contains("6084") && sayac == 18)
                {
                    file.Write(lblGelenVeri.Text);
                }
            }
            
        }

        private void btnCsvSave_Click(object sender, EventArgs e)
        {
          
            // Save csv files
            grbBoxCsvSave.BackColor = Color.Green;
            timerCsvSave.Start();
        }

       
        private void btnCsvSil_Click_1(object sender, EventArgs e)
        {
                    csvSil();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();  // butona koyunca istasyonu yeniden baslatabiliyoruz.
        }

        private void graphLblTP_Gyro_R_Click(object sender, EventArgs e)
        {

        }

        void csvSil()
        {
            if (File.Exists(txtBoxContainerCsv.Text))
            {
            
                File.Delete(txtBoxContainerCsv.Text);
                lblHata.Text = "Container Veriler silindi listeleyebilirsiniz !";
                timerCsvSave.Stop();
            }
            if (File.Exists(txtBoxTetherPayloadCsv.Text))
            {
              
              
                File.Delete(txtBoxTetherPayloadCsv.Text);
                lblHata.Text = " Payload Veriler silindi listeleyebilirsiniz !";
                timerCsvSave.Stop();
            }
            else { lblHata.Text = "dosya yok "; }
        }

    }
}
