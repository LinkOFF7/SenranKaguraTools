using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MIBTool.Formats
{
    internal class MIB
    {
        public string Magic { get; set; }
        public int Version { get; set; }
        public int FileType { get; set; } // 7 - textures, 10 - MFT
        public short Unknown3 { get; set; }
        public short Count { get; set; }
        public int[] Offsets { get; set; }

        public void Extract(string filename)
        {
            Encoding enc = Encoding.ASCII;
            BinaryReader reader = new BinaryReader(File.OpenRead(filename));
            Magic = enc.GetString(reader.ReadBytes(4));
            Version = reader.ReadInt32();
            FileType = reader.ReadInt32();
            Unknown3 = reader.ReadInt16();
            Count = reader.ReadInt16();
            Offsets = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                Offsets[i] = reader.ReadInt32();
                new MIBEntry().GetData(reader, Offsets[i], true, Path.GetFileNameWithoutExtension(filename));
            }
        }
    }
}
