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
            using (var cts = new CancellationTokenSource())
            {
                var loop1 = Looper.LoopAsync("Task 1", 1, 50, 0.3M, progress, cts.Token);
                loop1.Wait();

                Console.WriteLine("Press \"X\" to cancel...");

                var keyboardTask = Task.Run(() =>
                {
                    ConsoleKeyInfo keyinfo;
                    do
                    {
                        keyinfo = Console.ReadKey();
                        if (keyinfo.Key == ConsoleKey.X)
                            cts.Cancel();
                    }
                    while (!(loop1.IsCanceled || loop1.IsCompleted));
                });

                Console.WriteLine(loop1.Result);
            }
        }

        #endregion
    }
}
