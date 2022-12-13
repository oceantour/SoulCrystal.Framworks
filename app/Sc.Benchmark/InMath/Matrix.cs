using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siro.Benchmark.InMath
{
    public class Matrix
    {
        public static void TEST()
        {
            var CalculationLine = new int[][]
            {
                new int[] { 2, 4, 5, 6, 8 },            // 1
                new int[] { 1, 3, 4, 5, 6, 7, 9 },      // 2
                new int[] { 2, 4, 5, 6, 8 },            // 3
                new int[] { 1, 2, 3, 5, 7, 8, 9 },      // 4
                new int[] { 1, 2, 3, 4, 6, 7, 8, 9 },   // 5
                new int[] { 1, 2, 3, 5, 7, 8, 9 },      // 6
                new int[] { 2, 4, 5, 6, 8 },            // 7
                new int[] { 1, 3, 4, 5, 6, 7, 9 },      // 8
                new int[] { 2, 4, 5, 6, 8 },            // 9
            };

            var Lines0 = new int[] // 3 * 3
            {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9,
            };

            var Lines1 = new int[] // 4 * 4
            {
                1,  2,  3,  4,

                5,  6,  7,  8,

                9,  10, 11, 12,

                13, 14, 15, 16,
            };

            var Lines2 = new int[] // 5 * 5
            {
                1,  2,  3,  4,  5,

                6,  7,  8,  9,  10,

                11, 12, 13, 14, 15,

                16, 17, 18, 19, 20,

                21, 22, 23, 24, 25,
            };

            var Lines3 = new int[] // 5 * 5
            {
                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,
            };

            var Lines4 = new int[] // 5 * 5
            {
                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,

                0,  0,  0,  0,  0,
            };
        }
    }
}
