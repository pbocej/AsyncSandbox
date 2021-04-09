using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole.Samples
{
    public class Looper
    {
        public static async Task<string> LoopAsync(string name, int from, int to, decimal waitSeconds, IProgress<string> progress, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var sw = new Stopwatch();
            sw.Start();
            int ret = 0;
            try
            {
                for (int i = from; i <= to; i++)
                {
                    ret = await Task.Run(() => ret + i);
                    progress.Report(string.Format("{0}:\t{1}", name, i));
                    Thread.Sleep(Convert.ToInt32(waitSeconds * 1000));
                }
            }
            catch (TaskCanceledException ex)
            {
                return string.Format("{0}: {1}:\t{2}\t {3}s", ex.Message, name, ret, sw.ElapsedMilliseconds / 1000f);
            }
            sw.Stop();
            return string.Format("RESULT {0}:\t{1}\t {2}s", name, ret, sw.ElapsedMilliseconds / 1000f);
        }
    }
}
