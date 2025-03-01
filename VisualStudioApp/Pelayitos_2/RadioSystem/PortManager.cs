using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestForCansat.SaveSytem;
using TestForCansat;
using TestForCansat.ScreenUtilities;
using Pelayitos_2;
using Newtonsoft;
using Newtonsoft.Json;
using System.Numerics;

namespace TestForCansat.RadioSystem
{
    public class PortManager
    {
        public SerialPort Port;
        public int MessageFromPacketCount;
        public int PacketID;

        public Dictionary<int, Packet> PacketsReceived;
        public Dictionary<int, Packet> PacketsLoaded;
        private Packet LastPacket;

        public Screen3 PacketsScreenData;

        public void InitializePorts(string _portName)
        {
            Port = new SerialPort(_portName, 9600, Parity.None);
            Port.Open();
            Port.DataReceived += OnDataReceived;

            PacketsReceived = new Dictionary<int, Packet>();
            PacketsLoaded = new Dictionary<int, Packet>();

            MessageFromPacketCount = -1;
            PacketID = 0;
        }

        private void OnDataReceived(object _sender, SerialDataReceivedEventArgs _args)
        {
            int intBuffer;
            intBuffer = Port.BytesToRead;
            byte[] byteBuffer = new byte[intBuffer];
            Port.Read(byteBuffer, 0, intBuffer);
            string _message = Encoding.Default.GetString(byteBuffer);

            Console.WriteLine($"Packet entered: {MessageFromPacketCount}");
            _message = _message.Replace(System.Environment.NewLine, "");
            if (_message == "Packet Start")
            {
                MessageFromPacketCount = 0;
                LastPacket = new Packet();
            }
            else if (MessageFromPacketCount == 0)
            {
                LastPacket.Temperature = (float.Parse(_message) / 100000);
                MessageFromPacketCount++;
            }
            else if (MessageFromPacketCount == 1)
            {
                LastPacket.Pressure = (float.Parse(_message) / 100000);
                MessageFromPacketCount++; 
            }
            else if (MessageFromPacketCount == 2)
            {
                LastPacket.Altitude = (float.Parse(_message) / 100000);
                MessageFromPacketCount++;
            }
            else if (MessageFromPacketCount == 3)
            {
                LastPacket.Latitude = _message;
                MessageFromPacketCount++;
            }
            else if (MessageFromPacketCount == 4)
            {
                LastPacket.Longitude = _message;

                
                LastPacket.PacketID = PacketID;
                LastPacket.TimeReceived = DateTime.Now;
                //new Saver().Save(PacketsReceived.Select(item => item.Value).ToList());

                PacketsReceived.Add(PacketID, LastPacket);
                PacketsLoaded.Add(PacketID, LastPacket);
                //if (PacketsScreenData != null) { PacketsScreenData.PrintToConsole($"Packet Received \n {LastPacket} \n", false); }
                if (PacketsScreenData != null)
                {
                    MainWindow.Instance.ChangeConsoleText($"Packet Received: {LastPacket} ; {PacketsScreenData != null}");
                }
                System.Console.WriteLine($"Packet Received: {LastPacket}");
                PacketID++;

                if (MainWindow.Instance.RefreshRealtime)
                {
                    switch (MainWindow.Instance.ScreenID)
                    {
                        case 2:
                            //MainWindow.Instance.DisplayManager.ResizeScreen_25(new Vector2((float)MainWindow.GetWindow(MainWindow.Instance.ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(MainWindow.Instance.ParentGrid).RenderSize.Height), MainWindow.Instance.Screen2_5Data);
                            break;

                        case 25:
                            MainWindow.Instance.DisplayManager.ResizeScreen_25(new Vector2((float)MainWindow.GetWindow(MainWindow.Instance.ParentGrid).RenderSize.Width, (float)MainWindow.GetWindow(MainWindow.Instance.ParentGrid).RenderSize.Height), MainWindow.Instance.Screen2_5Data);
                            break;
                    }
                }

                MessageFromPacketCount++;
            }
            /*/
            else if(MessageFromPacketCount == 3)
            {
                LastPacket.Coordinates = _message;
            }
            /*/
        }

        public void TransformToJson()
        {
            ExportPacket _packet = new ExportPacket();

            int _numberPackets = PacketsReceived.Count();
            _packet.Temperatures = new float[_numberPackets];
            _packet.Pressures = new float[_numberPackets];
            _packet.Altitudes = new float[_numberPackets];

            int _count = 0;

            foreach(KeyValuePair<int, Packet> _pair in PacketsReceived)
            {
                Packet _pack = _pair.Value;

                _packet.Temperatures[_count] = _pack.Temperature;
                _packet.Pressures[_count] = _pack.Pressure;
                _packet.Altitudes[_count] = _pack.Altitude;

                _count++;
            }

            string json = JsonConvert.SerializeObject(_packet);
            Console.WriteLine(json);
            string _route = "C:users/user/YourRoute.json";
            File.WriteAllText(_route, json);
        }
    }
}
