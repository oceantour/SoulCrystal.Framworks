//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ihei
//{
//    public class Util
//    {
//        private static HttpClient _httpClient = new HttpClient();

//        /// <summary> Post请求 </summary>
//        /// <param name="url"> 需要请求的Url </param>
//        /// <param name="data"> Post请求需要的参数 </param>
//        /// <returns> 请求成功获取数据 </returns>
//        public static async Task<string> PostAsync(string url, object data)
//        { 
//            try
//            {
//                if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url), $"{nameof(PostAsync)}请求失败: {nameof(url)}不能为空.");
//                if (data is null) throw new ArgumentNullException(nameof(data), $"{nameof(PostAsync)}请求失败: {nameof(data)}数据不能为空");

//                using (HttpContent content = new StringContent(JsonHelper.ObjectToJson(data), Encoding.UTF8, ContextType.ApplicationJson))
//                {
//                    using (var response = (await _httpClient.PostAsync(url, content)).EnsureSuccessStatusCode())
//                    {
//                        return await response.Content.ReadAsStringAsync();
//                    }
//                }
//            }
//            catch (Exception exc)
//            {
//                IheiException.PritingExcption(exc);
//                return string.Empty; // TODO Logger
//            }
//        }

//        /// <summary> Post请求 </summary>
//        /// <typeparam name="TObject"> 需要返回的数据类型 </typeparam>
//        /// <param name="url"> 需要请求的Url </param>
//        /// <param name="data"> Post请求需要的参数 </param>
//        /// <returns> 请求成功获取数据 </returns>
//        public static async Task<TObject> PostAsync<TObject>(string url, object data) where TObject : class
//        {
//            try
//            {
//                var errorExc = $"{nameof(PostAsync)}<TObject>请求失败: ";
//                if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url), $"{errorExc}{nameof(url)}不能为空.");
//                if (data is null) throw new ArgumentNullException(nameof(data), $"{errorExc}{nameof(data)}不能为空");

//                using (HttpContent content = new StringContent(JsonHelper.ObjectToJson(data), Encoding.UTF8, ContextType.ApplicationJson))
//                {
//                    using (var response = (await _httpClient.PostAsync(url, content)).EnsureSuccessStatusCode())
//                    {
//                        var responseJson = await response.Content.ReadAsStringAsync();
//                        return JsonHelper.JsonToObject<TObject>(responseJson);
//                    }
//                }
//            }
//            catch (Exception exc)
//            {
//                IheiException.PritingExcption(exc);
//                return null; // TODO Logger
//            }
//        }

//        /// <summary> Post Context-Type </summary>
//        public class ContextType
//        {
//            public const string ApplicationJson = "application/json";
//            public const string AllApplicationJson = "application/*+json";
//            public const string ArrayJson = "application/json-patch+json";
//            public const string TextJson = "text/json";
//        }
//    }
//}
