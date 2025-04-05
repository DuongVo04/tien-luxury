using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class DesktopOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString().ToLower();

        if (userAgent.Contains("mobile") || userAgent.Contains("android") || userAgent.Contains("iphone") || userAgent.Contains("ipad"))
        {
            context.Result = new ContentResult
            {
                StatusCode = 403,
                Content = "Truy cập chỉ được phép từ máy tính để bàn hoặc laptop."
            };
        }

        base.OnActionExecuting(context);
    }
}
