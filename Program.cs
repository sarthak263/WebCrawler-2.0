using System;
using System.Diagnostics;

namespace WikipediaCrawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //starts the timer to time the program
            Stopwatch sw = Stopwatch.StartNew();

            //used to get the memory usages of this program
            Process currPros = Process.GetCurrentProcess();

            //instantiating the Crawler class
            Crawler c = new Crawler("Microsoft");
            c.printData();
            sw.Stop();

            //get the memory usages in mb
            double memoryused = currPros.PrivateMemorySize64*0.000001;

            Console.WriteLine("\nRuntime of the program: {0} ms",sw.ElapsedMilliseconds);
            
            Console.WriteLine("Memory used in this Program: {0} mb", memoryused);
            Console.ReadLine(); 
            
        }
    }
}
