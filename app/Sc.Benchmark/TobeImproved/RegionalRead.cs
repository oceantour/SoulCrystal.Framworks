using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace Siro.Benchmark.Other
{
    public class RegionalRead
    {
        #region AreaClass

        public class Area
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class Province : Area { public string CountryCode { get; set; } }
        public class City : Province { public string ProvinceCode { get; set; } }
        public class District : City { public string CityCode { get; set; } }
        public class Town : District { public string DistrictCode { get; set; } }

        public class Regional
        { 
            public string Name { get; set; }
            public List<Regional> Regionals { get; set; }
        }

        #endregion

        /// <summary> 导入地区 </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public List<Regional> ImportRegional(string path)
        {
            using var fileStream = File.OpenRead(path);
            using var xlWorkbook = new XLWorkbook(fileStream);
            var workSheet = xlWorkbook.Worksheet(1);

            int rowIndex = 1, rowCount = workSheet.LastRowUsed().RowNumber(),
                batchSize = (int)Math.Pow(2, 8);

            var regional = new List<Regional>();

            while (true)
            {
                var rowNumber = Math.Min(batchSize, rowCount - rowIndex);
                if (rowNumber <= 0) break;

                for (int idx = 1; idx <= rowNumber; idx++)
                {
                    var row = workSheet.Row(rowIndex + idx);

                    var province = row.Cell(1).Value.ToString();
                    var city = row.Cell(2).Value.ToString();
                    var district = row.Cell(3).Value.ToString();
                    var town = row.Cell(4).Value.ToString();

                    if (string.IsNullOrWhiteSpace(province)) continue;
                    if (!string.IsNullOrWhiteSpace(district)) if (string.IsNullOrWhiteSpace(city)) continue;
                    if (!string.IsNullOrWhiteSpace(town)) if (string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(district)) continue;

                    var area1 = regional.FirstOrDefault(_ => _.Name == province);
                    if (area1 is object)
                    {
                        if (string.IsNullOrWhiteSpace(city)) continue;
                        var area2 = area1.Regionals.FirstOrDefault(_ => _.Name == city);
                        if (area2 is null)
                        {
                            area1.Regionals.Add(new Regional { Name = city, Regionals = new List<Regional>() });
                            area2 = area1.Regionals.FirstOrDefault(_ => _.Name == city);
                        }

                        if (string.IsNullOrWhiteSpace(district)) continue;
                        var area3 = area2.Regionals.FirstOrDefault(_ => _.Name == district);
                        if (area3 is null)
                        {
                            area2.Regionals.Add(new Regional { Name = district, Regionals = new List<Regional>() });
                            area3 = area2.Regionals.FirstOrDefault(_ => _.Name == district);
                        }

                        if (string.IsNullOrWhiteSpace(town)) continue;
                        var area4 = area3.Regionals.FirstOrDefault(_ => _.Name == town);
                        if (area4 is null) area3.Regionals.Add(new Regional { Name = town });
                    }
                    else
                    {
                        regional.Add(new Regional
                        {
                            Name = province,
                            Regionals = string.IsNullOrWhiteSpace(city) ? new List<Regional>() :
                                new List<Regional> { new Regional
                                {
                                    Name = city,
                                    Regionals = string.IsNullOrWhiteSpace(district) ? new List<Regional>() :
                                        new List<Regional>{ new Regional {
                                            Name = district,
                                            Regionals = string.IsNullOrWhiteSpace(town) ? new List<Regional>() :
                                                new List<Regional>{ new Regional {
                                                    Name = town
                                                } }
                                        } }
                                } }
                        });
                    }
                }

                rowIndex += rowNumber;
            }

            return regional;
        }
    }
}
