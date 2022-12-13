using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Siro.Benchmark.MathOperations
{
    /// <summary> 数组数列 </summary>
    /// <remarks> G(e) = C[e - 1] ∉ e; 1 -> e </remarks>
    public class ArraySeries
    {
        private static readonly object s_valueLock = new object();
        private static IReadOnlyList<List<string>> s_value;

        public static IReadOnlyList<List<string>> Value
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get => s_value ?? GenerateSeries(9);
        }

        /// <summary> 生成数组数列 </summary>
        /// <param name="possibilities"> 可能性 </param>
        /// <returns> 组列 </returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IReadOnlyList<List<string>> GenerateSeries(int possibilities)
        {
            lock (s_valueLock)
            {
                if (s_value is null)
                {
                    List<List<string>> values = new();

                    for (int idx = 0; idx <= possibilities; idx++)
                    {
                        values.Add(Exhaustions(idx, possibilities));
                    }

                    s_value = values;
                }
            }

            return s_value;
        }

        /// <summary> 穷举 </summary>
        /// <param name="line"> 行 </param>
        /// <param name="position"> 位 </param>
        /// <returns> 穷举值 </returns>
        private static List<string> Exhaustions(int line, int position)
        {
            var exhaustions = new List<string>();

            for (int idx = 0; idx <= position; idx++)
            {

            }

            return exhaustions;
        }
    }
}
