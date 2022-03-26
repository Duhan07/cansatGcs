//container  x1

int CteamID=1084; //1
String CmissionTime="12:35:24:31";  //2
int CpacketCount=0;  //3
char CpacketType='C';  //4
char Cmode='F';        //5
char Ctp_Released='R';  //6
double Caltitude=395.6; //7
double Ctemperature=24.1; //8
double Cvoltage=5.13;  //9
String CgpsTime="23:04:19/12:35:24"; //10
double CgpsLatitude=38.895189;  //11
double CgpsLongtitude=-77.0364894;  //12
double CgpsAltitude=145.3;  //13
int CgpsStats=3;  //14
String CsoftwareState="ROCKET_ASCENT"; //15
String Ccmd_Echo="SP101325";   //16

//T.Payload x4

int PteamID=6084; //1 
String PmissionTime="12:34:24:31"; //2
int PpacketCount=0; //3
char PpacketType='T';  //4
double Paltitude=155.2;  //5
double Ptp_Tempearature=30.45;  //6
double Ptp_Voltage=5.42;  //7
double Pgyro_R=90.1;  //8
double Pgyro_P=90.2;  //9
double Pgyro_Y=90.3;  //10
double Paccel_R=12.3;  //11
double Paccel_P=8.5;  //12
double Paccel_Y=3.2;  //13 
double Pmag_R=1300;  //14
double Pmag_P=1000;  //15
double Pmag_Y=-900;  //16
int Ppointing_Error=4;  //17
String Ptp_Software_State="TARGET_POİNT";  //18

void setup() {
  // put your setup code here, to run once:
Serial.begin(9600);


CpacketCount+=1;
Caltitude +=5;

}

void loop() {
  // put your main code here, to run repeatedly:


Serial.print(CteamID);Serial.print(",");
Serial.print(CmissionTime);Serial.print(",");
Serial.print(CpacketCount);Serial.print(",");
Serial.print(CpacketType);Serial.print(",");
Serial.print(Cmode);Serial.print(",");
Serial.print(Ctp_Released);Serial.print(",");
Serial.print(Caltitude);Serial.print(",");
Serial.print(Ctemperature);Serial.print(",");
Serial.print(Cvoltage);Serial.print(",");
Serial.print(CgpsTime);Serial.print(",");
Serial.print(CgpsLatitude);Serial.print(",");
Serial.print(CgpsLongtitude);Serial.print(",");
Serial.print(CgpsAltitude);Serial.print(",");
Serial.print(CgpsStats);Serial.print(",");
Serial.print(CsoftwareState);Serial.print(",");
Serial.print(Ccmd_Echo);Serial.println("");

CpacketCount+=1;
Ctemperature += 0.02;
Cvoltage += 0.01;
Caltitude +=0.05;
delay(200);

Serial.print(CteamID);Serial.print(",");
Serial.print(CmissionTime);Serial.print(",");
Serial.print(CpacketCount);Serial.print(",");
Serial.print(CpacketType);Serial.print(",");
Serial.print(Cmode);Serial.print(",");
Serial.print(Ctp_Released);Serial.print(",");
Serial.print(Caltitude);Serial.print(",");
Serial.print(Ctemperature);Serial.print(",");
Serial.print(Cvoltage);Serial.print(",");
Serial.print(CgpsTime);Serial.print(",");
Serial.print(CgpsLatitude);Serial.print(",");
Serial.print(CgpsLongtitude);Serial.print(",");
Serial.print(CgpsAltitude);Serial.print(",");
Serial.print(CgpsStats);Serial.print(",");
Serial.print(CsoftwareState);Serial.print(",");
Serial.print(Ccmd_Echo);Serial.println("");

CpacketCount+=1;
Ctemperature += 0.02;
Cvoltage += 0.01;
Caltitude +=0.05;
delay(200);

Serial.print(CteamID);Serial.print(",");
Serial.print(CmissionTime);Serial.print(",");
Serial.print(CpacketCount);Serial.print(",");
Serial.print(CpacketType);Serial.print(",");
Serial.print(Cmode);Serial.print(",");
Serial.print(Ctp_Released);Serial.print(",");
Serial.print(Caltitude);Serial.print(",");
Serial.print(Ctemperature);Serial.print(",");
Serial.print(Cvoltage);Serial.print(",");
Serial.print(CgpsTime);Serial.print(",");
Serial.print(CgpsLatitude);Serial.print(",");
Serial.print(CgpsLongtitude);Serial.print(",");
Serial.print(CgpsAltitude);Serial.print(",");
Serial.print(CgpsStats);Serial.print(",");
Serial.print(CsoftwareState);Serial.print(",");
Serial.print(Ccmd_Echo);Serial.println("");

CpacketCount+=1;
Ctemperature += 0.02;
Cvoltage += 0.01;
Caltitude +=0.05;
delay(200);

Serial.print(CteamID);Serial.print(",");
Serial.print(CmissionTime);Serial.print(",");
Serial.print(CpacketCount);Serial.print(",");
Serial.print(CpacketType);Serial.print(",");
Serial.print(Cmode);Serial.print(",");
Serial.print(Ctp_Released);Serial.print(",");
Serial.print(Caltitude);Serial.print(",");
Serial.print(Ctemperature);Serial.print(",");
Serial.print(Cvoltage);Serial.print(",");
Serial.print(CgpsTime);Serial.print(",");
Serial.print(CgpsLatitude);Serial.print(",");
Serial.print(CgpsLongtitude);Serial.print(",");
Serial.print(CgpsAltitude);Serial.print(",");
Serial.print(CgpsStats);Serial.print(",");
Serial.print(CsoftwareState);Serial.print(",");
Serial.print(Ccmd_Echo);Serial.println("");

CpacketCount+=1;
Ctemperature += 0.02;
Cvoltage += 0.01;
Caltitude +=0.05;
delay(200);

Serial.print(PteamID);Serial.print(",");
Serial.print(PmissionTime);Serial.print(",");
Serial.print(PpacketCount);Serial.print(",");
Serial.print(PpacketType);Serial.print(",");
Serial.print(Paltitude);Serial.print(",");
Serial.print(Ptp_Tempearature);Serial.print(",");
Serial.print(Ptp_Voltage);Serial.print(",");
Serial.print(Pgyro_R);Serial.print(",");
Serial.print(Pgyro_P);Serial.print(",");
Serial.print(Pgyro_Y);Serial.print(",");
Serial.print(Paccel_R);Serial.print(",");
Serial.print(Paccel_P);Serial.print(",");
Serial.print(Paccel_Y);Serial.print(",");
Serial.print(Pmag_R);Serial.print(",");
Serial.print(Pmag_P);Serial.print(",");
Serial.print(Pmag_Y);Serial.print(",");
Serial.print(Ppointing_Error);Serial.print(",");
Serial.print(Ptp_Software_State);Serial.println("");
PpacketCount--;
Ptp_Tempearature -= 0.02;
Ptp_Voltage -= 0.01;
Paltitude -= 0.03;
delay(200);


}
