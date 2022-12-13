using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Siro.Benchmark.Other
{
    public class XAL
    {
        public static T XML<T>(string path, Type type) where T : class
        {
            using (FileStream fs = File.OpenRead(path))
            using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
            {
                var xz = new XmlSerializer(type);
                return (T)xz.Deserialize(sr);
            }
        }
    }
}
