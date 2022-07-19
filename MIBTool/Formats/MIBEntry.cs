using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MIBTool.Formats
{
    internal class MIBEntry
    {
        //header. size is 0x10
        public int Unknown1 { get; set; } //0
        public int EntryLength { get; set; } //without 0x30
        public int HeaderLength { get; set; } //0x30
        public short Unknown2 { get; set; } //1?
        public short Count { get; set; } //1?

        //entries. size is 0x20
        public int Unknown3 { get; set; } //0xA
        public short Width { get; set; }
        public short Heigth { get; set; }
        public short Unknown4 { get; set; } 
        public short Unknown5 { get; set; } 
        public int Unknown6 { get; set; } //0
        public int DataLength { get; set; }
        public int Unknown7 { get; set; } //0
        public string Filename { get; set; }

        public string FileType { get; set; }

        public void GetData(BinaryReader reader, long start, bool return2pos, string outputDir = "")
        {
            var savepos = reader.BaseStream.Position;
            reader.BaseStream.Position = start;
            Unknown1 = reader.ReadInt32();
            EntryLength = reader.ReadInt32();
            HeaderLength = reader.ReadInt32();
            Unknown2 = reader.ReadInt16();
            Count = reader.ReadInt16();
            if(Count != 1)
            {
                Console.WriteLine("Кол-во в итерации не равно 1\nСвяжитесь со мной в ЛС: https://forum.zoneofgames.ru/profile/747420-linkoff/");
                Console.ReadKey();
                return;
            }
            Unknown3 = reader.ReadInt32();
            Width = reader.ReadInt16();
            Heigth = reader.ReadInt16();
            Unknown4 = reader.ReadInt16();
            Unknown5 = reader.ReadInt16();
            Unknown6 = reader.ReadInt32();
            DataLength = reader.ReadInt32();
            Unknown7 = reader.ReadInt32();
            Filename = Encoding.ASCII.GetString(reader.ReadBytes(8)).Replace("\0", "");
            byte[] data = reader.ReadBytes(DataLength);
            if (data[0] == 0x47 && data[1] == 0x58 && data[2] == 0x54)
                FileType = ".gxt";
            Console.WriteLine("Extraction: {0}", Filename + FileType);
            if (outputDir == "")
                File.WriteAllBytes(Filename + FileType, data);
            else
            {
                Directory.CreateDirectory(outputDir);
                File.WriteAllBytes(outputDir + Path.DirectorySeparatorChar + Filename + FileType, data);
            }
            if (return2pos)
                reader.BaseStream.Position = savepos;
        }
    }
}
