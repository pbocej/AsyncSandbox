using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var loop1_20 = Worker.LoopAsync(1, 20);
            var loop20_50 = Worker.LoopAsync(20, 50);
            loop1_20.Wait();
            Console.WriteLine("Loop 1...20: {0}", loop1_20.Result);
            Console.WriteLine("Loop 20...50: {0}", loop20_50.Result);
            loop20_50.Wait();
            Console.Read();
        }
    }
}
