using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncConsole.Samples
{
    public class Looper
    {
        public static void Loop(IProgress<string> progress)
        {
            string[] names = { "Task 1", "Task 2", "Task 3" };
            Point[] fromTos = { new Point(0, 10), new Point(20, 30), new Point(50, 60) };
            for (int i = 0; i < names.Length; i++)
            {
                var loop = LoopAsync(names[i], fromTos[i].X, fromTos[i].Y, progress);
                loop.Wait();
            }
        }
        public static async Task<string> LoopAsync(string name, int from, int to, IProgress<string> progress)
        {
            var sw = new Stopwatch();
            sw.Start();
            int ret = 0;
            for (int i = from; i <= to; i++)
            {
                ret = await Task.Run(() => ret + i);
                progress.Report(string.Format("{0}:\t{1}", name, i));
            }
            sw.Stop();
            return string.Format("RESULT {0}:\t{1}\t {2}ms", name, ret, sw.ElapsedMilliseconds);
        }
    }
}
