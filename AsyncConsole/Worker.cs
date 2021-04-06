using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncConsole
{
    public class Worker
    {
        public static async Task<int> LoopAsync(int from, int to)
        {
            int ret = 0;
            for (int i = from; i <= to; i++)
            {
                Console.WriteLine(i);
                ret = await Task.Run(() => ret + i);
            }
            return ret;
        }
    }
}
