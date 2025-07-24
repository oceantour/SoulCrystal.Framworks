namespace SoulCrystal.Other;

public class UserInfo
{
    /// <summary> 头像 </summary>
    public string AvatarUrl { get; set; }
    /// <summary> 昵称 </summary>
    public string NickName { get; set; }
    /// <summary> 签名 </summary>
    public string Autograph { get; set; }
    /// <summary> 性别 </summary>
    public e_Gender Gender { get; set; }
    /// <summary> 国家 </summary>
    public string County { get; set; }
    /// <summary> 省份 </summary>
    public string Province { get; set; }
    /// <summary> 城市 </summary>
    public string City { get; set; }
    /// <summary> 详细地址 </summary>
    public string FullAddress { get; set; }
    /// <summary> 出生日期 </summary>
    public DateTime BirthDate { get; set; }
    /// <summary> 邮政编码 </summary>
    public int MailboxCode { get; set; }
}

/// <summary> 用户隐私信息 </summary>
public class UserPrivacyInfo
{
    /// <summary> 用户名称 </summary>
    public string UserName { get; set; }
    /// <summary> 手机号码 </summary>
    public string Phone { get; set; }
    /// <summary> 电子邮箱 </summary>
    public string Email { get; set; }
    /// <summary> 用户密码 </summary>
    public string Password { get; set; }
    /// <summary> 实名验证 </summary>
    public string RealCertificate { get; set; }
    /// <summary> 银行卡 / 信用卡 </summary>
    public string BankCard { get; set; }
    /// <summary> 密保问题 </summary>
    public string SecurityQuestion { get; set; }
}

/// <summary> 用户设备信息 </summary>
public class UserEquipmentInfo
{
    /// <summary> 设备类型 </summary>
    public string DeviceType { get; set; }
    /// <summary> 操作系统 </summary>
    public string OS { get; set; }
    /// <summary> 系统版本 </summary>
    public string OSVersion { get; set; }
    /// <summary> 系统语言 </summary>
    public string Language { get; set; }
    /// <summary> 屏幕高度 </summary>
    public string ScreenHeight { get; set; }
    /// <summary> 屏幕宽度 </summary>
    public string ScreenWidth { get; set; }
    /// <summary> 屏幕方向 </summary>
    public string Orientation { get; set; }
    /// <summary> 浏览器信息 </summary>
    public string BrowserInfo { get; set; }
    /// <summary> 用户代理信息 </summary>
    public string UserAgent { get; set; }
    /// <summary> 指纹 </summary>
    public string Fingerprint { get; set; }
    /// <summary> 地理位置 </summary>
    public string GeoPosition { get; set; }
}

/// <summary> 企业信息 </summary>
public class EnterpriseInfo
{
    /// <summary> 企业名称 </summary>
    public string Enterprise { get; set; }
    /// <summary> 企业邮箱 </summary>
    public string EnterpriseEmail { get; set; }
    /// <summary> 企业规模 </summary>
    public string EnterpriseScale { get; set; }
    /// <summary> 机构类型 </summary>
    public string MechanismType { get; set; }
    /// <summary> 注册资金 </summary>
    public string RegistrationFee { get; set; }
    /// <summary> 所在地址 </summary>
    public string Address { get; set; }
    /// <summary> 运营者身份证姓名 </summary>
    public string OperatorName { get; set; }
    /// <summary> 运营者手机号码 </summary>
    public string OperatorPhone { get; set; }
    /// <summary> 运营者验证码 </summary>
    public string VerifyCode { get; set; }
    /// <summary> 运营者联系邮箱 </summary>
    public string ContactEmail { get; set; }
    /// <summary> 运营者授权确认函 </summary>
    public string ConfirmLetter { get; set; }
    /// <summary> 统一社会信用代码 </summary>
    public string Credit { get; set; }
}

public enum e_Gender
{
    Male = 1,
    Female = 2,
}
