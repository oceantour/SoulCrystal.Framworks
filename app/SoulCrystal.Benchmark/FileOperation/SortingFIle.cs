namespace SoulCrystal.FileOperation
{
    /// <summary> 整理文件 </summary>
    internal class SortingFIle
    {
        /// <summary> 根据创建日期整理</summary>
        /// <param name="sourcePath"> 源路径 </param>
        /// <param name="destPath"> 目标路径 </param>
        public static void AccordingToDateTime(string sourcePath, string destPath)
        {
            var files = Directory.GetFiles(sourcePath);
            for (int fileIdx = 0; fileIdx < files.Length; fileIdx++)
            {
                var file = files[fileIdx];
                var fileName = file.Substring(file.LastIndexOf("\\"));
                var fileExtension = fileName.Substring(fileName.LastIndexOf("."));
                var filePath = Path.Combine(sourcePath, fileName);

                var fileCreateTime = File.GetCreationTime(filePath);
                var fileCreateDate = fileCreateTime.ToString("yyyy_MM_dd");

                var fileNewName = fileCreateDate + Guid.NewGuid() + fileExtension;
                var fileNewPath = Path.Combine(destPath, fileCreateDate);

                if (!Directory.Exists(fileNewPath)) Directory.CreateDirectory(fileNewPath);

                fileNewPath = Path.Combine(fileNewPath, fileNewName);

                File.Move(filePath, fileNewPath);
            }
        }
    }
}
