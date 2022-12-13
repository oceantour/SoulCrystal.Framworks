using System;
using Siro.Benchmark.Other;

namespace Siro.Benchmark.TryCode
{
    /// <summary> 控制台进度条 </summary>
    public class ConsoleProgressBar
    {
        public static void DateTimeBar()
        {
            var bar = new ProgressBar();

            for (int i = 0; i <= 100; i++)
            {
                var dateTime = DateTime.UtcNow;

                while (true)
                {
                    if (DateTime.UtcNow.Ticks - dateTime.Ticks > (TimeSpan.TicksPerSecond / 5)) break;
                }

                bar.Dispaly(i);
            }
        }
    }
}
