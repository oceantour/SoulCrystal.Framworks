using System.Runtime.CompilerServices;

namespace SoulCrystal.Algorithm
{
    /// <summary> 数组数列 </summary>
    /// <remarks> G(e) = C[e - 1] ∉ e; 1 -> e </remarks>
    public class ArraySeries
    {
        private static char[] Chars = new[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

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
    }

    // 性能与概念优化 - 未完善
    //public class ArraySeries
    //{
    //    private static readonly object s_valueLock = new object();
    //    private static IReadOnlyList<List<string>> s_value;

    //    public static IReadOnlyList<List<string>> Value
    //    {
    //        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    //        get => s_value ?? GenerateSeries(9);
    //    }

    //    /// <summary> 生成数组数列 </summary>
    //    /// <param name="possibilities"> 可能性 </param>
    //    /// <returns> 组列 </returns>
    //    [MethodImpl(MethodImplOptions.NoInlining)]
    //    public static IReadOnlyList<List<string>> GenerateSeries(int possibilities)
    //    {
    //        lock (s_valueLock)
    //        {
    //            if (s_value is null)
    //            {
    //                List<List<string>> values = new();

    //                for (int idx = 0; idx <= possibilities; idx++)
    //                {
    //                    values.Add(Exhaustions(idx, possibilities));
    //                }

    //                s_value = values;
    //            }
    //        }

    //        return s_value;
    //    }

    //    /// <summary> 穷举 </summary>
    //    /// <param name="line"> 行 </param>
    //    /// <param name="position"> 位 </param>
    //    /// <returns> 穷举值 </returns>
    //    private static List<string> Exhaustions(int line, int position)
    //    {
    //        var exhaustions = new List<string>();

    //        for (int idx = 0; idx <= position; idx++)
    //        {

    //        }

    //        return exhaustions;
    //    }
    //}
}
