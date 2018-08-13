using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using EWallet.Web.Extensions;

namespace EWallet.Web.Controllers
{
    //[Authorize]
    public class BaseController : Controller
    {
        public class HasRoleAdministratorAttribute : ActionFilterAttribute
        {
            //public override void OnActionExecuting(ActionExecutingContext filterContext)
            //{
            //    var userId = User.GetUserId();
            //    if (!User.Identity.IsAuthenticated)
            //    {
            //        var redirectUrl = "/login?message=login";
            //        filterContext.Result = new RedirectResult(redirectUrl);
            //    }
            //}
        }
    }
}