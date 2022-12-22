namespace SoulCrystal.Other
{
    internal class OverdueTime
    {
        public static void PriDateTime(DateTime OverdueTime)
        {
            string overdueTime = null;

            var currentTime = DateTime.UtcNow;
            var yesterday = currentTime.AddDays(-1).Date;
            var yesterday2 = currentTime.AddDays(-2).Date;
            if (OverdueTime > yesterday2 && OverdueTime < yesterday) overdueTime = "一天后过期";
            if (OverdueTime < currentTime && OverdueTime > yesterday) overdueTime = $"{OverdueTime.Subtract(yesterday).Hours}小时后过期";
            if (OverdueTime < currentTime) overdueTime = "已过期";
            if (string.IsNullOrWhiteSpace(overdueTime)) overdueTime = $"{OverdueTime}";

            Console.WriteLine($"{OverdueTime:yyyy-MM-dd hh:mm:ss}");
        }

        public enum TermTime : int
        {
            /// <summary>今日</summary>
            Day = 1,
            /// <summary>周</summary>
            Week = 7,
            /// <summary>月</summary>
            Month = 30,
            /// <summary>季</summary>
            Season = 90,
            /// <summary>年</summary>
            Year = 365,
            /// <summary>自定义</summary>
            Custom = -1,
        }
    }
}
