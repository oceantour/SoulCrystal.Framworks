using System;

namespace Siro.Benchmark.Other
{
    public class Game
    {
        public static void GameCritialHitRateTest(double rate, int number = 100)
        {
            int hit = 0, noHit = 0;

            for (int i = 0; i < number; i++)
            {
                var rd = new Random().NextDouble();

                if (rate >= System.Math.Round(rd * 100, 2))
                {
                    hit += 1;
                }
                else
                {
                    noHit += 1;
                }
            }

            Console.WriteLine($"暴击命中: {hit}");
            Console.WriteLine($"未暴击: {noHit}");
        }
    }
}
