using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncConsole.Samples
{
    public class Looper
    {
        public static async Task<string> LoopAsync(string name, int from, int to, IProgress<string> progress, CancellationToken ct)
        {
            var sw = new Stopwatch();
            sw.Start();
            int ret = 0;
            try
            {
                for (int i = from; i <= to; i++)
                {
                    ret = await Task.Run(() => ret + i);
                    progress.Report(string.Format("{0}:\t{1}", name, i));
                }
            }
            catch (OperationCanceledException ex)
            {
                return string.Format("{0}: {1}:\t{2}\t {3}ms", ex.Message, name, ret, sw.ElapsedMilliseconds);
            }
            sw.Stop();
            return string.Format("RESULT {0}:\t{1}\t {2}ms", name, ret, sw.ElapsedMilliseconds);
        }
    }
}
