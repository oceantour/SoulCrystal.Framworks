//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Ihei.Robot.Posts;
//using Ihei.Robot.Reporting;
//using Ihei.Robot.Reporting.Datas;
//using Ihei.Robot.Responses;
//using Newtonsoft.Json.Linq;

//namespace Ihei.Robot
//{
//    partial class CQHttpClient
//    {
//        #region -- SendMessage --

//        /// <summary> 发送私聊信息 </summary>
//        /// <param name="userId"> 用户QQ号 </param>
//        /// <param name="message"> 需要发送的消息(文本) </param>
//        /// <returns>  </returns>
//        public Task<CQAPIResponse<MessageData>> SendPrivateMessageAsync(long userId, string message)
//        {
//            return PostAsync<CQAPIResponse<MessageData>>(SendPrivateUrl, new
//            {
//                user_id = userId,
//                message,
//                auto_escape = true,
//            });
//        }

//        /// <summary>
//        /// 发送私聊消息。
//        /// </summary>
//        /// <param name="userId">对方 QQ 号。</param>
//        /// <param name="message">要发送的内容。</param>
//        /// <returns>包含消息 ID 的响应数据。</returns>
//        public Task<CQAPIResponse<MessageData>> SendPrivateMessageAsync(long userId, Message message)
//        {
//            return PostAsync<CQAPIResponse<MessageData>>(SendPrivateUrl, new
//            {
//                user_id = userId,
//                message = message.Serializing,
//            });
//        }

//        /// <summary>
//        /// 发送群消息。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="message">要发送的内容（文本）。</param>
//        /// <returns>包含消息 ID 的响应数据。</returns>
//        public Task<CQAPIResponse<MessageData>> SendGroupMessageAsync(long groupId, string message)
//        {
//            return PostAsync<CQAPIResponse<MessageData>>(SendGroupUrl, new
//            {
//                group_id = groupId,
//                message,
//                auto_escape = true,
//            });
//        }

//        /// <summary>
//        /// 发送群消息。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="message">要发送的内容。</param>
//        /// <returns>包含消息 ID 的响应数据。</returns>
//        public Task<CQAPIResponse<MessageData>> SendGroupMessageAsync(long groupId, Message message)
//        {
//            return PostAsync<CQAPIResponse<MessageData>>(SendGroupUrl, new
//            {
//                group_id = groupId,
//                message = message.Serializing,
//            });
//        }

//        /// <summary>
//        /// 发送讨论组消息。
//        /// </summary>
//        /// <param name="discussId">讨论组 ID。</param>
//        /// <param name="message">要发送的内容（文本）。</param>
//        /// <returns>包含消息 ID 的响应数据。</returns>
//        public Task<CQAPIResponse<MessageData>> SendDiscussMessageAsync(long discussId, string message)
//        {
//            return PostAsync<CQAPIResponse<MessageData>>(SendDiscussUrl, new
//            {
//                discuss_id = discussId,
//                message,
//                auto_escape = true,
//            });
//        }

//        /// <summary>
//        /// 发送讨论组消息。
//        /// </summary>
//        /// <param name="discussId">讨论组 ID。</param>
//        /// <param name="message">要发送的内容。</param>
//        /// <returns>包含消息 ID 的响应数据。</returns>
//        public Task<CQAPIResponse<MessageData>> SendDiscussMessageAsync(long discussId, Message message)
//        {
//            return PostAsync<CQAPIResponse<MessageData>>(SendDiscussUrl, new
//            {
//                discuss_id = discussId,
//                message = message.Serializing,
//            });
//        }

//        /// <summary>
//        /// 发送消息。
//        /// </summary>
//        /// <param name="endpoint">要发送到的终结点。</param>
//        /// <param name="message">要发送的消息。</param>
//        /// <returns>包含消息 ID 的响应数据。</returns>
//        public Task<CQAPIResponse<MessageData>> SendMessageAsync(Endpoint endpoint, Message message)
//        {
//            var data = JObject.FromObject(endpoint);
//            data["message"] = JToken.FromObject(message.Serializing);
//            return PostAsync<CQAPIResponse<MessageData>>(SendMessageUrl, data);
//        }

//        /// <summary>
//        /// 发送消息。
//        /// </summary>
//        /// <param name="endpoint">要发送到的终结点。</param>
//        /// <param name="message">要发送的消息（文本）。</param>
//        /// <returns>包含消息 ID 的响应数据。</returns>
//        public Task<CQAPIResponse<MessageData>> SendMessageAsync(Endpoint endpoint, string message)
//        {
//            var data = JObject.FromObject(endpoint);
//            data["message"] = JToken.FromObject(message);
//            data["auto_escape"] = true;

//            return PostAsync<CQAPIResponse<MessageData>>(SendMessageUrl, data);
//        }

//        #endregion

//        #region -- GroupSetting --

//        /// <summary>
//        /// 群组踢人。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="userId">要踢的 QQ 号。</param>
//        /// <returns>是否成功。注意：酷 Q 未处理错误，所以无论是否成功都会返回<c>true</c>。</returns>
//        public async Task<bool> KickGroupMemberAsync(long groupId, long userId)
//        {
//            var result = await PostAsync<CQAPIResponse>(RemoveGroupMemberUrl, new
//            {
//                group_id = groupId,
//                user_id = userId,
//            });
//            return result.Success;
//        }

//        /// <summary>
//        /// 撤回消息（需要Pro）。
//        /// </summary>
//        /// <param name="message">消息返回值。</param>
//        /// <returns>是否成功。</returns>
//        public Task<bool> RecallMessageAsync(MessageData message) => RecallMessageAsync(message.MessageId);

//        /// <summary>
//        /// 撤回消息（需要Pro）
//        /// </summary>
//        /// <param name="messageId">消息 ID。</param>
//        /// <returns>是否成功。</returns>
//        public async Task<bool> RecallMessageAsync(int messageId)
//        {
//            var result = await PostAsync<CQAPIResponse<MessageData>>(RecallMessageUrl, new
//            {
//                message_id = messageId
//            });
//            return result.Success;
//        }

//        /// <summary>
//        /// 群组单人禁言。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="userId">要禁言的 QQ 号。</param>
//        /// <param name="duration">禁言时长，单位秒，0 表示取消禁言。</param>
//        /// <exception cref="ApiAccessException"></exception>
//        /// <returns>如果操作成功，返回 <c>true</c>。</returns>
//        public async Task<bool> BanGroupMember(long groupId, long userId, int duration)
//        {
//            var result = await PostAsync<CQAPIResponse>(GroupSingleBanUrl, new
//            {
//                group_id = groupId,
//                user_id = userId,
//                duration,
//            });
//            return result.Success;
//        }

//        /// <summary>
//        /// 自动识别发送者类型（普通/匿名）并禁言。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="messageSource">群消息上报的 <see cref="MessageSource"/> 属性。</param>
//        /// <param name="duration">禁言时长，单位秒，0 表示取消禁言，无法取消匿名用户禁言。</param>
//        /// <returns>如果操作成功，返回 <c>true</c>。</returns>
//        public Task<bool> BanMessageSource(long groupId, MessageSource messageSource, int duration)
//        {
//            if (messageSource is null)
//            {
//                throw new ArgumentNullException(nameof(messageSource));
//            }

//            return messageSource.IsAnonymous
//                ? BanAnonymousMember(groupId, messageSource.AnonymousFlag, duration)
//                : BanGroupMember(groupId, messageSource.UserId, duration);
//        }

//        /// <summary>
//        /// 群组匿名用户禁言。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="flag">要禁言的匿名用户的 flag。</param>
//        /// <param name="duration">禁言时长，单位秒，无法取消匿名用户禁言。</param>
//        /// <returns>如果操作成功，返回 <c>true</c>。</returns>
//        public async Task<bool> BanAnonymousMember(long groupId, string flag, int duration)
//        {
//            //TODO 匿名
//            var result = await PostAsync<CQAPIResponse>(GroupAnonymousBanUrl, new
//            {
//                group_id = groupId,
//                anonymous_flag = flag,
//                duration,
//            });
//            return result.Success;
//        }

//        /// <summary>
//        /// 群组匿名用户禁言。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="anonymousInfo">要禁言的匿名用户对象（<see cref="AnonymousInfo.Flag"/> 属性）。</param>
//        /// <param name="duration">禁言时长，单位秒，无法取消匿名用户禁言。</param>
//        /// <returns>如果操作成功，返回 <c>true</c>。</returns>
//        public Task<bool> BanAnonymousMember(long groupId, AnonymousInfo anonymousInfo, int duration)
//        {
//            if (anonymousInfo == null)
//            {
//                throw new ArgumentNullException(nameof(anonymousInfo));
//            }

//            return BanAnonymousMember(groupId, anonymousInfo.Flag, duration);
//        }

//        /// <summary>
//        /// 群组全员禁言。
//        /// </summary>
//        /// <param name="groupId"></param>
//        /// <param name="enable"></param>
//        /// <returns></returns>
//        public async Task<bool> BanWholeGroup(long groupId, bool enable)
//        {
//            var result = await PostAsync<CQAPIResponse>(GroupAllBanUrl, new
//            {
//                group_id = groupId,
//                enable,
//            });
//            return result.Success;
//        }

//        /// <summary>
//        /// 设置群名片。
//        /// </summary>
//        /// <param name="groupId">群号。</param>
//        /// <param name="userId">要设置的 QQ 号。</param>
//        /// <param name="card">群名片内容，不填或空字符串表示删除群名片。</param>
//        /// <returns>是否成功。</returns>
//        public async Task<bool> SetGroupCard(long groupId, long userId, string card)
//        {
//            var result = await PostAsync<CQAPIResponse>(SetGroupCardUrl, new
//            {
//                group_id = groupId,
//                user_id = userId,
//                card,
//            });
//            return result.Success;
//        }

//        #endregion

//        #region -- GetUserInfo --

//        /// <summary>
//        /// 获取登录信息。
//        /// </summary>
//        /// <returns>登录信息。</returns>
//        public Task<LoginInfo> GetLoginInfoAsync()
//        {
//            var data = new object();
//            return PostAsync<LoginInfo>(GetLoginUserInfoUrl, data);
//        }

//        /// <summary>
//        /// 获取好友列表。
//        /// </summary>
//        /// <returns>好友数组。</returns>
//        public Task<CQAPIResponse<Friend[]>> GetFriendListAsync() => PostAsync<CQAPIResponse<Friend[]>>(GetFriendListUrl, new object());

//        /// <summary>
//        /// 获取群列表。
//        /// </summary>
//        /// <returns></returns>
//        public Task<CQAPIResponse<GroupInfo[]>> GetGroupListAsync() => PostAsync<CQAPIResponse<GroupInfo[]>>(GetGroupListUrl, new object());

//        /// <summary>
//        /// 获取群成员信息。
//        /// </summary>
//        /// <param name="group">群号。</param>
//        /// <param name="qq">QQ 号（不可以是登录号）。</param>
//        /// <returns>获取到的成员信息。</returns>
//        public Task<GroupMemberInfo> GetGroupMemberInfoAsync(long group, long qq)
//        {
//            var data = new
//            {
//                group_id = group,
//                user_id = qq,
//                no_cache = true,
//            };
//            return PostAsync<GroupMemberInfo>(GetGroupMemberInfoUrl, data);
//        }

//        /// <summary>
//        /// 获取群成员列表。
//        /// </summary>
//        /// <param name="group">群号。</param>
//        /// <returns>响应内容为数组，每个元素的内容和上面的 GetGroupMemberInfoAsync() 方法相同，但对于同一个群组的同一个成员，获取列表时和获取单独的成员信息时，某些字段可能有所不同，例如 area、title 等字段在获取列表时无法获得，具体应以单独的成员信息为准。</returns>
//        public Task<CQAPIResponse<GroupMemberInfo[]>> GetGroupMemberListAsync(long group)
//        {
//            var data = new
//            {
//                group_id = group,
//            };
//            return PostAsync<CQAPIResponse<GroupMemberInfo[]>>(GetGroupMemberListUrl, data);
//        }

//        #endregion

//        #region -- HandleRequest --

//        #endregion

//        #region -- GetCQHTTPInfo --

//        #endregion

//        #region -- CQHTTPSettings --

//        private int _isReadyToCleanData;

//        /// <summary>
//        /// 是否已设置定期清理图片缓存。
//        /// </summary>
//        public bool IsCleaningData => _isReadyToCleanData != 0;

//        /// <summary>
//        /// 开始定期访问清理图片的 API。
//        /// </summary>
//        /// <param name="intervalMinutes">间隔的毫秒数。</param>
//        /// <returns>成功开始则为 <c>true</c>，如果之前已经开始过，则为 <c>false</c>。</returns>
//        public bool StartClean(int intervalMinutes)
//        {
//            if (Interlocked.CompareExchange(ref _isReadyToCleanData, 1, 0) == 0)
//            {
//                var task = new Task(async () =>
//                {
//                    while (true)
//                    {
//                        try
//                        {
//                            await this.CleanImageData();
//                        }
//                        catch (Exception)
//                        {
//                            // ignored
//                        }

//                        await Task.Delay(60000 * intervalMinutes);
//                    }
//                }, TaskCreationOptions.LongRunning);
//                task.Start();
//                return true;
//            }

//            return false;
//        }

//        /// <summary>
//        /// 获取插件运行状态。
//        /// </summary>
//        /// <returns></returns>
//        public Task<Status> GetStatusAsync() => PostAsync<Status>(GetCQStatusUrl, new object());

//        /// <summary>
//        /// 清理数据目录中的图片。经测试可能无效。
//        /// </summary>
//        /// <returns></returns>
//        public Task CleanImageData() => PostAsync(CleanDatadirUrl, new { data_dir = "image" });

//        #endregion
//    }
//}
