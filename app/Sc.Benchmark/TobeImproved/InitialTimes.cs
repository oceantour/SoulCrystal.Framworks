namespace Siro.Benchmark.Other
{
    public static class InitialTimes
    {
        public static int Seconds = 1; // 秒

        public static int UpBinary = 60; // 向上进制
        public static int DownBinary = 1000; // 向下进制

        public static int Millisecond = Seconds * DownBinary; // 毫秒
        public static int Microsecond = Millisecond * DownBinary; // 微秒
        public static int Nanosecond = Microsecond * DownBinary; // 纳秒

        public static int Minutes = Seconds * UpBinary; // 分
        public static int Hours = Minutes * UpBinary;  // 时
        public static int Day = Hours * 24; // 天
        public static int Month = Day * 30;
        public static int Season = Month * 3;
        public static int Year = Season * 4;

        public static int[] DaysToMonth365 = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 }; // 年
        public static int[] DaysToMonth366 = { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 }; // 闰年

        //DayToHours = 24；
        //DayToMinutes = 1440;
        //DayToSeconds = 86400;

        //YearToHours = 8760;
        //YearToMinutes = 525600;
        //YearToSeconds = 31536000;

        //EarthQuality = 5974200000000000000000; // 地球质量 单位吨
        //EarthPerimeter = 40075.02; // 地球赤道周长 单位 千米

        //RotateHours = 42897.6; // 平均每小时转动速度
        //RotateMinutes = 1787.4; // 平均每分钟转动速度
        //RotateSeconds = 29.79; // 平均每秒转动速度
    }
}
