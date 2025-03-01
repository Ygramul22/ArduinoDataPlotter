using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TestForCansat.RadioSystem;

namespace TestForCansat.SaveSytem
{
    public class Saver
    {
        public void Save(List<Packet> _packet, string _path)
        {
            WriteToFile(_packet, _path, true);
        }

        public Dictionary<int, Packet> Load(string _path)
        {
            List<Packet> listofa = new List<Packet>();
            XmlSerializer formatter = new XmlSerializer(listofa.GetType());
            FileStream aFile = new FileStream(_path, FileMode.Open);
            byte[] buffer = new byte[aFile.Length];
            aFile.Read(buffer, 0, (int)aFile.Length);
            MemoryStream stream = new MemoryStream(buffer);
            List<Packet> packets = (List<Packet>)formatter.Deserialize(stream);
            aFile.Close();

            Dictionary<int, Packet> _packetsDic = new Dictionary<int, Packet>();
            //Organise the packets in a dictionary
            foreach(Packet _packet in packets)
            {
                _packetsDic.Add(_packet.PacketID, _packet);
            }

            return _packetsDic;
        }

        public List<Packet> Load_List(string _path)
        {
            List<Packet> listofa = new List<Packet>();
            XmlSerializer formatter = new XmlSerializer(listofa.GetType());
            FileStream aFile = new FileStream(_path, FileMode.Open);
            byte[] buffer = new byte[aFile.Length];
            aFile.Read(buffer, 0, (int)aFile.Length);
            MemoryStream stream = new MemoryStream(buffer);
            List<Packet> packets = (List<Packet>)formatter.Deserialize(stream);
            aFile.Close();
            return packets;
        }

        public void WriteToFile(List<Packet> listofa, string _path, bool append)
        {
            Console.WriteLine($"Writtind data to path. Path exists: {File.Exists(_path)}");
            if (File.Exists(_path))
            {
                List<Packet> _savedPackets = Load_List(_path);
                Console.WriteLine($"Saved packets length: {_savedPackets.Count}");
                List<Packet> _packetsToSave = CombinePackets(_savedPackets, listofa);

                FileStream outFile = File.Create(_path);
                XmlSerializer formatter = new XmlSerializer(_packetsToSave.GetType());
                formatter.Serialize(outFile, _packetsToSave);
                outFile.Close();
            }
            else
            {
                FileStream outFile = File.Create(_path);
                XmlSerializer formatter = new XmlSerializer(listofa.GetType());
                formatter.Serialize(outFile, listofa);
                outFile.Close();
            }
        }

        public List<Packet> CombinePackets(List<Packet> _batch1, List<Packet> _batch2)
        {
            List<Packet> FinalList = _batch1;

            foreach (Packet _packet in _batch2)
            {
                bool _samePacketIsContained = false;
                foreach (Packet _packetCycled in FinalList)
                {
                    if (_packet.TimeReceived == _packetCycled.TimeReceived)
                    {
                        _samePacketIsContained = true;
                        Console.WriteLine("Coincidence found");
                    }
                }
                if (!_samePacketIsContained)
                {
                    FinalList.Add(_packet);
                }
            }

            for (int i = 0; i < FinalList.Count; i++)
            {
                FinalList[i].PacketID = i;
            }

            return FinalList;
        }

        public void SaveCanvas(Canvas _canvas, string _path)
        {
            Rect rect = new Rect(_canvas.RenderSize);
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Size.Width, (int)rect.Size.Height, 750, 100, System.Windows.Media.PixelFormats.Default);
            rtb.Render(_canvas);
            //endcode as PNG
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

            //save to memory stream
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            pngEncoder.Save(ms);
            ms.Close();
            /*/
            //Checking if selected file already exists
            if(File.Exists(_path))
            {
                //File exists
                FileInfo _saveFile = new FileInfo(_path);
                _saveFile.
            }
            else
            {
                //file doesn't exist
            }
            /*/
            System.IO.File.WriteAllBytes(_path, ms.ToArray());
        }
    }
}
