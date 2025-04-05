using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MinhTienHairSalon.Areas.Filter
{
    public class DesktopOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString().ToLower();

            if (userAgent.Contains("mobile") || userAgent.Contains("android") || userAgent.Contains("iphone") || userAgent.Contains("ipad"))
            {
                context.Result = new RedirectToActionResult("DeviceNotSupported", "Index", null);
            }

            base.OnActionExecuting(context);
        }
    }
}

