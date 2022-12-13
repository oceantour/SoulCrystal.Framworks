//using System;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using Ihei.Robot.Enums;
//using Ihei.Robot.Reporting;

//namespace Ihei.Robot
//{
//    /// <summary> 上报数据监听器 </summary>
//    public sealed class CQPostListener
//    {
//        #region @@ Filed @@

//        private const string SelfID = "X-Self-ID";
//        private const string Signature = "X-Signature";
//        private const string Length = "Content-Length";
//        private const string Type = "Content-Type";
//        private const string Accept = "Accept";
//        private const string Host = "Host";
//        private const string Agent = "User-Agent";

//        private byte[] _secretBytes;
//        private readonly HttpListener _listener = new HttpListener();
//        private readonly object _listenerLock = new object();
//        private Task _listenTask;

//        #endregion

//        #region @@ Properties @@

//        /// <summary> 获取是否已开始监听 HTTP 上报数据 </summary>
//        public bool IsListening => _listener.IsListening;

//        /// <summary> 事件处理器 </summary>
//        public CQHttpClient ApiClient { get; set; }

//        /// <summary> 获取或设置转发地址 </summary>
//        public string ForwardTo { get; set; }

//        private string _postAddress;

//        /// <summary> 获取或设置 CQHTTP 上报地址(以设置则无效) </summary>
//        public string PostAddress
//        {
//            get => _postAddress;
//            set
//            {
//                string address = value;
//                if (!address.EndsWith("/")) address += "/";
//                _postAddress = address;
//            }
//        }

//        #endregion

//        #region @@ Constructors @@

//        /// <summary>
//        /// 构造 <see cref="CQPostListener"/> 的实例，并指定 <see cref="PostAddress"/>
//        /// </summary>
//        /// <param name="address"> 监听地址 </param>
//        /// <param name="apiClient"> 事件处理器 </param>
//        public CQPostListener(string address, CQHttpClient apiClient = null)
//        {
//            PostAddress = address;
//            ApiClient = apiClient;
//        }

//        /// <summary>
//        /// 构造 <see cref="CQPostListener"/> 的实例，并监听所有发往该端口的数据
//        /// </summary>
//        /// <exception cref="ArgumentOutOfRangeException">
//        /// 端口小于等于 <see cref="IPEndPoint.MinPort"/>
//        /// 端口大于 <see cref="IPEndPoint.MaxPort"/>
//        /// </exception>
//        /// <param name="port"> 需监听端口 </param>
//        /// <param name="apiClient"> 事件处理器 </param>
//        public CQPostListener(int port, CQHttpClient apiClient = null)
//        {
//            if (port <= IPEndPoint.MinPort || port > IPEndPoint.MaxPort) throw new ArgumentOutOfRangeException();
//            PostAddress = $"http://+:{port.ToString(System.Globalization.CultureInfo.InvariantCulture)}/";
//            ApiClient = apiClient;
//        }

//        public void ConsolePriting(string message) => Console.WriteLine($"[{DateTime.Now}] CQ Air: {message}");
//        public void ConsolePriting(object type, string message) => Console.WriteLine($"[{DateTime.Now}] [{type}] CQ Air: {message}");

//        #endregion

//        #region ** StartListener **

//        /// <summary> 开始监听上报 </summary>
//        /// <returns> 是否已开始监听 </returns>
//        public bool StartListening()
//        {
//            try
//            {
//                lock (_listenerLock)
//                {
//                    if (IsListening) return IsListening;
//                    string prefix = PostAddress;
//                    _listener.Prefixes.Add(prefix);
//                    _listener.Start();
//                    _listenTask = Task.Run(Listening);
//                    return IsListening;
//                }
//            }
//            catch (Exception exc)
//            {
//                IheiException.PritingExcption(exc);
//                return IsListening;
//            }
//        }

//        private void Listening()
//        {
//            while (true)
//            {
//                var listenerContext = _listener.GetContext();
//                ProcessContext(listenerContext);
//            }
//        }

//        /// <summary> 获取上下文内容 </summary>
//        /// <param name="context"> 上下文 </param>
//        private async void ProcessContext(HttpListenerContext context)
//        {
//            try
//            {
//                await Task.Run(() =>
//                {
//                    var request = context.Request;
//                    using (var response = context.Response)
//                    {
//                        if (!request.ContentType.StartsWith(Util.ContextType.ApplicationJson, StringComparison.Ordinal)) return;
//                        response.ContentType = Util.ContextType.ApplicationJson;

//                        var requestContent = GetContentAndForward(request).Result;
//                        if (string.IsNullOrEmpty(requestContent)) return;

//                        var responseObject = AnalysisData(requestContent);
//                        if (responseObject is object)
//                        {
//                            using (var outStream = response.OutputStream)
//                            using (var streamWriter = new StreamWriter(outStream))
//                            {
//                                string jsonResponse = JsonHelper.ObjectToJson(responseObject);
//                                streamWriter.Write(jsonResponse);
//                            }
//                        }
//                        else response.StatusCode = 204;
//                    }
//                }).ConfigureAwait(false);
//            }
//            catch (Exception exc)
//            {
//                IheiException.PritingExcption(exc);
//            }
//        }

//        /// <summary> 从请求中读取内容 </summary>
//        /// <param name="request"> 收到的 Http 请求 </param>
//        /// <returns> 读取到的内容 </returns>
//        private async ValueTask<string> GetContentAndForward(HttpListenerRequest request)
//        {
//            var ms = new MemoryStream();
//            request.InputStream.CopyTo(ms);
//            request.InputStream.Dispose();

//            byte[] bytes = ms.ToArray();
//            var signature = request.Headers.Get(Signature); // HTTP头部签名
//            if (Verify(_secretBytes, signature, bytes, 0, bytes.Length))
//            {
//                var requestContent = request.ContentEncoding.GetString(bytes);
//                var toResult = await ForwardAsync(bytes, request.ContentEncoding, signature);
//                if (toResult is object) Console.WriteLine($"转发{(toResult.Value ? "成功" : "失败")}");
//                return requestContent;
//            }

//            return null;
//        }

//        /// <summary> 签名验证 </summary>
//        private bool Verify(byte[] secret, string signature, byte[] buffer, int offset, int length)
//        {
//            if (secret is null) return true;
//            if (signature is null) return false;
//            using (var hmac = new HMACSHA1(secret))
//            {
//                hmac.Initialize();
//                string result = BitConverter.ToString(hmac.ComputeHash(buffer, offset, length)).Replace("-", "");
//                return string.Equals(signature, $"sha1={result}", StringComparison.OrdinalIgnoreCase);
//            }
//        }

//        #endregion

//        #region ** ForwardListener **

//        /// <summary> 异步通过 HTTP 转发上报事件 </summary>
//        /// <param name="content"> 转发内容 </param>
//        /// <param name="encoding"> 字符编码 </param>
//        /// <param name="signature"> HTTP 头部的签名 </param>
//        private async Task<bool?> ForwardAsync(byte[] content, Encoding encoding, string signature)
//        {
//            string to = ForwardTo;
//            if (string.IsNullOrEmpty(to)) return null;
//            try
//            {
//                using (var client = new HttpClient())
//                {
//                    if (signature != null) client.DefaultRequestHeaders.Add(Signature, signature);

//                    var headerValue = new MediaTypeHeaderValue(Util.ContextType.ApplicationJson);
//                    headerValue.CharSet = encoding.WebName;
//                    var byteArrayContent = new ByteArrayContent(content);
//                    byteArrayContent.Headers.ContentType = headerValue;
//                    using (var result = await client.PostAsync(to, byteArrayContent).ConfigureAwait(false))
//                    {
//                        return result.StatusCode == HttpStatusCode.OK;
//                    }
//                }
//            }
//            catch (Exception exc)
//            {
//                IheiException.PritingExcption(exc);
//                return false;
//            }
//        }

//        #endregion

//        #region ** ReportingProcess **

//        /// <summary> 解析接收到的上报数据 </summary>
//        /// <param name="content"> JSON </param>
//        /// <returns> 处理完成返回的数据 </returns>
//        public Responds AnalysisData(string content)
//        {
//            if (string.IsNullOrWhiteSpace(content)) return null;

//            var reporting = JsonHelper.JsonToObject<ReportingVerify>(content);
//            if (reporting is null) return null;
//            switch (reporting.PostType)
//            {
//                case PostType.MetaEvent: MetaEventVerify(reporting); break;
//                case PostType.Message: ProcessMessage(content); break;
//                    //case PostType.Notice: ProcessNotice(null); break;
//                    //case PostType.Request: return ProcessRequest(null);
//            }
//            return null;
//        }

//        /// <summary> 元事件验证 </summary>
//        /// <param name="meta"> 生命周期 / 心跳 </param>
//        private void MetaEventVerify(ReportingVerify meta)
//        {
//            if (meta.MetaEventType == MetaEventType.Lifecycle)
//            {
//                var info = meta.SubType switch
//                {
//                    SubType.Enable => "正常启动",
//                    SubType.Disable => "已停用",
//                    SubType.Connect => "WebSocket 连接成功",
//                    _ => null,
//                };
//                ConsolePriting(meta.SubType, info);
//            }
//            else
//            {
//                ConsolePriting(meta.Status, $"心跳秒数间隔 {meta.Interval / 1000}");
//            }
//        }

//        #region -- Message --

//        /// <summary> 消息上报过程 </summary>
//        /// <param name="content"> 内容 </param>
//        private void ProcessMessage(string content)
//        {
//            var message = JsonHelper.JsonToObject<PrivateMessage>(content);
//            switch (message.MessageType)
//            {
//                case MessageType.Private: MessageEvent?.Invoke(ApiClient, message); break;
//                case MessageType.Group: ProcessGroupMessage(JsonHelper.JsonToObject<AnonymousMessage>(content)); break;
//                case MessageType.Discuss: MessageEvent?.Invoke(ApiClient, JsonHelper.JsonToObject<DiscussMessage>(content)); break;
//                default: throw new Exception(nameof(ProcessMessage));
//            }
//        }

//        /// <summary> 接收群消息 </summary>
//        /// <param name="message"></param>
//        private void ProcessGroupMessage(AnonymousMessage message)
//        {
//            switch (message.SubType)
//            {
//                case SubType.Normal:
//                case SubType.Notice:
//                    MessageEvent?.Invoke(ApiClient, message as GroupMessage);
//                    break;
//                case SubType.Anonymous: AnonymousMessageEvent?.Invoke(ApiClient, message); break;
//                default: throw new Exception(nameof(ProcessGroupMessage));
//            }
//        }

//        #endregion

//        #region -- Notice --

//        //private void ProcessNotice(JObject contentObject)
//        //{
//        //    switch (contentObject[Posts.Notice.TypeField].ToObject<string>())
//        //    {
//        //        case Notice.GroupUpload:
//        //            GroupFileUploadedEvent?.Invoke(ApiClient, contentObject.ToObject<GroupFileNotice>());
//        //            break;
//        //        case Notice.GroupAdmin:
//        //            ProcessGroupAdminNotice(contentObject);
//        //            break;
//        //        case Notice.GroupDecrease:
//        //            ProcessGroupMemberDecrease(contentObject);
//        //            break;
//        //        case Notice.GroupIncrease:
//        //            ProcessGroupMemberIncrease(contentObject);
//        //            break;
//        //        case Notice.FriendAdd:
//        //            FriendAddedEvent?.Invoke(ApiClient, contentObject.ToObject<FriendAddNotice>());
//        //            break;
//        //        case Notice.GroupBan:
//        //            // TODO: 此处代码未测试。
//        //            GroupBanEvent?.Invoke(ApiClient, contentObject.ToObject<GroupBanNotice>());
//        //            break;
//        //        default:
//        //            // TODO: Logging
//        //            break;
//        //    }
//        //}

//        //private void ProcessGroupAdminNotice(JObject contentObject)
//        //{
//        //    var data = contentObject.ToObject<GroupAdminNotice>();
//        //    switch (data.SubType)
//        //    {
//        //        case GroupAdminNotice.SetAdmin:
//        //            GroupAdminSetEvent?.Invoke(ApiClient, data);
//        //            break;
//        //        case GroupAdminNotice.UnsetAdmin:
//        //            GroupAdminUnsetEvent?.Invoke(ApiClient, data);
//        //            break;
//        //        default:
//        //            // TODO
//        //            break;
//        //    }
//        //}

//        //private void ProcessGroupMemberDecrease(JObject contentObject)
//        //{
//        //    switch (contentObject[""].ToObject<string>())
//        //    {
//        //        case KickedNotice.Kicked:
//        //            KickedEvent?.Invoke(ApiClient, contentObject.ToObject<KickedNotice>());
//        //            break;
//        //        default:
//        //            GroupMemberDecreasedEvent?.Invoke(ApiClient, contentObject.ToObject<GroupMemberDecreaseNotice>());
//        //            break;
//        //    }
//        //}

//        //private void ProcessGroupMemberIncrease(JObject contentObject)
//        //{
//        //    var data = contentObject.ToObject<GroupMemberIncreaseNotice>();
//        //    if (data.IsMe)
//        //    {
//        //        GroupAddedEvent?.Invoke(ApiClient, data);
//        //    }
//        //    else
//        //    {
//        //        GroupMemberIncreasedEvent?.Invoke(ApiClient, data);
//        //    }
//        //}

//        #endregion

//        #region -- Resquest --

//        //private RequestResponse ProcessRequest(JObject jObject)
//        //{
//        //    switch (jObject["request_type"].ToObject<string>())
//        //    {
//        //        case nameof(SubRequestType.Friend):
//        //            return ProcessFriendRequest(jObject.ToObject<FriendRequest>());
//        //        case nameof(SubRequestType.Group):
//        //            return ProcessGroupRequest(jObject.ToObject<GroupRequest>());
//        //    }

//        //    return null;
//        //}

//        //private RequestResponse ProcessFriendRequest(FriendRequest friendRequest) => FriendRequestHappen(friendRequest);

//        //private RequestResponse ProcessGroupRequest(GroupRequest groupRequest)
//        //{
//        //    switch (groupRequest.SubType)
//        //    {
//        //        case nameof(SubRequestType.Group.Add): return GroupRequestHappen(groupRequest);
//        //        case nameof(SubRequestType.Group.Invite): return GroupInviteHappen(groupRequest);
//        //    }

//        //    return null;
//        //}

//        #endregion

//        #endregion

//        #region ** Event **

//        /// <summary> 接收私聊、群聊、讨论组消息事件 </summary>
//        public Action<CQHttpClient, ReportingMessage> MessageEvent;

//        /// <summary> 接收匿名的群聊消息 </summary>
//        public Action<CQHttpClient, AnonymousMessage> AnonymousMessageEvent;

//        #endregion

//        #region ** Event Old **

//        #region Logging 日志

//        /// <summary>
//        /// 处理上报中发生了异常。可能是业务逻辑中的异常，也可能是数据传输或解析过程中的异常。
//        /// </summary>
//        public event Action<Exception> OnException;

//        /// <summary>
//        /// 处理上报中发生了异常。可能是业务逻辑中的异常，也可能是数据传输或解析过程中的异常。此事件会包含上报的原始数据。
//        /// </summary>
//        public event Action<Exception, string> OnExceptionWithRawContent;

//        /// <summary>
//        /// 触发 <see cref="OnException"/> 和 <see cref="OnExceptionWithRawContent"/> 事件。
//        /// </summary>
//        /// <param name="e">异常。</param>
//        /// <param name="content">上报内容。</param>
//        internal void LogException(Exception e, string content)
//        {
//            try
//            {
//                OnException?.Invoke(e);
//            }
//            catch (Exception)
//            {
//                // ignored
//            }

//            try
//            {
//                OnExceptionWithRawContent?.Invoke(e, content);
//            }
//            catch (Exception)
//            {
//                // ignored
//            }
//        }

//        #endregion

//        #region Request 接收好友或群邀请

//        ///// <summary>
//        ///// 收到加群请求事件。
//        ///// </summary>
//        //public event GroupRequestEventHandler GroupRequestEvent;

//        //private RequestResponse GroupRequestHappen(GroupRequest request) => GetFirstResponseOrDefault(GroupRequestEvent, h => h.Invoke(ApiClient, request));

//        ///// <summary>
//        ///// 收到加群邀请事件。此时 <see cref="Request.Comment"/> 并不存在。
//        ///// </summary>
//        //public event GroupRequestEventHandler GroupInviteEvent;

//        //private RequestResponse GroupInviteHappen(GroupRequest request) => GetFirstResponseOrDefault(GroupInviteEvent, h => h.Invoke(ApiClient, request));

//        ///// <summary>
//        ///// 收到好友请求事件。
//        ///// </summary>
//        //public event FriendRequestEventHandler FriendRequestEvent;

//        //private RequestResponse FriendRequestHappen(FriendRequest request) => GetFirstResponseOrDefault(FriendRequestEvent, h => h.Invoke(ApiClient, request));

//        private static TResponse GetFirstResponseOrDefault<TResponse, THandler>(THandler handler, Func<THandler, TResponse> invoker)
//            where THandler : Delegate
//            where TResponse : class
//            => handler?.GetInvocationList().Cast<THandler>().Select(invoker)
//                .FirstOrDefault(response => response != null);

//        #endregion

//        #region DefaultHandlers 默认请求处理器

//        ///// <summary>
//        ///// 同意全部群组请求（请求、邀请）的事件处理器。
//        ///// </summary>
//        ///// <param name="api"></param>
//        ///// <param name="groupRequest"></param>
//        ///// <returns></returns>
//        //public static GroupRequestResponse ApproveAllGroupRequests(CQHttpClient api, GroupRequest groupRequest) => new GroupRequestResponse { Approve = true };

//        ///// <summary>
//        ///// 同意全部好友请求的事件处理器。
//        ///// </summary>
//        ///// <param name="api"></param>
//        ///// <param name="friendRequest"></param>
//        ///// <returns></returns>
//        //public static FriendRequestResponse ApproveAllFriendRequests(CQHttpClient api, FriendRequest friendRequest) => new FriendRequestResponse { Approve = true };

//        ///// <summary>
//        ///// 当收到消息时，在同一处发送指定内容消息的事件处理器。并没有什么卵用。
//        ///// </summary>
//        ///// <param name="something"></param>
//        ///// <returns></returns>
//        //public static MessageEventHandler Say(string something) => (api, message) => api?.SendMessageAsync(message.Endpoint, something);

//        #endregion

//        #region Notice 好友/群 加入时或其他加入时

//        ///// <summary>
//        ///// 群文件上传。
//        ///// </summary>
//        //public event Action<CQHttpClient, GroupFileNotice> GroupFileUploadedEvent;

//        ///// <summary>
//        ///// 已设置新的群管理员。
//        ///// </summary>
//        //public event Action<CQHttpClient, GroupAdminNotice> GroupAdminSetEvent;

//        ///// <summary>
//        ///// 已取消群管理员。
//        ///// </summary>
//        //public event Action<CQHttpClient, GroupAdminNotice> GroupAdminUnsetEvent;

//        ///// <summary>
//        ///// 好友添加。
//        ///// </summary>
//        //public event Action<CQHttpClient, FriendAddNotice> FriendAddedEvent;

//        ///// <summary>
//        ///// 群成员减少。
//        ///// </summary>
//        //public event Action<CQHttpClient, GroupMemberDecreaseNotice> GroupMemberDecreasedEvent;

//        ///// <summary>
//        ///// 被踢出群。
//        ///// </summary>
//        //public event Action<CQHttpClient, KickedNotice> KickedEvent;

//        ///// <summary>
//        ///// 群成员增加。
//        ///// </summary>
//        //public event Action<CQHttpClient, GroupMemberIncreaseNotice> GroupMemberIncreasedEvent;

//        ///// <summary>
//        ///// 加入新群时发生的事件。注意此事件没有 <see cref="GroupMemberChangeNotice.OperatorId"/> 的数据（至少 Invite 没有，Approve 不清楚）。
//        ///// </summary>
//        //public event Action<CQHttpClient, GroupMemberIncreaseNotice> GroupAddedEvent;

//        ///// <summary>
//        ///// 群禁言事件。包括禁言和解除禁言。
//        ///// </summary>
//        //public event Action<CQHttpClient, GroupBanNotice> GroupBanEvent;

//        #endregion

//        #endregion

//        #region -- CQ Settings --

//        /// <summary> 设置 CQ Secret (用于验证上报数据是否来自插件) </summary>
//        /// <param name="secret"> CQApi Secret 签名秘钥 </param>
//        public void SetSecret(string secret) => _secretBytes = Encoding.UTF8.GetBytes(secret);

//        #endregion
//    }
//}
