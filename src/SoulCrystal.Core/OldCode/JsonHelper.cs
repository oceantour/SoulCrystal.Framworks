//using SpanJson;

//namespace Ihei
//{
//    /// <summary> Json帮助类 </summary>
//    public static class JsonHelper
//    {
//        /// <summary> 将Obj对象Json序列化 </summary>
//        /// <param name="data"> 需要序列化的对象 </param>
//        /// <returns> Json格式字符串 </returns>
//        public static string ObjectToJson(object data) => JsonSerializer.NonGeneric.Utf16.Serialize(data);

//        /// <summary> 将Json反序列化Obj对象 </summary>
//        /// <param name="json"> 需要反序列化的对象 </param>
//        /// <returns> TObject对象 </returns>
//        public static object JsonToObject(string json) => JsonCamelCaseSerializer.Generic.Utf16.Deserialize<object>(json);

//        /// <summary> 将Json反序列化Obj对象 </summary>
//        /// <typeparam name="TObject"> 所需转化的对象 </typeparam>
//        /// <param name="json"> 需要反序列化的对象 </param>
//        /// <returns> TObject对象 </returns>
//        public static TObject JsonToObject<TObject>(string json) where TObject : class => JsonCamelCaseSerializer.Generic.Utf16.Deserialize<TObject>(json);

//        /// <summary> 对象转换 </summary>
//        /// <typeparam name="TObject"> 需要转换的对象 </typeparam>
//        /// <param name="obj"> 需要反序列化的对象 </param>
//        /// <returns> TObject对象 </returns>
//        public static TObject ToObject<TObject>(object obj) where TObject : class => JsonToObject<TObject>(ObjectToJson(obj));
//    }
//}
