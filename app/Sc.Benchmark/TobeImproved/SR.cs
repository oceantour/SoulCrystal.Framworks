using System.Collections.Generic;
using System.IO;

namespace Siro.Benchmark.Other
{
    public static class SR
    {
        public static string[] Suffixs = new string[] { ".xls", ".xlsx", ".csv" };

        public static string CurrentPackage = Directory.GetCurrentDirectory();

        public static string DataPackage { get; } = CurrentPackage.Substring(0, CurrentPackage.IndexOf("bin")) + "Data\\";

        public static string TestAreaFile { get; } = $"{DataPackage}ok_data_level4{Suffixs[2]}";

        public static string AssemblyPackage(string name, int suffixsType) => $"{DataPackage}{name}{Suffixs[suffixsType]}";
    }
}
