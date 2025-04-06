using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SVAdminAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session;
        var user = session.GetString("CorreoAdmin");

        if (string.IsNullOrEmpty(user))
        {
            if (context.RouteData.Values["controller"]?.ToString() != "Html" ||
                context.RouteData.Values["action"]?.ToString() != "Index")
            {
                context.Result = new RedirectToActionResult("Index", "Html", null);
            }
        }
        base.OnActionExecuting(context);
    }
}