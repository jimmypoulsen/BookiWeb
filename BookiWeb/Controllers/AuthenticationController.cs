using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookiWeb.Controllers
{
    public class AuthenticationController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = Request.Cookies["AuthCookies"];
            if (cookie == null || cookie["email"] == null || cookie["customerId"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Sessions", action = "Create" }));

                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
        }
    }
}