using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MVC.Filters
{
    public class Delete : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("So'rov tugadi");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            context.HttpContext.Request.RouteValues.TryGetValue("id", out object id);

            if (id is null)
            {
                context.HttpContext.Response.StatusCode = 400;
                return;
            }

            int bookId = Convert.ToInt32(id);

            context.HttpContext.Response.Cookies.Append("ids", $"userId:{userId},bookId:{bookId}");

            /*User shu bookni egasi ekanini tekshiramiz*/

            /*   if (bookId == 2)
               {
                   context.Result = new StatusCodeResult(404);
                   return;
               }
               else
               {
               }*/
        }
    }
}
