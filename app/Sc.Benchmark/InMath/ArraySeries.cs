using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Siro.Benchmark.InMath
{
    /// <summary> 数组数列 </summary>
    /// <remarks> G(e) = C[e - 1] ∉ e; 1 -> e </remarks>
    public class ArraySeries
    {
        private static readonly object s_valueLock = new object();
        private static IReadOnlyList<List<String>> s_value;

        public static IReadOnlyList<List<String>> Value
        {
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            get => s_value ?? Generate(9);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static IReadOnlyList<List<String>> Generate(int e)
        {
            lock (s_valueLock)
            {
                if (s_value is null)
                {
                    #region -- 初始化数组 --

                    List<String> zero = new List<String>();
                    for (int i = 1; i <= e; i++) { zero.Add($"{i}"); }
                    List<List<String>> data = new List<List<String>> { zero };

                    #endregion

                    #region -- 开始计算 --

                    List<String> Series(List<String> ys)
                    {
                        var data = new List<String>();
                        for (int idx = 1; idx <= e; idx++)
                        {
                            string r = $"{idx}";
                            for (int y = 0; y < ys.Count; y++)
                            {
                                var l = ys[y];
                                if (l.Contains(r)) continue;
                                data.Add($"{l}{r}");
                            }
                        }

                        return data;
                    }

                    for (int idx = 1; idx < e; idx++) data.Add(Series(data[idx - 1]));

                    #endregion

                    s_value = data;
                }
            }

            return s_value;
        }

        /// <summary> 36位的可能性 </summary>
        public static Dictionary<int, IReadOnlyList<string>> ThinkingBit()
        {
            char[] e = new char[] {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z'
            };

            var zero = new List<string>();

            for (int idx = 0; idx < e.Length; idx++) { zero.Add(e[idx].ToString()); }

            var data = new Dictionary<int, IReadOnlyList<string>> { { 0, zero } };

            IReadOnlyList<string> Thin(IReadOnlyList<string> y)
            {
                var data = new List<string>();

                for (int z = 0; z < e.Length; z++)
                {
                    var r = e[z].ToString();

                    int idx = 0, total = y.Count, batch = byte.MaxValue;

                    while (true)
                    {
                        var count = Math.Min(batch, total - idx);
                        if (count <= 0) break;

                        for (int x = 0; x < count; x++)
                        {
                            var l = y[idx + x].ToString();

                            if (l.Contains(r)) continue;

                            data.Add($"{l}{r}");
                        }

                        idx += count;
                    }
                }

                return data;
            }

            for (int x = 1; x < e.Length; x++)
            {
                data.Add(x, Thin(data.GetValueOrDefault(x - 1)));
            }

            return data;
        }
    }
}
