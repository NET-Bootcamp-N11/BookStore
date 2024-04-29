using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC.Filters
{
    public class Permission : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Identity.Name;

            Console.WriteLine($"Ismi : {userName}");
        }
    }
}
