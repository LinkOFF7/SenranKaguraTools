using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIBTool.Formats;

namespace MIBTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                PrintUsage();
                return;
            }
            else
            {
                MIB mib = new MIB();
                mib.Extract(args[0]);
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Senran Kagura MIB Tool by LinkOFF v.0.1a");
            Console.WriteLine("");
            Console.WriteLine("Only font file supported yet. This tool extract GXT files (Ps Vita Swizzled) from MIB archive.");
            Console.WriteLine("zlib-compressed mib's not supported yet!");
            Console.WriteLine("");
        }
    }
}
