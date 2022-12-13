using System;
using System.Collections.Generic;

namespace Siro.Benchmark.InMath
{
    public class Calculation
    {
        /// <summary> 平方差 </summary>
        public static void SquareDifference(int count)
        {
            Console.WriteLine("次数\t平方\t平方差\t平方差/2\t平方个位");

            var data = new List<int> { 0 };
            for (int i = 1; i <= count; i++)
            {
                data.Add((int)Math.Pow(i, 2));
                var idx = $"{data[i]}".Length;
                var g = $"{data[i]}".Substring(idx - 1);

                if (i % 10 == 0) Console.WriteLine();

                Console.WriteLine(
                    $"{i}\t" +
                    $"{data[i]}\t" +
                    $"{data[i] - data[i - 1]}\t" +
                    $"{(data[i] - data[i - 1]) / 2}\t\t" +
                    $"{g}"
                );
            }
        }

        /// <summary> 自然常数E的极限 </summary>
        public void Lim(int x)
        {
            for (double i = 1; i <= x; i++)
            {
                double lim = Math.Pow(1 + 1 / i, i);

                Console.WriteLine($"{i} = {lim}");
            }
        }
    }
}
