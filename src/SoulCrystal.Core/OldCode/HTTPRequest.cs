namespace SoulCrystal.OldCode
{
    public class HTTPRequest
    {
        /// <summary> 请求方式 </summary>
        public enum Method : int
        {
            /// <summary> 向Web服务器请求一个文件 </summary>
            Get = 0,
            /// <summary> 向Web服务器发送数据让Web服务器进行处理 </summary>
            Post = 1,
            /// <summary> 向Web服务器发送数据并存储在Web服务器内部 </summary>
            Put = 2,
            /// <summary> 检查一个对象是否存在 </summary>
            Head = 3,
            /// <summary> 从Web服务器上删除一个文件 </summary>
            Delete = 4,
            /// <summary> 对通道提供支持 </summary>
            Connect = 5,
            /// <summary> 跟踪到服务器的路径 </summary>
            Trace = 6,
            /// <summary> 查询Web服务器的性能 </summary>
            Oprions = 7,
        }
    }
}
