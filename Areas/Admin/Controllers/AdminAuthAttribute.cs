using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace MinhTienHairSalon.Areas.Admin.Controllers
{
    public class AdminAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            // Kiểm tra xem action có AllowAnonymous không
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                .Any(em => em is AllowAnonymousAttribute);
            if (allowAnonymous)
            {
                return; // Bỏ qua xác thực
            }

            var session = context.HttpContext.Session.GetString("AdminLoggedIn");
            if (string.IsNullOrEmpty(session))
            {
                context.Result = new RedirectToActionResult(null, "Login", null);
            }
            base.OnActionExecuting(context);
        }
    }
}