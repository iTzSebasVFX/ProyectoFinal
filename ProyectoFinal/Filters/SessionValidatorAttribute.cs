using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SessionValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var session = context.HttpContext.Session;
        var user = session.GetString("NombreUser"); // Asegúrate de usar la clave correcta

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
