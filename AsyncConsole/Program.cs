using AsyncConsole.Samples;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncLoops();
            Console.ReadKey();
        }

        #region 3 async tasks
        // 3 async tasks with progress & cancel
        static void ProgressReport(string text)
        {
            Console.WriteLine(text);
        }
        private static void AsyncLoops()
        {
            var progress = new Progress<string>(ProgressReport);
            var cts = new CancellationTokenSource();
            
            Console.WriteLine("Press \"X\" to cancel...");
            
            var loop1 = Looper.LoopAsync("Task 1", 1, 10, 0.3M, progress, cts.Token);
            var loop2 = Looper.LoopAsync("Task 2", 11, 20, 0.3M, progress, cts.Token);
            var loop3 = Looper.LoopAsync("Task 3", 21, 30, 0.3M, progress, cts.Token);
            loop1.Wait();
            Thread.Sleep(300);
            loop2.Wait();
            Thread.Sleep(300);
            loop3.Wait();
            
            ConsoleKeyInfo keyinfo;
            bool completed = false;
            bool canceled = false;
            do
            {
                keyinfo = Console.ReadKey();
                if (keyinfo.Key == ConsoleKey.X)
                    cts.Cancel();
                completed = loop1.IsCompleted && loop2.IsCompleted && loop3.IsCompleted;
                canceled = loop1.IsCanceled || loop2.IsCanceled || loop3.IsCanceled;
            }
            while (!(completed || canceled));

            Console.WriteLine(loop1.Result);
            Console.WriteLine(loop2.Result);
            Console.WriteLine(loop3.Result);
        }

        #endregion
    }
}
