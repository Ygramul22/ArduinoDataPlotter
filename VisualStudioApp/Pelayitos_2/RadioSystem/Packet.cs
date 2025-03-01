using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForCansat.RadioSystem
{
    public class Packet
    {
        public float Temperature;
        public float Pressure;
        public float Altitude;
        public DateTime TimeReceived;
        public int PacketID;
        public string Latitude;
        public string Longitude;
        public override string ToString()
        {
            return $"PACKET Nº{PacketID} -> Temperature: {Temperature}; pressure: {Pressure}; altitude: {Altitude}; Moment Received: {TimeReceived}";
        }
    }

    public class ExportPacket
    {
        public float[] Temperatures;
        public float[] Pressures;
        public float[] Altitudes;
    }
}
