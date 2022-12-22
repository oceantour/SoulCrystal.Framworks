using System.Text;
using ClosedXML.Excel;

namespace SoulCrystal.FileOperation
{
    public class GenarateFile
    {
        public class Area
        {
            public long AreaId { get; set; }
            public long ParentId { get; set; }
            public string Name { get; set; }
        }

        /// <summary> 生成测试用地区文件 </summary>
        /// <param name="package"> 需要转换格式的文件 </param>
        /// <returns> 是否生成成功</returns>
        public bool GenarateAreaExcel(string package = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(package)) package = SR.TestAreaFile;

                var areas = new List<Area>();

                using var sr = new StreamReader(package, Encoding.UTF8);
                while (true)
                {
                    var data = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(data)) break;
                    var fields = data.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    areas.Add(new Area
                    {
                        AreaId = Convert.ToInt64(fields[1]),
                        ParentId = Convert.ToInt64(fields[0]),
                        Name = FixValueLocal(fields[8])
                    });
                }

                var idx = 2;
                using (var workbook = new XLWorkbook())
                {
                    var workSheets = workbook.Worksheets.Add("area");
                    workSheets.Cell(1, 1).Value = "Province";
                    workSheets.Cell(1, 2).Value = "City";
                    workSheets.Cell(1, 3).Value = "District";
                    workSheets.Cell(1, 4).Value = "Town";

                    var provinces = areas.Where(_ => _.AreaId == 0);
                    foreach (var province in provinces)
                    {
                        var citys = areas.Where(_ => _.AreaId == province.ParentId);
                        foreach (var city in citys)
                        {
                            var districts = areas.Where(_ => _.AreaId == city.ParentId);
                            foreach (var district in districts)
                            {
                                var towns = areas.Where(_ => _.AreaId == district.ParentId);
                                foreach (var town in towns)
                                {
                                    workSheets.Cell(idx, 1).Value = province.Name;
                                    workSheets.Cell(idx, 2).Value = city.Name;
                                    workSheets.Cell(idx, 3).Value = district.Name;
                                    workSheets.Cell(idx, 4).Value = town.Name;

                                    idx++;
                                }
                            }
                        }
                    }

                    var filePackagea = string.Empty;
                    for (int i = 1; i < int.MaxValue; i++)
                    {
                        var GenaratePackage = $"{SR.DataPackage}Area_Data{i}{SR.Suffixs[1]}";
                        if (File.Exists(GenaratePackage)) continue;
                        filePackagea = GenaratePackage; break; ;
                    }
                    workbook.SaveAs(filePackagea);
                }

                static string FixValueLocal(string field)
                {
                    if (string.IsNullOrWhiteSpace(field)) { return null; }
                    if (field.StartsWith('"')) field = field[1..^1];
                    if (string.IsNullOrWhiteSpace(field)) { return null; }
                    return field;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
