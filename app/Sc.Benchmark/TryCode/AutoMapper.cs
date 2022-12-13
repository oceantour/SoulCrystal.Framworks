using AutoMapper;

namespace Siro.Benchmark.TryCode
{
    /*
     * var a = new A();
     * var mapper = ToModel<B, A>(a);
     * 
     * Console.WriteLine(输出 mapper 的属性);
     */

    /// <summary> 自动映射测试 </summary>
    public class AutoMapper
    {
        /// <summary> 模型转换 </summary>
        /// <typeparam name="TSource"> 源类型 </typeparam>
        /// <typeparam name="TMapping"> 需转换类型 </typeparam>
        /// <param name="source"> 数据源 </param>
        /// <returns> 映射对象 </returns>
        public static TMapping ToModel<TSource, TMapping>(TSource source)
        {
            var mapping = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TMapping>())
                .CreateMapper().Map<TSource, TMapping>(source);
            return mapping;
        }
    }
}
