using System.Text;
using NLog;
using NLog.Web.LayoutRenderers;

namespace SoulCrystal.Logging
{
    public class TenantLayoutRenderer : AspNetLayoutRendererBase
    {
        public const string LayoutRendererName = "soulcrystal-tenant-name";
        public const string CurrentTenantName = "cnhuatu";

        protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
        {
            builder.Append(CurrentTenantName);
        }
    }
}
