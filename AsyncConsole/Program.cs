using AsyncConsole.Samples;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncLoops();
            Console.Read();
        }

        #region 3 async tasks
        // 3 async tasks with progress
        static void ProgressReport(string text)
        {
            Console.WriteLine(text);
        }
        private static void AsyncLoops()
        {
            var progress = new Progress<string>(ProgressReport);
            Looper.Loop(progress);
        }

        #endregion
    }
}
