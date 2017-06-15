using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DS32017_WeedHackers.Enums;
using DS32017_WeedHackers.Models.Authentication;
using WeedHackers_Data.Entities;

namespace DS32017_WeedHackers.Attributes
{
    public class WeedHackersAuthAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        private UserAuthType _authType;
        public WeedHackersAuthAttribute(UserAuthType authType)
        {
            _authType = authType;
        }
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            
            HttpCookie WeedHackSesh = HttpContext.Current.Request.Cookies["WeedHackersSession"];
            

            if (WeedHackSesh == default(HttpCookie) || !MvcApplication.Sessions.ContainsKey(WeedHackSesh.Value))
            {
                RedirectToLogin(actionContext);
                return;
            }

            var session = MvcApplication.Sessions[WeedHackSesh.Value];
            if (DateTime.Now > session.ExpiryTime)
            {
                SessionModel sessionToRemove;
                MvcApplication.Sessions.TryRemove(WeedHackSesh.Value, out sessionToRemove);
                RedirectToLogin(actionContext);
                return;
            }
            switch (_authType)
            {
                  case UserAuthType.Admin:
                    if (!session.User.SuperAdmin)
                    {
                        RedirectToLogin(actionContext);
                        return;
                    }
                    break;
                    case UserAuthType.Employee:
                    if (session.User.Employee==null)
                    {
                        RedirectToLogin(actionContext);
                        return;
                    }
                    break;
                    case UserAuthType.Customer:
                    if (session.User.Customer == null)
                    {
                        RedirectToLogin(actionContext);
                        return;
                    }
                    break;

            }
            actionContext.Controller.ViewBag.UserContext = session.User;
        }



        private void RedirectToLogin(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Security", action = "Index" }));
        }
        
    }
}