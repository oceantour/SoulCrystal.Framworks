//using System;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;
//using Ihei.Robot.Enums;

//namespace Ihei.Robot
//{
//    /// <summary> CQ HTTP API 客户端 </summary>
//    public partial class CQHttpClient
//    {
//        #region @@ Filed @@

//        /// <summary> 用于发送 HTTP 请求和接收来自通过 URI 确认的资源的 HTTP 响应。 </summary>
//        private HttpClient _httpClient = new HttpClient();

//        private string _apiAddress;

//        /// <summary> 获取或设置 HTTP API 的监听地址. </summary>
//        public string ApiAddress
//        {
//            get => _apiAddress;
//            set
//            {
//                if (value.EndsWith("/", StringComparison.OrdinalIgnoreCase))
//                {
//                    _apiAddress = value;
//                }
//                else
//                {
//                    _apiAddress = value + "/";
//                }
//            }
//        }

//        private string _accessToken;

//        /// <summary> API 访问 Token (详情见: https://richardchien.gitee.io/coolq-http-api/docs/4.15) </summary>
//        public virtual string AccessToken
//        {
//            get => _accessToken;
//            set
//            {
//                if (_accessToken != value)
//                {
//                    var httpClient = _httpClient;
//                    if (!string.IsNullOrEmpty(value))
//                    {
//                        httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse("Token " + value);
//                    }
//                    _httpClient = httpClient;
//                    _accessToken = value;
//                }
//            }
//        }

//        /// <summary> 是否启用异步 </summary>
//        public bool IsAsync { get; set; }

//        #endregion

//        #region @@ Constructors @@

//        /// <summary>
//        /// 构造 <see cref="CQHttpClient"/> 的实例 <see cref="ApiAddress"/> 默认: http://0.0.0.0:5700/
//        /// </summary>
//        public CQHttpClient() => ApiAddress = "http://0.0.0.0:5700/";

//        /// <summary>
//        /// 构造 <see cref="CQHttpClient"/> 的实例，并指定 <see cref="ApiAddress"/>
//        /// </summary>
//        /// <param name="address"> CQHTTP Api Address </param>
//        public CQHttpClient(string address) => ApiAddress = address;

//        /// <summary>
//        /// 构造 <see cref="CQHttpClient"/> 的实例，并指定 <see cref="ApiAddress"/>
//        /// </summary>
//        /// <param name="address"> IPEndPoint Localhost:Port </param>
//        public CQHttpClient(IPEndPoint address) => ApiAddress = $"http://{address}/";

//        /// <summary>
//        /// 构造 <see cref="CQHttpClient"/> 的实例，并指定 <see cref="ApiAddress"/>
//        /// </summary>
//        /// <param name="host"> 主机地址 Localhost 例: 0.0.0.0 </param>
//        /// <param name="port"> 端口号码 Port 例: 5700 </param>
//        public CQHttpClient(string host, int port) => ApiAddress = $"http://{host}:{port}/";

//        /// <summary>
//        /// 构造 <see cref="CQHttpClient"/> 的实例，并指定 <see cref="ApiAddress"/>
//        /// </summary>
//        /// <param name="host"> 主机地址 IPAddress </param>
//        /// <param name="port"> 端口号码 Port 例: 5700 </param>
//        public CQHttpClient(IPAddress host, int port) => ApiAddress = $"http://{host}:{port}/";

//        /// <summary>
//        /// 构造 <see cref="CQHttpClient"/> 的实例，并指定 <see cref="ApiAddress"/> 和 <see cref="AccessToken"/>。
//        /// </summary>
//        /// <param name="apiAddress"> CQHTTP Api Address </param>
//        /// <param name="accessToken"> CQHTTP Api CSRF Token </param>
//        public CQHttpClient(string apiAddress, string accessToken) : this(apiAddress) => AccessToken = accessToken;

//        #endregion

//        #region @@ Private CQHTTP Url @@

//        #region -- URI --

//        /// <summary> 异步标识 </summary>
//        private const string _uriAsync = "_async";
//        private string _uriType;

//        /// <summary> 资源标识符 </summary>
//        private string UriType
//        {
//            get => _uriType;
//            set
//            {
//                var uri = value;
//                if (IsAsync) uri += _uriAsync;
//                _uriType = uri;
//            }
//        }

//        #endregion

//        #region -- SendMessage --

//        /// <summary> 发送私聊信息 Url </summary>
//        private string SendPrivateUrl => _apiAddress + nameof(SendMessageUri.send_private_msg);

//        /// <summary> 发送群聊信息 Url </summary>
//        private string SendGroupUrl => _apiAddress + nameof(SendMessageUri.send_group_msg);

//        /// <summary> 发送讨论组信息 Url </summary>
//        private string SendDiscussUrl => _apiAddress + nameof(SendMessageUri.send_discuss_msg);

//        public string SendMessageUrl => _apiAddress + nameof(SendMessageUri.send_msg);

//        /// <summary> 撤回信息(Pro) Url </summary>
//        private string RecallMessageUrl => _apiAddress + nameof(SendMessageUri.delete_msg);

//        #endregion

//        #region -- GetUserInfo --

//        /// <summary> 获取当前登录账户信息 Url </summary>
//        private string GetLoginUserInfoUrl => _apiAddress + nameof(GetUserInfoUri.get_login_info);

//        /// <summary> 获取陌生人信息 Url </summary>
//        private string GetStrangerUserInfoUrl => _apiAddress + nameof(GetUserInfoUri.get_stranger_info);

//        /// <summary> 获取好友列表 Url </summary>
//        private string GetFriendListUrl => _apiAddress + nameof(GetUserInfoUri.get_friend_list);

//        /// <summary> 获取群列表 Url </summary>
//        private string GetGroupListUrl => _apiAddress + nameof(GetUserInfoUri.get_group_list);

//        /// <summary> 获取群成员列表 Url </summary>
//        private string GetGroupMemberListUrl => _apiAddress + nameof(GetUserInfoUri.get_group_member_list);

//        /// <summary> 获取群信息 Url </summary>
//        private string GetGroupInfoUrl => _apiAddress + nameof(GetUserInfoUri.get_group_info);

//        /// <summary> 获取群成员信息 Url </summary>
//        private string GetGroupMemberInfoUrl => _apiAddress + nameof(GetUserInfoUri.get_group_member_info);

//        /// <summary> 获取图片 Url </summary>
//        private string GetImageUrl => _apiAddress + nameof(GetUserInfoUri.get_image);

//        #endregion

//        #region -- GroupSettings --

//        /// <summary> 设置管理员 Url </summary>
//        private string SetGroupAdminUrl => _apiAddress + nameof(SetGroupUri.set_group_admin);

//        /// <summary> 设置群名片 Url </summary>
//        private string SetGroupCardUrl => _apiAddress + nameof(SetGroupUri.set_group_card);

//        /// <summary> 设置群头衔 Url </summary>
//        private string SetGroupSpecialUrl => _apiAddress + nameof(SetGroupUri.set_group_special_title);

//        /// <summary> 设置群匿名 Url (开启/关闭) </summary>
//        private string SetGroup => _apiAddress + nameof(SetGroupUri.set_group_anonymous);

//        /// <summary> 单人禁言 Url </summary>
//        private string GroupSingleBanUrl => _apiAddress + nameof(SetGroupUri.set_group_ban);

//        /// <summary> 全体禁言 Url </summary>
//        private string GroupAllBanUrl => _apiAddress + nameof(SetGroupUri.set_group_whole_ban);

//        /// <summary> 匿名禁用 Url </summary>
//        private string GroupAnonymousBanUrl => _apiAddress + nameof(SetGroupUri.set_group_anonymous_ban);

//        /// <summary> 移出群员 Url </summary>
//        private string RemoveGroupMemberUrl => _apiAddress + nameof(SetGroupUri.set_group_kick);

//        #endregion

//        #region -- HandleRequest --

//        /// <summary> 退出群 Url </summary>
//        private string SignOutGroupUrl => _apiAddress + nameof(SetGroupUri.set_group_leave);

//        /// <summary> 退出组 Url </summary>
//        private string SignOutDiscussUrl => _apiAddress + nameof(SetGroupUri.set_discuss_leave);

//        /// <summary> 接受或拒绝好友请求 Url </summary>
//        private string HandleFriendRequestUrl => _apiAddress + nameof(SetGroupUri.set_friend_add_request);

//        /// <summary> 接受或拒绝群组请求 Url </summary>
//        private string HandleGroupRequestUrl => _apiAddress + nameof(SetGroupUri.set_group_add_request);

//        #endregion

//        #region -- CQHTTPSettings --

//        /// <summary> 获取 Cookies Url </summary>
//        private string GetCookiesUrl => _apiAddress + nameof(AboutCQHttpUri.get_cookies);

//        /// <summary> 获取 CSRF Token Url </summary>
//        private string GetCSRFTokenUrl => _apiAddress + nameof(AboutCQHttpUri.get_csrf_token);

//        /// <summary> 获取 Cookies与CSRF Token Url </summary>
//        private string GetProofUrl => _apiAddress + nameof(AboutCQHttpUri.get_credentials);

//        /// <summary> 获取插件运行状态 Url </summary>
//        private string GetCQStatusUrl => _apiAddress + nameof(AboutCQHttpUri.get_status);

//        /// <summary> 获取CQ当前版本信息 Url </summary>
//        private string GetCQVersionUrl => _apiAddress + nameof(AboutCQHttpUri.get_version_info);

//        /// <summary> 检查是否可以发送图片 Url </summary>
//        private string CensorImageUrl => _apiAddress + nameof(AboutCQHttpUri.can_send_image);

//        /// <summary> 检查是否可以发送语言 Url </summary>
//        private string CensorVoiceUrl => _apiAddress + nameof(AboutCQHttpUri.can_send_record);

//        /// <summary> 清除数据目录 Url </summary>
//        private string CleanDatadirUrl => _apiAddress + nameof(AboutCQHttpUri.clean_data_dir);

//        /// <summary> 清除插件日志 Url </summary>
//        private string CleanPluginUrl => _apiAddress + nameof(AboutCQHttpUri.clean_plugin_log);

//        /// <summary> 重启CQHTTP插件 Url </summary>
//        private string RestartPluginUrl => _apiAddress + nameof(AboutCQHttpUri.set_restart_plugin);

//        #endregion

//        #endregion

//        #region ** Utilities **

//        /// <summary> Post请求CQHttpClient </summary>
//        /// <param name="data"> Post请求需要的参数 </param>
//        /// <returns> 请求成功获取数据 </returns>
//        private async Task<string> PostAsync(object data)
//        {
//            try
//            {
//                if (data is null) throw new ArgumentNullException(nameof(data), $"{nameof(PostAsync)}请求失败: {nameof(data)}不能为空.");
//                if (UriType is null) throw new ArgumentNullException(nameof(UriType), $"{nameof(PostAsync)}请求失败: {nameof(UriType)}不能为空.");

//                using (HttpContent content = new StringContent(JsonHelper.ObjectToJson(data), Encoding.UTF8, Util.ContextType.ApplicationJson))
//                {
//                    using (var response = (await _httpClient.PostAsync(_apiAddress + UriType, content)).EnsureSuccessStatusCode())
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

//        /// <summary> Post请求CQHttpClient </summary>
//        /// <param name="url"> 需要请求的Url </param>
//        /// <param name="data"> Post请求需要的参数 </param>
//        /// <returns> 请求成功获取数据 </returns>
//        public async Task<string> PostAsync(string url, object data)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url), $"{nameof(PostAsync)}请求失败: {nameof(url)}不能为空.");
//                if (data is null) throw new ArgumentNullException(nameof(data), $"{nameof(PostAsync)}请求失败: {nameof(data)}数据不能为空");

//                using (HttpContent content = new StringContent(JsonHelper.ObjectToJson(data), Encoding.UTF8, Util.ContextType.ApplicationJson))
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

//        /// <summary> Post请求CQHttpClient </summary>
//        /// <typeparam name="TObject"> 需要返回的数据类型 </typeparam>
//        /// <param name="url"> Post请求URL </param>
//        /// <returns> 请求成功获取数据 </returns>
//        private async Task<TObject> PostAsync<TObject>(string url) where TObject : class
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url), $"{nameof(url)}不能为空.");

//                using (var response = (await _httpClient.PostAsync(url, null)).EnsureSuccessStatusCode())
//                {
//                    var responseJson = await response.Content.ReadAsStringAsync();
//                    return JsonHelper.JsonToObject<TObject>(responseJson);
//                }
//            }
//            catch (Exception exc)
//            {
//                IheiException.PritingExcption(exc);
//                return null; // TODO Logger
//            }
//        }

//        /// <summary> Post请求CQHttpClient </summary>
//        /// <typeparam name="TObject"> 需要返回的数据类型 </typeparam>
//        /// <param name="data"> Post请求需要的参数 </param>
//        /// <returns> 请求成功获取数据 </returns>
//        private async Task<TObject> PostAsync<TObject>(object data) where TObject : class
//        {
//            try
//            {
//                var errorExc = $"{nameof(PostAsync)}<{default(TObject).GetType()}>请求失败: ";
//                if (UriType is null) throw new ArgumentNullException(nameof(UriType), $"{errorExc}{nameof(UriType)}不能为空.");
//                if (data is null) throw new ArgumentNullException(nameof(data), $"{errorExc}{nameof(data)}不能为空");

//                using (HttpContent content = new StringContent(JsonHelper.ObjectToJson(data), Encoding.UTF8, Util.ContextType.ApplicationJson))
//                {
//                    using (var response = (await _httpClient.PostAsync(_apiAddress + UriType, content)).EnsureSuccessStatusCode())
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

//        /// <summary> Post请求CQHttpClient </summary>
//        /// <typeparam name="TObject"> 需要返回的数据类型 </typeparam>
//        /// <param name="url"> 需要请求的Url </param>
//        /// <param name="data"> Post请求需要的参数 </param>
//        /// <returns> 请求成功获取数据 </returns>
//        public async Task<TObject> PostAsync<TObject>(string url, object data) where TObject : class
//        {
//            try
//            {
//                var errorExc = $"{nameof(PostAsync)}<TObject>请求失败: ";
//                if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url), $"{errorExc}{nameof(url)}不能为空.");
//                if (data is null) throw new ArgumentNullException(nameof(data), $"{errorExc}{nameof(data)}不能为空");

//                using (HttpContent content = new StringContent(JsonHelper.ObjectToJson(data), Encoding.UTF8, Util.ContextType.ApplicationJson))
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

//        #endregion
//    }
//}
