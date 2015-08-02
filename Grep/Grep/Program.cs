using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grep
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch s = new Stopwatch();
            //string comand = Console.ReadLine();
            s.Start();
            GrepClass.ApplyTheCommand(args);
            Console.WriteLine("Time Elasped: " + s.Elapsed);
        }
    }
}
