namespace SoulCrystal
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    public sealed class SiroOrigin
    {
        #region @@ 创作 @@

        /// <summary>作者</summary>
        public const string Author = "Yuan Lin";
        /// <summary>版权</summary>
        public const string Copyright = "Copyright © 2020 " + Author + " Personal Development";
        /// <summary>最初项目</summary>
        public static readonly Project FirstProject = Projects?[0];
        /// <summary>项目列表</summary>
        public static readonly IReadOnlyList<Project> Projects = new List<Project>
        {
            new Project("Siro", "Underlying layers of all Siro projects"),
            new Project("Abyss", "Awakening in the abyss."),
            new Project("Chaos", "The energy of chaos."),
            new Project("SoulCrystal", "Use of the medium of energy"),
            new Project("Frameworks", "Personal framework"),
            new Project("Payment", "Payment systems"),
        };

        #endregion

        #region @@ 默认 @@

        /// <summary>图片后缀</summary>
        public const string DefaultImageSuffix = ".png";
        /// <summary>图片链接</summary>
        public const string DefaultImagePath = "siro/image/default" + DefaultImageSuffix;
        /// <summary>图标链接</summary>
        public const string DefaultIconPath = "siroicon/default.icon";
        /// <summary>项目语言</summary>
        public const string DefaultLanguage = "zh-CN";
        /// <summary>项目文化</summary>
        public static readonly CultureInfo DefaultCulture = new CultureInfo(DefaultLanguage);
        /// <summary>储存库类型</summary>
        public static readonly LinkType DefaultRepositoryType = LinkType.GitSSH;

        #endregion

        #region @@ 链接 @@

        /// <summary>储存库链接</summary>
        public static readonly IReadOnlyDictionary<LinkType, string> RepositoryUrl = new Dictionary<LinkType, string>
        {
            { LinkType.GitHTTPS , "https://github.com/Stardream-Noel/Siro.Abyss.git" },
            { LinkType.GitSSH , "git@github.com:Stardream-Noel/Siro.Abyss.git" }
        };

        #endregion

        #region ** 类 **

        public sealed class Project
        {
            public Project() => Id = Guid.NewGuid().ToString();
            public Project(string name) : this() => Name = name;
            public Project(string name, string description) : this(name) => Description = description;

            /// <summary>唯一标识</summary>
            public string Id { get; }

            /// <summary>项目名称</summary>
            public string Name { get; }

            /// <summary>项目描述</summary>
            public string Description { get; }

            public override string ToString() => $"{Name}: {Id}";
        }

        #endregion
    }

    public enum LinkType
    {
        GitHTTPS = 1,
        GitSSH = 2,
    }

    public enum SiroAgreementType
    {
        /// <summary>功能说明</summary>
        AboutSiro = 0,
        /// <summary>用户协议</summary>
        UserTreaty = 1,
        /// <summary>隐私政策</summary>
        PrivacyPolicy = 2,
        /// <summary>功能说明</summary>
        FunctionExplain = 3,
        /// <summary>帮助中心</summary>
        HelpCenter = 4,
        /// <summary>用户反馈</summary>
        UserFeedback = 5,
        /// <summary>营业执照</summary>
        BusinessLicense = 8,
        /// <summary>Siro验证</summary>
        SiroAuthentication = 9,
    }
}
